@ignore
#ignored because of this bug: https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/80807
Feature: ARDetail

Azure: https://dev.azure.com/dentonsglobal/GFT%203E/_testPlans/execute?planId=78404&suiteId=78571


@CancelProcess @ft
Scenario Outline: 010 Verify that 'Open amount' has been added to Query Attributes for ARDETAIL
	Given I navigate to the receipts apply/reverse process
	Then I verify the 'Receipt Amount' field exists on the advanced find
	And I verify the 'Open Amount' field exists on the advanced find
	And I verify the 'Currency' field exists on the advanced find
