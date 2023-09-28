@release2 @frd046 @AddTransactionType
Feature: AddTransactionType

Scenario Outline: Add a new Anticipated Transaction Type
	Given I add the transaction type
		| Transaction Type | Description | Currency Type  | Anticipated   |
		| {Auto}+6         | {Auto}+36   | <CurrencyType> | <Anticipated> |
	And add the transaction type standard postings
		| GL Type  | Revenue Recognition  | AR   |
		| <GLType> | <RevenueRecognition> | <AR> |
	Then I validate submit was successful
	When I search for the saved transaction type
	Then the transaction type is saved

@ft @qa
Examples:
	| CurrencyType                  | Anticipated | GLType                                   | RevenueRecognition | AR                                                             |
	| Daily - European Central Bank | Yes         | Local Adj - Dentons UK & Middle East LLP | Bill posting       | BTkWMBill-203360  -Dflt    -0000    -0000    -BTkWMBill-000000 |

@training @canada @europe @uk @singapore
Examples:
	| CurrencyType                  | Anticipated | GLType                                   | RevenueRecognition | AR                                     |
	| Daily - European Central Bank | Yes         | Local Adj - Dentons UK & Middle East LLP | Bill posting       | 3000-757010-1000-0000-0000-8054-000000 |
@staging
Examples:
	| CurrencyType                  | Anticipated | GLType                                             | RevenueRecognition | AR                                                           |
	| Daily - European Central Bank | Yes         | Local Dentons Global Services (UK) Holding Limited | Bill posting       | 3000    -757010  -1000    -0000    -0000    -8054    -000000 |
