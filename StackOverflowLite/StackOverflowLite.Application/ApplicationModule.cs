using Autofac;

using StackOverflowLite.Application.Features.Posting.Services;

namespace StackOverflowLite.Application
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<QuestionPostingService>().As<IQuestionPostingService>()
                .InstancePerLifetimeScope();
        }
    }
}