// ChatX UI template interactions. Replace demo data with your API/backend later.

/* ============================================================
   Helpers
   ============================================================ */

function toast(msg) {
    const t = document.getElementById('toast');
    if (!t) return;

    t.textContent = msg;
    t.classList.remove('hidden');

    clearTimeout(window.toastTimer);
    window.toastTimer = setTimeout(() => t.classList.add('hidden'), 2600);
}

function escapeHtml(s) {
    return String(s).replace(/[&<>"']/g, c => ({
        '&': '&amp;',
        '<': '&lt;',
        '>': '&gt;',
        '"': '&quot;',
        "'": '&#39;'
    }[c]));
}

/* ============================================================
   Message form (Messages page)
   ============================================================ */

const form = document.getElementById('messageForm');

if (form) {
    form.addEventListener('submit', e => {
        e.preventDefault();

        const input = document.getElementById('messageInput');
        const v = input.value.trim();

        if (!v) {
            input.focus();
            return;
        }

        const b = document.createElement('div');
        b.className = 'bubble mine';
        b.append(document.createTextNode(v));

        const meta = document.createElement('small');
        meta.textContent = 'Just now · ✓ Sent (preview)';
        b.appendChild(meta);

        const m = document.getElementById('messages');
        m.appendChild(b);

        input.value = '';
        m.scrollTop = m.scrollHeight;
        input.focus();
    });
}

/* ============================================================
   Chat rows & conversation switching
   ============================================================ */

function openChat(name, initials, last) {
    const nameEl = document.getElementById('activeName');
    const avatar = document.getElementById('activeAvatar');
    const messages = document.getElementById('messages');

    if (!nameEl || !messages) return;

    nameEl.textContent = name;
    avatar.textContent = initials;

    document.querySelectorAll('.chatrow').forEach(r =>
        r.classList.toggle('active', r.dataset.name === name)
    );

    messages.innerHTML =
        '<div class="dayline">TODAY</div>' +
        '<div class="bubble">Hi! Good to hear from you.<small>10:24 AM</small></div>' +
        '<div class="bubble mine">Hey ' + escapeHtml(name.split(' ')[0]) +
            '! How are things going?<small>10:26 AM · ✓✓</small></div>' +
        '<div class="bubble">' + escapeHtml(last || 'Thanks for your message!') +
            '<small>10:27 AM</small></div>';

    if (innerWidth <= 700) {
        document.getElementById('chatlist')?.classList.add('collapsed');
        document.getElementById('chatarea')?.classList.remove('hiddenmobile');
    }
}

function filterChats() {
    const q = (document.getElementById('chatSearch')?.value || '').toLowerCase();

    document.querySelectorAll('.chatrow').forEach(r =>
        r.style.display = r.dataset.name.toLowerCase().includes(q) ? 'flex' : 'none'
    );
}

function setFilter(btn, type) {
    document.querySelectorAll('.filters button').forEach(b => b.classList.remove('active'));
    btn.classList.add('active');

    document.querySelectorAll('.chatrow').forEach(r =>
        r.style.display = (type === 'all' || r.dataset.type === type) ? 'flex' : 'none'
    );
}

/* ============================================================
   People search
   ============================================================ */

function filterUsers() {
    const q = (document.getElementById('userSearch')?.value || '').toLowerCase();
    let n = 0;

    document.querySelectorAll('.result').forEach(r => {
        const yes = (r.dataset.user || '').includes(q);
        r.style.display = yes ? 'flex' : 'none';
        if (yes) n++;
    });

    const count = document.getElementById('userCount');
    if (count) count.textContent = n + ' people';

    document.getElementById('noUsers')?.classList.toggle('hidden', n !== 0);
}

