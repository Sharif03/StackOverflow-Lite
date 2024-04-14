using StackOverflowLite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflowLite.Domain.Repositories
{
	public interface IQuestionRepository : IRepositoryBase<Question, Guid>
	{
		Task<bool> IsTitleDuplicateAsync(string title, Guid? id = null);
		Task<(IList<Question> records, int total, int totalDisplay)>GetTableDataAsync(
			string searchTitle, uint searchFeesFrom,uint searchFeesTo, string orderBy, int pageIndex, int pageSize);
	}
}
