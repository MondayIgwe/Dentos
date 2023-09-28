@us
Feature: FutureRateInd
	Test 3: Creation of new rate set creation via Rate Type Maintenance to confirm Future rate indicator

Scenario Outline: 010 Rate Type Maintenance
	When I add a new rate type
		| Code     | Description | Default Currency  |
		| {Auto}+7 | {Auto}+36   | <DefaultCurrency> |
	And add the effective dates
		| Start Date |
		| {Today}    |
	And add rate type details
		| Amount    | Currency   | Title   | Office   |
		| <Amount1> | <Currency> | <Title> | <Office> |
	And add the effective dates
		| Start Date |
		| {Today}+26 |
	And add rate type details
		| Amount    | Currency   | Title   | Office   |
		| <Amount2> | <Currency> | <Title> | <Office> |
	Then I submit the form
	
Examples:
	| DefaultCurrency | Amount1 | Amount2 | Currency | Title                       | Office  |
	| USD             | 500     | 200     | USD      | US Administrative Assistant | Chicago |


Scenario Outline: 020_Create New Rate Amount
	Given I search for a rate type
	And I enter a start date
		| Start Date |
		| {Today}    |
	When I create a new rate set
	And I select future
	Then I submit the form
