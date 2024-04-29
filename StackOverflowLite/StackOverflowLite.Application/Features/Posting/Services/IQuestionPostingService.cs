using StackOverflowLite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflowLite.Application.Features.Posting.Services
{
	public interface IQuestionPostingService
	{
        Task CreateQuestionAsync(string questionTitle, string questionContent, string questionTags);
        Task<(IList<Question> records, int total, int totalDisplay)>GetPagedCoursesAsync(string sortBy, int pageIndex, int pageSize);

    }
}
