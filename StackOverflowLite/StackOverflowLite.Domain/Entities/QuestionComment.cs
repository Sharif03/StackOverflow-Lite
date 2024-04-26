using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflowLite.Domain.Entities
{
    public class QuestionComment : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime CommentPosted { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
