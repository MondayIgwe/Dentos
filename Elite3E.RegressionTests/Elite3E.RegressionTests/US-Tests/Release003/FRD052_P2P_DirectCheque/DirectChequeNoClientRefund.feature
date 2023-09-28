@us
Feature: DirectCheque_NoClientRefund
	In the Direct Cheque process, a record cannot be submitted unless IsClientRefund is True.

Scenario: 010 _Payee Maintenance
	Given I create a new Payee with the Api
		| PayeeName |
		| {Auto}+12 |

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

Examples:
	| ClientRefund | BankAccount                                                 | Amount | TransactionType | Office  | ChequeTemplate | ChequePrinter | ReceiptType |
	| False        | Dentons United States, LLP - Application Off-Set Bank - USD | 5000   | Credit Note     | Chicago | TE_AP Check    | NO_PRINTER    | NFCITIUSD   |

