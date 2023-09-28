@smoke @Matter
Feature: Matter
	Create, Edit, Delete a Matter

	#Matter defect - temporary defect

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
	Then verify the matter number is generated

@ft @training @staging @canada @europe @uk @singapore @qa
Scenario: 020 Edit an Existing Matter
	Given I view an existing matter
	When I edit the existing matter
		| Open date  |
		| 06/21/2021 |
	Then I can submit the matter

@ft @training @staging @canada @europe @uk @singapore @qa
Scenario: 030 Delete an Existing Matter
	Given I view an existing matter
	When I delete an existing matter
	Then I can submit the matter
