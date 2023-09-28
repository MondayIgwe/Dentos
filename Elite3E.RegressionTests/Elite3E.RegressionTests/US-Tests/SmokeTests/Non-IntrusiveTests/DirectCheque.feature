@us
Feature: DirectCheque


Scenario:Verify Direct Cheque
	Given I search for process 'Direct Cheque'
	When I select an existing record if present
	Then I verify the sections in direct cheque
