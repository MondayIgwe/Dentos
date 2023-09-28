Feature: Matter Creation with a Payer that has a Tax Area assigned

Bug: https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/81754

@ft @qa @training @staging @canada @europe @uk @singapore
Scenario Outline: 010 Create Matter
	Given I create the Payer with Api
		| PayerName  | Entity        |
		| Dave Peter | SeasonsEntity |
	And I create a new matter
	When I update the matter
		| Client                           | Status     | Open Date | Matter Name | Master Matter | Matter Currency Method | Statement Site     |
		| Client_Automation Matter_HTOvMOn | Fully Open | {Today}   | {Auto}+10   | True          | Bill                   | New/Edit Stmt Site |
	And I update the effective dated information
		| Child Form                  | Office  |
		| Effective Dated Information | Default |
	And I update the matter rates
		| Child Form   | Rate     |
		| Matter Rates | Standard |
	And I add a Matter Payer
		| Start Date |
		| {Today}+1  |
	And I add a new cost type group
		| Cost Type Group |
		| Desc_at_VowqCrR |
	And I add new charge type group
		| Charge Type Group |
		| Desc_at_3e8glw7   |
	Then I validate submit was successful