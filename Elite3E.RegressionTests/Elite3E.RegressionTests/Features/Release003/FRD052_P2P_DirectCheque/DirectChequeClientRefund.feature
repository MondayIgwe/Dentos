@release3 @frd052 @DirectCheque_ClientRefund
Feature: DirectCheque_ClientRefund
	A client refund is to be issued, a Direct Cheque request can be successfully submitted and a cheque generated



@ft @training @staging @canada @europe @uk @singapore @qa
Scenario Outline: 010 Payee Maintenance
	Given I create a new Payee with the Api
		| PayeeName                   |
		| Payee_Automation at_HTOvMOn |

@training @staging @canada @europe @uk @singapore @ft @qa
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
@ft @qa
Examples:
	| Currency            | CurrencyMethod | Office         | Department | Section | ChargeTypeGroup | CostTypeGroup   | PayorName      |
	| GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Desc_at_3e8glw7 | Desc_at_VowqCrR | Francis Little |
@training @staging
Examples:
	| Currency            | CurrencyMethod | Office         | Department | Section | ChargeTypeGroup | CostTypeGroup | PayorName      |
	| GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Desc_at_3e8glw7 | Auto__18Xeq   | Francis Little |
@canada
Examples:
	| Currency              | CurrencyMethod | Office    | Department          | Section           | ChargeTypeGroup | CostTypeGroup   | PayorName      |
	| CAD - Canadian Dollar | Bill           | Vancouver | Banking and Finance | Banking & Finance | Desc_at_3e8glw7 | Desc_at_VowqCrR | Francis Little |
@europe
Examples:
	| Currency   | CurrencyMethod | Office      | Department | Section | ChargeTypeGroup | CostTypeGroup   | PayorName      |
	| EUR - Euro | Bill           | London (EU) | Default    | Default | Desc_at_3e8glw7 | Desc_at_VowqCrR | Francis Little |
@uk
Examples:
	| Currency            | CurrencyMethod | Office         | Department | Section | ChargeTypeGroup | CostTypeGroup   | PayorName      |
	| GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Desc_at_3e8glw7 | Desc_at_VowqCrR | Francis Little |
@singapore
Examples:
	| Currency               | CurrencyMethod | Office    | Department | Section | ChargeTypeGroup | CostTypeGroup   | PayorName      |
	| SGD - Singapore Dollar | Bill           | Singapore | Default    | Default | Desc_at_3e8glw7 | Desc_at_VowqCrR | Francis Little |


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

	
@ft
Examples:
	| OperatingUnit                  | UnallocatedType           | Code | Description |
	| Dentons UK and Middle East LLP | Retainer - Adv Fees/Costs | CASH | CASH        |
@qa
Examples:
	| OperatingUnit                  | UnallocatedType           | Code         | Description |
	| Dentons UK and Middle East LLP | Retainer - Adv Fees/Costs | IGB_3000_GBP | CASH        |
@training
Examples:
	| OperatingUnit                  | UnallocatedType     | Code         | Description                        |
	| Dentons UK and Middle East LLP | Retainer - Adv Fees | IGB_3000_GBP | IGB - 3000 - UKME - IGB Bank - GBP |
@staging
Examples:
	| OperatingUnit           | UnallocatedType      | Code          | Description   |
	| Dentons Europe LLP (UK) | Retainer - Adv Costs | AUSYDANZ01AUD | AUSYDANZ01AUD |
@canada
Examples:
	| OperatingUnit      | UnallocatedType | Code   | Description                      |
	| Dentons Canada LLP | Unallocated     | CRC-01 | Credit Card / Interactions - CAD |
@uk
Examples:
	| OperatingUnit                  | UnallocatedType | Code         | Description                        |
	| Dentons UK and Middle East LLP | Balance Held    | IGB_3000_GBP | IGB - 3000 - UKME - IGB Bank - GBP |
@europe
Examples:
	| OperatingUnit           | UnallocatedType | Code        | Description                    |
	| Dentons Europe LLP (UK) | Balance Held    | PAY_3501EUR | IVB Dentons Europe LLP (UK EUR |
@singapore
Examples:
	| OperatingUnit                | UnallocatedType | Code        | Description |
	| Dentons Rodyk & Davidson LLP | Balance Held    | SGPRMBLYHKD | SGPRMBLYHKD |


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

@ft
Examples:
	| OperatingUnit                  | ReceiptType | ClientRefund | BankAccount                        | Amount | TransactionType | Office         |
	| Dentons UK and Middle East LLP | CASH        | True         | London UKME - HSBC Off 1 Acc - GBP | 5000   | Credit Note     | London (UKIME) |
@qa
Examples:
	| OperatingUnit                  | ReceiptType  | ClientRefund | BankAccount                        | Amount | TransactionType | Office         |
	| Dentons UK and Middle East LLP | IGB_3000_GBP | True         | London UKME - HSBC Off 1 Acc - GBP | 5000   | Credit Note     | London (UKIME) |
@training
Examples:
	| OperatingUnit                  | ReceiptType  | ClientRefund | BankAccount       | Amount | TransactionType | Office         |
	| Dentons UK and Middle East LLP | PAY_BBLOBGBP | True         | Barclays BARCLAYS | 5000   | Credit Note     | London (UKIME) |
@staging
Examples:
	| OperatingUnit                  | ReceiptType   | ClientRefund | BankAccount | Amount | TransactionType | Office         |
	| Dentons UK and Middle East LLP | AUSYDANZ01AUD | True         | UKIME CONV  | 5000   | Credit Note     | London (UKIME) |
@canada
Examples:
	| OperatingUnit      | ReceiptType                      | ClientRefund | BankAccount                        | Amount | TransactionType | Office    |
	| Dentons Canada LLP | Credit Card / Interactions - CAD | True         | CY - TD CT - Law Practice TDCY CAD | 5000   | Credit Note     | Vancouver |
@europe
Examples:
	| OperatingUnit           | ReceiptType | ClientRefund | BankAccount                                       | Amount | TransactionType | Office      |
	| Dentons Europe LLP (UK) | PAY_3501EUR | True         | Dentons EU LLP - Barclays - Operating Acc 1 - EUR | 5000   | Credit Note     | London (EU) |
@uk
Examples:
	| OperatingUnit                  | ReceiptType  | ClientRefund | BankAccount  | Amount | TransactionType | Office         |
	| Dentons UK and Middle East LLP | IGB_3000_GBP | True         | IGB_3000_GBP | 5000   | Credit Note     | London (UKIME) |
@singapore
Examples:
	| OperatingUnit                | ReceiptType | ClientRefund | BankAccount                       | Amount | TransactionType | Office    |
	| Dentons Rodyk & Davidson LLP | SGPRMBLYHKD | True         | Singapore -Petty Cash-RMB-L&Y-HKD | 5000   | Credit Note     | Singapore |
