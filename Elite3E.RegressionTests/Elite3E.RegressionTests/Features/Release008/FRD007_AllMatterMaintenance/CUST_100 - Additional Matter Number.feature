@release8 @frd007 @ValidateAdditionalFields
Feature: Validate additional fields
		Verify Additional Matter Numberfields

Scenario Outline: 001 Create Matter
	Given I create the Payer with Api
		| PayerName   | Entity            |
		| <PayorName> | Linear Associates |
	When I create a matter with details:
		| Client   | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| <Client> | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <ChargeTypeGroup>   | <CostTypeGroup>   | <PayorName> |

@europe
Examples:
	| Client                       | Currency            | CurrencyMethod | Office      | Department | Section | ChargeTypeGroup | CostTypeGroup | PayorName |
	| Client_Automation at_HTOvMOn | GBP - British Pound | Bill           | London (EU) | Default    | Default | Desc_at_3e8glw7 | Auto__18Xeq   | Dana Cook |
@singapore
Examples:
	| Client                       | Currency               | CurrencyMethod | Office    | Department            | Section | ChargeTypeGroup | CostTypeGroup | PayorName |
	| Client_Automation at_HTOvMOn | SGD - Singapore Dollar | Bill           | Singapore | Corporate - Singapore | Default | Desc_at_3e8glw7 | Auto__18Xeq   | Dana Cook |
@uk
Examples:
	| Client                       | Currency            | CurrencyMethod | Office         | Department | Section | ChargeTypeGroup | CostTypeGroup | PayorName |
	| Client_Automation at_HTOvMOn | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Desc_at_3e8glw7 | Auto__18Xeq   | Dana Cook |
@canada
Examples:
	| Client                       | Currency              | CurrencyMethod | Office    | Department | Section | ChargeTypeGroup | CostTypeGroup | PayorName |
	| Client_Automation at_HTOvMOn | CAD - Canadian Dollar | Bill           | Vancouver | Default    | Default | Desc_at_3e8glw7 | Auto__18Xeq   | Dana Cook |
@training @staging
Examples:
	| Client                       | Currency            | CurrencyMethod | Office         | Department | Section | ChargeTypeGroup | CostTypeGroup | PayorName |
	| Client_Automation at_HTOvMOn | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Desc_at_3e8glw7 | Auto__18Xeq   | Dana Cook |
@qa
Examples:
	| Client                       | Currency            | CurrencyMethod | Office         | Department | Section | ChargeTypeGroup | CostTypeGroup | PayorName |
	| Client_Automation at_HTOvMOn | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Desc_at_3e8glw7 | Auto__18Xeq   | Dana Cook |
@ft
Examples:
	| Client                       | Currency            | CurrencyMethod | Office         | Department | Section | ChargeTypeGroup | CostTypeGroup | PayorName |
	| Client_Automation at_HTOvMOn | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Desc_at_3e8glw7 | Auto__18Xeq   | Dana Cook |
@singapore
Examples:
	| Client                       | Currency            | CurrencyMethod | Office    | Department | Section | ChargeTypeGroup | CostTypeGroup | PayorName |
	| Client_Automation at_HTOvMOn | GBP - British Pound | Bill           | Singapore | Default    | Default | Desc_at_3e8glw7 | Auto__18Xeq   | Dana Cook |

@ft @training @staging @canada @europe @uk @singapore @qa
Scenario Outline: 002 Validate additional fields
	Given I navigate to the matter maintenance process
	Then the Additional Fields should exist

Scenario Outline: 003 Validate additional fields values
	Given I navigate to the matter maintenance process
	And I reopen a saved Matter
	When I update the matter with the details:
		| AdditionalMatterNumber   | CostMarkUp   | GrossMarkUp   |
		| <AdditionalMatterNumber> | <CostMarkUp> | <GrossMarkUp> |
	And I submit the form
	Then the values should be saved
	
@ft @training @staging @canada @europe @uk @singapore @qa
Examples:
	| AdditionalMatterNumber | CostMarkUp | GrossMarkUp |
	| {Auto}+7               | 3.5        | 0           |
