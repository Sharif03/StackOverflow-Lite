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
        public int Upvote { get; set; }
        public int Downvote { get; set; }
        public Guid UserId { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
