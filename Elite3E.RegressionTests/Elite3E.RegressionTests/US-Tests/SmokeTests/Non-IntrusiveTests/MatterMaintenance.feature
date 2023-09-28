@us
Feature: MatterMaintenance


Scenario: Verify matter mainatenance
	Given I search for process 'Matter Maintenance'
	When I select an existing record if present
	Then I verify sections in matter maintenance 