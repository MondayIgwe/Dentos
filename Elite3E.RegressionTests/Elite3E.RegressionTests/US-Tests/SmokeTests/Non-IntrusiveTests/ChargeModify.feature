@us
Feature: ChargeModify

Scenario:Verify charge modify process 
	Given I search for process 'Charge Modify'
	When I select an existing record if present
	Then I verify the sections in charge modify
