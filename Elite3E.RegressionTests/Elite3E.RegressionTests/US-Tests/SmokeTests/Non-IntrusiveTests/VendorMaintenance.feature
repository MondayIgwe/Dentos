@us
Feature: VendorMaintenance


Scenario: Verify Vendor maintenance
	Given I search for process 'Vendor Maintenance'
	When I select an existing record if present
	Then I verify the sections in vendor maintenance
