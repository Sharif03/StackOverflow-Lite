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

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(QuestionCreateModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.Resolve(_scope);
                    await model.CreateQuestionAsync();

                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "Question created successfully",
                        Type = ResponseTypes.Success
                    });

                    return RedirectToAction("Index");
                }
                catch (DuplicateTitleException ex)
                {
                    _logger.LogError(ex, "Server Error");
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "Question creation failed. Title is duplicate",
                        Type = ResponseTypes.Danger
                    });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Server Error");
                }
            }
            return View(model);
        }
    }
}
