@us
Feature: DEV005_ClientAccountAdjustmentFlags


@CancelProcess
Scenario: Verify EFT Flag is set to true in Client Adjustment Type form
	Given I add a new 'Client Account Adjustment'
	Then I verify EFT flag is set to true 

@CancelProcess
Scenario: Verify Deposit Flag is set to false in Client Adjustment Type form
	Given I add a new 'Client Account Adjustment'
	Then I verify Deposit Flag is set to false