function startChat(name, initials) {
    if (location.pathname.split('/').pop() !== 'messages') {
        location.href = 'messages?chat=' + encodeURIComponent(name);
        return;
    }

    let row = [...document.querySelectorAll('.chatrow')].find(r => r.dataset.name === name);

    if (!row) {
        row = document.createElement('div');
        row.className = 'chatrow';
        row.dataset.name = name;
        row.dataset.type = 'all';
        row.onclick = () => openChat(name, initials, 'Conversation started');
        row.innerHTML =
            '<div class="avatar">' + escapeHtml(initials) + '</div>' +
            '<div class="person-info"><strong>' + escapeHtml(name) +
                '</strong><small>Conversation started</small></div>' +
            '<span class="chatmeta">Now</span>';

        document.getElementById('chatRows').prepend(row);
    }

    openChat(name, initials, 'Conversation started');
    toast('Opened conversation with ' + name + ' (preview)');
}

/* ============================================================
   Toggles (settings page)
   ============================================================ */

document.querySelectorAll('.toggle').forEach(t =>
    t.setAttribute('aria-pressed', t.classList.contains('on'))
);

document.querySelectorAll('.toggle').forEach(t =>
    t.addEventListener('click', () =>
        t.setAttribute('aria-pressed', t.classList.contains('on'))
    )
);



/* ============================================================
   Deep link: ?chat=Name — opens conversation on Messages page
   ============================================================ */

const params = new URLSearchParams(location.search);

if (params.has('chat')) {
    const name = params.get('chat');
    const row = [...document.querySelectorAll('.chatrow')].find(r => r.dataset.name === name);
    if (row) row.click();
}

/* ============================================================
   Responsive sidebar toggle
   ============================================================ */

document.querySelectorAll('.humberger').forEach(button => {
    button.addEventListener('click', () => {
        const app = button.closest('.app');
        if (!app) return;

        if (window.matchMedia('(max-width: 700px)').matches) {
            const opened = app.classList.toggle('mobile-sidebar-open');
            button.setAttribute('aria-expanded', String(opened));
        } else {
            const collapsed = app.classList.toggle('sidebar-collapsed');
            button.setAttribute('aria-expanded', String(!collapsed));
        }
    });
});

document.addEventListener('click', event => {
    const app = document.querySelector('.app');
    if (!app || !app.classList.contains('mobile-sidebar-open')) return;

    if (event.target.closest('.humberger') || event.target.closest('.sidebar')) return;

    app.classList.remove('mobile-sidebar-open');
    app.querySelectorAll('.humberger').forEach(button =>
        button.setAttribute('aria-expanded', 'false')
    );
});

document.addEventListener('keydown', event => {
    if (event.key !== 'Escape') return;

    const app = document.querySelector('.app');
    if (!app) return;

    app.classList.remove('mobile-sidebar-open');
    app.querySelectorAll('.humberger').forEach(button =>
        button.setAttribute('aria-expanded', 'false')
    );
});

/* ============================================================
   Theme preference (shared across all pages)
   ============================================================ */

(function () {
    const saved = localStorage.getItem('chatx-theme');
    if (saved === 'dark') document.body.setAttribute('data-theme', 'dark');

    const button = document.getElementById('themeToggle');
    const label = document.getElementById('themeToggleLabel');
    const icon = document.querySelector('.theme-toggle-icon');

    function sync() {
        const dark = document.body.getAttribute('data-theme') === 'dark';
        if (button) button.setAttribute('aria-pressed', String(dark));
        if (label) label.textContent = dark ? 'Light mode' : 'Dark mode';
        if (icon) icon.textContent = dark ? '☀' : '☾';
    }

    sync();

    if (button) {
        button.addEventListener('click', () => {
            const dark = document.body.getAttribute('data-theme') !== 'dark';

            if (dark) document.body.setAttribute('data-theme', 'dark');
            else document.body.removeAttribute('data-theme');

            localStorage.setItem('chatx-theme', dark ? 'dark' : 'light');
            sync();

            if (typeof toast === 'function') {
                toast(dark ? 'Dark mode enabled' : 'Light mode enabled');
            }
        });
    }
})();

