Feature: Cash Receipts GL

A client sends a EFT cash receipt to the Cashier Team for a firm item and to be allocated directly to the GL.
Azure link: https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/41416

@CancelProcess @e2eft
Scenario: 001 Create a new Client
	Given I search or create a client
		| Entity Name           | DateOpened |
		| Client_Aut ReceiptsGL | {Today}-1  |

@CancelProcess
Scenario Outline: 010 Create a Receipt with GL
	Given I add a new receipt
		| Receipt Type  | Receipt Date | Document Number | Narrative |
		| <ReceiptType> | {Today}      | {Auto}+36       | {Auto}+36 |
	And change the operating unit "<OperatingUnit>"
	When I add a new general ledger row
		| GLType   | Receipt Amount  |
		| <GLType> | <ReceiptAmount> |
	And I add the gl account
		| GLAccount   |
		| <GLAccount> |
	And I receipt the total amount
	And I add the payer
	Then I update it
	And I submit it
	And I validate submit was successful

@e2eft
Examples:
	| ReceiptType | OperatingUnit                  | ReceiptAmount | GLType             | GLAccount                              |
	| CASH        | Dentons UK and Middle East LLP | 6000          | All Books GL Types | 3000-101010-1000-0000-0000-0000-000000 |