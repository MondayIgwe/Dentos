Feature: DEV005_ClientAccountAdjustmentFlags


@CancelProcess @training @staging @canada @europe @uk @singapore @ft @qa
Scenario: 001 Verify EFT Flag is set to true in Client Adjustment Type form
	Given I add a new 'Client Account Adjustment'
	Then I verify EFT flag is set to true 

@CancelProcess @training @staging @canada @europe @uk @singapore @ft @qa
Scenario: 002 Verify Deposit Flag is set to false in Client Adjustment Type form
	Given I add a new 'Client Account Adjustment'
	Then I verify Deposit Flag is set to false
