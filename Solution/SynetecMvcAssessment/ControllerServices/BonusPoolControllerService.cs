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
            return new BonusPoolCalculatorModel {
                AllEmployees = _bonusPoolModelData.Employees.ToList()
            };
        }

        public BonusPoolCalculatorResultModel CalculateBonusForEmployee(BonusPoolCalculatorModel model)
        {
            var employee = SelectEmployeeById(model);
            var bonus = CalculateBonus(employee.Salary, model.BonusPoolAmount);
            return CreateResultModel(employee, (int)bonus);
        }

        private Employee SelectEmployeeById(BonusPoolCalculatorModel model)
        {
            return _bonusPoolModelData.Employees.FirstOrDefault(
                employee => employee.Id == model.SelectedEmployeeId);
        }

        private int CalculateTotalSalary()
        {
            return _bonusPoolModelData.Employees.Sum(employee => employee.Salary);
        }

        private decimal CalculateBonus(int employeeSalary, int bonusPoolAmount)
        {
            var totalSalary = CalculateTotalSalary();
            var bonusPercentage = (decimal) employeeSalary / totalSalary;
            return bonusPercentage * bonusPoolAmount;
        }

        private BonusPoolCalculatorResultModel CreateResultModel(Employee employee, int bonus)
        {
            return new BonusPoolCalculatorResultModel {
                Employee = employee, BonusPoolAllocation = bonus
            };
        }
    }
}