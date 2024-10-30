using AventStack.ExtentReports;
using OpenQA.Selenium;
using System.Text.RegularExpressions;
using TFL.Automation.UI.Helpers;
using TFL.Automation.UI.PageObjects;
using static TFL.Automation.UI.Factory.BrowserFactory;

namespace TFL.Automation.UI.StepDefinitions
{

    [Collection("JourneyPlannerCollection")]
    [Binding]
    public class JourneyPlannerStepDefinitions : IDisposable
    {
        private IWebDriver webDriver;
        private Homepage homepage;
        private SearchResultsPage searchResultsPage;
        public string environment;
        private readonly ExtentTest extentTest;


        public JourneyPlannerStepDefinitions(ScenarioContext scenario)
        {
            extentTest = scenario.Get<ExtentTest>("ExtentTest");

            webDriver = GetBrowser(BrowserType.Chrome, ConfigRead.Get("TflUrl"));

            homepage = new Homepage(webDriver, scenario);

            searchResultsPage = new SearchResultsPage(webDriver, scenario);
        }



        [Given(@"the journey planner website is up and running")]
        public void GivenTheJourneyPlannerWebsiteIsUpAndRunning()
        {
            homepage.AcceptCookies();

            homepage.ValidateTflHomepageisLoaded();

            extentTest.Info("Journey Planner website is up and running");
        }

        [Then(@"the route should use appropriate cycling paths or roads")]
        public void ThenTheRouteShouldUseAppropriateCyclingPathsOrRoads()
        {
            throw new PendingStepException();
        }

        [When(@"I request a route from ""([^""]*)"" to ""([^""]*)""")]
        public void WhenIRequestARouteFromTo(string p0, string p1)
        {
            throw new PendingStepException();
        }

        [Then(@"I should receive a valid route")]
        public void ThenIShouldReceiveAValidRoute()
        {
            string expectedPageTitle = "Journey results";

            string actualPageTitle = searchResultsPage.GetPageTitle();

            Assert.True(actualPageTitle.Contains(expectedPageTitle), "Search results page did not load successfully");

            extentTest.Info("Page title is: " + actualPageTitle);

            searchResultsPage.ValidateSearchResultsDisplayed();
        }

        [Then(@"the walking time should be between (.*) and (.*) minutes")]
        public void ThenTheWalkingTimeShouldBeBetweenAndMinutes(int minTime, int maxTime)
        {
            string getWalkingTime = searchResultsPage.GetWalkingTime();

            Assert.True(!string.IsNullOrEmpty(getWalkingTime), "Walking time was not displayed");

            // Extract the numeric value from the string
            int actualWalkingTime = int.TryParse(getWalkingTime, out actualWalkingTime) ? actualWalkingTime : 0;

            Console.WriteLine($"Actual walking time: {actualWalkingTime} minutes");
            Console.WriteLine($"Expected walking time range: {minTime} to {maxTime} minutes");

            extentTest.Info($"Actual walking time: {actualWalkingTime} minutes");
            extentTest.Info($"Expected walking time range: {minTime} to {maxTime} minutes");

            Assert.True(actualWalkingTime >= minTime && actualWalkingTime <= maxTime,
                $"Actual walking time {actualWalkingTime} minutes is not between {minTime} and {maxTime} minutes");

            extentTest.Info($"Actual walking time {actualWalkingTime} minutes is between {minTime} and {maxTime} minutes");

        }

        [Then(@"the walking distance should be approximately (.*) kilometers")]
        public void ThenTheWalkingDistanceShouldBeApproximatelyKilometers(Decimal distance)
        {
            string getJourneyDistance = searchResultsPage.GetWalkingJourneyDistance();

            string trimmedJourneyDistance = Regex.Match(getJourneyDistance, @"\d+(\.\d+)?").Value;

            Assert.True(!string.IsNullOrEmpty(trimmedJourneyDistance), "Journey distance was not displayed");

            extentTest.Info($"Actual walking journey distance: {trimmedJourneyDistance} kilometers");

            // Extract the numeric value from the string
            decimal actualJourneyDistance = decimal.TryParse(trimmedJourneyDistance, out actualJourneyDistance) ? actualJourneyDistance : 0;

            Console.WriteLine($"Actual walking journey distance: {actualJourneyDistance} kilometers");
            Console.WriteLine($"Expected journey distance: {distance} kilometers");

            extentTest.Info($"Expected journey distance: {distance} kilometers");

            Assert.True(actualJourneyDistance <= distance,
                $"Actual walking journey distance {actualJourneyDistance} kilometers is less than expected distance {distance} kilometers");

            extentTest.Info($"Actual walking journey distance {actualJourneyDistance} kilometers is less than or equal to expected distance {distance} kilometers");
        }

