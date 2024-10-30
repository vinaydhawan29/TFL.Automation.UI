using AventStack.ExtentReports;
using OpenQA.Selenium;
using TFL.Automation.UI.Helpers;
using TFL.Automation.UI.PageObjects;
using static TFL.Automation.UI.Factory.BrowserFactory;

namespace TFL.Automation.UI.StepDefinitions
{

    [Collection("InvalidJourneyPlanningCollection")]
    [Binding]
    public class InvalidJourneyPlanningStepDefinitions : IDisposable
    {
        private IWebDriver webDriver;
        private Homepage homepage;
        private SearchResultsPage searchResultsPage;
        private readonly ExtentTest extentTest;

        public InvalidJourneyPlanningStepDefinitions(ScenarioContext scenario)
        {
            extentTest = scenario.Get<ExtentTest>("ExtentTest");

            webDriver = GetBrowser(BrowserType.Chrome, ConfigRead.Get("TflUrl"));

            homepage = new Homepage(webDriver, scenario);

            searchResultsPage = new SearchResultsPage(webDriver, scenario);

        }


        [Given(@"the journey planning widget is open")]
        public void GivenTheJourneyPlanningWidgetIsOpen()
        {

            #region Navigate to TFL Homepage and search results page

            homepage.AcceptCookies();

            homepage.ValidateTflHomepageisLoaded();

            extentTest.Log(Status.Info, "Journey Planner website is up and running");

            #endregion

        }

        [When(@"I enter ""([^""]*)"" in the ""([^""]*)"" field")]
        public void WhenIEnterInTheField(string location, string fieldName)
        {
            homepage.EnterFromOrToLocation(location, fieldName);

        }

        [When(@"I submit the journey plan")]
        public void WhenISubmitTheJourneyPlan()
        {
            homepage.ClickPlanJourneyButton();
        }

        [Then(@"I should see an error message indicating both locations are invalid")]
        public void ThenIShouldSeeAnErrorMessageIndicatingBothLocationsAreInvalid()
        {
            string expectedErrorMessage = "Journey planner could not find any results to your search. Please try again";

            string actualErrorMessage = searchResultsPage.GetOnScreenJourneyPlannerMessage();

            Assert.True(actualErrorMessage.Contains(expectedErrorMessage), "Search results with valid error message : - Journey planner could not find any results to your search. Please try again --- not displayed");

            extentTest.Log(Status.Info, $"Expected error message is: " + expectedErrorMessage + "and the actual error message is: " + actualErrorMessage);

        }

        [Then(@"no journey results should be displayed")]
        public void ThenNoJourneyResultsShouldBeDisplayed()
        {
            IWebElement viewDetailsElement = searchResultsPage.ValidateViewDetailsElementNotPresent();

            Assert.True(viewDetailsElement == null, "View details element should not be displayed for invalid journey plan");

            extentTest.Log(Status.Info, "View details element is not displayed for invalid journey plan");

        }


        [Then(@"I should see an error message indicating the ""([^""]*)"" location is invalid")]
        public void ThenIShouldSeeAnErrorMessageIndicatingTheLocationIsInvalid(string fromLocation)
        {
            string expectedErrorMessage = "Invalid Place";

            string actualErrorMessage = searchResultsPage.GetOnScreenJourneyPlannerMessageForInvalidPlace();

            Assert.True(actualErrorMessage.Contains(expectedErrorMessage), "Search results with valid error message : We found more than one location matching -Invalid Place --- not displayed");

            extentTest.Log(Status.Info, $"Expected error message is: " + expectedErrorMessage + "and the actual error message is: " + actualErrorMessage);
        }

        [When(@"I leave the ""([^""]*)"" field empty")]
        public void WhenILeaveTheFieldEmpty(string from)
        {
            throw new PendingStepException();
        }


        [Then(@"I should see error messages for both empty fields")]
        public void ThenIShouldSeeErrorMessagesForBothEmptyFields()
        {
            throw new PendingStepException();
        }

        [Then(@"the plan journey button should be disabled")]
        public void ThenThePlanJourneyButtonShouldBeDisabled()
        {
            throw new PendingStepException();
        }


        [Then(@"I should see an error message for the empty field")]
        public void ThenIShouldSeeAnErrorMessageForTheEmptyField()
        {
            throw new PendingStepException();
        }

        [Then(@"I should see appropriate error messages")]
        public void ThenIShouldSeeAppropriateErrorMessages()
        {
            throw new PendingStepException();
        }

        [When(@"I clear both ""([^""]*)"" and ""([^""]*)"" fields")]
        public void WhenIClearBothAndFields(string from, string to)
        {
            throw new PendingStepException();
        }

        [Then(@"I should see error messages for empty fields")]
        public void ThenIShouldSeeErrorMessagesForEmptyFields()
        {
            throw new PendingStepException();
        }

        [Then(@"the journey plan should be processed without errors")]
        public void ThenTheJourneyPlanShouldBeProcessedWithoutErrors()
        {
            throw new PendingStepException();
        }

        [Then(@"journey results should be displayed")]
        public void ThenJourneyResultsShouldBeDisplayed()
        {
            throw new PendingStepException();
        }

        [When(@"I enter a (.*)-character long string in the ""([^""]*)"" field")]
        public void WhenIEnterA_CharacterLongStringInTheField(int p0, string from)
        {
            throw new PendingStepException();
        }


        [Then(@"I should see an error message indicating the location names are too long")]
        public void ThenIShouldSeeAnErrorMessageIndicatingTheLocationNamesAreTooLong()
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
