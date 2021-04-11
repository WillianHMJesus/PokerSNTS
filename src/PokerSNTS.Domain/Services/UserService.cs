using PokerSNTS.Domain.Entities;
using PokerSNTS.Domain.Helpers;
using PokerSNTS.Domain.Interfaces.Repositories;
using PokerSNTS.Domain.Interfaces.Services;
using PokerSNTS.Domain.Interfaces.UnitOfWork;
using PokerSNTS.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            INotificationHandler notifications)
            : base(unitOfWork, notifications)
        {
            _userRepository = userRepository;
        }

        public async Task AddAsync(User user)
        {
            var users = await GetAllAsync();

            if (users.Any(x => x.UserName == user.UserName))
                AddNotification("Já existe outro usuário cadastrado com esse e-mail.");

            if (ValidateEntity(user))
            {
                _userRepository.Add(user);

                if (!await CommitAsync()) 
                    AddNotification("Não foi possível cadastrar o usuário.");
            }
        }

        public async Task UpdateAsync(Guid id, User user)
        {
            var existingUser = await _userRepository.GetByIdAsync(id);

            if (existingUser == null) 
                AddNotification("Usuário não encontrado.");

            existingUser.Update(user.UserName, user.Password);
            if (ValidateEntity(existingUser))
            {
                _userRepository.Update(existingUser);

                if (!await CommitAsync()) 
                    AddNotification("Não foi possível atualizar o usuário.");
            }
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<User> ValidateLoginAsync(string userName, string password)
        {
            var user = await _userRepository.GetByUserNameAsync(userName);
            if (user == null)
            {
                AddNotification("Usuário e/ou senha inválido(s).");
                return null;
            }

            var encryptedPassword = CryptographyHelper.Sha256(password);
            if (user.Password.Equals(encryptedPassword)) return user;

            AddNotification("Usuário e/ou senha inválido(s).");

            return null;
        }
    }
}
