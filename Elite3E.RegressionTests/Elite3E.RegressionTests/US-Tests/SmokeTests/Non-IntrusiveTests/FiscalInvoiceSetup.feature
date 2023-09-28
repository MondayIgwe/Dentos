@us @ignore
Feature: FiscalInvoiceSetup

Please note: This test has been removed for all the regions except europe and singapore as all the other regions do 
not use Fiscal invoice renumbering process. 

Scenario:Verify Fiscal Invoice Setup
	Given I search for process 'Fiscal Invoice Setup'
	When I select an existing record if present
	Then I verify the sections in fiscal invoice setup
