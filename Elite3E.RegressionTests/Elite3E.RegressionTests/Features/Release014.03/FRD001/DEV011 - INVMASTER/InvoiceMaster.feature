Feature: InvoiceMaster

Verify that for INVMASTER Quick find has been changed to Contains Invoice Number rather than Equals
Azure: https://dev.azure.com/dentonsglobal/GFT%203E/_testPlans/execute?planId=78404&suiteId=78543

@CancelProcess @training @staging @canada @europe @uk @singapore @ft @qa
Scenario Outline: 010 Verify that for INVMASTER Quick find has been changed to Contains Invoice Number rather than Equals
	Given I navigate to the invoices process
	Then I verify that the quick find include 'Contains Invoice Number'
	And I verify the 'Original Amount' field exists on the advanced find
	And I verify the 'Balance Amount' field exists on the advanced find
	And I verify the 'Matter Supervising Timekeeper Name' field exists on the advanced find
	And I verify the 'Collecting Timekeeper ' field does not exists on the advanced find
