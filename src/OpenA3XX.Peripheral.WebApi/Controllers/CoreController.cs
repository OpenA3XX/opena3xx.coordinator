using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using OpenA3XX.Core.Repositories;
using OpenA3XX.Peripheral.WebApi.Hubs;

namespace OpenA3XX.Peripheral.WebApi.Controllers
{
    [ApiController]
    [Route("core")]
    public class CoreController : ControllerBase
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly ILogger<CoreController> _logger;

        public CoreController(ILogger<CoreController> logger, IHttpContextAccessor accessor,
            IHardwarePanelTokensRepository hardwarePanelTokensRepository)
        {
            _logger = logger;
            _accessor = accessor;
        }

        /// <summary>
        ///     Ping endpoint used for network discovery functionality in the hardware panels
        /// </summary>
        /// <returns></returns>
        [HttpGet("heartbeat/ping")]
        public string Ping()
        {
            return "Pong from OpenA3XX";
        }
    }
    
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IHubContext<ChatHub> hubContext;

        public ChatController(IHubContext<ChatHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        [HttpPost]
        public async Task SendMessage(ChatMessage message)
        {
            //additional business logic 

            await this.hubContext.Clients.All.SendAsync("messageReceivedFromApi", message);

            //additional business logic 
        }
    }
}