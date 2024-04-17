using Autofac;
using StackOverflowLite.Web.Areas.Admin.Models;
using StackOverflowLite.Web.Models;

namespace StackOverflowLite.Web
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RegistrationModel>().AsSelf();
            builder.RegisterType<LoginModel>().AsSelf();
            builder.RegisterType<QuestionCreateModel>().AsSelf();

        }
    }
}
