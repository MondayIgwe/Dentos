Feature: OfficeToClientReversal

A short summary of the feature
https://dev.azure.com/dentonsglobal/GFT%203E/_testPlans/execute?view=_TestManagement&planId=12845&suiteId=38476


@CancelProcess @e2eft
Scenario: 001 Create Payee
	Given I create a new Payee with the Api
		| PayeeName              |
		| Payee_CashRcptReversal |

@CancelProcess
Scenario: 010 Prepare data for Receipt/Reversal
	Given I create a user with details
		| UserName | DataRoleAlias | DefaultOperatingAlias   | UserRoleList          |
		| <User>   | Admin         | <DefaultOperatingAlias> | DEFAULT_WORKFLOW_ROLE |
	And I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	And add a user '<User>' fee earner
	Given the charge type group exists
		| Code        | Description       | ChargeTypeGroupExcludeOrIncludeList |
		| SG_BILLABLE | <ChargeTypeGroup> | IsIncludeList                       |
	And the below charge type added to the group
		| Code            | Description  | TransactionType | Category          |
		| Withholding Tax | <ChargeType> | BOA Fees        | Billed on Account |
	And I create the Payer with Api
		| PayerName   | Entity        |
		| <PayorName> | LimeLight Ltd |
	And I create a matter with details:
		| Client   | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department | Section | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| <Client> | <FeeEarnerName>   | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | Bill                 | <Office> | Default    | Default | Desc_at_3e8glw7     | Desc_at_3e8glw7   | <PayorName> |
@e2eft
Examples:
	| User     | Client        | FeeEarnerName     | Office         | ChargeType    | TaxCode                    | ChargeAmount | ChargeTypeGroup | Currency            | PayorName | DefaultOperatingAlias          |
	| Ann Rose | Doctor Client | Doctor FeeEarner1 | London (UKIME) | Miscellaneous | UK Output Domestic Zero 0% | 500.00       | at_BOA_Group    | GBP - British Pound | Lara Moon | Dentons UK and Middle East LLP |

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
	| GoodFood         | GBP          | 4999       | UK Output Domestic Zero 0% |

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
	#And change the operating unit "<OperatingUnit>"
	And add the invoice on the receipt
	And I receipt the total amount
	And update the receipt
	Then I can submit the receipt
	And I validate submit was successful

@e2eft
Examples:
	| ReceiptType | OperatingUnit                  |
	| CASH        | Dentons UK and Middle East LLP |

@CancelProcess @e2eft
Scenario: 060 Reverse and Reallocate Receipt
	Given I search for process 'Receipts - Apply / Reverse Payments'
	When I select existing receipt
	Then I perform the reversal
		| ReversalDate | Reason    | Comment          | ReAllocate |
		| {Today}+1    | BILL_PAID | Receipt Reversal | true       |
	#And change the operating unit "<OperatingUnit>"
	Then I can submit the reciept allocation
	And I remove the invoice for the receipt
	When allocate the new reciept
		| Unallocated Type  | Receipt Amount  | Operating Unit  | Narrative |
		| <UnallocatedType> | <ReceiptAmount> | <OperatingUnit> |           |
	And I update it
	And I submit it

	And I validate submit was successful
@e2eft
Examples:
	| ReceiptAmount | UnallocatedType | OperatingUnit                  |
	| 5000          | Unidentified    | Dentons UK and Middle East LLP |
