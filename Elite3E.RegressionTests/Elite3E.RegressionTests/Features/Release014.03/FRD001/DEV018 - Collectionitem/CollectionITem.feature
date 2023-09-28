Feature: CollectionITem

Azure: https://dev.azure.com/dentonsglobal/GFT%203E/_testPlans/execute?planId=78404&suiteId=79045


@CancelProcess @ft
Scenario Outline: 010 Verify that Quick Find operator for 'Payer Name' has been changed to 'Contains'
	Given I navigate to the collection items process
	Then I verify that the quick find include 'Begins With Related Client Number'
	And I verify that the quick find include 'Contains Client Name'
	And I verify that the quick find include 'Contains Step Collector'
	And I verify that the quick find include 'Contains Item Collector'
	And I verify that the quick find include 'Contains Collection Description'
	And I verify that the quick find include 'Contains Payer Name'