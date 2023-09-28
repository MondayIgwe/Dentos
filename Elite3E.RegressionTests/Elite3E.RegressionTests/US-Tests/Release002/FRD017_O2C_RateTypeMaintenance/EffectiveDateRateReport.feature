@us
Feature: EffectiveDateRateReport
	Test 4: The custom report for effective dated rates can be opened via the buttom added to the child form via Rate Type Maintenance. 

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


Scenario: 020_Rate Type Maintenance
	Given I search for a rate type
	When I select effective dated rates
	Then I can run rates report
