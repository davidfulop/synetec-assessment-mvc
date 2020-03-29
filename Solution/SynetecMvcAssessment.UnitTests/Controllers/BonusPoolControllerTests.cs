using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            var emps = new List<HrEmployee>
            {
                new HrEmployee { ID = 0, FistName = "Alf", SecondName = "Stokes",
                    Full_Name = "Al Stokes", JobTitle = "Fixer", Salary = 10000,
                    DateOfBirth = new DateTime(1940, 6, 19), HrDepartmentId = 1},
                new HrEmployee { ID = 1, FistName = "Bender", SecondName = "Rodriguez",
                    Full_Name = "Bender Rodriguez", JobTitle = "Accountant", Salary = 1000,
                    DateOfBirth = new DateTime(2000, 1, 1), HrDepartmentId = 2},
            }.AsQueryable();

            var depts = new List<HrDepartment>
            {
                new HrDepartment { ID = 0, Title = "HR", BonusPoolAllocationPerc = 10,
                    Description = "x"},
                new HrDepartment { ID = 1, Title = "Finance", BonusPoolAllocationPerc = 10,
                    Description = "y" },
            }.AsQueryable();

            var fakeEmployees = Substitute.For<DbSet<HrEmployee>, IQueryable<HrEmployee>>();
            ((IQueryable<HrEmployee>)fakeEmployees).Provider.Returns(emps.Provider);
            ((IQueryable<HrEmployee>)fakeEmployees).Expression.Returns(emps.Expression);
            ((IQueryable<HrEmployee>)fakeEmployees).ElementType.Returns(emps.ElementType);
            ((IQueryable<HrEmployee>)fakeEmployees).GetEnumerator().Returns(emps.GetEnumerator());
            var fakeDepartments = Substitute.For<DbSet<HrDepartment>, IQueryable<HrDepartment>>();
            ((IQueryable<HrDepartment>)fakeDepartments).Provider.Returns(depts.Provider);
            ((IQueryable<HrDepartment>)fakeDepartments).Expression.Returns(depts.Expression);
            ((IQueryable<HrDepartment>)fakeDepartments).ElementType.Returns(depts.ElementType);
            ((IQueryable<HrDepartment>)fakeDepartments).GetEnumerator().Returns(depts.GetEnumerator());
            var fakeData = Substitute.For<IBonusPoolModelData>();
            fakeData.HrEmployees.Returns(fakeEmployees);
            fakeData.HrDepartments.Returns(fakeDepartments);
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
