Feature: DirectCheque

@ft @qa @staging @training @singapore @europe @canada @uk 
Scenario:Verify Direct Cheque
	Given I search for process 'Direct Cheque'
	When I select an existing record if present
	Then I verify the sections in direct cheque
