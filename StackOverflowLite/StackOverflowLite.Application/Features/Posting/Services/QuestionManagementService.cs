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
    public class QuestionManagementService : IQuestionManagementService
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        public QuestionManagementService(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }

    }
}
