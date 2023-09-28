@us
Feature: ChargeTypeGroup


Scenario:Verify Charge Type Group
	Given I search for process 'Charge Type Group'
	When I select an existing record if present
	Then I verify the sections in charge type group