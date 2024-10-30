using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Collections.ObjectModel;

namespace TFL.Automation.UI.Helpers
{
    public static class WebDriverExtensions
    {

        public static readonly Dictionary<string, IWebElement> Elements = null;

        public static string GetElementText(this IWebDriver WebDriver, string key, int timeout = 30)
        {
            var element = FindElement(WebDriver, key, key, timeout);
            if (element.Text.Length > 0)
            {
                return element.Text;
            }
            else
            {
                throw new Exception($"Failed as located the {key}.");
            }
        }

        public static string GetAttributeTextValue(this IWebDriver WebDriver, string key, string attributeName, int timeout = 30)
        {
            var element = FindElement(WebDriver, key, key, timeout);
            if (element.GetAttribute(attributeName) != null)
            {
                int i = 0;
                while (true)
                {
                    i++;
                    try
                    {
                        if (element.GetAttribute(attributeName) != string.Empty)
                        {
                            return element.GetAttribute(attributeName);
                        }
                    }
                    catch (Exception) when (i < 10) { Thread.Sleep(1000); }
                    Thread.Sleep(1000);
                    if (i >= 10)
                        return element.GetAttribute(attributeName);
                }
            }
            else
            {
                // This is needed as checked only exists when checked
                if (attributeName == "checked") { return false.ToString(); }

                throw new Exception($"Couldn't find the attribute {attributeName} on the element {key}.");
            }
        }

        public static void MoveToElement(this IWebDriver driver, IWebElement webElement)
        {
            if (webElement == null) throw new ArgumentNullException(nameof(webElement));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", webElement);
        }

        public static IWebElement FindElement(this IWebDriver WebDriver, string key, string elementDescription, int timeout = 30)
        {
            var element = TryFindElement(WebDriver, key, timeout);

            if (element == null)
                throw new NoSuchElementException($"Failed to locate the {elementDescription}.");
            return element;
        }

        public static IWebElement FindElementAndReturnNullIfNotPresent(this IWebDriver WebDriver, string key, string elementDescription, int timeout = 30)
        {
            var element = TryFindElement(WebDriver, key, timeout);

            if (element == null)
                return element;
            return element;
        }

        public static IWebElement TryFindElement(this IWebDriver WebDriver, string key, int timeout = 30)
        {
            var element = WaitForElementToBeVisible(WebDriver, key, timeout);

            if (element != null)
            {
                MoveToElement(WebDriver, element);
            }
            return element;
        }

        public static IWebElement WaitForElementToBeVisible(this IWebDriver WebDriver, string key, int timeout = 60)
        {
            var wait = new WebDriverWait(WebDriver, new TimeSpan(0, 0, timeout));
            try
            {
                if (key.Contains("/"))
                {
                    return wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(key)));
                }
                else
                {
                    return wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(key)));
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static IEnumerable<IWebElement> FindElements(this IWebDriver WebDriver, string key, string elementDescription, int timeout = 30)
        {
            IEnumerable<IWebElement> elements = WaitForElementsToBeVisible(WebDriver, key, timeout);

            return elements.Any() ? elements : throw new NoSuchElementException($"Failed to locate the {elementDescription}.");
        }

