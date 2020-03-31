using System.Linq;
using InterviewTestTemplatev2.Data;
using InterviewTestTemplatev2.Models;

namespace InterviewTestTemplatev2.ControllerServices
{
    public interface IBonusPoolIndexService
    {
        BonusPoolCalculatorModel GenerateIndexModel();
    }

    public class BonusPoolIndexService : IBonusPoolIndexService
    {
        private readonly IBonusPoolModelData _bonusPoolModelData;

        public BonusPoolIndexService(IBonusPoolModelData bonusPoolModelData)
        {
            _bonusPoolModelData = bonusPoolModelData;
        }

        public BonusPoolCalculatorModel GenerateIndexModel()
        {
            return new BonusPoolCalculatorModel {
                AllEmployees = _bonusPoolModelData.Employees.ToList()
            };
        }
    }
}
