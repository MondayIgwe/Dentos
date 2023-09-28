using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.TaxCode;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.TaxCode
{
    public class EnterTaxCodeTaxData : ITask
    {
        public TaxCodeTaxEntity TaxCodeTaxEntity { get; }

        private EnterTaxCodeTaxData(TaxCodeTaxEntity taxCodeTaxEntity) =>
            TaxCodeTaxEntity = taxCodeTaxEntity;

        public static EnterTaxCodeTaxData With(TaxCodeTaxEntity taxCodeTaxEntity) => new(taxCodeTaxEntity);

        public void PerformAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;
            string section = "Tax Code Tax";
            actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, section));

            actor.AttemptsTo(JScript.ClickOn(CommonLocator.ChildFormAction(section, LocatorConstants.AddButton)));

            actor.AttemptsTo(ChildProcessView.SwitchToView(section, GlobalConstants.Form));

            actor.AttemptsTo(Dropdown.SelectOptionByName(TaxCodeTaxLocator.Tax, TaxCodeTaxEntity.Tax));

            actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
        }
    }
}
