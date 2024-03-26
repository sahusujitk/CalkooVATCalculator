using CalckooTest.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CalckooTest
{
    [TestFixture]
    public class CalculatorTests
    {
        private IWebDriver driver;
        private CalculatorPage calculatorPage;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.calkoo.com/en/vat-calculator");
            calculatorPage = new CalculatorPage(driver);
            calculatorPage.ConfirmGdprPolicy();
        }

        [Test, TestCaseSource(nameof(TestData))]
        public void TestWithNetAmount(string country, string priceWithoutVAT, string expectedPriceWithVAT, string expectedTotalAmount)
        {
            calculatorPage.SelectCountryDropDown(country);
            calculatorPage.EnterNetPriceText(priceWithoutVAT);

            Assert.That(calculatorPage.GetVatSum(), Is.EqualTo(expectedPriceWithVAT));
            Assert.That(calculatorPage.GetTotalPrice(), Is.EqualTo(expectedTotalAmount));
        }

        [Test, TestCaseSource(nameof(TestData1))]

        public void TestWithVATGrossAmount(string country, string priceWithoutVAT, string expectedPriceWithVAT, string expectedTotalAmount)
        {
            calculatorPage.SelectCountryDropDown(country);
           
            calculatorPage.EnterTotalPriceText(expectedTotalAmount);

            Assert.That(calculatorPage.GetNetPrice(), Is.EqualTo(priceWithoutVAT));
            Assert.That(calculatorPage.GetVatSum(), Is.EqualTo(expectedPriceWithVAT));
        }


        public static IEnumerable<object[]> TestData
        {
            get { return ExcelReader.ReadExcelFile("../../../TestData/Calculator_Vat_Test_DataNet.xlsx"); }
        }

        public static IEnumerable<object[]> TestData1
        {
            get { return ExcelReader.ReadExcelFile("../../../TestData/Calculator_Vat_Test_DataGross.xlsx"); }
        }


        [TearDown]
        public void TearDown()
        {
            // Close the browser after each test
            driver.Quit();
        }
    }
}
