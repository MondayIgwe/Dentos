@release1 @frd013 @MatterTaxOverrideMaintenance
Feature: MatterTaxOverrideMaintenance
	Simple calculator for adding two numbers

Scenario Outline: 010 Add Matter Tax Override
	When I add the matter tax override
		| Code     | Description | Default | Active | Start Date | End Date    |
		| {Auto}+5 | {Auto}+10   | Yes     | Yes    | {Today}    | {Today}+100 |
	And tax override details
		| Country   | Tax Area Override |
		| <Country> | <TaxAreaOverride> |
	And I validate submit was successful

@ft @qa
Examples:
	| Country        | TaxAreaOverride |
	| UNITED KINGDOM | Australia       |
@training @staging @europe @uk
Examples:
	| Country        | TaxAreaOverride |
	| UNITED KINGDOM | Australia       |
@singapore
Examples:
	| Country   | TaxAreaOverride |
	| SINGAPORE | Singapore       |
@canada
Examples:
	| Country | TaxAreaOverride |
	| CANADA  | United Kingdom  |
@us
Examples:
	| Country       | TaxAreaOverride |
	| UNITED STATES | United Kingdom  |

 @ft @training @staging @canada @europe @uk @singapore @us @qa
Scenario: 020 Verify override details
	When I search for the tax matter override
	Then the matter tax override details are saved
	And the tax override details are saved

@CancelProcess
Scenario Outline: 030 Cannot Add Duplicate override
	When I try to add duplicate tax override details
		| Country   | Tax Area Override |
		| <Country> | <TaxAreaOverride> |
	Then an error message "Enter a unique Matter Tax Override, Country." is displayed

@ft @qa
Examples:
	| Country        | TaxAreaOverride |
	| UNITED KINGDOM | Canada          |
@training @staging @europe @uk
Examples:
	| Country        | TaxAreaOverride |
	| UNITED KINGDOM | Canada          |
@singapore
Examples:
	| Country   | TaxAreaOverride |
	| SINGAPORE | Singapore       |
@canada
Examples:
	| Country | TaxAreaOverride |
	| CANADA  | Australia       |
@us
Examples:
	| Country       | TaxAreaOverride |
	| UNITED STATES | Australia       |