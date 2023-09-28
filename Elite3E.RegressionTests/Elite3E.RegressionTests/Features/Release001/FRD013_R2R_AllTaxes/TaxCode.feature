@release1 @frd013 @TaxCode
Feature: TaxCode
	Verify TAx Codes

Scenario Outline: 010 Add Tax Code
	Given I add the tax code
		| Code      | Description |
		| {Auto}+10 | {Auto}+15   |
	And add the tax code tax
		| Tax   |
		| <Tax> |
	When I Search the tax code
	Then the tax code is saved

@ft @qa
Examples:
	| Tax                           |
	| AE ICB Input Domestic Zero 0% |
@training @staging @uk
Examples:
	| Tax                      |
	| AE Input Domestic Exempt |
@singapore
Examples:
	| Tax                        |
	| SG Input Domestic Standard |
@europe
Examples:
	| Tax                      |
	| EU Input Conversion Code |
@canada
Examples:
	| Tax                            |
	| CA Input Domestic Standard GST |
@us
Examples:
	| Tax                             |
	| US Input Domestic Standard Rate |

@ft @training @staging @canada @europe @uk @singapore @us @qa
Scenario: 020 Add Tax Code Tool Ref
	Given I add the tax code tool ref
		| Tax Tool Ref |
		| {Auto}+10    |
	When I Search the tax code
	Then the tax code tool ref is saved

	
