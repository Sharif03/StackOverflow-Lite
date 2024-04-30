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

            builder.RegisterType<QuestionListModel>().AsSelf();
            builder.RegisterType<QuestionCreateModel>().AsSelf();
            builder.RegisterType<QuestionViewModel>().AsSelf();
            builder.RegisterType<QuestionUpdateModel>().AsSelf();

            builder.RegisterType<UserIdentityService>().As<IUserIdentityService>()
                .InstancePerLifetimeScope();
        }
    }
}
