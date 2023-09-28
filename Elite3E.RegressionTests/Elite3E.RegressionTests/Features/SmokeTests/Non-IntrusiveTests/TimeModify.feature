Feature: TimeModify

@ft @qa @staging @training @singapore @europe @canada @uk  
Scenario: Verify Time Modify
	Given I search for process 'Time Modify'
	When I select an existing record if present
	Then I verify the section in time modify


