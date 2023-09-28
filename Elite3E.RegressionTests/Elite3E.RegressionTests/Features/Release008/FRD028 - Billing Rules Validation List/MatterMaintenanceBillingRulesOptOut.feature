@release8 @frd028 @MatterMaintenanceBillingRulesOptOut
Feature: MatterMaintenanceBillingRulesOptOut


Scenario Outline: Validate Client Billing Rules Opt Out Checkbox available for matter maintenance
	Given I create the Payer with Api
		| PayerName   | Entity         |
		| <PayorName> | BlackBoard Ltd |
	And I create a matter with details:
		| Client   | FeeEarnerFullName   | Status   | OpenDate   | MatterName   | Currency   | MatterCurrencyMethod   | Office   | Department   | Section   | ChargeTypeGroupName   | CostTypeGroupName   | PayorName   |
		| <Client> | <FeeEarnerFullName> | <Status> | <OpenDate> | <MatterName> | <Currency> | <MatterCurrencyMethod> | <Office> | <Department> | <Section> | <ChargeTypeGroupName> | <CostTypeGroupName> | <PayorName> |
	When I search for the matter
	Then I verify the client billing rules opt out checkbox is available
	
@CancelProcess @ft @training @staging @uk @qa
Examples:
	| Client              | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency            | MatterCurrencyMethod | Office         | Department | Section | ChargeTypeGroupName | CostTypeGroupName | PayorName |
	| BillingRules Client | Adam Smithone     | Fully Open | {Today}-1 | {Auto}+36  | GBP - British Pound | Bill                 | London (UKIME) | Default    | Default | FixedChargeType     | FixedCostType     | Janie Key |
@canada
Examples:
	| Client              | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency              | MatterCurrencyMethod | Office    | Department | Section | ChargeTypeGroupName | CostTypeGroupName |PayorName |
	| BillingRules Client | Adam Smithone     | Fully Open | {Today}-1 | {Auto}+36  | CAD - Canadian Dollar | Bill                 | Vancouver | Default    | Default | FixedChargeType     | FixedCostType     |Janie Key |
@singapore
Examples:
	| Client              | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency               | MatterCurrencyMethod | Office    | Department            | Section | ChargeTypeGroupName | CostTypeGroupName |PayorName |
	| BillingRules Client | Adam Smithone     | Fully Open | {Today}-1 | {Auto}+36  | SGD - Singapore Dollar | Bill                 | Singapore | Corporate - Singapore | Default | FixedChargeType     | FixedCostType     |Janie Key |
@europe
Examples:
	| Client              | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency            | MatterCurrencyMethod | Office      | Department | Section | ChargeTypeGroupName | CostTypeGroupName |PayorName |
	| BillingRules Client | Adam Smithone     | Fully Open | {Today}-1 | {Auto}+36  | GBP - British Pound | Bill                 | London (EU) | Default    | Default | FixedChargeType     | FixedCostType     |Janie Key |
