using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using TFL.Automation.UI.Helpers;

namespace TFL.Automation.UI.Hooks
{
    [Binding]
    public class Hooks
    {
        private static AventStack.ExtentReports.ExtentReports _extent;
        private static ExtentHtmlReporter _htmlReporter;
        private static ExtentTest _feature;
        private readonly ScenarioContext _scenarioContext;

        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {

            string projectDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
            string testResultsDirectory = Path.Combine(projectDirectory, "TestResults");
            Directory.CreateDirectory(testResultsDirectory);

            var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            var reportPath = Path.Combine(testResultsDirectory, $"TestAutomationReport_{timestamp}.html");

            _htmlReporter = new ExtentHtmlReporter(reportPath);

            // Configure the HTML reporter
            _htmlReporter.Config.DocumentTitle = "Test Automation Report";
            _htmlReporter.Config.ReportName = "Test Results";
            _htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;

            // Prevent creation of default index.html and dashboard.html
            _htmlReporter.Config.EnableTimeline = false;
            _htmlReporter.Start();

            _extent = new AventStack.ExtentReports.ExtentReports();
            _extent.AttachReporter(_htmlReporter);


            ConfigRead.LoadEnvironmentSettingsFile();

            _extent.AddSystemInfo("Environment", ConfigRead.Get("env"));
            _extent.AddSystemInfo("TflURL", ConfigRead.Get("tflURL"));
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            _feature = _extent.CreateTest(featureContext.FeatureInfo.Title);
        }

        [BeforeScenario]
        public void BeforeScenario(ScenarioContext scenarioContext)
        {
            // Create a test node for each scenario and store it in ScenarioContext
            var scenarioTest = _feature.CreateNode(scenarioContext.ScenarioInfo.Title);
            _scenarioContext["ExtentTest"] = scenarioTest; // Storing ExtentTest instance
        }

        [AfterStep]
        public void AfterStep(ScenarioContext scenarioContext)
        {
            // Log each step result
            var scenarioTest = (ExtentTest)_scenarioContext["ExtentTest"];

            if (scenarioContext.TestError == null)
            {
                scenarioTest.Pass(scenarioContext.StepContext.StepInfo.Text);
            }
            else
            {
                scenarioTest.Fail(scenarioContext.StepContext.StepInfo.Text);
            }
        }

        [AfterScenario]
        public void AfterScenario()
        {
            var scenarioTest = (ExtentTest)_scenarioContext["ExtentTest"];
            scenarioTest.Info("Scenario completed.");
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            _extent.Flush();
        }
    }
}