Feature: CloseReopenAMatter

End to End Test Case: https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/38260
Azure link: https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/38260

Scenario: 010_Close A Matter
	Given I create the Payer with Api
		| PayerName   | Entity       |
		| <PayorName> | CentralCoast |
	Given I create a matter with details:
		| Client   | FeeEarnerFullName | Status     | OpenDate   | MatterName | Currency   | MatterCurrencyMethod | Office   | Department | Section | Rate          | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| <Client> | <FeeEarnerName>   | Fully Open | {Today}-30 | {Auto}+36  | <Currency> | Bill                 | <Office> | Default    | Default | Standard Rate | Desc_at_3e8glw7     | Desc_at_3e8glw7   | <PayorName> |
	And I navigate to the matter maintenance process
	When I quick search by matter number
	And I update the matter
		| Status | CloseDate | CloseType |
		| Closed | {Today}   | Closed    |
	And I click update
	Then I verify that no error message is generated
	And I submit it
	And I validate submit was successful

@e2eft
Examples:
	| Client            | FeeEarnerName        | Office         | Currency | TimeType       | PayorName   |
	| ClosingMat Client | ClosingMat FeeEarner | London (UKIME) | GBP      | FEES (Default) | James Mayor |

@e2eft
Scenario: 020_Validate Matter is Closed
	Given I navigate to the matter maintenance process
	When I quick search by matter number
	Then I verify the matter status
		| Status | CloseDate | CloseType |
		| Closed | {Today}   | Closed    |
	And I cancel the process
	
@e2eft
Scenario: 030_Reopen A Matter
	Given I navigate to the matter maintenance process
	When I quick search by matter number
	And I update the matter
		| Status     |
		| Fully Open |
	And I submit it
	
@e2eft
Scenario: 040_Validate Matter is Reopened
	Given I navigate to the matter maintenance process
	When I quick search by matter number
	Then I verify the matter status
		| Status     | CloseDate | CloseType |
		| Fully Open |           |           |
	And I cancel the process
	