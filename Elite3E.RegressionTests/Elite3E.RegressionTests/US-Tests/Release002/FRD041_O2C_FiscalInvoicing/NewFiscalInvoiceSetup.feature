@ignore
@us
Feature: NewFiscalInvoiceSetup

Please note: This test has been removed for all the regions except europe and singapore as all the other regions do 
not use Fiscal invoice renumbering process. 
https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/42490

Scenario Outline: 010_Add Fiscal Invoice Setup -  active fiscal invoicing setup record already exists
	When I open the Fiscal Invoice Setup process
	And I fill the relevant fields - active fiscal invoicing setup record already exists
		| Unit   | BillGlType   | SuspenseGlType   |
		| <Unit> | <BillGlType> | <SuspenseGlType> |
	And I click update
	Then a error message "<ErrorMessage>" is displayed
	#Then Submit the form
	#And Cancel the process


Examples:
	| ErrorMessage         | Unit            | BillGlType   | SuspenseGlType     |
	| Enter a unique Unit. | Dentons US, LLP | Tax Clearing | US - Enterprise UX |

Scenario Outline: 020_Add Fiscal Invoice Setup -  an inactive fiscal invoicing setup record already exists
	When I open the Fiscal Invoice Setup process
	And I fill the relevant fields - an inactive fiscal invoicing setup record already exists
		| Unit   | BillGlType   | SuspenseGlType   |
		| <Unit> | <BillGlType> | <SuspenseGlType> |
	And I click update
	Then a error message "<ErrorMessage>" is displayed
	#Then Submit the form
	#And Cancel the process


Examples:
	| ErrorMessage         | Unit                       | BillGlType   | SuspenseGlType     |
	| Enter a unique Unit. | Dentons United States, LLP | Tax Clearing | US - Enterprise UX |
