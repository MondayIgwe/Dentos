using OpenQA.Selenium;
using Boa.Constrictor.WebDriver;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.DoA
{
    public static class DoALocators
    {
        public static IWebLocator Code => L(
          "Code",
          By.XPath("//input[contains(@data-automation-id,'Code')]"));
        public static IWebLocator Description => L(
         "Description",
          By.XPath("//input[contains(@data-automation-id,'Description')]"));
        public static IWebLocator DoARolesCard => L(
        "Description",
         By.XPath("//mat-card[contains(text(),'DoA Roles')]"));
        public static IWebLocator UnitDropdown => L(
       "UnitDropdown",
        By.XPath("//div[@col-id='NxUnit']//e3e-small-list-cell-editor//input"));
        public static IWebLocator OfficeDropdown => L(
       "OfficeDropdown",
        By.XPath("//div[@col-id='Office']//e3e-small-list-cell-editor//input"));
        public static IWebLocator DepartmentDropdown => L(
       "DepartmentDropdown",
        By.XPath("//div[@col-id='Department']//e3e-small-list-cell-editor//input"));
        public static IWebLocator DoARoleInput => L(
     "DepartmentDropdown",
      By.XPath("//input[contains(@name,'DOARoles_ccc')]"));

        public static IWebLocator RoleDropdown => L(
        "RoleDropdown",
        By.XPath("//div[@col-id='NxRole']//e3e-small-list-cell-editor//input"));
        public static IWebLocator RowDiv(string row, string title) => L(
       "RowDiv",
       By.XPath("//div[@row-index='" + row + "']//div[@col-id='" + title + "' ]"));
        public static IWebLocator RoleDiv(string title) => L(
       "RoleDiv",
        By.XPath("//div[@aria-rowindex='2']//div[@col-id='" + title + "']//div//span"));
        public static IWebLocator UnitInput => L(
          "UnitInput",
          By.XPath("//input[contains(@data-automation-id,'NxUnit')]"));
        public static IWebLocator DoANoRoles => L(
         "DoANoRoles",
         By.XPath("//span[text()='DoA Roles  (0)']"));
        public static IWebLocator DoAReportPage => L(
        "DoAReportPage",
        By.XPath("//mat-toolbar//span[text()='DoA Report']"));
        public static IWebLocator DoARoleTypeDropdown => L(
         "DoARoleTypeDropdown",
         By.XPath("//input[contains(@data-automation-id,'DOARoleType_ccc')]"));
        public static IWebLocator WorkflowReport => L(
        "WorkflowReport",
        By.XPath("//input[contains(@data-automation-id,'NxWfConfig')]"));
        public static IWebLocator UnitReportDropdown => L(
         "UnitReportDropdown",
         By.XPath("//input[contains(@data-automation-id,'NxUnit')]"));
        public static IWebLocator OfficeReportDropdown => L(
         "OfficeReportDropdown",
         By.XPath("//input[contains(@data-automation-id,'Office')]"));
        public static IWebLocator DepartmentReportDropdown => L(
         "DepartmentReportDropdown",
         By.XPath("//input[contains(@data-automation-id,'Department')]"));
        public static IWebLocator RoleReportDropdown => L(
        "RoleReportDropdown",
        By.XPath("//input[contains(@data-automation-id,'NxRole')]"));
    }

}