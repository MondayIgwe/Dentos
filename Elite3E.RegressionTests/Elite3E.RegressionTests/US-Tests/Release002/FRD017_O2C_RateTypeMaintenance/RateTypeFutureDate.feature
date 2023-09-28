@us
Feature: RateTypeFutureDate
	Test 2: Creation of new rate set creation via Rate Type Maintenance with a future date

Scenario Outline: 010 Rate Type Maintenance
	When I add a new rate type
		| Code     | Description | Default Currency  |
		| {Auto}+7 | {Auto}+36   | <DefaultCurrency> |
	And add the effective dates
		| Start Date |
		| {Today}    |
	And add rate type details
		| Amount   | Currency   | Title   | Office   |
		| <Amount> | <Currency> | <Title> | <Office> |
	Then I submit the form

Examples:
	| DefaultCurrency | Amount | Currency | Title                       | Office  |
	| USD             | 500    | USD      | US Administrative Assistant | Chicago |

Scenario Outline: 020_Create New Rate Amount
	Given I search for a rate type
	And I enter a future start date
		| Future Start Date |
		| {Today}+15        |
	When I create a new rate set
	And I enter new rate amount "<RateAmount>"
	Then I select the create new rate button
	And I submit the form

Examples:
	| RateAmount |
	| 10         |

