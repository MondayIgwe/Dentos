using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.ClientGroup
{
    public class ClientGroupLocators

    {
        public static IWebLocator FeeEarnerTab => L(
       "FeeEarnerTab",
       By.XPath ("//h5[contains(text(),'Fee Earner')]"));


        public static IWebLocator SearchButton => L(
        "SearchButton",
       By.XPath ("//span[contains(text(),'SEARCH ')]"));

        public static IWebLocator Select => L(
        "Select",
      By.XPath ("//button[@id='select-title-button']"));

        public static IWebLocator SelectExistingClient(string client) => L(
          "SelectExistingClient",
          By.XPath("//span[text()='" + client + "']"));

        public static IWebLocator ClientGroupSelectFirstRecord => L(
        "ClientGroupSelectFirstRecord",
         By.XPath("//div[@row-id='0']//input [@type= 'checkbox']"));


        public static IWebLocator ClientGroupSelectFirstRecordDesdcription => L(
        "ClientGroupSelectFirstRecordDesdcription",
        By.XPath("//div[@row-id='0']//div [@col-id= 'Description']"));

        public static IWebLocator ClientGroupSelectFirstRecordName => L(
        "ClientGroupSelectFirstRecordName",
       By.XPath ("//div[@row-id='0']//div [@col-id= 'DisplayName']"));

        public static IWebLocator SearchInput => L(
         "SearchInput",
         By.XPath("//input[@class= 'mat-input-element mat-form-field-autofill-control cdk-text-field-autofill-monitored ng-pristine ng-valid ng-touched' and not (contains(@name, 'Entity'))]"));

        public static IWebLocator Code => L(
        "Code",
        By.XPath("//input[contains(@name,'Code')]"));

        public static IWebLocator Description => L(
        "Description",
         By.XPath("//input[contains(@name,'Description')]"));

        public static IWebLocator IsResponsible(string feeEarnerName) => L(
       "IsResponsible",
        By.XPath("//span[@title='" + feeEarnerName + "']/ancestor::div[@aria-label='Grid.ariaRowDeselect']//div[@col-id='IsResponsible']//i"));

        public static IWebLocator IsResponsibleChkBox => L(
        "IsResponsibleChkBox",
         By.XPath("//div[@col-id='IsResponsible']//span/i"));

        public static IWebLocator IsOwnerChkBox => L(
       "IsOwnerChkBox",
        By.XPath("//div[@col-id='IsOwner']//span/i"));

        public static IWebLocator IsResponsibleSecondRow => L(
        "IsResponsibleSecondRow",
        By.XPath("//div[@col-id='IsResponsible'] [@comp-id ='6479']//descendant::i [contains(text(),'check_box_outline_blank')]"));

        public static IWebLocator IsResponsibleFirstRow => L(
        "IsResponsibleFirstRow",
                By.XPath("//div[@col-id='IsResponsible'][@comp-id='6471']//descendant::i [contains(text(),'check_box_outline_blank')]"));

        public static IWebLocator IsOwnerSecondRow => L(
        "IsOwnerSecondRow",
        By.XPath("//div[@col-id='IsOwner'][@comp-id='6478']//descendant::i [contains(text(),'check_box_outline_blank')]"));

        public static IWebLocator IsOwnerFirstRow => L(
        "IsOwnerFirstRow",
        By.XPath("//div[@col-id='IsOwner'][@comp-id='6470']//descendant::i [contains(text(),'check_box_outline_blank')]"));

        public static IWebLocator IsOwner(string feeEarnerName) => L(
       "IsOwner",
        By.XPath("//span[@title='"+ feeEarnerName+"']/ancestor::div[@aria-label='Grid.ariaRowDeselect']//div[@col-id='IsOwner']//i"));

        public static IWebLocator GetOwnerForFeeNumber(string feeNumber) => L(
          "GetOwnerForFeeNumber",
         By.XPath("//span[@title='" + feeNumber + "']/ancestor::div[@role='row']//div[@col-id='IsOwner']//i"));

        public static IWebLocator GetResponsibelForFeeNumber(string feeNumber) => L(
        "GetResponsibelForFeeNumber",
        By.XPath("//span[@title='" + feeNumber + "']/ancestor::div[@role='row']//div[@col-id='IsResponsible']//i"));

        public static IWebLocator InputSearch => L(
            "InputSearch",
       By.XPath("//div[contains(@class,'bound-column-container warn')]"));

        public static IWebLocator FeeOwner => L(
          "FeeOwner",
        By.XPath("//input[contains(@name,'/attributes/Owner')]"));

        public static IWebLocator Client => L(
        "Client",
       By.XPath ("//input[contains(@name,'attributes/Client')]  [not(contains(@name,'attributes/ClientGroupType_ccc'))]"));

        public static IWebLocator GetActive => L(
       "GetActive",
       By.XPath ("//mat-checkbox[contains(@data-automation-id,'IsActive')]/label/div/input"));

        public static IWebLocator GroupType => L("Client Group Type Text Box",
            By.XPath("//input[contains(@data-automation-id,'ClientGroupType_ccc')]"));

        public static IWebLocator SearchInputText => L(
     "SearchInputText",
     By.XPath( "//div[@class='search-container'] //input [not (contains(@data-automation-id,'global-auto-suggest-input'))]"));

        public static IWebLocator FeeEarner => L(
      "FeeEarner",
      By.XPath("//mat-card[contains(text(),'Fee Earner')]"));

        public static IWebLocator ClientChildForm => L(
      "ClientChildForm",
      By.XPath("//mat-card[contains(text(),'Client')]"));
    }

}
