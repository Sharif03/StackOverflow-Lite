using StackOverflowLite.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace StackOverflowLite.Infrastructure
{
    public interface IApplicationDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}