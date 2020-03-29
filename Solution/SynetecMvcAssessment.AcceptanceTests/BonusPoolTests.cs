using System;
using System.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SynetecMvcAssessment.AcceptanceTests
{
    public class BonusPoolTests : IDisposable
    {
        private readonly IWebDriver _driver;
        private readonly string _appBaseUrl;

        public BonusPoolTests()
        {
            _driver = new ChromeDriver();
            _appBaseUrl = ConfigurationManager.AppSettings["ServiceBaseUrl"];
        }

        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }

        [Test]
        public void Should_populate_EmployeeDropdown()
        {
            const int expectedElementCount = 12;
            Given_an_index_page();

            var employeeDropdown = new SelectElement(_driver.FindElement(By.Id("SelectedEmployeeId")));
            Assert.That(employeeDropdown.Options.Count, Is.EqualTo(expectedElementCount));
        }

        [TestCase("John Smith")]
        [TestCase("Janet Jones")]
        public void Should_return_data_for_selected_user(string employeeName)
        {
            Given_an_index_page_set_up_with(1000, employeeName);

            When_the_calculation_is_initiated();

            Assert.That(_driver.PageSource, Does.Contain(employeeName));
        }

        [TestCase(1000, "91")]
        [TestCase(10000, "916")]
        [TestCase(123456, "11313")]
        public void Should_return_expected_bonus_for_pool(int bonusPoolAmount, string expectedBonusAmount)
        {
            Given_an_index_page_set_up_with(bonusPoolAmount, "John Smith");

            When_the_calculation_is_initiated();

            Assert.That(_driver.PageSource, Does.Contain(expectedBonusAmount + "</p>"));
        }

        [TestCase("John Smith", "91")]
        [TestCase("Janet Jones", "137")]
        public void Should_return_expected_bonus_for_employee(string employeeName, string expectedBonusAmount)
        {
            Given_an_index_page_set_up_with(1000, employeeName);

            When_the_calculation_is_initiated();

            Assert.That(_driver.PageSource, Does.Contain(employeeName));
            Assert.That(_driver.PageSource, Does.Contain(expectedBonusAmount + "</p>"));
        }



        private void Given_an_index_page()
        {
            _driver.Navigate().GoToUrl(_appBaseUrl);
        }

        private void SetBonusPoolAmountTo(string bonusPoolAmount)
        {
            var bonusPoolTextBox = _driver.FindElement(By.Id("BonusPoolAmount"));
            bonusPoolTextBox.Clear();
            bonusPoolTextBox.SendKeys(bonusPoolAmount);
        }

        private void SelectEmployeeByName(string employeeName)
        {
            var dropdown = new SelectElement(_driver.FindElement(By.Id("SelectedEmployeeId")));
            dropdown.SelectByText(employeeName);
        }

        private void Given_an_index_page_set_up_with(int bonusPoolAmount, string employeeName)
        {
            Given_an_index_page();
            SetBonusPoolAmountTo(bonusPoolAmount.ToString());
            SelectEmployeeByName(employeeName);
        }

        private bool TryNavigateToCalculatePage(int timeoutSeconds = 5)
        {
            _driver.FindElement(By.Id("CalculateBonus")).Click();
            return new WebDriverWait(_driver, TimeSpan.FromSeconds(timeoutSeconds))
                .Until(driver => driver.Url == _appBaseUrl + "/BonusPool/Calculate");
        }

        private void When_the_calculation_is_initiated()
        {
            var success = TryNavigateToCalculatePage();
            if (!success)
                throw new AssertionException("Navigation to Calculate page failed.");
        }
    }
}
