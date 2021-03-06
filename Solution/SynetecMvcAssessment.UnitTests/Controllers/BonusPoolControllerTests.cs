using System.Collections.Generic;
using System.Web.Mvc;
using InterviewTestTemplatev2.Controllers;
using InterviewTestTemplatev2.ControllerServices;
using InterviewTestTemplatev2.Models;
using NSubstitute;
using NUnit.Framework;

namespace SynetecMvcAssessment.UnitTests.Controllers
{
    public class BonusPoolControllerTests
    {
        private BonusPoolController _controller;
        private IBonusPoolIndexService _mockIndexService;
        private IBonusPoolCalculatorService _mockCalculatorService;

        [SetUp]
        public void Before_each()
        {
            //NOTE: yes, I did not use an interface here - please see the readme
            var calculatorModel = new BonusPoolCalculatorModel {
                AllEmployees = new List<Employee> {
                    new Employee {Id = 0, FullName = "Alf Stokes", Salary = 10000},
                    new Employee {Id = 1, FullName = "Bender Rodriguez", Salary = 1000}
                }};

            _mockIndexService = Substitute.For<IBonusPoolIndexService>();
            _mockIndexService.GenerateIndexModel().Returns(calculatorModel);
            _mockCalculatorService = Substitute.For<IBonusPoolCalculatorService>();

            _controller = new BonusPoolController(_mockIndexService, _mockCalculatorService);
        }

        [Test]
        public void Should_return_correct_Model_for_Index()
        {
            var result = _controller.Index() as ViewResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.InstanceOf<BonusPoolCalculatorModel>());
        }

        [Test]
        public void Should_call_GenerateIndexModel_Index()
        {
            _controller.Index();

            _mockIndexService.Received(1).GenerateIndexModel();
        }

        [Test]
        public void Should_call_CalculateBonusForEmployee_for_Calculate()
        {
            _controller.Calculate(null);

            _mockCalculatorService.Received(1).CalculateBonusForEmployee(null);
        }

        [Test]
        public void Should_pass_down_model_to_CalculateBonusForEmployee_when_Calculate_called()
        {
            var fakeModel = new BonusPoolCalculatorModel();
            _controller.Calculate(fakeModel);

            _mockCalculatorService.Received(1).CalculateBonusForEmployee(fakeModel);
        }
    }
}
