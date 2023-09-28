@us
Feature: InvoiceFeature


Scenario: 010 Select an Invoice
	 When I navigate to the Invoice process
	 And add select an invoice
	 Then confirm invoice field exist
		 
