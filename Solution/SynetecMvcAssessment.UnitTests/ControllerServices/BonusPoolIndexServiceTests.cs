using System.Collections.Generic;
using System.Linq;
using InterviewTestTemplatev2.ControllerServices;
using InterviewTestTemplatev2.Data;
using InterviewTestTemplatev2.Models;
using NSubstitute;
using NUnit.Framework;

namespace SynetecMvcAssessment.UnitTests.ControllerServices
{
    public class BonusPoolIndexServiceTests
    {
        private List<Employee> _employees;
        private BonusPoolIndexService _service;

        [SetUp]
        public void Before_each()
        {
            _employees = new List<Employee> {
                new Employee {Id = 0, FullName = "Alf Stokes", Salary = 10000},
                new Employee {Id = 1, FullName = "Bender Rodriguez", Salary = 1000}
            };
            var fakeData = Substitute.For<IBonusPoolModelData>();
            fakeData.Employees.Returns(_employees.AsQueryable());

            _service = new BonusPoolIndexService(fakeData);
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
    }
}
