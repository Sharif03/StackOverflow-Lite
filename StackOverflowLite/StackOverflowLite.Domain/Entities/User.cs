using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
namespace StackOverflowLite.Domain.Entities
{
    public enum Level
    {
        Bronze,
        Silver,
        Gold
    }
    public class User : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public bool IsVerified { get; set; }
        public Level UserLevel { get; set; }
        public IList<Question> Questions { get; set; }
        public IList<QuestionComment> QuestionComments { get; set; }
        public IList<Answer> Answers { get; set; }
        public IList<AnswerComment> AnswerComments { get; set; }
    }
}
