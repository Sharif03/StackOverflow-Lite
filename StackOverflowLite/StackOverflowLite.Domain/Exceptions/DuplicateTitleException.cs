using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflowLite.Domain.Exceptions
{
	public class DuplicateTitleException : Exception 
	{
		public DuplicateTitleException() : base("Question Title is duplicate")
		{
			
		}
	}
}
