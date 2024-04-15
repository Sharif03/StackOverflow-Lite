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
        Beginner,
        Intermediate,
        Advanced
    }
    public class User : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsVerified { get; private set; }
        public Level UserLevel { get; set; }
    }
}
