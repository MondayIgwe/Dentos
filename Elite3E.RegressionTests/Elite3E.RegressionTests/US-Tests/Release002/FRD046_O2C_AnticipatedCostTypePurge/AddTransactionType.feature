@us
Feature: AddTransactionType

Scenario Outline: Add a new Anticipated Transaction Type
	Given I add the transaction type
		| Transaction Type | Description | Currency Type  | Anticipated   |
		| {Auto}+6         | {Auto}+36   | <CurrencyType> | <Anticipated> |
	And add the transaction type standard postings
		| GL Type  | Revenue Recognition  | AR   |
		| <GLType> | <RevenueRecognition> | <AR> |
	When I search for the saved transaction type
	Then the transaction type is saved

Examples:
	| CurrencyType       | Anticipated | GLType                           | RevenueRecognition | AR                                     |
	| Daily Rate Xe - US | Yes         | Local Dentons United States, LLP | Bill posting       | 5000-801010-1000-0000-0000-8025-000000 |
