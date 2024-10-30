using AventStack.ExtentReports;
using OpenQA.Selenium;
using TFL.Automation.UI.Helpers;
using TFL.Automation.UI.PageObjects;
using static TFL.Automation.UI.Factory.BrowserFactory;

namespace TFL.Automation.UI.StepDefinitions
{
    [Collection("EditJourneyPreferencesCollection")]
    [Binding]
    public class EditJourneyPreferencesStepDefinitions : IDisposable
    {
        private IWebDriver webDriver;
        private Homepage homepage;
        private SearchResultsPage searchResultsPage;
        public string environment;
        private readonly ExtentTest extentTest;

        public EditJourneyPreferencesStepDefinitions(ScenarioContext scenario)
        {
            extentTest = scenario.Get<ExtentTest>("ExtentTest");

            webDriver = GetBrowser(BrowserType.Chrome, ConfigRead.Get("TflUrl"));

            homepage = new Homepage(webDriver, scenario);

            searchResultsPage = new SearchResultsPage(webDriver, scenario);
        }

        [Given(@"I have planned a journey from ""([^""]*)"" to ""([^""]*)""")]
        public void GivenIHavePlannedAJourneyFromTo(string fromLocation, string toLocation)
        {

            #region Navigate to TFL Homepage and search results page

            homepage.AcceptCookies();

            homepage.ValidateTflHomepageisLoaded();

            extentTest.Info("Journey Planner website is up and running");

            homepage.EnterPlanJourneyDetails(fromLocation, toLocation);

            string expectedPageTitle = "Journey results";

            string actualPageTitle = searchResultsPage.GetPageTitle();

            Assert.True(actualPageTitle.Contains(expectedPageTitle), "Search results page did not load successfully");

            extentTest.Info("Page title is: " + actualPageTitle);

            #endregion

        }

        [Given(@"I have selected a journey with minimal walking")]
        public void GivenIHaveSelectedAJourneyWithMinimalWalking()
        {
            throw new PendingStepException();
        }

        [When(@"I navigate to the journey planner")]
        public void WhenINavigateToTheJourneyPlanner()
        {
            throw new PendingStepException();
        }

        [When(@"I edit the journey preferences")]
        public void WhenIEditTheJourneyPreferences()
        {
            throw new PendingStepException();
        }

        [When(@"I select ""([^""]*)"" as my preference")]
        public void WhenISelectAsMyPreference(string p0)
        {
            throw new PendingStepException();
        }

        [When(@"I select ""([^""]*)"" as my preference")]
        public void WhenISelectAsMyPreference(string p0, string p1)
        {
            throw new PendingStepException();
        }

        [When(@"I select ""([^""]*)"" option")]
        public void WhenISelectOption(string p0)
        {
            searchResultsPage.ClickEditPreferences();
        }

        [When(@"I choose ""([^""]*)"" as my preference")]
        public void WhenIChooseAsMyPreference(string p1)
        {
            searchResultsPage.ClickLeastWalkingOption();
        }

        [When(@"I update the journey")]
        public void WhenIUpdateTheJourney()
        {
            searchResultsPage.ClickOnUpdateJourney();
        }

        [Then(@"the system should recalculate the route")]
        public void ThenTheSystemShouldRecalculateTheRoute()
        {
            string recalcualtedRouteTime = searchResultsPage.GetRecalculatedRouteTime();

            Assert.True(!string.IsNullOrEmpty(recalcualtedRouteTime), "Recalculated route time was not displayed");

            extentTest.Info("Recalculated route time is displayed : " + recalcualtedRouteTime);
        }

        [Then(@"the new route should prioritize minimal walking distance")]
        public void ThenTheNewRouteShouldPrioritizeMinimalWalkingDistance()
        {
            string recalculatedRouteStartTime = searchResultsPage.GetRecalculatedRouteJourneyStartTime();

            string recalcualtedRouteEndTime = searchResultsPage.GetRecalculatedRouteJourneyEndTime();

            Assert.True(!string.IsNullOrEmpty(recalculatedRouteStartTime), "Recalculated route start time was not displayed");

            Assert.True(!string.IsNullOrEmpty(recalcualtedRouteEndTime), "Recalculated route end time was not displayed");

            extentTest.Info("Recalculated route start time is displayed : " + recalculatedRouteStartTime);

            extentTest.Info("Recalculated route end time is displayed : " + recalcualtedRouteEndTime);

        }

        [Then(@"the journey details should be updated accordingly")]
        public void ThenTheJourneyDetailsShouldBeUpdatedAccordingly()
        {
            searchResultsPage.ClickOnViewDetails();

            extentTest.Info("Viewing updated journey details");

            bool isUpstairsIconPresent = searchResultsPage.ValidateUpstairsIconPresent();

            Assert.True(isUpstairsIconPresent, "Upstairs icon is not displayed");

            extentTest.Info("Upstairs icon is displayed");

            bool isUpliftIconPresent = searchResultsPage.ValidateUpliftIconPresent();

            Assert.True(isUpliftIconPresent, "Uplift icon is not displayed");

            extentTest.Info("Uplift icon is displayed");

            bool isLevelWalkwayIconPresent = searchResultsPage.ValidateLevelWalkwayIconPresent();

            Assert.True(isLevelWalkwayIconPresent, "Level walkway icon is not displayed");

            extentTest.Info("Level walkway icon is displayed");

        }

        [Given(@"I have edited preferences to ""([^""]*)""")]
        public void GivenIHaveEditedPreferencesTo(string p0)
        {
            throw new PendingStepException();
        }

        [When(@"I view the updated journey details")]
        public void WhenIViewTheUpdatedJourneyDetails()
        {
            throw new PendingStepException();
        }

        [Then(@"I should see a route with minimal walking segments")]
        public void ThenIShouldSeeARouteWithMinimalWalkingSegments()
        {
            throw new PendingStepException();
        }

        [Then(@"the total walking distance should be less than or equal to the original route")]
        public void ThenTheTotalWalkingDistanceShouldBeLessThanOrEqualToTheOriginalRoute()
        {
            throw new PendingStepException();
        }

        [Then(@"the journey summary should indicate ""([^""]*)"" preference")]
        public void ThenTheJourneySummaryShouldIndicatePreference(string p0)
        {
            throw new PendingStepException();
        }

        [Given(@"I have noted the original walking distance")]
        public void GivenIHaveNotedTheOriginalWalkingDistance()
        {
            throw new PendingStepException();
        }

        [When(@"I change the preference to ""([^""]*)""")]
        public void WhenIChangeThePreferenceTo(string p0)
        {
            throw new PendingStepException();
        }

        [Then(@"the new walking distance should be less than or equal to the original")]
        public void ThenTheNewWalkingDistanceShouldBeLessThanOrEqualToTheOriginal()
        {
            throw new PendingStepException();
        }

        [Then(@"the difference in walking distance should be at least (.*) percent")]
        public void ThenTheDifferenceInWalkingDistanceShouldBeAtLeastPercent(int p0)
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
