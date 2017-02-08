using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SampleWebApp.Controllers
{
    public class HomeController : Controller
    {
        private ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UnhandledException()
        {
            throw new InvalidOperationException();
        }

        public IActionResult HandledException()
        {
            try
            {
                var  x = UhOh();
            }
            catch (System.Exception ex)
            {
                _logger.LogWarning(0, ex, "Something went wrong");
            }

            return Content("Exception handled");
        }

        public IActionResult LogMessage()
        {
            _logger.LogDebug("Hello, this is a debug message from {0}", nameof(LogMessage));
            return Content("Message recorded");
        }

        int z = 0;
        private int UhOh()
        {
            return 33 / z;
        }
    }
}
