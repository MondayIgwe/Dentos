@us
Feature: ClientGroup


Scenario:Verify Client Group
	Given I search for process 'Client Group'
	When I select an existing record if present
	Then I verify the sections in client group