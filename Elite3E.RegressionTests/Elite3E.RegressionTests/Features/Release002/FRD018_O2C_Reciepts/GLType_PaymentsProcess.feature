@release2 @frd018 @GLType_Payments
Feature: GLType_Payments
	GL Type and Access process general ledger is enabled with receipt type

@CancelProcess
Scenario Outline: 010_Confirm reciept box exist on ALL_BOOK type(Direct swap)
	When I navigate to the GL Type and Access process
	And I select the  "<GLType>" gl type'
	And I confirm and set receipts checkbox


@ft @qa @training @staging @canada @europe @uk @singapore 
Examples:
	| GLType   |
	| ALL_BOOK |

@CancelProcess
Scenario Outline: 020_Receipt type in general ledger
	When I navigate to the Receipts Apply/Reverse Payments process
	And I select the general ledger child form
	Then I can verify the Gl type exist.
	And Gltype  is "<GLTypeVal>"

@training @staging @singapore @ft
Examples:
	| GLTypeVal          |
	| All Books GL Types |
@qa @uk
Examples:
	| GLTypeVal    |
	| Journal Base |
@canada  @europe
Examples:
	| GLTypeVal                                         |
	| TBC GL Type to deal with Tran Types masked to TBC |

Scenario Outline: 030_Remove the default GL type from all books
	When I navigate to the GL Type and Access process
	And I select the  "<GLType>" gl type'
	And I confirm and set receipts checkbox

@ft @qa @training @staging @canada @europe @uk @singapore 
Examples:
	| GLType   |
	| ALL_BOOK |