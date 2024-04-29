using Autofac;
using MailKit.Search;
using StackOverflowLite.Application.Features.Posting.Services;
using StackOverflowLite.Domain.Entities;
using StackOverflowLite.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace StackOverflowLite.Web.Areas.Admin.Models
{
    public class QuestionListModel
    {
        private ILifetimeScope _scope;
        private IQuestionPostingService _questionPostingService;

        public QuestionListModel()
        {
        }

        public QuestionListModel(IQuestionPostingService questionPostingService)
        {
            _questionPostingService = questionPostingService;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _questionPostingService = _scope.Resolve<IQuestionPostingService>();
        }
        public async Task<object> GetPagedCoursesAsync(DataTablesAjaxRequestUtility dataTablesUtility)
        {
            var data = await _questionPostingService.GetPagedCoursesAsync(
                dataTablesUtility.SearchText,
                dataTablesUtility.GetSortText(new string[] { "Title", "Content", "Tags" }),
                dataTablesUtility.PageIndex,
                dataTablesUtility.PageSize);

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                HttpUtility.HtmlEncode(record.Title),
                                HttpUtility.HtmlEncode(record.Content),
                                HttpUtility.HtmlEncode(record.Tags),
                                record.Id.ToString()
                        }
                    ).ToArray()
            };
        }
    }
}
