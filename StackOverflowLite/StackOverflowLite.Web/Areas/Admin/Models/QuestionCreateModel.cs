using Autofac;
using AutoMapper;
using StackOverflowLite.Application.Features.Posting.Services;
using System.ComponentModel.DataAnnotations;

namespace StackOverflowLite.Web.Areas.Admin.Models
{
    public class QuestionCreateModel
    {
		private ILifetimeScope _scope;
        private IMapper _mapper;
        private IQuestionPostingService _questionPostingService;

		[Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required, StringLength(50, ErrorMessage = "Tag length should be between 2 & 50")]
        public string Tags { get; set; }

        public QuestionCreateModel() { }

		public QuestionCreateModel(IQuestionPostingService questionPostingService, IMapper mapper)
		{
			_questionPostingService = questionPostingService;
			_mapper = mapper;
		}

		internal void Resolve(ILifetimeScope scope)
		{
			_scope = scope;
			_questionPostingService = _scope.Resolve<IQuestionPostingService>();
            _mapper = _scope.Resolve<IMapper>();
        }

		internal async Task CreateQuestionAsync()
		{
			await _questionPostingService.CreateQuestionAsync(Title, Content, Tags);
		}
	}
}
