using Microsoft.AspNetCore.Http;

namespace Application_Layer.DTO_s.Request
{

    public class RegisterUserRequest
    {
        public Guid UserID { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; }
        public string? Contact { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string? ProfileImageUrl { get; set; }
    }
}

