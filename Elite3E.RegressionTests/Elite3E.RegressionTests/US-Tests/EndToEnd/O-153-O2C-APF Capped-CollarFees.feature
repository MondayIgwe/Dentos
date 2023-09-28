@us
Feature: O-153-O2C-APF Capped-CollarFees
Skipped 3EP steps
https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/82223

Scenario Outline: 010 Data Preparation
	Given I create a user with details
		| UserName | DataRoleAlias | UserRoleList                                                                               |
		| <User>   | Admin         | 0:AD:G:Common Authorisations,0:WC:P:Proforma Processor,0:WC:P:Proforma Processor - Finance |
	And I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	And I create the Payer with Api
		| PayerName   | Entity        |
		| <PayorName> | EntityDigital |
	And I add a workflow user to a FeeEarner
		| User   | Name            |
		| <User> | <FeeEarnerName> |
	When I create a matter with details:
        | Client   | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department | Section | ChargeTypeGroupName | CostTypeGroupName | Rate     | PayorName   |
        | <Client> | <FeeEarnerName>   | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | Bill                 | <Office> | Default    | Default | Desc_at_3e8glw7     | Desc_at_3e8glw7   | Standard | <PayorName> |

Examples:
	| User     | Client         | FeeEarnerName | Currency        | Office  | PayorName  |
	| Jim Beam | JimBeam Client | Jim Beam      | USD - US Dollar | Chicago | O153 Payer |


Scenario Outline: 020 Create a Time Modify and Submit
	When I submit a time modify
		| Time Type  | Narrative | FeeEarnerName   | WorkRate   | WorkCurrency | WorkAmount   | Hours   |
		| <TimeType> | {Auto}+10 | <FeeEarnerName> | <WorkRate> | <Currency>   | <WorkAmount> | <Hours> |

Examples:
	| FeeEarnerName | TimeType | Currency        | WorkRate | WorkAmount | Hours |
	| Jim Beam      | FEES     | USD - US Dollar | 7500     | 7500       | 1.00  |


Scenario Outline: 030 Create a Disbursement Modify and Submit
	When I submit the disbursement modify
		| Work Date | Disbursement Type  | Work Currency  | Work Amount  | Narrative | Tax Code  | Purge Type  |
		| {Today}-1 | <DisbursementType> | <WorkCurrency> | <WorkAmount> | {Auto}+36 | <TaxCode> | <PurgeType> |

Examples:
	| DisbursementType | WorkCurrency | WorkAmount | TaxCode                          | PurgeType      |
	| Official Fee     | USD          | 5000       | US Output Domestic Standard Rate | Billable Purge |

Scenario Outline: 040 Create a Voucher from Voucher Maintenance
	Given I create a new Payee with the Api
		| PayeeName |
		| US_013    |
	And I add a new voucher with voucher default information
		| Operation Unit  | Invoice Number | Invoice Date |
		| <OperationUnit> | v_             | {Today}      |
	And I add disbursement card details for voucher
		| Disbursement Type  | Narrative             | TaxCode   | InputTaxCode   | Voucher Amount |
		| <DisbursementType> | US Automation Testing | <TaxCode> | <InputTaxCode> | 5000           |
	And I update it
	And I add input amount "<InputAmount>" in voucher tax card
	When I submit the voucher
	Then I verify the voucher is created


Examples:
	| OperationUnit              | DisbursementType | TaxCode                          | InputTaxCode                    | InputAmount |
	| Dentons United States, LLP | Official Fee     | US Output Domestic Standard Rate | US Input Domestic Standard Rate | 0           |


Scenario: 050 Generate a Proforma
	Given I can generate the proforma
		| Description | Include Other Proformas | Invoice Date | Proforma Status |
		| {Auto}+36   | No                      | {Today}-1    | Draft           |


Scenario Outline: 060 Update matter bill to email in Matter Maintenance
	Given I navigate to the matter maintenance process
	And I reopen a saved Matter
	When I update EmailBillToField as 'test@dentons.us'
	And I update it
	Then I can submit the matter
	
@CancelProcess
Scenario Outline: 070 Perform proforma workflow and apply adjustments
	Given I proxy as user '<User>'
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	And I open the proforma for submission
	When I add apply adjustment details
		| Percentage | Adjustment Method                                 |
		| -2         | Proportional by soft cost entries on the proforma |
	When I add apply adjustment details
		| Amount | Adjustment Method                   | AdjustmentType |
		| 30000  | Proportional by billed and unbilled | Capped Fee     |
	Then I submit it
	And I cancel proxy
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	And I open the proforma for billing
	And I bill it without printing
	And the invoice number is generated
	And I search for 'Workflow Dashboard'
	And I open the Invoice Dispatch workflow task
	And I send the invoice routed to finance team when dispatch method not set
	When I view the invoices

Examples:
	| User     |
	| Jim Beam |

Scenario Outline: 080 Create a Receipt with Invoice
	When I add a new receipt
		| Receipt Type  | Receipt Date | Document Number | Narrative |
		| <ReceiptType> | {Today}      | {Auto}+36       | {Auto}+36 |
	And change the operating unit "<OperatingUnit>"
	And add the invoice on the receipt
	And I receipt the total amount
	And update the receipt
	Then I can submit the receipt
	And I validate submit was successful


Examples:
	| ReceiptType | OperatingUnit   |
	| DUSHIUSD    | Dentons US, LLP |