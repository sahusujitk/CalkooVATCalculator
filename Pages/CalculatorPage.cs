using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CalckooTest.Pages
{
    public class CalculatorPage
    {
        private readonly IWebDriver driver;

        public CalculatorPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        IWebElement totalprice => driver.FindElement(By.XPath("//*[@id=\"vatcalculator\"]/div[8]/div[1]/label"));

        public void ConfirmGdprPolicy()
        {
            IWebElement gdprConsentButton = driver.FindElement(By.ClassName("fc-cta-consent"));
            if (gdprConsentButton == null)
            {
                throw new Exception($"Unable to find element Country");
            }
            gdprConsentButton.Click();
        }

        public void SelectCountryDropDown(string country)
        {
            IWebElement countryDropdown = driver.FindElement(By.Name("Country"));
            if (countryDropdown == null)
            {
                throw new Exception($"Unable to find element Country");
            }

            SelectElement select = new SelectElement(countryDropdown);
            select.SelectByText(country);
        }

        public void EnterNetPriceText(string price)
        {
            IWebElement netPriceTextBox = driver.FindElement(By.Name("NetPrice"));
            if (netPriceTextBox == null)
            {
                throw new Exception($"Unable to find element NetPrice");
            }
            netPriceTextBox.SendKeys(price.ToString());
        }

        public void EnterTotalPriceText(string price)
        {
            IWebElement totalAmountTextBox = driver.FindElement(By.Name("Price"));
           
            if (totalAmountTextBox == null)
            {
                throw new Exception($"Unable to find element TotalPrice");
            }
            IWebElement gdprAcceptButton = driver.FindElement(By.XPath("/html/body/div[1]/div/a"));
            gdprAcceptButton.Click();
            totalprice.Click();
            totalAmountTextBox.SendKeys(price.ToString());
            
        }


        public string GetVatSum()
        {
            IWebElement priceWithVatTextBox = driver.FindElement(By.Name("VATsum"));
            if (priceWithVatTextBox == null)
                throw new Exception($"Unable to find element VATsum");

            return priceWithVatTextBox.GetAttribute("value");

        }

        public string GetNetPrice()
        {
            IWebElement netPriceTextBox = driver.FindElement(By.Name("NetPrice"));
            if (netPriceTextBox == null)
                throw new Exception($"Unable to find element VATsum");

            return netPriceTextBox.GetAttribute("value");

        }

        public string GetTotalPrice()
        {
            IWebElement totalAmountTextBox = driver.FindElement(By.Name("Price"));
            if (totalAmountTextBox == null)
                throw new Exception($"Unable to find element Price");

            return totalAmountTextBox.GetAttribute("value");
        }


       
    }
}
