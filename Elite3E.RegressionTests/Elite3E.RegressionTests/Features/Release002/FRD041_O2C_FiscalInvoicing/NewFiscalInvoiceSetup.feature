@release2 @frd041 @NewFiscalInvoiceSetup
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

@ft @qa
Examples:
	| ErrorMessage         | Unit                              | BillGlType                             | SuspenseGlType                                                   |
	| Enter a unique Unit. | Dentons Rodyk Myanmar (S) Pte Ltd | Local Adj - Dentons United States, LLP | Local Adj - The Law Firm of Wael A. Alissa in Assoc. with Denton |
@training 
Examples:
	| ErrorMessage         | Unit                              | BillGlType                           | SuspenseGlType                       |
	| Enter a unique Unit. | Dentons Rodyk Myanmar (S) Pte Ltd | Local Dentons UKIME Charitable Trust | Local Dentons UKIME Charitable Trust |
@staging
Examples:
	| ErrorMessage         | Unit                    | BillGlType            | SuspenseGlType                      |
	| Enter a unique Unit. | Dentons Myanmar Limited | Myanmar Cash Postings | Myanmar Fiscal Invoicing - Suspense |
@singapore
Examples:
	| ErrorMessage         | Unit                         | BillGlType                       | SuspenseGlType                     |
	| Enter a unique Unit. | Dentons Rodyk & Davidson LLP | Singapore Accrual as per Aderant | Local Dentons Rodyk & Davidson LLP |
@europe
Examples:
	| ErrorMessage         | Unit                    | BillGlType                    | SuspenseGlType               |
	| Enter a unique Unit. | Dentons Europe LLP (UK) | Local Dentons Europe LLP (UK) | EU Accrual as per Enterprise |

Scenario Outline: 020_Add Fiscal Invoice Setup -  an inactive fiscal invoicing setup record already exists
	When I open the Fiscal Invoice Setup process
	And I fill the relevant fields - an inactive fiscal invoicing setup record already exists
		| Unit   | BillGlType   | SuspenseGlType   |
		| <Unit> | <BillGlType> | <SuspenseGlType> |
	And I click update
	Then a error message "<ErrorMessage>" is displayed
	#Then Submit the form
	#And Cancel the process

@ft @qa
Examples:
	| ErrorMessage         | Unit                    | BillGlType                          | SuspenseGlType                      |
	| Enter a unique Unit. | Dentons Myanmar Limited | Local Adj - Dentons Myanmar Limited | Local Adj - Dentons Myanmar Limited |
@training 
Examples:
	| ErrorMessage         | Unit                              | BillGlType                           | SuspenseGlType                       |
	| Enter a unique Unit. | Dentons Rodyk Myanmar (S) Pte Ltd | Local Dentons UKIME Charitable Trust | Local Dentons UKIME Charitable Trust |
	@staging
Examples:
	| ErrorMessage         | Unit                    | BillGlType             | SuspenseGlType         |
	| Enter a unique Unit. | Dentons Myanmar Limited | Local UKIME CONV - EGL | Unbilled Disbursements |
@singapore
Examples:
	| ErrorMessage         | Unit                         | BillGlType                       | SuspenseGlType                     |
	| Enter a unique Unit. | Dentons Rodyk & Davidson LLP | Singapore Accrual as per Aderant | Local Dentons Rodyk & Davidson LLP |
@europe
Examples:
	| ErrorMessage         | Unit                    | BillGlType                   | SuspenseGlType                                |
	| Enter a unique Unit. | Dentons Europe LLP (UK) | Local Dentons Luxembourg SCS | Local Dentons Europe Studio Legale Tributario |

