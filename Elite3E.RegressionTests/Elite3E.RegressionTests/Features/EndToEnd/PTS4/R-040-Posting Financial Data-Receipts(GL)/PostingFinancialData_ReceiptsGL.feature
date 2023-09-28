Feature: PostingFinancialData_Receipts
R-040: Posting Financial Data - Receipts (GL)
https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/52600
@CancelProcess
	Scenario Outline: 010 Create a new Matter
	Given I search or create a client
		| Entity Name | DateOpened |
		| <Client>    | {Today}-1  |
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
	And I create a matter with details:
		| Client   | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | ChargeTypeGroupName | CostTypeGroupName | BillingOffice   | PayorName   |
		| <Client> | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <ChargeTypeGroup>   | <CostTypeGroup>   | <BillingOffice> | <PayorName> |

	@e2eft
	Examples:
	| FeeEarnerName | Client        | Currency            | CurrencyMethod | Office         | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName  | User     |
	| Mike Thanks   | Mikel Mandela | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | London (EU)   | James Matt | Ann Rose |


@CancelProcess
Scenario Outline: 020 Create a Receipt with GL
	Given I add a new receipt
		| Receipt Type  | Receipt Date | Document Number | Narrative |
		| <ReceiptType> | {Today}      | {Auto}+36       | {Auto}+36 |
	And change the operating unit "<OperatingUnit>"
	When I add a new general ledger row
		| GLType   | Receipt Amount  |
		| <GLType> | <ReceiptAmount> |
	And I add the gl account
		| GLAccount   |
		| <GLAccount> |
	And I receipt the total amount
	And I add the payer
	Then I update it
	And I submit it
	And I validate submit was successful
	And I locate the submitted receipt 
	And I get the receipt index of the receipt
	And  I validate the gl postings for operating unit '<OperatingUnitCode>'
@e2eft
Examples:
	| ReceiptType | OperatingUnit                  | ReceiptAmount | GLType             | GLAccount                              | OperatingUnitCode |
	| CASH        | Dentons UK and Middle East LLP | 6000          | All Books GL Types | 3000-101010-1000-0000-0000-0000-000000 | 3000              |

	@e2eft @CancelProcess
    Scenario Outline: 030 Verify the Post Queue
	Given I navigate to the post queue process
	When I verify the cost/time/charge/receipt card is not present in the post queue

	@e2eft @CancelProcess
	Scenario: 040 Verify in Posting Results
	Given I search for the entry in posting results process 
	Then verify the posting status is posted

	@e2eft
	Scenario: 050 Verify in Journal Register
	Given I search for the entry in journal register
	Then verify the posting details in journal register
            | Search Column   | Search Operator |
            | Journal Manager | Equals          |