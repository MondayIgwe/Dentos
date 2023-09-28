@us
Feature: DEV013 – Defer and Estimated Bill Date

Verify that when the proforma status changes to 'Closed' from Edit Bill process 
Add a matter note should be created

@CancelProcess
Scenario Outline: 010 Create a new Matter and data preparation
	Given I create the Payer with Api
		| PayerName   | Entity       |
		| <PayorName> | CentralCoast |
	And I create a user with details
		| UserName | DataRoleAlias |  
		| <User>   | Admin         |
	Then I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	And I add a workflow user to a FeeEarner
	| User   | Name            |
	| <User> | <FeeEarnerName> |
	When I search or create a client
		| Entity Name | FeeEarnerFullName |
		| <Client>    | <FeeEarnerName>   |
	And I create a matter with details:
		| Client   | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | ChargeTypeGroupName | CostTypeGroupName | BillingOffice   | PayorName   |
		| <Client> | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <ChargeTypeGroup>   | <CostTypeGroup>   | <BillingOffice> | <PayorName> |

Examples:
	| FeeEarnerName | User       | Client       | Currency        | CurrencyMethod | Office  | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName   | DefaultOperatingAlias      |
	| FRD19 John    | FRD19 John | FRD19 Client | USD - US Dollar | Bill           | Chicago | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | Chicago       | James Mayer | Dentons United States, LLP |

@CancelProcess
Scenario Outline: 020 Post Disbursement and Generate Proforma
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
Scenario Outline: 030 Verify Poforma Status
	Given I navigate to proforma status process
	And I perform quick find for '<Status>'
	Then I can verify that the matter note is set to true
	And I cancel it
	And I proxy as user '<User>'
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	When I open the proforma for submission
	And submit it

Examples:
	| User       | Status   |
	| FRD19 John | DEFERRED |
