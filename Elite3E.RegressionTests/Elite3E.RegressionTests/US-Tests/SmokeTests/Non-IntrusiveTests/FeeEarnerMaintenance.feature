@us
Feature: FeeEarnerMaintenance


Scenario: Verify Fee Earner Maintenance
	Given I search for process 'Fee Earner Maintenance'
	When I select an existing record if present
	Then I verify the sections in fee earner maintenance