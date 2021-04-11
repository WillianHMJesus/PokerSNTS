using System;

namespace PokerSNTS.Domain.DTOs
{
    public class UserDTO
    {
        public UserDTO(Guid id, string userName, TokenDTO token)
        {
            Id = id;
            UserName = userName;
            Token = token;
        }

        public Guid Id { get; private set; }
        public string UserName { get; private set; }
        public TokenDTO Token { get; private set; }
    }
}
