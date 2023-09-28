Feature: GLDetailDescription

@ft @qa @staging @training @singapore @europe @canada @uk 
Scenario:Verify GL Detail Description
	Given I search for process 'GL Detail Description'
	When I select an existing record if present
	Then I verify the sections in GL detail description