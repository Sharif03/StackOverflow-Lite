using Autofac;
using Markdig;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackOverflowLite.Domain.Exceptions;
using StackOverflowLite.Infrastructure;
using StackOverflowLite.Web.Areas.Admin.Models;
using static System.Formats.Asn1.AsnWriter;

namespace StackOverflowLite.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
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
        [HttpPost]
        public async Task<JsonResult> GetQuestions(QuestionListModel model)
        {
            var dataTablesModel = new DataTablesAjaxRequestUtility(Request);
            model.Resolve(_scope);

            var data = await model.GetPagedCoursesAsync(dataTablesModel);
            return Json(data);
        }

        [HttpGet]
        public async Task<IActionResult> ViewQuestion(Guid id)
        {
            var model = _scope.Resolve<QuestionViewModel>();
            await model.LoadAsync(id);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
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

                    // Get markdown-formatted text from the model
                    string markdownText = model.Content;

                    // Parse markdown text to HTML using Markdig
                    string htmlContent = Markdown.ToHtml(markdownText);

                    // Set the HTML content in the model
                    model.Content = htmlContent;

                    // Create the question asynchronously
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

        public async Task<IActionResult> Update(Guid id)
        {
            var model = _scope.Resolve<QuestionUpdateModel>();
            await model.LoadAsync(id);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Update(QuestionUpdateModel model)
        {
            model.Resolve(_scope);

            if (ModelState.IsValid)
            {
                try
                {
                    await model.UpdateQuestionAsync();
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "Question updated successfully",
                        Type = ResponseTypes.Success
                    });

                    return RedirectToAction("Index");
                }
                catch (DuplicateTitleException ex)
                {
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = ex.Message,
                        Type = ResponseTypes.Danger
                    });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Server Error");

                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "There was a problem in updating question",
                        Type = ResponseTypes.Danger
                    });
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var model = _scope.Resolve<QuestionListModel>();

            if (ModelState.IsValid)
            {
                try
                {
                    await model.DeleteQuestionAsync(id); 
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "Course deleted successfully",
                        Type = ResponseTypes.Success
                    });

                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Server Error");

                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "There was a problem in deleting course",
                        Type = ResponseTypes.Danger
                    });
                }
            }

            return RedirectToAction("Index");
        }
    }
}