        [Then(@"the cycling distance should be approximately (.*) kilometers")]
        public void ThenTheCyclingDistanceShouldBeApproximatelyKilometers(Decimal distance)
        {
            string getJourneyDistance = searchResultsPage.GetCyclingJourneyDistance();

            Assert.True(!string.IsNullOrEmpty(getJourneyDistance), "Journey distance was not displayed");

            string trimmedJourneyDistance = Regex.Match(getJourneyDistance, @"\d+(\.\d+)?").Value;

            // Extract the numeric value from the string
            decimal actualJourneyDistance = decimal.TryParse(trimmedJourneyDistance, out actualJourneyDistance) ? actualJourneyDistance : 0;

            Console.WriteLine($"Actual cycling journey distance: {actualJourneyDistance} kilometers");
            Console.WriteLine($"Expected cycling journey distance: {distance} kilometers");

            extentTest.Info($"Actual cycling journey distance: {actualJourneyDistance} kilometers");
            extentTest.Info($"Expected cycling journey distance: {distance} kilometers");

            Assert.True(actualJourneyDistance <= distance,
                $"Actual cycling journey distance {actualJourneyDistance} kilometers is less than expected distance {distance} kilometers");

            extentTest.Info($"Actual cycling journey distance {actualJourneyDistance} kilometers is less than or equal to expected distance {distance} kilometers");
        }


        [When(@"I request a walking route from ""([^""]*)"" to ""([^""]*)""")]
        public void WhenIRequestAWalkingRouteFromTo(string fromLocation, string toLocation)
        {
            homepage.EnterPlanJourneyDetails(fromLocation, toLocation);
        }

        [When(@"I request a cycling route from ""([^""]*)"" to ""([^""]*)""")]
        public void WhenIRequestACyclingRouteFromTo(string fromLocation, string toLocation)
        {

            homepage.EnterPlanJourneyDetails(fromLocation, toLocation);
        }

        [Then(@"the cycling time should be between (.*) and (.*) minutes")]
        public void ThenTheCyclingTimeShouldBeBetweenAndMinutes(int minTime, int maxTime)
        {
            string getCyclingTime = searchResultsPage.GetCyclingTime();

            Assert.True(!string.IsNullOrEmpty(getCyclingTime), "Cycling time was not displayed");

            //Extract the numeric value from the string
            int actualCyclingTime = int.TryParse(getCyclingTime, out actualCyclingTime) ? actualCyclingTime : 0;

            Console.WriteLine($"Actual cycling time: {actualCyclingTime} minutes");
            Console.WriteLine($"Expected cycling time range: {minTime} to {maxTime} minutes");
            extentTest.Info($"Actual cycling time: {actualCyclingTime} minutes");
            extentTest.Info($"Expected cycling time range: {minTime} to {maxTime} minutes");

            Assert.True(actualCyclingTime >= minTime && actualCyclingTime <= maxTime,
                $"Actual cycling time {actualCyclingTime} minutes is not between {minTime} and {maxTime} minutes");

            extentTest.Info($"Actual cycling time {actualCyclingTime} minutes is between {minTime} and {maxTime} minutes");
        }

        [Then(@"the estimated time should be within acceptable range for walking")]
        public void ThenTheEstimatedTimeShouldBeWithinAcceptableRangeForWalking()
        {
            throw new PendingStepException();
        }

        [Then(@"the route should use appropriate walking paths or roads")]
        public void ThenTheRouteShouldUseAppropriateWalkingPathsOrRoads()
        {
            throw new PendingStepException();
        }

        [When(@"I request both walking and cycling routes from ""([^""]*)"" to ""([^""]*)""")]
        public void WhenIRequestBothWalkingAndCyclingRoutesFromTo(string p0, string p1)
        {
            throw new PendingStepException();
        }

        [Then(@"the cycling time should be less than the walking time")]
        public void ThenTheCyclingTimeShouldBeLessThanTheWalkingTime()
        {
            throw new PendingStepException();
        }

        [Then(@"both routes should have the same approximate distance")]
        public void ThenBothRoutesShouldHaveTheSameApproximateDistance()
        {
            throw new PendingStepException();
        }

        public void Dispose()
        {
            // Close browser
            webDriver.CloseBrowser();
        }
    }
}