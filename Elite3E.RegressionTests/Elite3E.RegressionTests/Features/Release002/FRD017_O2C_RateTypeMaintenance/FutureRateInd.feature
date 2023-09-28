@release2 @frd017 @FutureRateInd
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
	
@ft @qa
Examples:
	| DefaultCurrency | Amount1 | Amount2 | Currency | Title              | Office    |
	| AED             | 500     | 200     | AED      | Associate (9 year) | Barcelona |
@singapore
Examples:
	| DefaultCurrency | Amount1 | Amount2 | Currency | Title        | Office    |
	| SGD             | 500     | 200     | SGD      | MM Associate | Singapore |
@training @staging @europe @uk
Examples:
	| DefaultCurrency | Amount1 | Amount2 | Currency | Title              | Office    |
	| AED             | 500     | 200     | AED      | Associate (9 year) | Barcelona |
@canada
Examples:
	| DefaultCurrency | Amount1 | Amount2 | Currency | Title                 | Office   |
	| CAD             | 500     | 200     | CAD      | CA Associate (1 year) | Montreal |

@ft @training @staging @canada @europe @uk @singapore  @qa
Scenario Outline: 020_Create New Rate Amount
	Given I search for a rate type
	And I enter a start date
		| Start Date |
		| {Today}    |
	When I create a new rate set
	And I select future
	Then I submit the form
