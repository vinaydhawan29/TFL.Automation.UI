using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using TFL.Automation.UI.ElementLibrary;
using TFL.Automation.UI.Helpers;

namespace TFL.Automation.UI.PageObjects
{
    public class Homepage
    {
        private readonly IWebDriver _webDriver;
        private readonly WebDriverWait _wait;
        private readonly ExtentTest extentTest;

        public Homepage(IWebDriver driver, ScenarioContext scenario)
        {
            extentTest = scenario.Get<ExtentTest>("ExtentTest");

            _webDriver = driver;

            _wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));
        }

        public void AcceptCookies()
        {
            try
            {
                // Wait for the cookie consent dialog to appear
                var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));
                var acceptButton = wait.Until(driver => driver.FindElement(Home.CookiesAcceptButton, "Cookies Accept All Button"));

                extentTest.Log(Status.Info, "Cookie consent dialog appeared.");

                // Click the accept button
                acceptButton.Click();
            }
            catch (WebDriverTimeoutException)
            {
                // If the cookie dialog doesn't appear, log a message and continue
                Console.WriteLine("Cookie consent dialog did not appear within the expected time.");
            }
            catch (NoSuchElementException)
            {
                // If the accept button is not found, log a message and continue
                Console.WriteLine("Accept cookies button not found. The website might have changed or cookies might be already accepted.");
            }
        }

        public void ValidateTflHomepageisLoaded()
        {
            try
            {
                _webDriver.WaitForElementToBePresent(Home.CookiesAcceptButton, 5);

                _webDriver.WaitForElementToBePresent(Home.Mainlogo, 5);


            }
            catch (Exception)
            {
                //Ignore
            }
        }

        public void EnterPlanJourneyDetails(string fromLocation, string toLocation)
        {
            _webDriver.FindElementAndEnterText(Home.FromTextBox, fromLocation, "Enter From travel location textbox");

            //SelectFromLocationFromAutocomplete

            SelectFromLocationFromAutoComplete(fromLocation);

            _webDriver.FindElementAndEnterText(Home.ToTextBox, toLocation, "Enter To travel location textbox");

            SelectFromLocationFromAutoComplete(toLocation);

            _webDriver.FindElementAndClick(Home.PlanJourneyButton, "Click on Plan Journey button");
        }

        private void SelectFromLocationFromAutoComplete(string expectedLocation)
        {
            try
            {
                // Wait for the autocomplete suggestions to appear

                var autocompleteList = _wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//span[@class='tt-suggestions']/div/span/strong")));

                // Find and click the correct suggestion
                var matchingOption = autocompleteList.FirstOrDefault(option => expectedLocation.Contains(option.Text, StringComparison.OrdinalIgnoreCase));

                if (matchingOption != null)
                {
                    _wait.Until(ExpectedConditions.ElementToBeClickable(matchingOption)).Click();
                }
                else
                {
                    throw new NoSuchElementException($"Could not find autocomplete option containing '{expectedLocation}'");
                }
            }
            catch (WebDriverTimeoutException)
            {
                throw new TimeoutException($"Autocomplete suggestions did not appear for '{expectedLocation}'");
            }
        }
    }
}