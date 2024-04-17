using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackOverflowLite.Domain;
using StackOverflowLite.Domain.Entities;
using StackOverflowLite.Domain.Exceptions;

namespace StackOverflowLite.Application.Features.Posting.Services
{
    public class QuestionPostingService : IQuestionPostingService
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        public QuestionPostingService(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }

        public async Task CreateQuestionAsync(string questionTitle, string questionContent, string questionTags)
        {
            bool isDuplicate = await _applicationUnitOfWork.QuestionRepository.IsTitleDuplicateAsync(questionTitle);
            if (isDuplicate)
                throw new DuplicateTitleException();
            var question = new Question
            {
                
            };

            await _applicationUnitOfWork.QuestionRepository.AddAsync(question);
            await _applicationUnitOfWork.SaveAsync();
        }


    }
}
