using Microsoft.AspNetCore.Http;

namespace Application_Layer.DTO_s.Response
{
    public class UserDataResponse
    {
        public Guid UserID { get; set; }
        public string? UserName { get; set; }
        public string? DisplayName { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string? ProfileImageUrl { get; set; }
        public bool? IsActive { get; set; }
    }
}
