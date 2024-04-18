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
        private readonly ICurrentUserEmailService _currentUserEmailService;
        public QuestionPostingService(IApplicationUnitOfWork applicationUnitOfWork, ICurrentUserEmailService currentUserEmailService)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _currentUserEmailService = currentUserEmailService;
        }

        public async Task CreateQuestionAsync(string questionTitle, string questionContent, string questionTags)
        {
            string userEmail = await _currentUserEmailService.GetCurrentLoggedInUserEmailAsync();

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
