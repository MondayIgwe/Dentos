@ignore
#This test is failing at the moment as the GL postings are not available for the Write off receipt
Feature: PostingFinancialData_ReceiptsClientInvoice
R-039 - R2R: Posting Financial Data - Receipts (Client Invoice)
https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/52596
@CancelProcess
Scenario: 010 Prepare data for Receipts
	Given I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	And I create a user with details
		| UserName | DataRoleAlias | DefaultOperatingAlias   | UserRoleList          |
		| <User>   | Admin         | <DefaultOperatingAlias> | DEFAULT_WORKFLOW_ROLE |
	And add a user '<User>' fee earner
	And I create the Payer with Api
		| PayerName   | Entity   |
		| <PayorName> | <Client> |
	Given I create a matter with details:
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
	| FeeEarnerName | Currency            | Client        | CurrencyMethod | Office         | Department | Section | Rate     | ChargeTypeGroup | CostTypeGroup   | DisbursementType        | TaxCode                    | PayorName  | User     | DefaultOperatingAlias          |
	| Mike Thanks   | GBP - British Pound | Mikel Mandela | Bill           | London (UKIME) | Default    | Default | Standard | TestGroup3      | Desc_at_3e8glw7 | Automation Accomodation | UK Output Domestic Zero 0% | James Matt | Ann Rose | Dentons UK and Middle East LLP |


	


@CancelProcess
Scenario Outline: 020 Create a Receipt with Invoice
	When I add a new receipt
		| Receipt Type  | Receipt Date | Cheque Date | Document Number | Narrative | Amount   | Deposit Number |
		| <ReceiptType> | {Today}      | {Today}-1   | {Auto}+36       | {Auto}+36 | <Amount> | {Auto}+12      |
	And change the operating unit "<OperatingUnit>"
	And add the invoice on the receipt
	And I add the receipt amount '<Amount>' and total it
	Then I can submit the receipt
	And I validate submit was successful

@e2eft
Examples:
	| ReceiptType | OperatingUnit                  | Amount  |
	| Cash        | Dentons UK and Middle East LLP | 4000.00 |

@CancelProcess
Scenario Outline: 030 Create a Write Off Receipt with Invoice
	When I add a new receipt
		| Receipt Type  | Receipt Date | Cheque Date | Document Number | Narrative | Deposit Number |
		| <ReceiptType> | {Today}      | {Today}-1   | {Auto}+36       | {Auto}+36 | {Auto}+12      |
	And change the operating unit "<OperatingUnit>"
	And add the invoice on the receipt
	And I verify the write off amount in the receipt '<WriteOffAmount>'
	Then I can submit the receipt
	And I validate submit was successful
	And I locate the submitted receipt 
	And I get the receipt index of the receipt
	#And  I validate the gl postings for operating unit '<OperatingUnitCode>'
	Given I search for process 'Matter Group Enquiry' without add button
	When I search in matter group enquiry
		| SearchPhrase | SearchValue |
		| Matter       |             |
	And I submit it
	Then I validate the transaction entries for 'Woff'
@e2eft
Examples:
	| ReceiptType | OperatingUnit                  | WriteOffAmount | OperatingUnitCode | SearchPhrase | SearchValue |
	| WriteOff    | Dentons UK and Middle East LLP | 1000.00        | 3000              | Matter       |             |

	@e2eft @CancelProcess
    Scenario Outline: 040 Verify the Post Queue
	Given I navigate to the post queue process
	When I verify the cost/time/charge/receipt card is not present in the post queue

	@e2eft @CancelProcess
	Scenario: 050 Verify in Posting Results
	Given I search for the entry in posting results process 
	Then verify the posting status is posted

	@e2eft
	Scenario: 060 Verify in Journal Register
	Given I search for the entry in journal register
	Then verify the posting details in journal register
            | Search Column   | Search Operator |
            | Journal Manager | Equals          |