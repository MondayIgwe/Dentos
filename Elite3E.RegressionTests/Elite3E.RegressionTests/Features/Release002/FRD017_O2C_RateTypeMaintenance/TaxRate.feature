@release2 @frd017 @TaxRate
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
	| CAD             | 500    | CAD      | CA Associate (1 year) | Montreal |

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

@ft @qa
Examples:
	| RateAmount | Amount | Title          | Currency | Office    |
	| 10         | 200    | Senior Counsel | AED      | Barcelona |
@training @staging @europe @uk
Examples:
	| RateAmount | Amount | Title          | Currency | Office    |
	| 10         | 200    | Senior Counsel | AED      | Barcelona |
@singapore
Examples:
	| RateAmount | Amount | Title               | Currency | Office    |
	| 10         | 200    | MM Senior Associate | SGD      | Singapore |

@canada
Examples:
	| RateAmount | Amount | Title          | Currency | Office   |
	| 10         | 200    | Senior Counsel | CAD      | Montreal |