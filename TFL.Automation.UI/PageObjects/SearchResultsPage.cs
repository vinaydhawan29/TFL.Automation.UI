using AventStack.ExtentReports;
using OpenQA.Selenium;
using TFL.Automation.UI.ElementLibrary;
using TFL.Automation.UI.Helpers;

namespace TFL.Automation.UI.PageObjects
{
    public class SearchResultsPage
    {
        private readonly IWebDriver _webDriver;
        private readonly ExtentTest extentTest;

        public SearchResultsPage(IWebDriver driver, ScenarioContext scenario)
        {
            extentTest = scenario.Get<ExtentTest>("ExtentTest");
            _webDriver = driver;
        }

        public string GetPageTitle() => _webDriver.Title;

        public string GetPageDescription() => _webDriver.FindElement(By.ClassName("title-description")).Text;


        public void ValidateSearchResultsDisplayed()
        {
            string searchResultsForWalkingAndCycling = _webDriver.FindElement(SearchResults.SearchResultsForWalkingAndCycling, "Walking and Cycling Results is displayed on the page").Text;

            string expectedSearchResultsForWalkingAndCycling = "Walking and cycling";

            Assert.True(searchResultsForWalkingAndCycling.Contains(expectedSearchResultsForWalkingAndCycling), "Search results for Walking and cycling are not displayed");

        }

        public string GetWalkingResults => _webDriver.FindElement(SearchResults.SearchResultsForWalking, "Walking Text with details are displayed on the page").Text;

        public string GetCyclingResults => _webDriver.FindElement(SearchResults.SearchResultsForCycling, "Cycling Text with details are displayed on the page").Text;

        public string GetWalkingSpeed() => _webDriver.FindElement(SearchResults.WalkingSpeed, "Walking speed is displayed").Text;

        public string GetCyclingRoute() => _webDriver.FindElement(SearchResults.CyclingRoute, "Cycling speed is displayed").Text;

        public string GetWalkingTime() => _webDriver.FindElement(SearchResults.WalkingTime, "Walking time is displayed").Text;

        public string GetCyclingTime() => _webDriver.FindElement(SearchResults.CyclingTime, "Cycling time is displayed").Text;

        public string GetCyclingJourneyDistance() => _webDriver.FindElement(SearchResults.CyclingDistance, "Cycling Journey distance is displayed").Text;

        public string GetWalkingJourneyDistance() => _webDriver.FindElement(SearchResults.WalkingDistance, "Walking Journey distance is displayed").Text;

        public void ClickEditPreferences() => _webDriver.FindElementAndClick(SearchResults.EditPreferencesLink, "Clicked on Edit Preferences link on serach page");

        public void ClickLeastWalkingOption() => _webDriver.FindElementAndClick(SearchResults.LeastWalkingOption, "Clicked on Least Walking Option");

        public void ClickOnUpdateJourney()
        {
            IJavaScriptExecutor je = (IJavaScriptExecutor)_webDriver;

            IWebElement element = _webDriver.FindElement(By.XPath(SearchResults.UpdateJourneyButton));

            je.ExecuteScript("arguments[0].scrollIntoView(true);", element);

            _webDriver.FindElementAndClick(SearchResults.UpdateJourneyButton, "Update Journey Button");

        }

        public string GetRecalculatedRouteTime() => _webDriver.FindElement(SearchResults.RecalculatingRouteTime, "Recalculated Route is displayed").Text;

        public string GetRecalculatedRouteJourneyStartTime() => _webDriver.FindElement(SearchResults.RecalculatingRouteJourneyStartTime, "Recalculated Route Journey Time is displayed").Text;

        public string GetRecalculatedRouteJourneyEndTime() => _webDriver.FindElement(SearchResults.RecalculatingRouteJourneyEndTime, "Recalculated Route Journey End Time is displayed").Text;

        public void ClickOnViewDetails() => _webDriver.FindElementAndClick(SearchResults.ViewDetailsButton, "Click on View Details button");

        public bool ValidateUpstairsIconPresent()
        {
            try
            {
                IWebElement ele = _webDriver.WaitForElementToBeVisible(SearchResults.UpstairsIcon);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while checking for the upstairs icon: {ex.Message}");
                return false;
            }
        }

        public bool ValidateUpliftIconPresent()
        {
            try
            {
                IWebElement ele = _webDriver.WaitForElementToBeVisible(SearchResults.UpliftIcon);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while checking for the UpliftIcon icon: {ex.Message}");
                return false;
            }
        }

        public bool ValidateLevelWalkwayIconPresent()
        {
            try
            {
                IWebElement ele = _webDriver.WaitForElementToBeVisible(SearchResults.LevelWalkwayIcon);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while checking for the LevelWalkwayIconicon: {ex.Message}");
                return false;
            }
        }

        public string GetOnScreenJourneyPlannerMessage() => _webDriver.FindElement(SearchResults.OnScreenJourneyPlannerMessage, "On Screen Journey Planner Message is displayed").Text;

        public IWebElement ValidateViewDetailsElementNotPresent() => _webDriver.FindElementAndReturnNullIfNotPresent(SearchResults.ViewDetailsButton, "View Details Button");

        public string GetOnScreenJourneyPlannerMessageForInvalidPlace() => _webDriver.FindElement(SearchResults.OnScreenJourneyPlannerMessageForInvalidPlace, "On Screen Journey Planner Message is displayed for Invalid Place").Text;

    }
}