Feature: ClientMaintenanceviaDenton

O-035 - O2C: Client Maintenance via DentonsOpen
 Azure link: https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/38259


Scenario Outline: 001 Matter Maintenance
	Given I create the Payer with Api
		| PayerName   | Entity       |
		| <PayorName> | CentralCoast |
	And I create a new matter
	When I update the matter
		| Client                           | Status     | Open Date | Matter Name | Matter Currency Method | Statement Site | MatterType | MatterAttribute | Language |
		| Client_Automation Matter_HTOvMOn | Fully Open | {Today}   | {Auto}+10   | Bill                   | London UK site | Rodyk IP   | Rodyk IP Matter | English  |
	And I add a Matter Payer
		| Start Date |
		| {Today}+1  |
	And I update the effective dated information
		| Child Form                  | Office      |
		| Effective Dated Information | London (EU) |
	And I update the matter rates
		| Child Form   | Rate     |
		| Matter Rates | Standard |
	And I add a new cost type group
		| Cost Type Group |
		| Desc_at_VowqCrR |
	And I add new charge type group
		| Charge Type Group |
		| Desc_at_3e8glw7   |
	And I submit it
	Then verify the matter number is generated

@e2eft
Examples: 
	| PayorName   |
	| James Mayor |