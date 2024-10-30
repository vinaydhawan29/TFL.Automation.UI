namespace TFL.Automation.UI.ElementLibrary
{
    public static class SearchResults
    {
        public readonly static string SearchResultsForWalkingAndCycling = "//h2[text()='Walking and cycling']";

        public readonly static string SearchResultsForWalking = "//h2[text()='Walking']";

        public readonly static string SearchResultsForCycling = "//h2[text()='Cycling']";

        public readonly static string WalkingSpeed = "//a[@class='journey-box walking']/div[2]/div[1]/div/p/strong";

        public readonly static string CyclingRoute = "//a[@class='journey-box cycling']/div[2]/div[1]/div/p/strong";

        public readonly static string WalkingDistance = "//a[@class='journey-box walking']/div[2]/div[1]/div/p[2]/strong";

        public readonly static string CyclingDistance = "//a[@class='journey-box cycling']/div[2]/div[1]/div/p[2]/strong";

        public readonly static string WalkingTime = "//a[@class='journey-box walking']/div[2]/div[2]/strong";

        public readonly static string CyclingTime = "//a[@class='journey-box cycling']/div[2]/div[2]/strong";

        public readonly static string EditPreferencesLink = "//button[contains(text(),'Edit preferences')]";

        public readonly static string LeastWalkingOption = "//label[contains(text(),'Routes with least walking')]";

        public readonly static string UpdateJourneyButton = "//input[@id='plan-journey-button']";

        public readonly static string RecalculatingRouteTime = "//*[@id='option-1-heading']/div[1]/div[2]/text()";

        public readonly static string RecalculatingRouteJourneyStartTime = "//*[@id='option-1-heading']/div[1]/div[1]/span/text()[1]";

        public readonly static string RecalculatingRouteJourneyEndTime = "//*[@id='option-1-heading']/div[1]/div[1]/span/text()[2]";

        public readonly static string ViewDetailsButton = "//button[@class='secondary-button show-detailed-results view-hide-details']";

        public readonly static string UpstairsIcon = "//a[@data-title='Up stairs']/span[contains(text(),'Up stairs')]";

        public readonly static string UpliftIcon = "//a[@data-title='Up lift']/span[contains(text(),'Up lift')]";

        public readonly static string LevelWalkwayIcon = "//a[@data-title='Level walkway']/span[contains(text(),'Level walkway')]";
    }
}
