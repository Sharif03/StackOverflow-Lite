using Autofac;
using StackOverflowLite.Application.Features.Posting.Services;
using StackOverflowLite.Infrastructure;

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
            throw new NotImplementedException();
        }
}
