@us
Feature: ProformaEdit

Scenario: Verify Proforma Edit
	Given I search for process 'Proforma Edit' without add button
	When I select an existing record if present
	Then I verify the sections in the proforma edit 
