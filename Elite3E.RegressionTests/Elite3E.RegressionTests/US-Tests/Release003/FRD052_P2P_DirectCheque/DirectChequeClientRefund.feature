@us
Feature: DirectCheque_ClientRefund
	A client refund is to be issued, a Direct Cheque request can be successfully submitted and a cheque generated



Scenario Outline: 010 Payee Maintenance
	Given I create a new Payee with the Api
		| PayeeName                   |
		| Payee_Automation at_HTOvMOn |

Scenario Outline: 020 Create a Person Entity
	Given I create a person entity
		| FirstName | LastName  |
		| {Auto}+10 | {Auto}+10 |

Scenario Outline: 030 Create Matter
	Given I create the Payer with Api
		| PayerName   | Entity      |
		| <PayorName> | RallyEntity |
	When I create a matter with details:
		| Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| Fully Open | {Today}-1 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <ChargeTypeGroup>   | <CostTypeGroup>   | <PayorName> |

Examples:
	| Currency        | CurrencyMethod | Office            | Department | Section | ChargeTypeGroup | CostTypeGroup   | PayorName      |
	| USD - US Dollar | Bill           | US Administration | Default    | Default | Desc_at_3e8glw7 | Desc_at_VowqCrR | Francis Little |

Scenario Outline: 040 Enter Receipt Information
	Given I create a receipt type with details:
		| Code   | Description   | Tolerance Amount | Tolerance Percentage |
		| <Code> | <Description> | 50.00            | 50.00                |
	And I add a new receipt
		| Receipt Date |
		| {Today}      |
	And change the operating unit "<OperatingUnit>"
	And I update the receipt
		| Narrative |
		| {Auto}+12 |
	When I add an unallocated child form
		| Unallocated Type  | Receipt Amount | Child Form  |
		| <UnallocatedType> | 5000           | Unallocated |
	And I receipt the total amount
	And I update the receipt
		| Document Number |
		| {Auto}+36       |
	Then the payer is auto populated
	And I can submit the receipt
	And I validate submit was successful


Examples:
	| OperatingUnit   | UnallocatedType | Code      | Description |
	| Dentons US, LLP | Balance Held    | NFCITIUSD | NFCITIUSD   |

Scenario Outline: 050 Check Client Refund Checkbox
	Given I add a new direct cheque
	And I update the client refund
		| Receipt Type  | Document Number | Client Refund |
		| <ReceiptType> | {Auto}+36       | True          |
	When I update the direct cheque
		| Bank Account  | Amount   | Transaction Type  | Office   | Cheque Template | Cheque Printer | ChequeNumber |
		| <BankAccount> | <Amount> | <TransactionType> | <Office> | TE_AP Check     | NO_PRINTER     | {Auto}+7     |
	And I verify client refund child form
		| Amount   | ClientRefundChildForm | Office   |
		| <Amount> | Client Refund         | <Office> |
	And change the operating unit "<OperatingUnit>"
	Then I can submit the direct cheque

Examples:
	| OperatingUnit   | ReceiptType | ClientRefund | BankAccount                      | Amount | TransactionType | Office  |
	| Dentons US, LLP | NFCITIUSD   | True         | Citibank MO IOLTA - (6315) | 5000   | Credit Note     | Chicago |
