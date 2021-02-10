using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace OpenA3XX.Peripheral.WebApi.Controllers
{
    public class HardwareSignalController : Controller
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly ILogger<HardwareSignalController> _logger;

        public HardwareSignalController(ILogger<HardwareSignalController> logger, IHttpContextAccessor accessor)
        {
            _logger = logger;
            _accessor = accessor;
        }
    }
}