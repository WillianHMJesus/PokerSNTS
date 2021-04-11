using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokerSNTS.API.InputModels;
using PokerSNTS.Domain.Adapters;
using PokerSNTS.Domain.DTOs;
using PokerSNTS.Domain.Entities;
using PokerSNTS.Domain.Helpers;
using PokerSNTS.Domain.Interfaces.Services;
using PokerSNTS.Domain.Notifications;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PokerSNTS.API.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService,
            INotificationHandler notifications)
            : base(notifications)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] UserInputModel model)
        {
            if (ModelState.IsValid)
            {
                var encryptedPassword = CryptographyHelper.Sha256(model.Password);
                var user = new User(model.UserName, encryptedPassword);
                await _userService.AddAsync(user);

                if (ValidOperation())
                    return CreatedAtAction(nameof(GetByIdAsync), user.Id);

                return ResponseInvalid();
            }

            NotifyModelStateError();

            return ResponseInvalid();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] UserInputModel model)
        {
            if (default(Guid).Equals(id))
            {
                AddNotification("O Id do usuário não foi informado.");
                return ResponseInvalid();
            }

            if (ModelState.IsValid)
            {
                var encryptedPassword = CryptographyHelper.Sha256(model.Password);
                var user = new User(model.UserName, encryptedPassword);
                await _userService.UpdateAsync(id, user);

                if (ValidOperation()) return NoContent();

                return ResponseInvalid();
            }

            NotifyModelStateError();

            return ResponseInvalid();
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var users = await _userService.GetAllAsync();

            if (users.Any())
                return Ok(users.Select(x => UserAdapter.ToUserDTO(x)));

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);

            if (user != null)
                return Ok(UserAdapter.ToUserDTO(user));

            return NoContent();
        }

        [HttpPost("Token")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserInputModel model)
        {
            if(ModelState.IsValid)
            {
                var user = await _userService.ValidateLoginAsync(model.UserName, model.Password);

                if(ValidOperation())
                {
                    var accessToken = Token.GenerateToken(user);
                    var tokenDTO = new TokenDTO(accessToken, Token.expirationDate);
                    var userDTO = UserAdapter.ToUserDTO(user, tokenDTO);

                    return Ok(userDTO);
                }

                return ResponseInvalid();
            }

            NotifyModelStateError();

            return ResponseInvalid();
        }

        [AllowAnonymous]
        [HttpGet("Anonymous")]
        public IActionResult Anonymous() => Ok("Olá usuário Anônimo");

        [Authorize]
        [HttpGet("Authorize")]
        public IActionResult Authorize() => Ok(string.Format("Olá {0}", User.Identity.Name));
    }
}
