using AutoMapper;
using StackOverflowLite.Domain.Entities;
using StackOverflowLite.Web.Areas.Admin.Models;

namespace StackOverflowLite.Web
{
    public class WebProfile : Profile
    {
        public WebProfile()
        {
            CreateMap<QuestionViewModel, Question>().ReverseMap();
            CreateMap<QuestionUpdateModel, Question>().ReverseMap();
        }
    }
}
