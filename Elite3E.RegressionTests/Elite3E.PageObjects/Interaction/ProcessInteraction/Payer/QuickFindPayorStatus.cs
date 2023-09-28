using System.Collections.Generic;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using OpenQA.Selenium;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.Payer
{
    public class QuickFindPayorStatus : IQuestion<List<PayorStatusEntity>>
    {

        private QuickFindPayorStatus()
        {
        }

        public static QuickFindPayorStatus GridData() => new();

        public List<PayorStatusEntity> RequestAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;
            
           
            var payorStatuses = new List<PayorStatusEntity>();

            var rows = driver.FindElements(By.CssSelector("div[ref='centerContainer'] div[role='row']"));

            foreach (var row in rows)
            {
                payorStatuses.Add(new PayorStatusEntity()
                {
                    Code = row.FindElement(By.CssSelector("div[col-id='Code']")).Text,
                    Description = row.FindElement(By.CssSelector("div[col-id='Description']")).Text
                });
            }

            return payorStatuses;
        }
    }
    
}
