@release3 @frd052 @DirectCheque_NoClientRefund
Feature: DirectCheque_NoClientRefund
	In the Direct Cheque process, a record cannot be submitted unless IsClientRefund is True.

@ft @training @staging @canada @europe @uk @singapore   @qa
Scenario: 010 _Payee Maintenance
	Given I create a new Payee with the Api
		| PayeeName |
		| {Auto}+12 |

@training @staging @canada @europe @uk @singapore   @ft @qa
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


Scenario Outline: 040 Client Refund Checkbox False
	Given I add a new direct cheque
	And I update the client refund
		| Client Refund  | Receipt Type  | Document Number |
		| <ClientRefund> | <ReceiptType> | {Auto}+36       |
	When I update the direct cheque
		| Bank Account  | Amount   | Transaction Type  | Office   | Cheque Template  | Cheque Printer  |
		| <BankAccount> | <Amount> | <TransactionType> | <Office> | <ChequeTemplate> | <ChequePrinter> |
	Then update the direct cheque
	And the mandatory error messages are displayed
		| Mandatory Field |
		| Client          |
		| Receipt Type    |
		| Document Number |

@ft @qa
Examples:
	| ClientRefund | BankAccount                     | Amount | TransactionType | Office   | ChequeTemplate | ChequePrinter | ReceiptType   |
	| False        | BMO AH Digital Cheque - EDM-AH2 | 5000   | Credit Note     | Aberdeen | TE_AP Check    | NO_PRINTER    | AUSYDANZ01AUD |
@canada
Examples:
	| ClientRefund | BankAccount                        | Amount | TransactionType | Office    | ChequeTemplate | ChequePrinter | ReceiptType                           |
	| False        | CY - TD CT - Law Practice TDCY CAD | 5000   | Credit Note     | Vancouver | TE_AP Check    | NO_PRINTER    | Calgary - BMO - Operating Acc 1 - CAD |
@uk
Examples:
	| ClientRefund | BankAccount                     | Amount | TransactionType | Office   | ChequeTemplate | ChequePrinter | ReceiptType   |
	| False        | BMO AH Digital Cheque - EDM-AH2 | 5000   | Credit Note     | Aberdeen | TE_AP Check    | NO_PRINTER    | AUSYDANZ01AUD |
@europe
Examples:
	| ClientRefund | BankAccount                                       | Amount | TransactionType | Office      | ChequeTemplate | ChequePrinter | ReceiptType |
	| False        | Dentons EU LLP - Barclays - Operating Acc 1 - EUR | 5000   | Credit Note     | London (EU) | TE_AP Check    | NO_PRINTER    | PAY_3501EUR |
@singapore
Examples:
	| ClientRefund | BankAccount                             | Amount | TransactionType | Office    | ChequeTemplate | ChequePrinter | ReceiptType     |
	| False        | ICB - 1201 - Singapore - ICB Bank - SGD | 5000   | Credit Note     | Singapore | TE_AP Check    | NO_PRINTER    | CTO-SCBONO2-SGD |

@training
Examples:
	| ClientRefund | BankAccount                        | Amount | TransactionType | Office  | ChequeTemplate | ChequePrinter | ReceiptType  |
	| False        | IGB - 3000 - UKME - IGB Bank - GBP | 5000   | Credit Note     | Default | TE_AP Check    | NO_PRINTER    | IGB_3000_GBP |
@staging
Examples:
	| ClientRefund | BankAccount       | Amount | TransactionType | Office  | ChequeTemplate | ChequePrinter | ReceiptType   |
	| False        | Barclays BARCLAYS | 5000   | Credit Note     | Default | TE_AP Check    | NO_PRINTER    | AUSYDANZ01AUD |