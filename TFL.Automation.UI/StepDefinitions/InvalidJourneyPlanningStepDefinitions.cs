namespace TFL.Automation.UI.StepDefinitions
{
    [Binding]
    public class InvalidJourneyPlanningStepDefinitions
    {
        [Given(@"the journey planning widget is open")]
        public void GivenTheJourneyPlanningWidgetIsOpen()
        {
            throw new PendingStepException();
        }

        [When(@"I enter ""([^""]*)"" in the ""([^""]*)"" field")]
        public void WhenIEnterInTheField(string p0, string from)
        {
            throw new PendingStepException();
        }

        [When(@"I submit the journey plan")]
        public void WhenISubmitTheJourneyPlan()
        {
            throw new PendingStepException();
        }

        [Then(@"I should see an error message indicating both locations are invalid")]
        public void ThenIShouldSeeAnErrorMessageIndicatingBothLocationsAreInvalid()
        {
            throw new PendingStepException();
        }

        [Then(@"no journey results should be displayed")]
        public void ThenNoJourneyResultsShouldBeDisplayed()
        {
            throw new PendingStepException();
        }


        [Then(@"I should see an error message indicating the ""([^""]*)"" location is invalid")]
        public void ThenIShouldSeeAnErrorMessageIndicatingTheLocationIsInvalid(string from)
        {
            throw new PendingStepException();
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
    }
}
