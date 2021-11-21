using System;

namespace APICatalog.DTOs
{
    public class TokenDTO
    {
        public string Token { get; set; }
        public bool Authenticated { get; set; }
        public DateTime TokenExpiration { get; set; }
        public string Message { get; set; }
    }
}
