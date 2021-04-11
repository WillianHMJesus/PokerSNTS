using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokerSNTS.Domain.Notifications;
using System;
using System.Linq;

namespace PokerSNTS.API.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("v1/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        private readonly INotificationHandler _notifications;

        protected BaseController(INotificationHandler notifications)
        {
            _notifications = notifications;
        }

        protected void NotifyModelStateError()
        {
            var erros = ModelState.Values.SelectMany(x => x.Errors);
            foreach (var erro in erros)
            {
                var mensagemErro = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                AddNotification(mensagemErro);
            }
        }

        protected void AddNotification(string message)
        {
            _notifications.HandleNotification("ApiValidation", message);
        }

        protected bool ValidOperation()
        {
            return !_notifications.HasNotification();
        }

        protected IActionResult ResponseInvalid()
        {
            return BadRequest(new
            {
                Messages = _notifications.GetNotifications().Select(x => x.Value)
            });
        }

        protected string GetRouteById(Guid id)
        {
            return string.Format("{0}/{1}", ControllerContext.HttpContext.Request.Path, id);
        }
    }
}
