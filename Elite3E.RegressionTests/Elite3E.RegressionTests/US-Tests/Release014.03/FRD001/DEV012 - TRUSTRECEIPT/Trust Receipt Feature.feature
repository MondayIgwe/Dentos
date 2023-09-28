@us @ignore
#ignored because of: https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/80782
Feature: Trust Receipt Feature

Azure: https://dev.azure.com/dentonsglobal/GFT%203E/_testPlans/execute?planId=78404&suiteId=78564

@CancelProcess
Scenario Outline: 010 Verify that operator for 'Bank Account Name' has been changed to 'Contains' for Quick Find within TRUSTRECEIPT
	Given I navigate to the client account receipt process
	Then I verify that the quick find include 'Contains Bank Account Name'
	And I verify that the quick find include 'Equals Transaction Date'


@CancelProcess
Scenario Outline: 020 Verify that 'Transaction Date' add  'Equal' is available for quick find TRUSTDISBURSEMENT
	Given I navigate to the client account disbursement process
	Then I verify that the quick find include 'Equals Tran Date'
	And I verify the 'Client Account Acct ' field exists on the advanced find
