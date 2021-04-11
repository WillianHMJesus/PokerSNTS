using System;

namespace PokerSNTS.Domain.DTOs
{
    public class TokenDTO
    {
        public TokenDTO(string accessToken, DateTime expires)
        {
            AccessToken = accessToken;
            Expires = expires;
        }

        public string AccessToken { get; private set; }
        public DateTime Expires { get; private set; }
    }
}
