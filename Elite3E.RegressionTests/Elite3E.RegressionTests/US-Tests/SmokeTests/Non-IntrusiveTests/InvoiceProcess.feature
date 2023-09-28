@us
Feature: InvoiceProcess


Scenario: Verify Invoice process
	Given I search for process 'Invoices' without add button
	When I select an existing record if present
	Then I verify the sections in the invoices 

