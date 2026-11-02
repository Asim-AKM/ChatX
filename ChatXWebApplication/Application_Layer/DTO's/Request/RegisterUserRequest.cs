using Microsoft.AspNetCore.Http;

namespace Application_Layer.DTO_s.Request
{

    public class RegisterUserRequest
    {
  
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
 
    }
}

