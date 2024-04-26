using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflowLite.Domain.Entities
{
    public class AnswerComment : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime CommentPosted { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid AnswerId { get; set; }
        public Answer Answer { get; set; }
    }
}
