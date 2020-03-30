using System.Linq;
using System.Web.Mvc;
using InterviewTestTemplatev2.Data;
using InterviewTestTemplatev2.Models;

namespace InterviewTestTemplatev2.Controllers
{
    public class BonusPoolController : Controller
    {
        private readonly IBonusPoolModelData _bonusPoolModelData;

        public BonusPoolController(IBonusPoolModelData bonusPoolModelData)
        {
            _bonusPoolModelData = bonusPoolModelData;
        }

        public ActionResult Index()
        {
            BonusPoolCalculatorModel model = new BonusPoolCalculatorModel();
            model.AllEmployees = _bonusPoolModelData.Employees.ToList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Calculate(BonusPoolCalculatorModel model)
        {
            int selectedEmployeeId = model.SelectedEmployeeId;
            int totalBonusPool = model.BonusPoolAmount;

            var employee = _bonusPoolModelData.Employees
                .FirstOrDefault(item => item.Id == selectedEmployeeId);

            int employeeSalary = employee.Salary;

            //get the total salary budget for the company
            int totalSalary = _bonusPoolModelData.Employees.Sum(item => item.Salary);

            //calculate the bonus allocation for the employee
            decimal bonusPercentage = (decimal)employeeSalary / (decimal)totalSalary;
            int bonusAllocation = (int)(bonusPercentage * totalBonusPool);

            BonusPoolCalculatorResultModel result = new BonusPoolCalculatorResultModel();
            result.Employee = employee;
            result.BonusPoolAllocation = bonusAllocation;
            
            return View(result);
        }
    }
}