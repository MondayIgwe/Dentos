@us
Feature: DEV040_WorkflowRouting

Verify that a proforma can be generated via My billable matters process

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
	| FeeEarnerName | User       | Client       | Currency        | CurrencyMethod | Office  | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName   | DefaultOperatingAlias         |
	| FRD19 John    | FRD19 John | FRD19 Client | USD - US Dollar | Bill           | Chicago | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | Chicago       | James Mayer | Dentons United States Funding |

@CancelProcess
Scenario Outline: 020 Post Disbursement
	When I post the disbursement
		| Work Date | Disbursement Type  | Work Currency  | Work Amount  | Narrative | Tax Code  |
		| {Today}-1 | <DisbursementType> | <WorkCurrency> | <WorkAmount> | {Auto}+36 | <TaxCode> |
	And I validate the disbursement is posted with no errors


Examples:
	| DisbursementType              | WorkCurrency | WorkAmount | TaxCode                          |
	| Dentons United States Funding | USD          | 5000       | US Output Domestic Standard Rate |

Scenario Outline: 030 Generate the Proforma via My Billable Matters
	Given I search for process 'My Billable Matters' without add button
	When I search or filter by matter
	And I verify proforma has been generated
	Then I close my billable matter


Examples:
	| User       |
	| FRD19 John |

@CancelProcess
Scenario Outline: 040 Verification on Proforma Workflow
 Given I proxy as user '<User>'
	Given I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	When I open the proforma for submission
	And I add apply adjustment details
		| Percentage |
		| 25         |
	And I submit it
	And I cancel proxy
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	Then I open the proforma for billing
	And I bill it without printing
	And the invoice number is generated

Examples:
	| User       |
	| FRD19 John |
