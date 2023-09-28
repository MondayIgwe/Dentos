Feature: BankAccountMaintenance_Client

https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/53179
To confirm: Bank-to-Bank and Cash-to-Cash checkboxes in Bank Group Maintenance page are not ticked by default.
Please refer to the above ADO link to see updated comments as the test may require to be amended based on that. 

A Region is setting up three new Bank Accounts 1, 2, and 3. Bank Account 1 is in USD.  
Bank Account 2  belongs to the same Bank Group as Bank Account 3 and both bank account currency in EUR. 

@CancelProcess
Scenario Outline: 010 Search or create entity and create bank account 1 
	Given I create an entity organisation with details
		| EntityType | OrganisationName | SiteType   | Description | Street   | Country   | Language   |
		| Entity     | E2E HeadOffice   | <SiteType> | {Auto}+10   | <Street> | <Country> | <Language> |
	When I try to add an account for bank account client account
	And I enter the required data and submit the bank account client account 
   | PositivePayTemplate   | SiteType   | Street   | Language   | MoneyType   | Office   | Currency   | GLType   | CashGLAccount   | ContraGLAccount   |
   | <PositivePayTemplate> | <SiteType> | <Street> | <Language> | <MoneyType> | <Office> | <Currency> | <GLType> | <CashGLAccount> | <ContraGLAccount> |
   Then  I verify and submit bank group maintenance 
   | ModuleType    | ReconciliationRuleSet |
   | Trust Account | Standard Match Rule   |
   And I validate submit was successful
@e2eft
Examples:
	| SiteType | Street          | Country        | Language | PositivePayTemplate | MoneyType           | Office         | Currency | GLType               | CashGLAccount                          | ContraGLAccount                        |
	| Billing  | 1 Entity Street | United Kingdom | English  | TE_PositivePay      | General Trust Money | London (UKIME) | USD      | Trust_Client Account | 3000-202010-1000-0000-0000-8054-000000 | 3000-203110-3021-0000-0000-8054-000000 |
   
@CancelProcess
Scenario Outline: 020 Search or create entity and create bank account 2 
	Given I create an entity organisation with details
		| EntityType | OrganisationName | SiteType   | Description | Street   | Country   | Language   |
		| Entity     | E2E Subsidiary   | <SiteType> | {Auto}+10   | <Street> | <Country> | <Language> |
	When I try to add an account for bank account client account
	And I enter the required data and submit the bank account client account 
   | PositivePayTemplate   | SiteType   | Street   | Language   | MoneyType   | Office   | Currency   | GLType   | CashGLAccount   | ContraGLAccount   |
   | <PositivePayTemplate> | <SiteType> | <Street> | <Language> | <MoneyType> | <Office> | <Currency> | <GLType> | <CashGLAccount> | <ContraGLAccount> |
   Then  I verify and submit bank group maintenance 
   | ModuleType    | ReconciliationRuleSet |
   | Trust Account | Standard Match Rule   |
   And I validate submit was successful
@e2eft
Examples:
	| SiteType | Street          | Country        | Language | PositivePayTemplate | MoneyType           | Office         | Currency | GLType               | CashGLAccount                          | ContraGLAccount                        |
	| Billing  | 1 Entity Street | United Kingdom | English  | TE_PositivePay      | General Trust Money | London (UKIME) | EUR      | Trust_Client Account | 3000-202010-1000-0000-0000-8054-000000 | 3000-203110-3021-0000-0000-8054-000000 |
      
@CancelProcess
Scenario Outline: 030 Create bank account 3 with the same bank group as bank account 2
	Given I try to add an account for bank account client account
	When I enter the required data and submit the bank account client account 
   | PositivePayTemplate   | SiteType   | Street   | Language   | MoneyType   | Office   | Currency   | GLType   | CashGLAccount   | ContraGLAccount   | IsNewBankGroup |
   | <PositivePayTemplate> | <SiteType> | <Street> | <Language> | <MoneyType> | <Office> | <Currency> | <GLType> | <CashGLAccount> | <ContraGLAccount> | No             |
   Then I validate submit was successful
@e2eft
Examples:
	| Language | PositivePayTemplate | MoneyType           | Office         | Currency | GLType               | CashGLAccount                          | ContraGLAccount                        |
	| English  | TE_PositivePay      | General Trust Money | London (UKIME) | EUR      | Trust_Client Account | 3000-202010-1000-0000-0000-8054-000000 | 3000-203110-3021-0000-0000-8054-000000 |
      