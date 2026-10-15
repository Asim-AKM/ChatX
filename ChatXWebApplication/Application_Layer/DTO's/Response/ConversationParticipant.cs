using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.DTO_s.Response
{
    public class ConversationParticipant
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string? ProfileImageUrl { get; set; }
    }
}
