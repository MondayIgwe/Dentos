@smoke @Search
Feature: Search

Scenario: 010 Search for a matter
	Given I create the Payer with Api
		| PayerName   | Entity        |
		| <PayorName> | SeasonsEntity |
	And I create a matter with details:
		| Client   | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | Rate   | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| <Client> | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <Rate> | <ChargeTypeGroup>   | <CostTypeGroup>   | <PayorName> |
	When I search for the matter
	Then verify matter is returned

@ft @training @staging @europe @uk @qa
Examples:
	| Client                       | Currency            | CurrencyMethod | Office  | Department | Section | Rate     | ChargeTypeGroup | CostTypeGroup   | PayorName  |
	| Client_Automation at_HTOvMOn | GBP - British Pound | Bill           | Default | Default    | Default | Standard | Desc_at_3e8glw7 | Desc_at_VowqCrR | Dave Peter |
@canada
Examples:
	| Client                       | Currency              | CurrencyMethod | Office    | Department | Section | Rate     | ChargeTypeGroup | CostTypeGroup   | PayorName  |
	| Client_Automation at_HTOvMOn | CAD - Canadian Dollar | Bill           | Vancouver | Default    | Default | Standard | Desc_at_3e8glw7 | Desc_at_VowqCrR | Dave Peter |
@singapore
Examples:
	| Client                       | Currency               | CurrencyMethod | Office    | Department            | Section | Rate     | ChargeTypeGroup | CostTypeGroup   | PayorName  |
	| Client_Automation at_HTOvMOn | SGD - Singapore Dollar | Bill           | Singapore | Corporate - Singapore | Default | Standard | Desc_at_3e8glw7 | Desc_at_VowqCrR | Dave Peter |
