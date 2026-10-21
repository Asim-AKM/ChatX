using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.DTO_s.Response
{
    public class LoginResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = "";
        public Guid UserId { get; set; }          // apni User entity ke hisab se type rakhein
        public string Identifier { get; set; } = "";
    }
}
