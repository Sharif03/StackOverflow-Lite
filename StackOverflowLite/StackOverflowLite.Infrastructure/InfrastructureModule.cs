using Autofac;
using StackOverflowLite.Application;
using StackOverflowLite.Application.Utilities;
using StackOverflowLite.Domain.Repositories;
using StackOverflowLite.Infrastructure.Email;
using StackOverflowLite.Infrastructure.Repositories;

namespace StackOverflowLite.Infrastructure
{
    public class InfrastructureModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssembly;
        public InfrastructureModule(string connectionString, string migrationAssembly)
        {
            _connectionString = connectionString;
            _migrationAssembly = migrationAssembly;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationDbContext>().AsSelf()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssembly", _migrationAssembly)
                .InstancePerLifetimeScope();
            builder.RegisterType<ApplicationDbContext>().As<IApplicationDbContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssembly", _migrationAssembly)
            .InstancePerLifetimeScope();

            builder.RegisterType<ApplicationUnitOfWork>().As<IApplicationUnitOfWork>()
                .InstancePerLifetimeScope();
            builder.RegisterType<QuestionRepository>().As<IQuestionRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<HtmlEmailService>().As<IEmailService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<UserRepository>().As<IUserRepository>()
                .InstancePerLifetimeScope();
        }
    }
}
