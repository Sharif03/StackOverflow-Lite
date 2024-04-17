using Autofac;
using StackOverflowLite.Application.Features.Posting.Services;
using System.ComponentModel.DataAnnotations;

namespace StackOverflowLite.Web.Areas.Admin.Models
{
    public class QuestionCreateModel
    {
		private ILifetimeScope _scope;
		private IQuestionPostingService _questionPostingService;

		[Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required, StringLength(50, ErrorMessage = "Tag length should be between 2 & 50")]
        public string Tags { get; set; }

        public QuestionCreateModel() { }

		public QuestionCreateModel(IQuestionPostingService questionPostingService)
		{
			_questionPostingService = questionPostingService;
		}

		internal void Resolve(ILifetimeScope scope)
		{
			_scope = scope;
			_questionPostingService = _scope.Resolve<IQuestionPostingService>();
		}

		internal async Task CreateQuestionAsync()
		{
			await _questionPostingService.CreateQuestionAsync(Title, Content, Tags);
		}
	}
}
