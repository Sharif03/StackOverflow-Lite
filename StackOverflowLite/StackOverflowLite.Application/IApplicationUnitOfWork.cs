using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using StackOverflowLite.Domain;
using StackOverflowLite.Domain.Repositories;

namespace StackOverflowLite.Application
{
	public interface IApplicationUnitOfWork : IUnitOfWork
	{
        IQuestionRepository QuestionRepository { get; }
    }
}
