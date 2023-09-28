@us
@ignore
Feature: ReceiptWithoutFiscalInvoice
	A proforma Invoice can be receipted without making it a fiscal Invoice

	Please note: This test has been removed for all the regions except europe and singapore as all the other regions do 
not use Fiscal invoice renumbering process. 
https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/42490

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
	| FeeEarnerName | User     | Client    | Currency        | CurrencyMethod | Office  | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName |
	| Kate Lee      | Kate Lee | Kate Team | USD - US Dollar | Bill           | Chicago | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | Montreal      | James May |



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
Scenario: 030 I want to 'Bill No Print' the proforma
	Given I proxy as user '<User>'
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	And I open the proforma for submission
	Then I submit it
	And I cancel proxy
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	When I open the proforma for billing
	And I bill it without printing
	Then the invoice number is generated

Examples:
	| User     |
	| Kate Lee |

#Defect #https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/69247: Fixed
#Below scenarios are ignored as well 

Scenario: 040 I want to Send the Fiscal Invoice
	Given I search for 'Workflow Dashboard'
	And I open the Invoice Dispatch workflow task
	When I send the invoice routed to finance team when dispatch method not set

Scenario Outline: 050 Enter receipt information
	When I add a new receipt
		| Receipt Type  | Receipt Date | Document Number | Narrative |
		| <ReceiptType> | {Today}      | {Auto}+36       | {Auto}+36 |
	And change the operating unit "<OperatingUnit>"
	And add the invoice on the receipt
	Then the payer is auto populated
	When I receipt the total amount
	And update the receipt
	Then I can submit the receipt

Examples:
	| OperatingUnit                     | ReceiptType |
	| 5000 - Dentons United States, LLP | NFCITIUSD   |


Scenario: 060 Create a Credit Note
	When I view the invoices
	Then the invoice is set as paid
	And the tax invoice number displayed on invoices