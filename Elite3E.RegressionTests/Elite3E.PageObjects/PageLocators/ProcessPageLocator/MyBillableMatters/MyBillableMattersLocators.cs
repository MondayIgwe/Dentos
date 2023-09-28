using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.MyBillableMatters
{
    public class MyBillableMattersLocators
    {
        public static By MyBillableMattersDashboard => By.XPath("//div[contains(@title,'My Billable Matters')]");
      
        public static IWebLocator ClientInput => L(
        "ClientInput",
         By.XPath("//input[contains(@data-automation-id,'Client')]"));

        public static By MyMatterMetric => By.XPath("//div[contains(@title,'My Matters Metric')]");

        public static IWebLocator ProformaOptions => L(
       "ProformaOptions",
        By.XPath("//button//span[text()=' Proforma Options ']"));

        public static IWebLocator ProformaSearch => L(
     "ProformaSearch",
      By.XPath("//button//span[text()=' Search ']"));

        public static IWebLocator FirstRowCheckbox => L(
    "FirstRowCheckbox",
     By.XPath("//div[@row-index='0']//span//i"));

        public static IWebLocator InfoOnlyButton => L(
     "InfoOnlyButton",
      By.XPath("//button//span[text()=' Info Only ']"));

        public static IWebLocator Generate => L(
    "Generate",
     By.XPath("//button//span[text()=' Generate ']"));

        public static IWebLocator IndividualCheckbox => L(
      "IndividualCheckbox",
       By.XPath("//mat-checkbox[contains(@data-automation-id,'IsPrintSeparate')]//div//input"));

        public static IWebLocator MatterInput => L(
       "MatterInput",
        By.XPath("(//input[contains(@data-automation-id,'attributes/Matter')])[1]"));

        public static IWebLocator MetricRunDate => L(
       "MetricRunDate",
        By.XPath("//div[contains(@name,'/MetricRunDate')]"));

        public static IWebLocator FeeEarnerInput => L(
          "FeeEarnerInput",
         By.XPath("//input[contains(@data-automation-id,'TkprMyMatters')]"));

        public static IWebLocator GetIsBillingCheckbox => L(
          "SetIsBillingCheckbox",
         By.XPath("//mat-checkbox[contains(@data-automation-id,'IsBillTkpr')]/label/div/input"));

        public static IWebLocator GetIsExcludeZeroWIPCheckbox => L(
         "SetIsExcludeZeroWIPCheckbox",
        By.XPath("//mat-checkbox[contains(@data-automation-id,'IsExcludeZeroWIP')]/label/div/input"));

        public static IWebLocator GetIsExcludeZeroARCheckbox => L(
        "SetIIsExcludeZeroARCheckbox",
       By.XPath("//mat-checkbox[contains(@data-automation-id,'IsExcludeZeroAR')]/label/div/input"));

        public static IWebLocator ClientAccountCheckbox => L(
       "ClientAccountCheckbox",
      By.XPath("//mat-checkbox[contains(@data-automation-id,'IsExcludeZeroTrust')]"));

        public static IWebLocator GetIsExcludeZeroTrustCheckbox => L(
      "SetIsExcludeZeroTrustCheckbox",
     By.XPath("//mat-checkbox[contains(@data-automation-id,'IsExcludeZeroTrust')]/label/div/input"));

        public static IWebLocator GetIsSupervisingCheckbox => L(
      "SetIsSupervisingCheckbox",
     By.XPath("//mat-checkbox[contains(@data-automation-id,'IsSpvTkpr')]/label/div/input"));

        public static IWebLocator GetIsResponsibleCheckbox => L(
      "SetIsResponsibleCheckbox",
     By.XPath("//mat-checkbox[contains(@data-automation-id,'IsRspTkpr')]/label/div/input"));

        public static IWebLocator Search => L(
       "Search",
        By.XPath("(//span[contains(text(),'Search')])[3]"));

        public static IWebLocator GetIsResposibleCheckbox => L(
        "SetIsResposibleCheckbox",
       By.XPath("//mat-checkbox[contains(@data-automation-id,'IsRspTkpr')]/label/div/input"));

        public static IWebLocator GetIsSaveSettingsCheckbox => L(
       "SetIsSaveSettingsCheckbox",
        By.XPath("//mat-checkbox[contains(@data-automation-id,'IsSaveSettings')]/label/div/input"));

        public static IWebLocator ExistMyBillableMattersDashboard => L(
       "ExistMyBillableMattersDashboard", MyBillableMattersDashboard);

        public static IWebLocator MyMattersHorizontalScrollBar => L(
       "MyMattersHorizontalScrollBar",
       By.XPath("//div[contains(@class,'horizontal-scroll-container')]/parent::div[contains(@class,'horizontal-scroll')]"));
     
        public static IWebLocator MyMattersBOALabel => L(
       "MyMattersBOALabel",
       By.XPath("//div[@ref='eLabel']/child::span[text()='BOA']"));

        public static IWebLocator MyMattersBOAGridValue => L(
       "MyMattersBOAGridValue",
       By.XPath("//div[@col-id='BOABal_ccc'][@role='gridcell']//span[@title]"));

        public static IWebLocator MyMatterMetricSearch => L(
       "ExistMyBillableMattersDashboard", MyMatterMetric);

        public static IWebLocator MyMatterSearchFilter => L(
      "MyMatterSearchFilter",
      By.XPath("//span[contains(text(),'My Matters')]//following::mat-icon[text()='filter_list']"));

        public static IWebLocator MyMatterSearchInput => L(
    "MyMatterSearchInput",
    By.XPath("//e3e-form-anchor-view-header//input"));

        public static IWebLocator MyMatterResultSelect => L(
     "MyMatterResultSelect",
     By.XPath("//div[@row-index='0']//div[@col-id='IsSelected']"));

        public static IWebLocator CloseMyMatterIcon => L(
     "CloseMyMatterIcon",
     By.XPath("//button//mat-icon[text()='close']"));

        public static IWebLocator CloseFilterMyMatterIcon => L(
"CloseFilterMyMatterIcon",
By.XPath("//mat-tab-body//button//mat-icon[text()='close']"));


    }
}
