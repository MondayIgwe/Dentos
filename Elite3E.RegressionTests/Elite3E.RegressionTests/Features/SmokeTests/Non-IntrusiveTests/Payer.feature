Feature: Payer

@ft @qa @staging @training @singapore @europe @canada @uk 
Scenario: Verify Payer
	Given I search for process 'Payer'
	When I select an existing record if present
	Then I verify the sections in payer
