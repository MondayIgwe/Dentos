@ignore


@us
Feature: CannotCreateDoubtfulInvoice
	Cannot create a Doubtful invoice from invoicesprocess

Please note: This test has been removed for all the regions except europe and singapore as all the other regions do
not use Fiscal invoice renumbering process.
https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/42490

Scenario Outline: 010 Create a new Matter and Proforma Setup Data
	Given there exists a fiscal invoice setup
		| Unit | Unit Description           |
		| 5000 | Dentons United States, LLP |
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
		| Client   | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | Rate     | ChargeTypeGroupName | CostTypeGroupName | BillingOffice   | PayorName   |
		| <Client> | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | Standard | <ChargeTypeGroup>   | <CostTypeGroup>   | <BillingOffice> | <PayorName> |

Examples:
	| FeeEarnerName | User     | Client    | Currency        | CurrencyMethod | Office  | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName   |
	| Kate Lee      | Kate Lee | Kate Team | USD - US Dollar | Bill           | Chicago | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | Montreal      | James Mayor |
	
		
Scenario Outline: 020 Create a proforma Edit
	When I post the disbursement
		| Work Date | Disbursement Type  | Work Currency  | Work Amount  | Narrative | Tax Code  |
		| {Today}-1 | <DisbursementType> | <WorkCurrency> | <WorkAmount> | {Auto}+36 | <TaxCode> |
	And I validate the disbursement is posted with no errors
	Then I can generate the proforma
		| Description | Include Other Proformas | Proforma Status |
		| {Auto}+36   | <IncludeOtherProformas> | Draft           |

Examples:
	| DisbursementType                      | WorkCurrency | WorkAmount | TaxCode                          | IncludeOtherProformas |
	| Court & Stamp Fees - Anticipated (NT) | USD          | 5000       | US Output Domestic Standard Rate | No                    |


Scenario: 030 Generate the Fiscal Invoice
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
	And the invoice number is generated
	And I navigate to the home page

Examples:
	| User     |
	| Kate Lee |

	
Scenario Outline: 040 Enter receipt information
	When I add a new receipt
		| Receipt Type  | Receipt Date | Document Number | Narrative |
		| <ReceiptType> | {Today}      | {Auto}+36       | {Auto}+36 |
	And change the operating unit "<OperatingUnit>"
	And add the invoice on the receipt
	Then the payer is auto populated

Examples:
	| OperatingUnit                     | ReceiptType |
	| 5000 - Dentons United States, LLP | NFCITIUSD   |
	

Scenario Outline: 050 Try to create a receipt
	When I receipt the total amount
	And update the receipt
	Then an error message "<ErrorMessage>" is displayed

Examples:
	| ErrorMessage                                                        |
	| Cannot create a non-doubtful writeoff for a proforma invoice (NONE) |