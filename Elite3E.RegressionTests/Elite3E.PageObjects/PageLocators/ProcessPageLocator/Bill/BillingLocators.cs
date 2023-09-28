using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.Bill
{
    public class BillingLocators
    {
        public static IWebLocator AttributeInput(int count, string name) => L(
           "AttributeInput ",
            By.XPath("//input[@name='advancedParameters["+name+"].where.predicates."+count+".attribute']"));
        public static IWebLocator ValueInput(int count, string name) => L(
           "ValueInput ",
            By.XPath("//input[@name='advancedParameters["+name+"].where.predicates."+count+".value']"));
        public static IWebLocator TImeBillHeader(string title)=> L(
          "TImeBillHeader ",
           By.XPath("//span[contains(text(),'"+title+"')]"));

        public static IWebLocator RemoveFieldIcon => L(
         "RemoveFieldIcon ",
          By.XPath("//mat-icon[text()=' remove_circle_outline ']"));

        public static IWebLocator AttributeNameText(string name) => L(
        "AttributeNameText ",
         By.XPath("//span[text()='"+name+"']"));








    }
}
