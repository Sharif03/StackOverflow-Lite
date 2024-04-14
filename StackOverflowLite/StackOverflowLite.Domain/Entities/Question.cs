using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StackOverflowLite.Domain.Entities
{
    public class Question : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public List<string> Tags { get; set; }
        public int Upvotes { get; set; }
        public int Downvotes { get; set; }
        public DateTime DatePosted { get; set; }
    }
}
