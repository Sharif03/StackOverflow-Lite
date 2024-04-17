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
        public async Task<bool> IsTitleDuplicateAsync(string questionTitle, Guid? id = null)
        {
            if (id.HasValue)
            {
                return await GetCountAsync(x => x.Id != id.Value && x.Title == questionTitle) > 0;
            }
            else
            {
                return await GetCountAsync(x => x.Title == questionTitle) > 0;
            }
        }

    }
}
