Feature: InvoicePayor

Azure: https://dev.azure.com/dentonsglobal/GFT%203E/_testPlans/execute?planId=78404&suiteId=78570

@CancelProcess @ft
Scenario Outline: 020 Verify that Quick Find operator for 'Payer Name' has been changed to 'Contains'
	Given I navigate to the Receipts Apply/Reverse Payments process
	When I navigate to the invoices child form
	Then I verify that the quick find include 'Contains Client Name'
	And I verify that the quick find include 'Contains Payer Name'
	And I verify that the quick find include 'Contains Invoice Number'
	And I verify that the quick find include 'Contains Receipt Number'
	And I verify the 'Open Amount' field exists on the advanced find
