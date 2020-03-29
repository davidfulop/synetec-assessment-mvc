using System.Data.Entity;

namespace InterviewTestTemplatev2.Data
{
    public interface IBonusPoolModelData
    {
        DbSet<HrDepartment> HrDepartments { get; set; }
        DbSet<HrEmployee> HrEmployees { get; set; }
    }

    public class BonusPoolModelData : IBonusPoolModelData
    {
        private readonly MvcInterviewV3Entities1 _context;

        public BonusPoolModelData()
        {
            _context = new MvcInterviewV3Entities1();
        }

        public DbSet<HrDepartment> HrDepartments
        {
            get { return _context.HrDepartments; }
            set { _context.HrDepartments = value; }
        }

        public DbSet<HrEmployee> HrEmployees
        {
            get { return _context.HrEmployees; }
            set { _context.HrEmployees = value; }
        }
    }
}
