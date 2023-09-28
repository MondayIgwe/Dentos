@smoke @TimeEntry
Feature: TimeEntry

Scenario Outline: 010 Create Matter
	Given I create the Payer with Api
		| PayerName   | Entity        |
		| <PayorName> | SeasonsEntity |
	When I create a matter with details:
		| Client   | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | Rate   | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| <Client> | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <Rate> | <ChargeTypeGroup>   | <CostTypeGroup>   | <PayorName> |

@ft @training @staging @uk @qa
Examples:
	| Client                       | Currency            | CurrencyMethod | Office         | Department | Section | Rate     | ChargeTypeGroup | CostTypeGroup   | PayorName  |
	| Client_Automation at_HTOvMOn | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Standard | Desc_at_3e8glw7 | Desc_at_VowqCrR | Dave Peter |
@europe
Examples:
	| Client                       | Currency   | CurrencyMethod | Office      | Department | Section | Rate     | ChargeTypeGroup | CostTypeGroup   |PayorName |
	| Client_Automation at_HTOvMOn | EUR - Euro | Bill           | London (EU) | Default    | Default | Standard | Desc_at_3e8glw7 | Desc_at_VowqCrR |Dave Peter |
@canada
Examples:
	| Client                       | Currency              | CurrencyMethod | Office    | Department          | Section           | Rate     | ChargeTypeGroup | CostTypeGroup |PayorName |
	| Client_Automation at_HTOvMOn | CAD - Canadian Dollar | Bill           | Vancouver | Banking and Finance | Banking & Finance | Standard | Desc_at_3e8glw7 | Auto__18Xeq   |Dave Peter |
@singapore
Examples:
	| Client                       | Currency               | CurrencyMethod | Office    | Department            | Section           | Rate     | ChargeTypeGroup | CostTypeGroup |PayorName |
	| Client_Automation at_HTOvMOn | SGD - Singapore Dollar | Bill           | Singapore | Corporate - Singapore | Banking & Finance | Standard | Desc_at_3e8glw7 | Auto__18Xeq   |Dave Peter |

Scenario Outline: 020 Create a time entry
	When I view the time entry setup process
	And add the time entry
		| Fee Earner  | TimeType   | Hours | Narrative |
		| <FeeEarner> | <TimeType> | 0.05  | {Auto}+10 |

@canada @europe @singapore
Examples:
	| FeeEarner      | TimeType          |
	| Dentons Brazil | Fixed-Capped Fees |
@qa
Examples:
	| FeeEarner      | TimeType          |
	| Dentons Brazil | Fixed-Capped Fees |
@uk
Examples:
	| FeeEarner      | TimeType          |
	| Dentons Brazil | Fixed-Capped Fees |
@ft
Examples:
	| FeeEarner      | TimeType          |
	| Dentons Brazil | Fixed-Capped Fees |
@training @staging
Examples:
	| FeeEarner      | TimeType          |
	| Dentons Brazil | Fixed-Capped Fees |
@europe
Examples:
	| FeeEarner      | TimeType          |
	| Dentons Brazil | Fixed-Capped Fees |