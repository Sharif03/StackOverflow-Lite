using Autofac;
using MailKit.Search;
using StackOverflowLite.Application.Features.Posting.Services;
using StackOverflowLite.Domain.Entities;
using StackOverflowLite.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace StackOverflowLite.Web.Areas.Identity.Models
{
    public class UserModel
    {
        private ILifetimeScope _scope;
        private IQuestionPostingService _questionPostingService;

        public UserModel()
        {
        }

        public UserModel(IQuestionPostingService questionPostingService)
        {
            _questionPostingService = questionPostingService;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _questionPostingService = _scope.Resolve<IQuestionPostingService>();
        }
    }
}
