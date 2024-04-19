using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflowLite.Domain.Exceptions
{
    public class GUIDNullValueException : Exception
    {
        public GUIDNullValueException() : base("GUID has Null value")
        {

        }
    }
}
