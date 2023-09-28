//using System;
//using Boa.Constrictor.Screenplay;
//using Boa.Constrictor.WebDriver;
//using Elite3E.Infrastructure.Selenium;
//using OpenQA.Selenium;
//using Elite3E.PageObjects.PageLocators.ProcessPageLocator.ProformaTrust;


//namespace Elite3E.PageObjects.Interaction.ProcessInteraction.IntendedUse
//{

//    public class IntendedUse : ITask
//    {


//        private IntendedUse() { }


//        public static IntendedUse Select() =>
//            new IntendedUse();
//        public void PerformAs(IActor actor)
//        {
//            var driver = actor.Using<BrowseTheWeb>().WebDriver;
           

//            {
//                IWebElement allowBillingChecboxGet = driver.FindElements(ProformaTrustLocators.GetAllowForBillingCheckbox.Query)[0];
//                IWebElement allowBillingChecboxSet = driver.FindElements(ProformaTrustLocators.SetAllowForBillingCheckbox.Query)[0];

//                var selected = allowBillingChecboxGet.Selected;
                
//                if (!selected && allowBillingChecboxGet())
//                    allowBillingChecboxSet.Click();
//                else if (selected && !allowBillingChecboxGet())
//                    allowBillingChecboxSet.Click();
//            }


//            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);

//            driver.WaitForPageToLoad();
//        }
//    }

    
    
//    }
//}
