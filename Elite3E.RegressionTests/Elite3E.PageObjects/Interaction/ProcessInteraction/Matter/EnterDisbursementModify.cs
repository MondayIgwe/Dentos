using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Purge;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using System.Collections.Generic;
using Elite3E.Infrastructure.Constant;
using OpenQA.Selenium;
using Elite3E.Infrastructure.Selenium;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.Matter
{
    public class EnterDisbursementModify : ITask
    {
        public List<DisbursementModifyEntity> DisbursementModiferEntity { get; }
        public string MatterNumber { get; }

        private EnterDisbursementModify(List<DisbursementModifyEntity> disbursementModifyEntity, string matterNumber)
        {
            DisbursementModiferEntity = disbursementModifyEntity;
            MatterNumber = matterNumber;
        }

        public static EnterDisbursementModify With(List<DisbursementModifyEntity> disbursementModifyEntity,
            string matterNumber) => new(disbursementModifyEntity, matterNumber);

        public void PerformAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;
            var clickAdd = false;

            foreach(var disbursementModifyEntity in DisbursementModiferEntity)
            {
                if(clickAdd)
                {
                    actor.AttemptsTo(MainProcessMenu.ClickOn(MainProcessMenuAction.Add));
                   
                    while(true)
                    {
                        var narrative = driver.FindElement(DisbursementModifyLocator.Narrative.Query);
                       // Thread.Sleep(TimeSpan.FromMilliseconds(200));
                        if (narrative.Text.Length == 0)
                            break;
                    }
                }
                
                actor.AttemptsTo(DateControl.SelectDate(GlobalConstants.WorkDate, disbursementModifyEntity.WorkDate));
                actor.AttemptsTo(SendKeys.To(DisbursementModifyLocator.Matter, MatterNumber));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                actor.AttemptsTo(Lookup.SearchAndSelectSingle("Disbursement Type",disbursementModifyEntity.DisbursementType)) ;
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                actor.AttemptsTo(SendKeys.To(DisbursementModifyLocator.WorkCurrency, disbursementModifyEntity.WorkCurrency));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                actor.AttemptsTo(SendKeys.To(DisbursementModifyLocator.WorkAmount, disbursementModifyEntity.WorkAmount +Keys.Tab));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                actor.AttemptsTo(Click.On(DisbursementModifyLocator.InternalComments));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                //Check to see if the Narrative text area is populated or not, if not the element will exist
                if (actor.DoesElementExist(DisbursementModifyLocator.Narrative))
                {
                    driver.FindElement(DisbursementModifyLocator.Narrative.Query).SendKeys(disbursementModifyEntity.Narrative);
                    actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                }  

                if (!string.IsNullOrEmpty(disbursementModifyEntity.PurgeType))
                    actor.AttemptsTo(Dropdown.SelectOptionByName(DisbursementModifyLocator.PurgeType, disbursementModifyEntity.PurgeType));

                if (!string.IsNullOrEmpty(disbursementModifyEntity.TaxCode))
                    actor.AttemptsTo(SendKeys.To(DisbursementModifyLocator.TaxCode, disbursementModifyEntity.TaxCode + Keys.Tab));
                
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                clickAdd = true;
            }
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}
