@us
Feature: GLType_Payments
	GL Type and Access process general ledger is enabled with receipt type

Scenario Outline: 010_Confirm reciept box exist on ALL_BOOK type(Direct swap)
	When I navigate to the GL Type and Access process
	And I select the  "<GLType>" gl type'
	And I confirm and set receipts checkbox
	Then I cancel it
Examples:
	| GLType   |
	| ALL_BOOK |


Scenario Outline: 020_Receipt type in general ledger
	When I navigate to the Receipts Apply/Reverse Payments process
	And I select the general ledger child form
	Then I can verify the Gl type exist.
	And Gltype  is "<GLTypeVal>"
	Then I cancel it
Examples:
	| GLTypeVal          |
	| All Books GL Types |

Scenario Outline: 030_Remove the default GL type from all books
	When I navigate to the GL Type and Access process
	And I select the  "<GLType>" gl type'
	And I confirm and set receipts checkbox

Examples:
	| GLType   |
	| ALL_BOOK |