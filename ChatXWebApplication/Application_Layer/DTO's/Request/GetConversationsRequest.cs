using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.DTO_s.Request
{
    public sealed class GetConversationsRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set ; } = 20;

      
    }
}