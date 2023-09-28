@us
Feature: Validate additional fields
		Verify Additional Matter Numberfields

Scenario Outline: 001 Create Matter
	Given I create the Payer with Api
		| PayerName   | Entity            |
		| <PayorName> | Linear Associates |
	When I create a matter with details:
		| Client   | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| <Client> | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <ChargeTypeGroup>   | <CostTypeGroup>   | <PayorName> |

Examples:
	| Client                       | Currency | CurrencyMethod | Office  | Department | Section | ChargeTypeGroup | CostTypeGroup | PayorName |
	| Client_Automation at_HTOvMOn | USD      | Bill           | Chicago | Default    | Default | Desc_at_3e8glw7 | Auto__18Xeq   | Dana Ross |

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
	
Examples:
	| AdditionalMatterNumber | CostMarkUp | GrossMarkUp |
	| {Auto}+7               | 3.5        | 0           |
