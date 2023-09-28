@us
Feature: VerifyRemittanceAccountOnMatter
	Verify the Remittance Account on Matter


Scenario Outline: 010 Create Matter
	Given I create the Payer with Api
		| PayerName   | Entity                |
		| <PayorName> | BottomLine Associates |
	And I create a matter with details:
		| Client   | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod   | Office   | Department   | Section   | ChargeTypeGroupName   | CostTypeGroupName   | PayorName   |
		| <Client> | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | <MatterCurrencyMethod> | <Office> | <Department> | <Section> | <ChargeTypeGroupName> | <CostTypeGroupName> | <PayorName> |


Examples:
	| Client                       | Currency        | MatterCurrencyMethod | Office  | Department | Section | ChargeTypeGroupName | CostTypeGroupName |PayorName |
	| Client_Automation at_HTOvMOn | USD - US Dollar | Bill                 | Chicago | Default    | Default | Desc_at_3e8glw7     | Desc_at_VowqCrR   |Kenton Ford |

Scenario Outline: 020 Remmittance Account is available on Matter
	When I save the Remittance Account "<RemittanceAccount>"
	Then the remittance account is saved


Examples:
	| RemittanceAccount                |
	| Dentons United States Llp NFCITI |
