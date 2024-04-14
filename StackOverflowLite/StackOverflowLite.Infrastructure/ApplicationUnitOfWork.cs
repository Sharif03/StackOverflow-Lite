using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackOverflowLite.Application;
using StackOverflowLite.Application.Features.Posting;
using StackOverflowLite.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace StackOverflowLite.Infrastructure
{
	public class ApplicationUnitOfWork : UnitOfWork, IApplicationUnitOfWork
	{
		public IQuestionRepository QuestionRepository { get; private set; }
		public ApplicationUnitOfWork(IApplicationDbContext context, IQuestionRepository questionRepository) : base((DbContext)context)
		{
            QuestionRepository = questionRepository;
		}

	}
}
