using System.Linq;
using InterviewTestTemplatev2.Data;
using InterviewTestTemplatev2.Models;

namespace InterviewTestTemplatev2.ControllerServices
{
    public interface IBonusPoolControllerService
    {
        BonusPoolCalculatorModel GenerateIndexModel();
        BonusPoolCalculatorResultModel CalculateBonusForEmployee(BonusPoolCalculatorModel model);
    }

    public class BonusPoolControllerService : IBonusPoolControllerService
    {
        private readonly IBonusPoolModelData _bonusPoolModelData;

        public BonusPoolControllerService(IBonusPoolModelData bonusPoolModelData)
        {
            _bonusPoolModelData = bonusPoolModelData;
        }

        public BonusPoolCalculatorModel GenerateIndexModel()
        {
            var model = new BonusPoolCalculatorModel();
            model.AllEmployees = _bonusPoolModelData.Employees.ToList();
            return model;
        }

        public BonusPoolCalculatorResultModel CalculateBonusForEmployee(BonusPoolCalculatorModel model)
        {
            int selectedEmployeeId = model.SelectedEmployeeId;
            int totalBonusPool = model.BonusPoolAmount;

            var employee = _bonusPoolModelData.Employees
                .FirstOrDefault(item => item.Id == selectedEmployeeId);

            int employeeSalary = employee.Salary;

            //get the total salary budget for the company
            int totalSalary = _bonusPoolModelData.Employees.Sum(item => item.Salary);

            //calculate the bonus allocation for the employee
            decimal bonusPercentage = (decimal) employeeSalary / (decimal) totalSalary;
            int bonusAllocation = (int) (bonusPercentage * totalBonusPool);

            BonusPoolCalculatorResultModel result = new BonusPoolCalculatorResultModel();
            result.Employee = employee;
            result.BonusPoolAllocation = bonusAllocation;
            return result;
        }
    }
}