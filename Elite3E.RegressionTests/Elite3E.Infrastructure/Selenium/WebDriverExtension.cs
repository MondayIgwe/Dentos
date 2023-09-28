using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace Elite3E.Infrastructure.Selenium
{
    public static class WebDriverExtension
    {
        public static IWebDriver GetDriver(this IActor actor)
        {
            return actor.Using<BrowseTheWeb>().WebDriver;
        }

        public static IWebElement FindOne(this IActor actor, By loc, int timeout = 5)
        {
            var ele = new WebDriverWait(actor.GetDriver(), TimeSpan.FromSeconds(timeout)).Until((IWebDriver drv) =>
            {
                IWebElement found = null;
                try
                {
                    found = drv.FindElement(loc);
                }
                catch (Exception)
                {
                    found = null;
                }
                return found;
            });

            return ele;
        }

        public static IWebElement FindOne(this IActor actor, IWebLocator loc, int timeout = 5)
        {
            var ele = new WebDriverWait(actor.GetDriver(), TimeSpan.FromSeconds(timeout)).Until((IWebDriver drv) =>
            {
                IWebElement found = null;
                try
                {
                    found = drv.FindElement(loc.Query);
                }
                catch (Exception)
                {
                    found = null;
                }
                return found;
            });

            return ele;
        }

        public static IList<IWebElement> FindAll(this IActor actor, By loc, int timeout = 5)
        {
            var ele = new WebDriverWait(actor.GetDriver(), TimeSpan.FromSeconds(timeout)).Until((IWebDriver drv) =>
            {
                IList<IWebElement> found = null;
                try
                {
                    found = drv.FindElements(loc);
                }
                catch (Exception)
                {
                    found = null;
                }
                return found;
            });

            return ele;
        }

        public static IList<IWebElement> FindAll(this IActor actor, IWebLocator loc, int timeout = 5)
        {
            var ele = new WebDriverWait(actor.GetDriver(), TimeSpan.FromSeconds(timeout)).Until((IWebDriver drv) =>
            {
                IList<IWebElement> found = null;
                try
                {
                    found = drv.FindElements(loc.Query);
                }
                catch (Exception)
                {
                    found = null;
                }
                return found;
            });

            return ele;
        }

        /// <summary>
        ///  This method will accept a locator going to grid: //e3e-report-data-grid
        ///  We must have one of one
        /// </summary>
        /// <param name="gridLocator"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>

        public static DataTable FindTable(this IActor actor, IWebLocator gridLocator, int timeout = 5)
        {
            DataTable dataTable = new DataTable();
            dataTable.Clear();

            var gridEle = actor.FindOne(gridLocator, timeout);

            //Get Columns: //e3e-report-data-grid//tr[contains(@class,'thead')]//div[text()]
            var columnEleList = gridEle.FindElements(By.XPath(".//tr[contains(@class,'thead')]//div[text()]"));
            var columnTextList = GetElementTextList(columnEleList);

            foreach (var text in columnTextList)
            {
                dataTable.Columns.Add(text);
            }

            //Get Rows: //e3e-report-data-grid//tr[contains(@class,'tbody')]
            var rowsEleList = gridEle.FindElements(By.XPath(".//tr[contains(@class,'tbody')]"));
            foreach (var row in rowsEleList)
            {
                //Get individual grid values per row. e.g. //e3e-report-data-grid//tr[contains(@class,'tbody')][1]//div[contains(@class,'container-text')]
                var rowGridEleList = row.FindElements(By.XPath(".//div[contains(@class,'container-text')]"));
                var rowGridTextList = GetElementTextList(rowGridEleList);

                DataRow dtRow = dataTable.NewRow();
                for (int i = 0; i < rowGridTextList.Count; i++)
                {
                    string colName = columnTextList[i];
                    string rowVal = rowGridTextList[i];
                    dtRow[colName] = rowVal;
                }

                dataTable.Rows.Add(dtRow);
            }

            return dataTable;
        }


        public static bool DoesElementExist(this IActor actor, IWebLocator loc, int timeout = 5)
        {
            try
            {
                var ele = actor.FindOne(loc, timeout);
                return ele != null;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool DoesElementExist(this IActor actor, By loc, int timeout = 5)
        {
            try
            {
                var ele = actor.FindOne(loc, timeout);
                return ele != null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void PressKeyWithActions(this IActor actor, string key)
        {
            Actions action = new Actions(actor.GetDriver());

            switch (key.ToLower())
            {
                case "tab":
                    action = action.SendKeys(Keys.Tab);
                    break;
                case "pagedown":
                    action = action.SendKeys(Keys.PageDown);
                    break;
                case "pageup":
                    action = action.SendKeys(Keys.PageUp);
                    break;
                case "enter":
                    action = action.SendKeys(Keys.Enter);
                    break;
                case "delete":
                    action = action.SendKeys(Keys.Delete);
                    break;
                case "selectall":
                    action = action.SendKeys(Keys.Control + "a");
                    break;
                case "copy":
                    action = action.SendKeys(Keys.Control + "c");
                    break;
                case "cut":
                    action = action.SendKeys(Keys.Control + "x");
                    break;
                case "paste":
                    action = action.SendKeys(Keys.Control + "v");
                    break;
            }

            action.Build().Perform();
            Thread.Sleep(250);
        }

        public static void ScrollByPage(this IActor actor)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)actor.GetDriver();

            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");

        }

        public static string GetElementText(this IActor actor, IWebLocator loc, int timeout = 5)
        {
            var ele = actor.FindOne(loc.Query, timeout);
            string getText = (string.IsNullOrEmpty(ele.Text)) ? ele.GetAttribute("value") : ele.Text;
            return (string.IsNullOrEmpty(getText)) ? getText : getText.Trim();
        }

        public static string GetElementText(this IActor actor, By loc, int timeout = 5)
        {
            var ele = actor.FindOne(loc, timeout);
            string getText = (string.IsNullOrEmpty(ele.Text)) ? ele.GetAttribute("value") : ele.Text;
            return (string.IsNullOrEmpty(getText)) ? getText : getText.Trim();
        }

        public static List<string> GetElementTextList(this IActor actor, IWebLocator loc, int timeout = 5)
        {
            List<string> list = actor.FindAll(loc, timeout)
                .Where(element => !string.IsNullOrEmpty(element.Text))
                .Select(element => element.Text.Trim())
                .ToList();
            return list;
        }

        public static List<string> GetElementTextList(IList<IWebElement> eleList)
        {
            List<string> list = eleList
                .Select(element =>
                {
                    return (string.IsNullOrEmpty(element.Text) ? "" : element.Text.Trim());
                })
                .ToList();
            return list;
        }

        public static string GetElementAttribute(this IActor actor, IWebLocator loc, string attribute)
        {
            var ele = actor.FindOne(loc.Query);
            string getAttr = ele.GetAttribute(attribute);
            return getAttr;
        }

        public static List<string> GetElementAttributeList(this IActor actor, IWebLocator loc, string attribute)
        {
            List<string> list = actor.FindAll(loc)
                .Where(element => !string.IsNullOrEmpty(element.GetAttribute(attribute)))
                .Select(element => element.GetAttribute(attribute).Trim())
                .ToList();
            return list;
        }

        public static void ChangeElementVisibility(this IActor actor, IWebLocator loc, bool makeVisible)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)actor.GetDriver();
            var element = actor.FindOne(loc.Query);

            //Extract Element Style
            string elementCurrentSylte = element.GetAttribute("style").ToLower();

            if (makeVisible)
            {
                //Confirm element is hidden
                if (!elementCurrentSylte.Contains("none"))
                {
                    return;
                }

                //Make element visible with JavaScript
                js.ExecuteScript("arguments[0].style.display='inline-block';", element);
            }
            else
            {
                //Confirm element is visisble
                if (!elementCurrentSylte.Contains("block"))
                {
                    return;
                }

                //Make element hidden with JavaScript
                js.ExecuteScript("arguments[0].style.display='none';", element);
            }
        }

        public static bool AttemptToClick(this IActor actor, IWebLocator eleToClick, IWebLocator validationEle, int numOfAttempts = 3, int validationTimeout = 5)
        {
            for (int i = 0; i < numOfAttempts; i++)
            {
                switch (i)
                {
                    case 0:
                        IJavaScriptExecutor js = (IJavaScriptExecutor)actor.GetDriver();
                        js.ExecuteScript("arguments[0].click();", actor.FindOne(eleToClick));
                        break;
                    case 1:
                        actor.AttemptsTo(Click.On(eleToClick));
                        break;
                    default:
                        actor.FindOne(eleToClick).Click();
                        break;
                }
                WaitForBackgroundProcess(actor);
                if (DoesElementExist(actor, validationEle, validationTimeout))
                    return true;
            }
            return false;
        }
        public static bool AttemptToClick(this IActor actor, IWebLocator eleToClickAndDisappear, int numOfAttempts = 3)
        {
            for (int i = 0; i < numOfAttempts; i++)
            {
                switch (i)
                {
                    case 0:
                        IJavaScriptExecutor js = (IJavaScriptExecutor)actor.GetDriver();
                        js.ExecuteScript("arguments[0].click();", actor.FindOne(eleToClickAndDisappear));
                        break;
                    case 1:
                        actor.AttemptsTo(Click.On(eleToClickAndDisappear));
                        break;
                    default:
                        actor.FindOne(eleToClickAndDisappear).Click();
                        break;
                }
                WaitForBackgroundProcess(actor);
                if (!DoesElementExist(actor, eleToClickAndDisappear))
                    return true;
            }
            return false;
        }

        public static void WaitForBackgroundProcess(this IActor actor)
        {
            //Waits for the page to load.
            actor.GetDriver().Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);
            int timeout = (5 * 60 * 4); //5 minutes
            //int timeout = 1250; // 5 minutes 1250 / 4 = 312.5 seconds == 5 minutes 12 seconds
            int counter = 0;
            while (counter < timeout)
            {
                try
                {
                    var element = actor.FindOne(By.XPath("//e3e-progress[contains(@class,'e3e-progress_page-loader')]"));
                    Thread.Sleep(TimeSpan.FromMilliseconds(250));
                    counter++;
                    continue;
                }
                catch (Exception)
                {
                    Thread.Sleep(TimeSpan.FromMilliseconds(1000));
                    break;
                }
            }
        }

        public static void ScrollIntoElement(this IActor actor, IWebLocator locator, int numberOfTries, string scrollingPosition)
        {
            if (!DoesElementExist(actor, locator))
            {
                int max = numberOfTries;
                int counter = 0;
                while (counter < max)
                {
                    PressKeyWithActions(actor, scrollingPosition);
                    ScrollByPage(actor);
                    if (DoesElementExist(actor, locator))
                        break;
                }
            }

        }

        public static void scrollToElementInView(this IActor actor, IWebElement element)
        {
            ((IJavaScriptExecutor)actor.GetDriver()).ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

        public static void scrollToElementInView(this IActor actor, By locator)
        {
            var element = actor.FindOne(locator);
            ((IJavaScriptExecutor)actor.GetDriver()).ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

        public static void scrollToElementInView(this IActor actor, IWebLocator locator)
        {
            scrollToElementInView(actor, locator.Query);
        }

        public static void scrollBarHorizontal(this IActor actor, By scrollBarLoc, By elementLoc, int rightLeftAttempts = 5) // +5 right. -5 left
        {
            var action = new Actions(actor.GetDriver());

            int tot = (rightLeftAttempts < 0) ? rightLeftAttempts * -1 : rightLeftAttempts;
            for (int i = 0; i < tot; i++)
            {
                int rightLeft = (rightLeftAttempts < 0) ? -150 : 150;
                action.ClickAndHold(actor.FindOne(scrollBarLoc));
                action.MoveByOffset(rightLeft, 0).Build().Perform();

                if (DoesElementExist(actor, elementLoc))
                    break;
            }
            //action.MoveToElement(actor.FindOne(elementLoc)).Build().Perform();
        }

        public static void scrollBarHorizontalToElement(this IActor actor, IWebLocator scrollBarLoc, IWebLocator elementLoc, int rightLeftAttempts = 5) // +5 right. -5 left
        {
            scrollBarHorizontal(actor, scrollBarLoc.Query, elementLoc.Query, rightLeftAttempts);
        }
    }
}
