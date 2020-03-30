using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using InterviewTestTemplatev2.Controllers;
using InterviewTestTemplatev2.Data;
using InterviewTestTemplatev2.Models;
using NSubstitute;
using NUnit.Framework;

namespace SynetecMvcAssessment.UnitTests.Controllers
{
    public class BonusPoolControllerTests
    {
        private BonusPoolController _controller;

        [SetUp]
        public void Before_each()
        {
            var emps = new List<Employee> {
                new Employee { Id = 0, FullName = "Alf Stokes", Salary = 10000 },
                new Employee { Id = 1, FullName = "Bender Rodriguez", Salary = 1000 }
            }.AsQueryable();

            var fakeEmployees = Substitute.For<IQueryable<Employee>>();
            fakeEmployees.Provider.Returns(emps.Provider);
            fakeEmployees.Expression.Returns(emps.Expression);
            fakeEmployees.ElementType.Returns(emps.ElementType);
            fakeEmployees.GetEnumerator().Returns(emps.GetEnumerator());
            var fakeData = Substitute.For<IBonusPoolModelData>();
            fakeData.Employees.Returns(fakeEmployees);
            _controller = new BonusPoolController(fakeData);
        }

        [Test]
        public void Should_return_correct_Model_for_Index()
        {
            var result = _controller.Index() as ViewResult;

            Assert.That(result?.Model, Is.InstanceOf<BonusPoolCalculatorModel>());
        }
    }
}
