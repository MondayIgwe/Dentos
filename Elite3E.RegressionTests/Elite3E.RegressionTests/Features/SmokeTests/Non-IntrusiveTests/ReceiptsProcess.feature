Feature: ReceiptsProcess

@ft @qa @staging @training @singapore @europe @canada @uk 
Scenario: Verify Receipts process
	Given I search for process 'Receipts - Apply / Reverse Payments'
	When I select an existing record if present
	Then I verify the sections in the receipts 
