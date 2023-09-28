@us
Feature: TaxRate
	Test 1: Creation of new rate set via Rate Type Maintenance 

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
	And I enter a start date
		| Start Date |
		| {Today}    |
	When I create a new rate set
	And I enter new rate amount "<RateAmount>"
	And I select the create new rate button
	And I add a new title and set rate
		| Amount   | Title   | Currency   | Office   |
		| <Amount> | <Title> | <Currency> | <Office> |
	And I submit the form

Examples:
	| RateAmount | Amount | Title          | Currency | Office  |
	| 10         | 200    | Senior Counsel | USD      | Chicago |
