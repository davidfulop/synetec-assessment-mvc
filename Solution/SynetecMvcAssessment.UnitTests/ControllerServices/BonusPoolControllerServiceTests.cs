using System.Collections.Generic;
using System.Linq;
using InterviewTestTemplatev2.ControllerServices;
using InterviewTestTemplatev2.Data;
using InterviewTestTemplatev2.Models;
using NSubstitute;
using NUnit.Framework;

namespace SynetecMvcAssessment.UnitTests.ControllerServices
{
    public class BonusPoolControllerServiceTests
    {
        private IBonusPoolModelData _fakeData;
        private List<Employee> _employees;
        private BonusPoolControllerService _service;

        [SetUp]
        public void Before_each()
        {
            _employees = new List<Employee> {
                new Employee {Id = 0, FullName = "Alf Stokes", Salary = 10000},
                new Employee {Id = 1, FullName = "Bender Rodriguez", Salary = 1000}
            };
            _fakeData = Substitute.For<IBonusPoolModelData>();
            _fakeData.Employees.Returns(_employees.AsQueryable());

            _service = new BonusPoolControllerService(_fakeData);
        }

        [Test]
        public void Should_return_correct_model_for_Index()
        {
            var model = _service.GenerateIndexModel();

            Assert.That(model, Is.Not.Null);
            Assert.That(model, Is.InstanceOf<BonusPoolCalculatorModel>());
        }

        [Test]
        public void Should_have_all_employees_in_model()
        {
            var model = _service.GenerateIndexModel();

            Assert.That(model.AllEmployees, Is.EquivalentTo(_employees));
        }

        [Test]
        public void Should_return_correct_model_for_Calculate()
        {
            var inputModel = SetUpInputModelForCalculateBonus();
            var model = _service.CalculateBonusForEmployee(inputModel);

            Assert.That(model, Is.Not.Null);
            Assert.That(model, Is.InstanceOf<BonusPoolCalculatorResultModel>());
        }

        [TestCase(0, 0)]
        [TestCase(1000, 909)]
        [TestCase(500, 454)]
        [TestCase(-1, 0)]
        public void Should_set_correct_bonus_for_pool(int pool, int expectedBonus)
        {
            var inputModel = SetUpInputModelForCalculateBonus(pool, 0);

            var resultModel = _service.CalculateBonusForEmployee(inputModel);

            Assert.That(resultModel.BonusPoolAllocation, Is.EqualTo(expectedBonus));
        }

        [TestCase(0, 90909)]
        [TestCase(1, 9090)]
        public void Should_set_correct_bonus_for_employee(int employeeId, int expectedBonus)
        {
            var inputModel = SetUpInputModelForCalculateBonus(100000, employeeId);

            var resultModel = _service.CalculateBonusForEmployee(inputModel);

            Assert.That(resultModel.BonusPoolAllocation, Is.EqualTo(expectedBonus));
        }

        private BonusPoolCalculatorModel SetUpInputModelForCalculateBonus(
            int bonusPoolAmount = 1000, int selectedEmployeeId = 0)
        {
            return new BonusPoolCalculatorModel {
                BonusPoolAmount = bonusPoolAmount,
                SelectedEmployeeId = selectedEmployeeId,
                AllEmployees = _employees.ToList() };
        }
    }
}
