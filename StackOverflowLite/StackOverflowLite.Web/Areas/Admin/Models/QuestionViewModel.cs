using Autofac;
using AutoMapper;
using StackOverflowLite.Application.Features.Posting.Services;
using StackOverflowLite.Domain.Entities;

namespace StackOverflowLite.Web.Areas.Admin.Models
{
    public class QuestionViewModel
    {
        private ILifetimeScope _scope;
        private IMapper _mapper;
        private IQuestionPostingService _questionPostingService;

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Tags { get; set; }
        public int Upvote { get; set; }
        public int Downvote { get; set; }
        public int VoteCount { get; set; }
        public DateTime QuestionPosted { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
        public IList<Answer> Answers { get; set; }
        public IList<QuestionComment> QuestionComments { get; set; }

        public QuestionViewModel() { }

        public QuestionViewModel(IQuestionPostingService questionPostingService, IMapper mapper)
        {
            _questionPostingService = questionPostingService;
            _mapper = mapper;
        }

        internal void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _questionPostingService = _scope.Resolve<IQuestionPostingService>();
        }

        internal async Task LoadAsync(Guid id)
        {
            Question question = await _questionPostingService.GetQuestionAsync(id);
            if (question != null)
            {
                _mapper.Map(question, this);
            }
        }
    }   
}
