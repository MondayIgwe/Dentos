Feature: Cash Receipt Reversal Invoice

A requestor notifies a collector of misallocation and the collector requests for the 
receipt to be reversed and reallocated against another bill. 
The Cashier Team reviews the request, 
accept and reverses the receipt accordingly (Cash Receipts Use converted matter and bill ).
Azure link: https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/41536

@CancelProcess
Scenario: 010 Prepare data for Receipt/Reversal

	Given I create a user with details
		| UserName | DataRoleAlias | DefaultOperatingAlias   | UserRoleList          |
		| <User>   | Admin         | <DefaultOperatingAlias> | DEFAULT_WORKFLOW_ROLE |
	And I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	And add a user '<User>' fee earner
	And I search or create a client
		| Entity Name | FeeEarnerFullName |
		| <Client>    | <FeeEarnerName>   |
	And I create the Payer with Api
		| PayerName   | Entity   |
		| <PayorName> | <Client> |
	When I create a matter with details:
		| Client   | Status     | OpenDate   | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | Rate   | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| <Client> | Fully Open | {Today}-31 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <Rate> | <ChargeTypeGroup>   | <CostTypeGroup>   | <PayorName> |	
@e2eft
Examples:
	| User     | Client        | FeeEarnerName         | Office    | ChargeType    | TaxCode                 | ChargeAmount | ChargeTypeGroup | Currency               | PayorName | DefaultOperatingAlias        |
	| Ann Rose | Mister Client | DentonsAPI FeeEarner3 | Singapore | Miscellaneous | SG Output Domestic Zero | 500.00       | at_BOA_Group    | SGD - Singapore Dollar | Lara Moon | Dentons Rodyk & Davidson LLP |

Scenario Outline: 020 Disbursement Entry
	Given I create a hard cost disbursement type with details
		| Code               | Description        | TransactionTypeAlias  |
		| <DisbursementType> | <DisbursementType> | Anticipated Hard Cost |
	Given I post the disbursement
		| Work Date | Disbursement Type  | Work Currency  | Work Amount  | Narrative | Tax Code  |
		| {Today}-1 | <DisbursementType> | <WorkCurrency> | <WorkAmount> | {Auto}+36 | <TaxCode> |
	When I validate the disbursement is posted with no errors

@e2eft
Examples:
	| DisbursementType | WorkCurrency | WorkAmount | TaxCode                    |
	| GoodFood         | GBP          | 500        | UK Output Domestic Zero 0% |

Scenario Outline: 030 Create a time entry/modify
	Given I submit a time modify
		| Time Type  | Hours   | Narrative   | FeeEarnerName   | WorkRate | WorkAmount | WorkCurrency | Tax Code  |
		| <TimeType> | <Hours> | <Narrative> | <FeeEarnerName> | 1        | 1          | <Currency>   | <TaxCode> |
@e2eft
Examples:
	| FeeEarnerName         | TimeType          | Hours | Narrative         | TaxCode                    | Currency            |
	| DentonsAPI FeeEarner3 | Fixed-Capped Fees | 1     | test automation 1 | UK Output Domestic Zero 0% | GBP - British Pound |

@e2eft
Scenario: 035 Proforma generate Bill
	And I can generate the proforma
		| Description | Include Other Proformas | Proforma Status |
		| {Auto}+36   | No                      | Draft           |
	And I cancel the process

Scenario: 040 Create a Proforma Edit and generate Bill
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
	And remove a fee earner '<User>' from the user
@e2eft
Examples:
	| User     |
	| Ann Rose |

@CancelProcess
Scenario Outline: 050 Create a Receipt with Invoice
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
Scenario: 060 Reverse and Reallocate Receipt
	Given I locate and search for a receipt to reverse
	And I perform the reversal
		| ReversalDate | Reason    | Comment          | ReAllocate |
		| {Today}+1    | BILL_PAID | Receipt Reversal | true       |
	And change the operating unit "<OperatingUnit>"
	Then I can submit the reciept allocation
	And I remove the invoice for the receipt
	When add the invoice on the receipt
	And I receipt the total amount
	And update the receipt
	And submit it
	And I validate submit was successful

@e2eft
Examples:
	| OperatingUnit                  |
	| Dentons UK and Middle East LLP |

@CancelProcess @e2eft
Scenario: 070 Verify Receipt is posted
	Given I locate a submitted receipt
	And I can verify that the receipt is reversed

