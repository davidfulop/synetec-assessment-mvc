using AutoMapper;
using InterviewTestTemplatev2.Data;
using InterviewTestTemplatev2.Models;

namespace InterviewTestTemplatev2
{
    public interface IMapperFactory
    {
        IMapper CreateDataToModelMapper();
    }

    public class MapperFactory : IMapperFactory
    {
        public IMapper CreateDataToModelMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<DataToModelMapper>();
            });

            return config.CreateMapper();
        }
    }

    public class DataToModelMapper : Profile
    {
        public DataToModelMapper()
        {
            CreateMap<HrEmployee, Employee>()
                .ForMember(e => e.FullName, expression => expression.MapFrom(he => he.Full_Name))
                .ForMember(e => e.Id, expression => expression.MapFrom(he => he.ID));
        }
    }
}
