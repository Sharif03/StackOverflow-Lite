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
        Task<(IList<Question> records, int total, int totalDisplay)>GetPagedQuestionsAsync(string searchText, string sortBy, int pageIndex, int pageSize);
        Task<Question> GetQuestionAsync(Guid id);
        Task UpdateQuestionAsync(Guid id, string title, string content, string tags);
    }
}
