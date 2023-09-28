@release1 @frd013 @AddTaxOverrideToMatter
Feature: Add TaxOverrideToMatter

Scenario: 010 Add Matter Tax Override
	When I add the matter tax override
		| Code      | Description | Active | Start Date | End Date     |
		| {Auto}+12 | {Auto}+20   | Yes    | {Today}    | {Today}+1000 |
	And tax override details
		| Country   | Tax Area Override |
		| <Country> | <TaxAreaOverride> |
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
	| CANADA  | Australia       |
@us
Examples:
	| Country       | TaxAreaOverride |
	| UNITED STATES | Australia       |
			
Scenario Outline: 020 Create a Section
	Given I search or create a section
		| Code          | Description   | GlSection   |
		| <SectionCode> | <Description> | <GLSection> |

@training @staging @canada @europe @uk @singapore @us @ft @qa
Examples:
	| SectionCode     | Description               | GLSection |
	| Automation Test | This ia a automation test | 0000      |


Scenario Outline: 030 Verify override details
	Given I create the Payer with Api
		| PayerName   | Entity         |
		| <PayerName> | BlueMoonEntity |
	When I create a matter with details:
		| Client   | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| <Client> | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <ChargeTypeGroup>   | <CostTypeGroup>   | <PayerName> |
	When I search for the matter
	Then I can add the tax override

@ft @training @staging @uk @qa
Examples:
	| Client                          | Currency            | CurrencyMethod | Office         | Department | Section | ChargeTypeGroup | CostTypeGroup   | PayerName   |
	| Client_Automation DentonsGlobal | GBP - British Pound | Bill           | London (UKIME) | Default    | default | Desc_at_3e8glw7 | Desc_at_VowqCrR | Noe Preston |
@singapore
Examples:
	| Client                          | Currency               | CurrencyMethod | Office    | Department | Section | ChargeTypeGroup | CostTypeGroup   | PayerName   |
	| Client_Automation DentonsGlobal | SGD - Singapore Dollar | Bill           | Singapore | Default    | default | Desc_at_3e8glw7 | Desc_at_VowqCrR | Noe Preston |
@europe
Examples:
	| Client                          | Currency            | CurrencyMethod | Office      | Department | Section                   | ChargeTypeGroup | CostTypeGroup   | PayerName   |
	| Client_Automation DentonsGlobal | GBP - British Pound | Bill           | London (EU) | Default    | This ia a automation test | Desc_at_3e8glw7 | Desc_at_VowqCrR | Noe Preston |
@canada
Examples:
	| Client                          | Currency              | CurrencyMethod | Office   | Department | Section | ChargeTypeGroup | CostTypeGroup   | PayerName   |
	| Client_Automation DentonsGlobal | CAD - Canadian Dollar | Bill           | Montreal | Default    | default | Desc_at_3e8glw7 | Desc_at_VowqCrR | Noe Preston |
@us
Examples:
	| Client                          | Currency        | CurrencyMethod | Office  | Department | Section | ChargeTypeGroup | CostTypeGroup   | PayerName   |
	| Client_Automation DentonsGlobal | USD - US Dollar | Bill           | Chicago | Default    | default | Desc_at_3e8glw7 | Desc_at_VowqCrR | Noe Preston |

@ft @training @staging @canada @europe @uk @singapore @us @qa @PipelineFailTest 
Scenario: 040 Search override details
	When I search for the matter
	Then the matter tax override is saved for the matter
	And I can save the matter without the matter tax override