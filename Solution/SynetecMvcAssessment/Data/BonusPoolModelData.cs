using System.Linq;
using AutoMapper;
using InterviewTestTemplatev2.Models;

namespace InterviewTestTemplatev2.Data
{
    public interface IBonusPoolModelData
    {
        IQueryable<Employee> Employees { get; }
    }

    public class BonusPoolModelData : IBonusPoolModelData
    {
        private readonly MvcInterviewV3Entities1 _context;
        private readonly IMapper _employeeMapper;

        public BonusPoolModelData(IMapperFactory mapperFactory)
        {
            _context = new MvcInterviewV3Entities1();
            _employeeMapper = mapperFactory.CreateDataToModelMapper();
        }

        public IQueryable<Employee> Employees
        {
            get { return _context.HrEmployees.ToList()
                .Select(e => _employeeMapper.Map<Employee>(e))
                .AsQueryable(); }
        }
    }
}
