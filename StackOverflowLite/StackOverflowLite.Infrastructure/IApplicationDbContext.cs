using StackOverflowLite.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace StackOverflowLite.Infrastructure
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Question> Questions { get; set; }
        DbSet<Answer> Answers { get; set; }
        DbSet<Comment> Comments { get; set; }
    }
}