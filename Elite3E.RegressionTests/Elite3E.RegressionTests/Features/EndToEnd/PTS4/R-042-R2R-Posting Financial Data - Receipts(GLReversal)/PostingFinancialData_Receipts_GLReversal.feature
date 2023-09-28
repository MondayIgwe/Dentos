Feature: PostingFinancialData_Receipts_GLReversal

R-042 - R2R: Posting Financial Data - Receipts (GL - Reversal)
https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/55668

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

@CancelProcess
Scenario Outline: 020 Create a Receipt with Invoice
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

@CancelProcess
Scenario: 030 Reverse and Reallocate Receipt
	Given I locate and search for a receipt to reverse
	And I perform the reversal
		| ReversalDate | Reason    | Comment          | ReAllocate |
		| {Today}+1    | BILL_PAID | Receipt Reversal | true       |
	And change the operating unit "<OperatingUnit>"
	Then I can submit the reciept allocation
	And I remove the invoice for the receipt
	When I add a new general ledger row
		| GLType   | Receipt Amount  |
		| <GLType> | <ReceiptAmount> |
	And I add the gl account
		| GLAccount   |
		| <GLAccount> |
	And I update it
	And I submit it
	And I validate submit was successful
	Then I locate the reversed receipt
	And I get the receipt index of the receipt
	And  I validate the gl postings for operating unit '<OperatingUnit>'

@e2eft
Examples:
	| OperatingUnit                  | ReceiptAmount | GLType             | GLAccount                              | OperatingUnitCode |
	| Dentons UK and Middle East LLP | 6000          | All Books GL Types | 3000-101010-1000-0000-0000-0000-000000 | 3000              |

	@e2eft @CancelProcess
    Scenario Outline: 040 Verify the Post Queue
	Given I navigate to the post queue process
	When I verify the cost/time/charge/receipt card is not present in the post queue

	@e2eft @CancelProcess
	Scenario: 050 Verify in Posting Results
	Given  I search for the entry in posting results process 
	Then verify the posting status is posted

	@e2eft
	Scenario: 060 Verify in Journal Register
	Given I search for the entry in journal register
	Then verify the posting details in journal register
            | Search Column   | Search Operator |
            | Journal Manager | Equals          |