/* ============================================================
   Accessible dropdowns (account, conversation options, attachments)
   ============================================================ */

document.querySelectorAll('[data-dropdown-trigger]').forEach(trigger => {
    trigger.addEventListener('click', event => {
        event.preventDefault();
        event.stopPropagation();

        const dropdown = trigger.closest('.dropdown');
        if (!dropdown) return;

        const shouldOpen = !dropdown.classList.contains('is-open');

        document.querySelectorAll('.dropdown.is-open').forEach(open => {
            open.classList.remove('is-open');
            open.querySelector('[data-dropdown-trigger]')?.setAttribute('aria-expanded', 'false');
        });

        if (shouldOpen) {
            dropdown.classList.add('is-open');
            trigger.setAttribute('aria-expanded', 'true');
        }
    });
});

document.addEventListener('click', event => {
    if (event.target.closest('.dropdown')) return;

    document.querySelectorAll('.dropdown.is-open').forEach(dropdown => {
        dropdown.classList.remove('is-open');
        dropdown.querySelector('[data-dropdown-trigger]')?.setAttribute('aria-expanded', 'false');
    });
});

document.addEventListener('keydown', event => {
    if (event.key !== 'Escape') return;

    document.querySelectorAll('.dropdown.is-open').forEach(dropdown => {
        dropdown.classList.remove('is-open');
        const trigger = dropdown.querySelector('[data-dropdown-trigger]');
        trigger?.setAttribute('aria-expanded', 'false');
        trigger?.focus();
    });
});

/* ============================================================
   Shared profile photo (saved to localStorage, shown on all pages)
   ============================================================ */

(function setupProfilePhoto() {
    const storageKey = 'chatx-profile-photo';
    const images = () => document.querySelectorAll('img.profile-photo');

    let savedPhoto = null;
    try {
        savedPhoto = localStorage.getItem(storageKey);
    } catch (_) {
        /* Storage can be unavailable in restricted browsing modes. */
    }

    if (savedPhoto) {
        images().forEach(image => { image.src = savedPhoto; });
    }

    const input = document.getElementById('profileImageInput');
    if (!input) return;

    input.addEventListener('change', () => {
        const file = input.files && input.files[0];
        if (!file) return;

        if (!file.type || !file.type.startsWith('image/')) {
            toast('Please choose an image file.');
            input.value = '';
            return;
        }

        if (file.size > 10 * 1024 * 1024) {
            toast('Please choose an image smaller than 10 MB.');
            input.value = '';
            return;
        }

        const objectUrl = URL.createObjectURL(file);
        const photo = new Image();

        photo.onload = () => {
            const maxDimension = 512;
            const scale = Math.min(1, maxDimension / Math.max(photo.naturalWidth, photo.naturalHeight));

            const canvas = document.createElement('canvas');
            canvas.width = Math.max(1, Math.round(photo.naturalWidth * scale));
            canvas.height = Math.max(1, Math.round(photo.naturalHeight * scale));

            const context = canvas.getContext('2d');
            if (!context) {
                URL.revokeObjectURL(objectUrl);
                toast('Could not process this image.');
                return;
            }

            context.drawImage(photo, 0, 0, canvas.width, canvas.height);
            const dataUrl = canvas.toDataURL('image/jpeg', .86);

            try {
                localStorage.setItem(storageKey, dataUrl);
            } catch (_) {
                URL.revokeObjectURL(objectUrl);
                toast('Could not save the photo in this browser. Try a smaller image.');
                return;
            }

            images().forEach(image => { image.src = dataUrl; });
            URL.revokeObjectURL(objectUrl);
            toast('Profile photo updated.');
            input.value = '';
        };

        photo.onerror = () => {
            URL.revokeObjectURL(objectUrl);
            toast('Could not open that image. Please try another.');
            input.value = '';
        };

        photo.src = objectUrl;
    });
})();