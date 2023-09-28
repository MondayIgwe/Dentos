Feature: CAT_ProcessButton

36586 - Verify that the  Client Account Transfer Request process has the following process buttons

@ft @singapore
Scenario: 010 Verify Client Account Transfer Request has the process buttons
	Given I search for 'Trust Transfer Request'
	When I verify the following buttons
		| Buttons   |
		| Submit    |
		| Terminate |
		| Close     |
	Then I close the process
