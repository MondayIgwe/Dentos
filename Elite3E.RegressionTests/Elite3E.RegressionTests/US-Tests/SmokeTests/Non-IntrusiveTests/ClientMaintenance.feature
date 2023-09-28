@us
Feature: ClientMaintenance


Scenario:Verify client maintenance
	Given I search for process 'Client Maintenance'
	When I select an existing record if present
	Then I verify the sections in client maintenance