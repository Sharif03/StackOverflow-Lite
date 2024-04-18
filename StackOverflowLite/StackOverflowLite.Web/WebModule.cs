using Autofac;
using StackOverflowLite.Application.Features.Posting.Services;
using StackOverflowLite.Web.Areas.Admin.Models;
using StackOverflowLite.Web.Models;
using StackOverflowLite.Web.Services;

namespace StackOverflowLite.Web
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RegistrationModel>().AsSelf();
            builder.RegisterType<LoginModel>().AsSelf();
            builder.RegisterType<QuestionCreateModel>().AsSelf();
            builder.RegisterType<CurrentUserEmailService>().As<ICurrentUserEmailService>()
                .InstancePerLifetimeScope();
        }
    }
}
