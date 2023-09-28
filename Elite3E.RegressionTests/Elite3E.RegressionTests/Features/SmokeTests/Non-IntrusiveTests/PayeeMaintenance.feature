Feature: PayeeMaintenance

@ft @qa @staging @training @singapore @europe @canada @uk 
Scenario: Verify Payee Maintenance
	Given I search for process 'Payee Maintenance'
	When I select an existing record if present
	Then I verify the sections in payee maintenance 
