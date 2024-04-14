using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using StackOverflowLite.Domain.Entities;
using StackOverflowLite.Domain.Repositories;
using Microsoft.EntityFrameworkCore;


namespace StackOverflowLite.Infrastructure.Repositories
{
	public class QuestionRepository : Repository<Question, Guid>, IQuestionRepository
	{
		public QuestionRepository(IApplicationDbContext context) : base((DbContext)context)
		{

		}

	}
}
