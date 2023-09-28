@release2 @frd017 @EffectiveDateRateReport
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

@ft @qa
Examples:
	| DefaultCurrency | Amount | Currency | Title              | Office    |
	| AED             | 500    | AED      | Associate (9 year) | Barcelona |
@training @staging @europe @uk
Examples:
	| DefaultCurrency | Amount | Currency | Title              | Office    |
	| AED             | 500    | AED      | Associate (9 year) | Barcelona |
@singapore
Examples:
	| DefaultCurrency | Amount | Currency | Title        | Office    |
	| SGD             | 500    | SGD      | MM Associate | Singapore |

@canada
Examples:
	| DefaultCurrency | Amount | Currency | Title                 | Office   |
	| CAD             | 500    | CAD      | CA Accounting Analyst | Montreal |

@ft @training @staging @canada @europe @uk @singapore  @qa
Scenario: 020_Rate Type Maintenance
	Given I search for a rate type
	When I select effective dated rates
	Then I can run rates report
