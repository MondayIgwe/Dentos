Feature: ClientAccountReceipt

@ft @qa @staging @training @singapore @europe @canada @uk 
Scenario:Verify client account receipt
	Given I search for process 'Client Account Receipt'
	When I select an existing record if present
	Then I verify the sections in client account receipt
