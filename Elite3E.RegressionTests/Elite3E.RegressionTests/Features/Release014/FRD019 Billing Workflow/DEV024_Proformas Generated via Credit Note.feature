Feature: DEV024_Proformas Generated via Credit Note

Verify that ‘Partial_Cr_Note_Receipt_ccc’ Override system option has been created and can be set at the unit level
Verify that within Partial Credit Note Process use the override system option to automatically populate Receipt Type field

@CancelProcess @ft @training @staging @canada @europe @uk @singapore @qa
Scenario Outline: 010 Verify Override System options
	Given I navigate to the Override/Set System Options process
	Then the credit note options should be set to the override this
		| OptionName                   | SystemDefault |
		| Partial_Cr_Note_Template_ccc | OVERRIDE THIS |
		| Partial_Cr_Note_Receipt_ccc  | OVERRIDE THIS |
		| Partial_Cr_Note_Prof_ccc     | OVERRIDE THIS |
		| Partial_Cr_Note_Adj_ccc      | OVERRIDE THIS |


@CancelProcess @ft @training @staging @canada @europe @uk @singapore @qa
Scenario Outline: 020 Verify Auto Populated fields on the Credit Note process
	Given I search for 'Partial Credit Notes'
	And I click add
	When I get adjustment code from Api
	Then I verify the auto populated fields

