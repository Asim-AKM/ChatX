using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.DTO_s.Response
{
    public record ConversationParticipant
    {
        

        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string? Contact {get; set; }
        public string? ProfileImageUrl { get; set; }
    }
}
