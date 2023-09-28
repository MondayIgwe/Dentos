using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.UserRoleManagement
{
    public class UserRoleManagementLocators
    {
        public static IWebLocator FeeEarnerMapFilterButton => L(
            "User Fee Earner Map Filter Button",
            By.XPath("//span[contains(text(),'User Fee Earner Map')]/parent::div/particle-button//mat-icon[contains(text(),'filter_list')]"));

        public static IWebLocator FeeEarnerMapFilterInput => L(
            "User Fee Earner Map Filter Input",
            By.XPath("//span[contains(text(),'Fee earner')]/ancestor::div[@ref='eHeaderViewport']/ancestor::e3e-form-anchor-view/mat-card/e3e-form-anchor-view-header/div/div/input"));

        public static IWebLocator CloseFeeEarnerMapChildForm => L(
        "FeeEarnerCloseChildForm",
         By.XPath("//span[contains(text(),'Fee earner')]/ancestor::div[@ref='eHeaderViewport']/ancestor::e3e-form-anchor-view/mat-card/e3e-form-anchor-view-header/div//mat-icon[text()='close']"));

        public static IWebLocator FeeEarnerCheckbox(string feeEarner) => L(
        "Fee Earner Checkbox",
         By.XPath("//span[text()='"+feeEarner+"']//ancestor::div[@role='row']/div[@col-id='IsDefault']//span/i"));

        public static IWebLocator FeeEarnerName(string feeEarnerName) => L(
        "Fee Earner Name",
         By.XPath("//div[@col-id='TimekeeperRel.DisplayName']//span[text()='"+feeEarnerName+"']"));

        public static IWebLocator FeeEarnerFindButton => L(
            "Fee Earner Find Button",
            By.XPath("//span[contains(text(),'User Fee Earner Map')]/parent::div/particle-button//mat-icon[contains(text(),'search')]"));
    }
}
