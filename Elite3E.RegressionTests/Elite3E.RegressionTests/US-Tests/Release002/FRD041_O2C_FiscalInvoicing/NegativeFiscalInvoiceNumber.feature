@us
Feature: NegativeFiscalInvoiceNumber
	Confirm NegativeFiscalInvoiceNumber is not allowed

Scenario Outline: Create Invoice
	When I open the Fiscal Invoice Setup process
	And I enter a negative value for next fiscal invoice number
	Then error messages displayed should contain
		| Error Message  |
		| <ErrorMessage> |

Examples:
	| ErrorMessage                                      |
	| The next invoice number must be greater than zero |