        public static IEnumerable<IWebElement> WaitForElementsToBeVisible(this IWebDriver WebDriver, string key, int timeout = 10)
        {
            var wait = new WebDriverWait(WebDriver, new TimeSpan(0, 0, timeout));
            try
            {
                return key.Contains("/")
                    ? wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath(key)))
                    : wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector(key)));
            }
            catch (Exception)
            {
                return Enumerable.Empty<IWebElement>();
            }
        }

        public static IWebElement FindElementAndClick(this IWebDriver WebDriver, string key, string elementDescription, int timeout = 30)
        {
            var element = TryFindElementAndClick(WebDriver, key, elementDescription, timeout);

            if (element == null)
                throw new NoSuchElementException($"Failed to locate the {elementDescription}.   ");
            return element;
        }

        public static IWebElement TryFindElementAndClick(this IWebDriver WebDriver, string key, string elementDescription, int timeout = 30)
        {
            var element = TryFindElement(WebDriver, key, timeout);
            if (element != null)
            {
                int i = 0;
                while (true)
                {
                    i++;
                    try
                    {
                        element.Click();
                        break;
                    }
                    catch (ElementClickInterceptedException) when (i < 10) { MoveToElement(WebDriver, element); }
                    catch (StaleElementReferenceException) when (i < 10) { element = TryFindElement(WebDriver, key, timeout); }
                    catch (ElementNotInteractableException) when (i < 10) { Thread.Sleep(1000); }
                }
            }
            return element;
        }

        public static IWebElement FindElementAndDoubleClick(this IWebDriver WebDriver, string key, string elementDescription, int timeout = 30)
        {
            var element = FindElement(WebDriver, key, elementDescription);
            if (element != null)
            {
                int i = 0;
                while (true)
                {
                    i++;
                    try
                    {
                        new Actions(WebDriver).DoubleClick(element).Perform();
                        break;
                    }
                    catch (ElementClickInterceptedException) when (i < 10)
                    {
                        Thread.Sleep(1000);
                    }
                }

                return element;
            }
            else
            {
                throw new NoSuchElementException($"Failed to click on the {elementDescription}.");
            }
        }

        /// <summary>
        /// This only works with html 'select' elements.
        /// </summary>
        /// <param name="WebDriver"></param>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="elementDescription"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static IWebElement FindElementAndSelectFromDropdown(this IWebDriver WebDriver, string key, string data, string elementDescription, int timeout = 30)
        {
            var element = new SelectElement(FindElement(WebDriver, key, elementDescription, timeout));
            if (element != null)
            {
                int i = 0;
                while (true)
                {
                    i++;
                    try
                    {
                        element.SelectByText(data);
                        break;
                    }
                    catch (NoSuchElementException) when (i < 10)
                    {
                        Thread.Sleep(1000);
                    }
                }
                return element.WrappedElement;
            }
            else
            {
                throw new Exception($"Failed to select the value '{data}' from the {elementDescription}.");
            }
        }

        public static IWebElement FindElementAndEnterText(this IWebDriver WebDriver, string key, string data, string elementDescription, int timeout = 30, bool disableClearStep = false)
        {
            var element = FindElement(WebDriver, key, elementDescription, timeout);
            int i = 0;
            if (element != null)
            {
                while (i < 5)
                {
                    i++;
                    try
                    {
                        element.Click();
                        break;
                    }
                    catch (ElementClickInterceptedException)
                    {
                        Thread.Sleep(1000);
                    }
                    catch (StaleElementReferenceException)
                    {
                        Thread.Sleep(1000);
                    }
                }

                if (!disableClearStep)
                {
                    element.Clear();
                }

                element.SendKeys(data);
                return element;
            }
            else
            {
                throw new Exception($"Failed to enter the value '{data}' into the {elementDescription}.");
            }
        }

        public static string[] TryFindAllElements(this IWebDriver WebDriver, string key, string elementDescription, int timeout = 30)
        {
            var elements = WebDriver.FindElements(By.XPath(key));

            return elements?.Select(x => x.Text).Where(x => !string.IsNullOrWhiteSpace(x)).ToArray() ?? Array.Empty<string>();
        }

        public static bool WaitForElementToDissapear(this IWebDriver WebDriver, string key, int timeout = 60)
        {
            var wait = new WebDriverWait(WebDriver, new TimeSpan(0, 0, timeout));
            try
            {
                By path;

                if (key.Contains("/"))
                    path = By.XPath(key);
                else
                    path = By.CssSelector(key);

                return wait.Until((IWebDriver driver) =>
                {
                    try
                    {
                        var element = driver.FindElement(By.XPath(key));

                        return !(element?.Displayed ?? false);
                    }
                    catch (StaleElementReferenceException)
                    {
                        return true;
                    }
                });
            }
            catch (Exception)
            {
                return true;
            }
        }

        public static IWebElement SwitchToAFrame(this IWebDriver webDriver, string key, int timeout = 60)
        {
            var wait = new WebDriverWait(webDriver, new TimeSpan(0, 0, timeout));

            try
            {
                return (IWebElement)wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.Id(key)));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static ReadOnlyCollection<IWebElement> WaitForElementToBePresent(this IWebDriver WebDriver, string key, int timeout = 60)
        {
            var wait = new WebDriverWait(WebDriver, new TimeSpan(0, 0, timeout));
            try
            {
                return wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath(key)));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static IWebElement WaitForElementToBeClickable(this IWebDriver WebDriver, string key, int timeout = 30)
        {
            var wait = new WebDriverWait(WebDriver, new TimeSpan(0, 0, timeout));
            try
            {
                return wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(key)));
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
