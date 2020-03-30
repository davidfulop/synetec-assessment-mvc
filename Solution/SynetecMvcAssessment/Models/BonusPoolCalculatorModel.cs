using System.Collections.Generic;

namespace InterviewTestTemplatev2.Models
{
    public class BonusPoolCalculatorModel
    {
        public int BonusPoolAmount { get; set; }
        public List<Employee> AllEmployees { get; set; }
        public int SelectedEmployeeId { get; set; }
    }
}
