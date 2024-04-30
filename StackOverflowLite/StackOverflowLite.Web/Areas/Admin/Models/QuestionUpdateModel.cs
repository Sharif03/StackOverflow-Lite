using Autofac;
using AutoMapper;
using StackOverflowLite.Application.Features.Posting.Services;
using StackOverflowLite.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace StackOverflowLite.Web.Areas.Admin.Models
{
    public class QuestionUpdateModel
    {
        private ILifetimeScope _scope;
        private IMapper _mapper;
        private IQuestionPostingService _questionPostingService;

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Tags { get; set; }

        public QuestionUpdateModel() { }

        public QuestionUpdateModel(IQuestionPostingService questionPostingService, IMapper mapper)
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

        internal async Task LoadAsync(Guid id)
        {
            Question question = await _questionPostingService.GetQuestionAsync(id);
            if (question != null)
            {
                _mapper.Map(question, this);
            }
        }
        internal async Task UpdateQuestionAsync()
        {
            if (!string.IsNullOrWhiteSpace(Title) || !string.IsNullOrWhiteSpace(Tags))
            {
                await _questionPostingService.UpdateQuestionAsync(Id, Title, Content, Tags);
            }
        }
    }
}
