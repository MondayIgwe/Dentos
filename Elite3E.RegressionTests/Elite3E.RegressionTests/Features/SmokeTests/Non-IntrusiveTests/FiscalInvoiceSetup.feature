Feature: FiscalInvoiceSetup

@ft @qa @staging @training @singapore @europe @canada @uk 
Scenario:Verify Fiscal Invoice Setup
	Given I search for process 'Fiscal Invoice Setup'
	When I select an existing record if present
	Then I verify the sections in fiscal invoice setup
