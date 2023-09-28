@us
Feature: MatterMaintenanceBillingRulesOptOut

@CancelProcess
Scenario Outline: Validate Client Billing Rules Opt Out Checkbox available for matter maintenance
	Given I create the Payer with Api
		| PayerName   | Entity         |
		| <PayorName> | BlackBoard Ltd |
	And I create a matter with details:
		| Client   | FeeEarnerFullName   | Status   | OpenDate   | MatterName   | Currency   | MatterCurrencyMethod   | Office   | Department   | Section   | ChargeTypeGroupName   | CostTypeGroupName   | PayorName   |
		| <Client> | <FeeEarnerFullName> | <Status> | <OpenDate> | <MatterName> | <Currency> | <MatterCurrencyMethod> | <Office> | <Department> | <Section> | <ChargeTypeGroupName> | <CostTypeGroupName> | <PayorName> |
	When I search for the matter
	Then I verify the client billing rules opt out checkbox is available
	
Examples:
	| Client              | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency | MatterCurrencyMethod | Office  | Department | Section | ChargeTypeGroupName | CostTypeGroupName |PayorName |
	| BillingRules Client | Adam Smithone     | Fully Open | {Today}-1 | {Auto}+36  | USD      | Bill                 | Chicago | Default    | Default | FixedChargeType     | FixedCostType     |Janie Key |
