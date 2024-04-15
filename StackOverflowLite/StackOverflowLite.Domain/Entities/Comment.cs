using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflowLite.Domain.Entities
{
    public class Comment : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public Guid UserId { get; set; }
        public Guid QuestionId { get; set; }
    }
}
