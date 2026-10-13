namespace Application_Layer.DTO_s.Response
{
   

    public class AuthenticationResponse
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? ProfileImageUrl { get; set; }
    }
}
