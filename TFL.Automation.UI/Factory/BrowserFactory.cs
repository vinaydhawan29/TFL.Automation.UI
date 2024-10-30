using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace TFL.Automation.UI.Factory
{
    public static class BrowserFactory
    {
        public enum BrowserType
        {
            Chrome,
            FireFox,
            Edge
        }

        public static IWebDriver GetBrowser(BrowserType browser, string url, bool headless = false)
        {
            IWebDriver? driver = null;

            while (driver == null)
            {
                try
                {
                    switch (browser)
                    {
                        case BrowserType.Chrome:

                            ChromeOptions options = new();
                            options.AddArguments("disable-infobars");
                            options.AddArguments("--incognito");
                            options.AddArgument("--disable-blink-features=AutomationControlled");
                            options.SetLoggingPreference(LogType.Browser, LogLevel.All);
                            if (headless)
                                options.AddArgument("headless");

                            ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService();

                            driver = new ChromeDriver(chromeDriverService, options, TimeSpan.FromMinutes(2));

                            break;
                        case BrowserType.FireFox:

                            FirefoxDriverService firefoxDriverService = FirefoxDriverService.CreateDefaultService();

                            driver = new FirefoxDriver(firefoxDriverService, new FirefoxOptions(), TimeSpan.FromMinutes(2));

                            break;
                        case BrowserType.Edge:

                            var edgeOptions = new EdgeOptions();

                            driver = new EdgeDriver(edgeOptions);

                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    driver = null;
                }
            }

            try
            {
                driver.Manage().Window.Maximize();
            }
            catch (Exception) { }

            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(120);

            try
            {
                driver.Navigate().GoToUrl(url);
            }
            catch (Exception) { }

            return driver;
        }

        public static void CloseBrowser(this IWebDriver driver)
        {
            using (driver)
            {
                try
                {
                    driver.Close();
                    driver.Quit();
                }
                catch { }
            }
        }
    }
}