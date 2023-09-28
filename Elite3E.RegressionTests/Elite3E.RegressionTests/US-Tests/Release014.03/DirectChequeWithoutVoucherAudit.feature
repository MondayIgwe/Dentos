﻿ @us @ignore
# CR - https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/37653
Feature: DirectChequeWithoutVoucherAudit


@CancelProcess
Scenario: 010 Prepare data for Cash Receipt
	Given I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	And I search or create a client
		| Entity Name | FeeEarnerFullName |
		| <Client>    | <FeeEarnerName>   |
	And I create the Payer with Api
		| PayerName   | Entity   |
		| <PayorName> | <Client> |
	And I create a matter with details:
		| Client   | Status     | OpenDate   | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | Rate   | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| <Client> | Fully Open | {Today}-31 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <Rate> | <ChargeTypeGroup>   | <CostTypeGroup>   | <PayorName> |
	And I create a new Payee with the Api
		| PayeeName          |
		| Payee_ CashReceipt |


Examples:
	 | Currency            | CurrencyMethod | Office         | Department | Section | ChargeTypeGroup | CostTypeGroup   | PayorName       |Client        | FeeEarnerName |
	 | USD - US Dollar     | Bill           | Chicago        | Default    | Default | Desc_at_3e8glw7 | Desc_at_VowqCrR | Francis Little  |Mikel Mandela | Mike Thanks   |

@CancelProcess
Scenario: 020 Create a New Cash Receipt
	Given I navigate to the Receipts Apply/Reverse Payments process
	When allocate the new reciept
		| Unallocated Type  | Receipt Amount  | Operating Unit  | Narrative |
		| <UnallocatedType> | <ReceiptAmount> | <OperatingUnit> | {Auto}+10 |
	And add a new reciept
		| ReceiptType   | Receipt Amount  | Document Number | Payer   | Narrative |
		| <ReceiptType> | <ReceiptAmount> | {Auto}+10       | <Payer> | {Auto}+10 |
	Then I can submit the reciept allocation
	And I validate submit was successful

Examples:
	| ReceiptType | ReceiptAmount | Payer          | UnallocatedType | OperatingUnit   |
	| NFCITIUSD   | 5000          | Francis Little | Unidentified    | Dentons US, LLP |

@CancelProcess
Scenario: 030 Create a New Direct Cheque
	Given I add a new direct cheque
	And I update the client refund
		| Receipt Type  | Client   | Client Refund  | Document Number |
		| <ReceiptType> | <Client> | <ClientRefund> |                 |
	Then I verify that the client refund form has been populated
	When I update the direct cheque
		| Bank Account  | Amount   | Transaction Type  | Office   | Cheque Template | Cheque Printer | ChequeNumber |
		| <BankAccount> | <Amount> | <TransactionType> | <Office> | TE_AP Check     | NO_PRINTER     | {Auto}+7     |
	And I verify that the cheque number and the cheque date have been auto populated
	Then I update the amount for the client refund
		| Amount   |
		| <Amount> |
	And change the operating unit "<OperatingUnit>"
	And submit it
    And I search for process 'Cheque Maintenance'
	And I select an existing cheque
	And I verify the audit button opens the auditing window

Examples:
	| OperatingUnit   | ReceiptType | ClientRefund | BankAccount                | Amount | TransactionType | Office  | Client        |
	| Dentons US, LLP | NFCITIUSD   | True         | Citibank MO IOLTA - (6315) | 5000   | Credit Note     | Chicago | Mikel Mandela |