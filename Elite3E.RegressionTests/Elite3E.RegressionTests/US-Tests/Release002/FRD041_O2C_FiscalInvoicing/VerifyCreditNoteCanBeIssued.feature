@ignore
@us
Feature: VerifyCreditNoteCanBeIssued
	Verify that credit note can be issued and verify credit not is disabled on Proforma edit

Please note: This test has been removed for all the regions except europe and singapore as all the other regions do 
not use Fiscal invoice renumbering process. 
https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/42490

#Defect #https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/69247
#Below scenarios are ignored as well

Scenario Outline: 010 Create a new Matter
	Given there exists a fiscal invoice setup
		| Unit   | Unit Description  |
		| <Unit> | <UnitDescription> |
	And I create a user with details
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
	And I create the Payer with Api
		| PayerName   | Entity   |
		| <PayorName> | <Client> |
	When I create a matter with details:
		| Client   | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | Rate   | ChargeTypeGroupName | CostTypeGroupName | BillingOffice   | PayorName   |
		| <Client> | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <Rate> | <ChargeTypeGroup>   | <CostTypeGroup>   | <BillingOffice> | <PayorName> |

Examples:
	| FeeEarnerName | User          | Client        | Unit | UnitDescription            | Currency        | CurrencyMethod | Office  | Department | Section | Rate     | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName |
	| Sabelo McGill | Sabelo McGill | Sabelo McGill | 5000 | Dentons United States, LLP | USD - US Dollar | Bill           | Chicago | Default    | Default | Standard | Auto_IncludeALL | Auto_IncludeALL | Montreal      | James May |
	
Scenario Outline: 020 Create a proforma Edit
	When I post the disbursement
		| Work Date | Disbursement Type  | Work Currency  | Work Amount  | Narrative | Tax Code  |
		| {Today}-1 | <DisbursementType> | <WorkCurrency> | <WorkAmount> | {Auto}+36 | <TaxCode> |
	And I validate the disbursement is posted with no errors
	Then I can generate the proforma
		| Description | Include Other Proformas | Proforma Status |
		| {Auto}+36   | <IncludeOtherProformas> | Draft           |
Examples:
	| DisbursementType                   | WorkCurrency | WorkAmount | TaxCode                          | IncludeOtherProformas |
	| Data Storage & Costs - Anticipated | USD          | 5000       | US Output Domestic Standard Rate | No                    |

#Update the Proforma Edit process as per the latest release
Scenario: 030 Proforma Edit
	Given I proxy as user '<User>'
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	And I open the proforma for submission
	Then I submit it
	And I cancel proxy
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	When I open the proforma for billing
	Then the full credit note is disabled
	Then I set create fiscal invoice
	And bill it without printing
	Then the tax invoice number is generated
	
Examples:
	| User     |
	| Kate Lee |

Scenario: 040 I want to Send the Fiscal Invoice
	Given I search for 'Workflow Dashboard'
	And I open the Invoice Dispatch workflow task
	When I send the invoice routed to finance team when dispatch method not set
	And I navigate to the home page
 
Scenario: 050 Create a Credit Note
	When I view the invoices
	And submit the invoice with the full credit note set with '<Invoices>'
	Then the credit note is generated

Examples:
	| Invoices |
	| Invoices |

Scenario: 060 Print the creditnote
	Given I print the invoice

Scenario: 070 View the Credit Note and Verify
	When I search and select all the invoices
	Then the credit note is available