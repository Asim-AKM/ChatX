namespace Application_Layer.DTO_s.Request
{
   
        public class RegisterUserRequest
        {
            public string UserName { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
            public string? ProfileImageUrl { get; set; }
        }
    }

