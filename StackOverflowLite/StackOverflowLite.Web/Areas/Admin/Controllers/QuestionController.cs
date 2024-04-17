using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackOverflowLite.Domain.Exceptions;
using StackOverflowLite.Infrastructure;
using StackOverflowLite.Web.Areas.Admin.Models;
using static System.Formats.Asn1.AsnWriter;

namespace StackOverflowLite.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QuestionController : Controller
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<QuestionController> _logger;

        public QuestionController(ILifetimeScope scope, ILogger<QuestionController> logger)
        {
            _scope = scope;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = _scope.Resolve<QuestionCreateModel>();
            return View(model);
        }

    }
}
