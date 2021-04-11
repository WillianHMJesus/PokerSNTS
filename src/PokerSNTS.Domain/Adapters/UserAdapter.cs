using PokerSNTS.Domain.DTOs;
using PokerSNTS.Domain.Entities;

namespace PokerSNTS.Domain.Adapters
{
    public class UserAdapter
    {
        public static UserDTO ToUserDTO(User user, TokenDTO token = null)
        {
            return new UserDTO(user.Id, user.UserName, token);
        }
    }
}
