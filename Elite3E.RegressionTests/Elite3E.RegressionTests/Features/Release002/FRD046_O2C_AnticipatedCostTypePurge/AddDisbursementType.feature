@release2 @frd046 @AddDisbursementType
Feature: AddDisbursementType

Scenario Outline: 010 Add a new Anticipated Transaction Type
	Given I add the transaction type
		| Transaction Type | Description | Currency Type  | Anticipated   |
		| {Auto}+6         | {Auto}+36   | <CurrencyType> | <Anticipated> |
	And add the transaction type standard postings
		| GL Type  | Revenue Recognition  | AR   |
		| <GLType> | <RevenueRecognition> | <AR> |

@ft @qa
Examples:
	| CurrencyType                  | Anticipated | GLType                                   | RevenueRecognition | AR                                               |
	| Daily - European Central Bank | Yes         | Local Adj - Dentons UK & Middle East LLP | Bill posting       | BTkWMBill-203360-Dflt-0000-0000-BTkWMBill-000000 |
@training @staging  @canada @europe @uk @singapore  
Examples:
	| CurrencyType                  | Anticipated | GLType                                   | RevenueRecognition | AR                                               |
	| Daily - European Central Bank | Yes         | Local Adj - Dentons UK & Middle East LLP | Bill posting       | BTkWMBill-203360-Dflt-0000-0000-BTkWMBill-000000 |

Scenario Outline: 020 Add a Disbursement Type
	When I add the disbursement
		| Code     | Description | Hard Disbursement  |
		| {Auto}+6 | {Auto}+36   | <HardDisbursement> |
	Then "Transaction Type" is mandatory

@ft @qa
Examples:
	| HardDisbursement |
	| Yes              |
@training @staging  @canada @europe @uk @singapore  
Examples:
	| HardDisbursement |
	| Yes              |
	
Scenario: 030 Add a Transaction Type
	When I add the transaction type to the disbursement
		| TransactionType   |
		| <TransactionType> |
	Then the disbursement type is saved
@ft
Examples:
	| TransactionType       |
	| Anticipated Hard Cost |
@qa @training @staging  @canada @europe @uk @singapore  
Examples:
	| TransactionType       |
	| Anticipated Hard Cost |
