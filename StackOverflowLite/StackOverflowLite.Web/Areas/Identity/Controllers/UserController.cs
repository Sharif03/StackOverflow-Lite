using Autofac;
using Markdig;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackOverflowLite.Domain.Exceptions;
using StackOverflowLite.Infrastructure;
using StackOverflowLite.Web.Areas.Admin.Models;
using static System.Formats.Asn1.AsnWriter;

namespace StackOverflowLite.Web.Areas.Identity.Controllers
{
    [Area("Identity")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<UserController> _logger;

        public UserController(ILifetimeScope scope, ILogger<UserController> logger)
        {
            _scope = scope;
            _logger = logger;
        }

        public IActionResult AdminPanel()
        {
            return View();
        }
        public IActionResult UserPanel()
        {
            return View();
        }
    }
}
