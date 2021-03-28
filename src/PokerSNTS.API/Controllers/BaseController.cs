using Microsoft.AspNetCore.Mvc;
using PokerSNTS.Domain.Notifications;
using System.Linq;

namespace PokerSNTS.API.Controllers
{
    [Produces("application/json")]
    public abstract class BaseController : ControllerBase
    {
        private readonly IDomainNotificationHandler _notifications;

        protected BaseController(IDomainNotificationHandler notifications)
        {
            _notifications = notifications;
        }

        protected new IActionResult Response(object result = null)
        {
            if (_notifications.HasNotification())
            {
                return BadRequest(new { Messages = _notifications.GetNotifications().Select(x => x.Value) } );
            }

            return Ok(new { data = result });
        }

        protected void NotifyModelStateError()
        {
            var erros = ModelState.Values.SelectMany(x => x.Errors);
            foreach (var erro in erros)
            {
                var mensagemErro = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                AddError(mensagemErro);
            }
        }

        protected void AddError(string errorMessage)
        {
            _notifications.HandleNotification("ApiValidation", errorMessage);
        }
    }
}
