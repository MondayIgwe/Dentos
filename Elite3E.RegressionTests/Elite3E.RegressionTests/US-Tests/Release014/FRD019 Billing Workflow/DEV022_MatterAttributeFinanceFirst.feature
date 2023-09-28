@us
Feature: DEV022_MatterAttributeFinanceFirst

Verify 'Finance First Step' is present in Matter Attribute process (True) - Proxy as a BillingTeam user

@CancelProcess
Scenario Outline: 010 Create a new Matter and data preparation
	Given I create the Payer with Api
		| PayerName   | Entity       |
		| <PayorName> | CentralCoast |
	When I create a user with details
		| UserName | DataRoleAlias |
		| <User>   | Admin         |
	Then I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	And I add a workflow user to a FeeEarner
	| User   | Name            |
	| <User> | <FeeEarnerName> |
	And I search or create a client
		| Entity Name | FeeEarnerFullName |
		| <Client>    | <FeeEarnerName>   |
	And I create a matter with details:
		| Client   | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | ChargeTypeGroupName | CostTypeGroupName | BillingOffice   | PayorName   |
		| <Client> | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <ChargeTypeGroup>   | <CostTypeGroup>   | <BillingOffice> | <PayorName> |

Examples:
	| FeeEarnerName | User       | Client       | Currency        | CurrencyMethod | Office  | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName   | DefaultOperatingAlias      |
	| FRD19 John    | FRD19 John | FRD19 Client | USD - US Dollar | Bill           | Chicago | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | Chicago       | James Mayer | Dentons United States, LLP |

@CancelProcess
Scenario Outline: 020 Create a Matter Attribute then add it to the Matter Maintenance process
	Given I search for 'Matter Attribute'
	And I add new matter attribute
		| Code      | Description |
		| {Auto}+10 | {Auto}+18   |
	And I set the finance first step checkbox to true
	And I update it
	And I submit it
	And I validate submit was successful
	Then I navigate to the matter maintenance process
	And I reopen a saved Matter
	Then I set the matter attribute
	And I submit it

@CancelProcess
Scenario Outline: 030 Post Disbursement and Generate Proforma
	When I post the disbursement
		| Work Date | Disbursement Type  | Work Currency  | Work Amount  | Narrative | Tax Code  |
		| {Today}-1 | <DisbursementType> | <WorkCurrency> | <WorkAmount> | {Auto}+36 | <TaxCode> |
	And I validate the disbursement is posted with no errors
	Then I can generate the proforma
		| Description | Include Other Proformas | Proforma Status |
		| {Auto}+36   | <IncludeOtherProformas> | Draft           |

Examples:
	| DisbursementType              | WorkCurrency | WorkAmount | TaxCode                          | IncludeOtherProformas |
	| Dentons United States Funding | USD          | 5000       | US Output Domestic Standard Rate | No                    |

@CancelProcess
Scenario Outline: 040 Verify the Finance First Step 
	Given I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	When I open the proforma finance first step task
	And I submit it

Examples:
	| User       |
	| FRD19 John |


