Feature: ClientInvoice

Dentons receive a trust receipt from a client and a Fee Earner/Secretary requests
an office to client transfer to offset against Dentons client invoice. 
Azure Link: https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/56859


@CancelProcess
Scenario: 010 Prepare data for Receipt/Reversal
	Given I search or create a client
		| Entity Name | DateOpened |
		| <Client>    | {Today}-1  |
	And I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	And I create a user with details
		| UserName | DataRoleAlias | DefaultOperatingAlias   | UserRoleList          |
		| <User>   | Admin         | <DefaultOperatingAlias> | DEFAULT_WORKFLOW_ROLE |
	And add a user '<User>' fee earner
	And I create the Payer with Api
		| PayerName   | Entity   |
		| <PayorName> | <Client> |
	And I create a matter with details:
		| Client   | Status     | OpenDate   | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | Rate   | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| <Client> | Fully Open | {Today}-31 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <Rate> | <ChargeTypeGroup>   | <CostTypeGroup>   | <PayorName> |
	And I post the disbursement
		| Work Date | Disbursement Type  | Work Currency | Work Amount | Narrative | Tax Code  |
		| {Today}-1 | <DisbursementType> | <Currency>    | 5000        | {Auto}+36 | <TaxCode> |
	When I validate the disbursement is posted with no errors
	And I can generate the proforma
		| Description | Include Other Proformas | Proforma Status |
		| {Auto}+36   | No                      | Draft           |
	And I proxy as user '<User>'
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

@e2eft
Examples:
	| FeeEarnerName | Client        | Currency            | CurrencyMethod | Office         | Department | Section | Rate     | ChargeTypeGroup | CostTypeGroup   | DisbursementType        | TaxCode                         | PayorName  | User     |
	| Mike Thanks   | Mikel Mandela | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Standard | TestGroup3      | Desc_at_3e8glw7 | Automation Accomodation | UK Output Domestic Standard 20% | James Matt | Ann Rose |

@CancelProcess @e2eft
Scenario Outline: 020 Create a Client Account Receipt
	Given I search for process 'Client Account Receipt'
	And I add a new client account receipt
		| TransactionDate | ClientAccountReceiptType | ClientAccountAcct                    | DocumentNumber |
		| {Today}-1       | Cheque                   | Singapore - SCB Bank Trust Acc - GBP | {Auto}+10      |
	When I add client account receipt detail child form data
		| Amount | IntendedUse | Reason                 |
		| 100    | General     | Client Account Receipt |
	Then I update it
	And submit it

@CancelProcess
Scenario Outline: 030 Create a Receipt
	When I add a new receipt
		| Receipt Type  | Receipt Date | Document Number | Narrative |
		| <ReceiptType> | {Today}      | {Auto}+36       | {Auto}+36 |
	And change the operating unit "<OperatingUnit>"
	And add the invoice on the receipt
	And I receipt the total amount
	And update the receipt
	Then I can submit the receipt
	And I validate submit was successful

@e2eft
Examples:
	| ReceiptType | OperatingUnit                  |
	| CASH        | Dentons UK and Middle East LLP |