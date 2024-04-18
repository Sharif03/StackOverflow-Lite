using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Identity;
using StackOverflowLite.Domain;
using StackOverflowLite.Domain.Entities;
using StackOverflowLite.Domain.Exceptions;

namespace StackOverflowLite.Application.Features.Posting.Services
{
    public class QuestionPostingService : IQuestionPostingService
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly IUserIdentityService _userIdentityService;
        public QuestionPostingService(IApplicationUnitOfWork applicationUnitOfWork, IUserIdentityService userIdentityService)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _userIdentityService = userIdentityService;
        }

        public async Task CreateQuestionAsync(string questionTitle, string questionContent, string questionTags)
        {
            var userId = await _userIdentityService.GetCurrentLoggedInUserGuidAsync();

            bool isDuplicate = await _applicationUnitOfWork.QuestionRepository.IsTitleDuplicateAsync(questionTitle);
            if (isDuplicate)
                throw new DuplicateTitleException();
            var question = new Question
            {
                Id = Guid.NewGuid(),
                Title = questionTitle,
                Content = questionContent,
                Tags = questionTags,
                Upvote = 0,
                Downvote = 0,
                QuestionPosted = DateTime.Now,
                // UserId = Guid.NewGuid()
			};
			
			await _applicationUnitOfWork.QuestionRepository.AddAsync(question);
            await _applicationUnitOfWork.SaveAsync();
        }


    }
}
