Feature: P-020- P2P: Cheque Production and Management

The Payment Run has been completed in the US for the Cheque Print run.  
These are printed off, approved in line with US process and sent to the Suppliers.  
An error is identified and so one cheque requires to be cancelled with the bank and corrected for re-issue for a different amount.
Azure link: https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/56004

@CancelProcess
Scenario: 010 Prepare data for Cash Receipt
	Given I create the Payer with Api
		| PayerName   | Entity       |
		| <PayorName> | CentralCoast |
	And I create a matter with details:
		| Client   | Status     | OpenDate   | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | Rate   | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| <Client> | Fully Open | {Today}-31 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <Rate> | <ChargeTypeGroup>   | <CostTypeGroup>   | <PayorName> |
	And I create a new Payee with the Api
		| PayeeName          |
		| Payee_ ChequeManag |
	Then I add a payee bank
		| Description | AccountNumber |
		| {Auto}+8    | {Auto}+5      |
	And I submit it

@e2eft
Examples:
	| Client                | Currency            | CurrencyMethod | Office         | Department | Section | Rate     | ChargeTypeGroup | CostTypeGroup   | PayorName |
	| Client_Automation CMF | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Standard | TestGroup3      | Desc_at_3e8glw7 | James May |

@CancelProcess
Scenario Outline: 020 Create a Receipt
	Given I navigate to the Receipts Apply/Reverse Payments process
	When allocate the new reciept
		| Unallocated Type  | Receipt Amount  | Operating Unit  | Narrative |
		| <UnallocatedType> | <ReceiptAmount> | <OperatingUnit> | {Auto}+10 |
	And add a new reciept
		| ReceiptType   | Receipt Amount  | Document Number | Payer   | Narrative |
		| <ReceiptType> | <ReceiptAmount> | {Auto}+10       | <Payer> | {Auto}+10 |
	Then I can submit the reciept allocation
	And I validate submit was successful

@e2eft
Examples:
	| ReceiptType | ReceiptAmount | Payer                                  | UnallocatedType | OperatingUnit                  |
	| CASH        | 5000          | Sydney - Domestic Client - Head office | Unidentified    | Dentons UK and Middle East LLP |

@CancelProcess
Scenario: 030 Create a New Direct Cheque
	Given I add a new direct cheque
	And I update the client refund
		| Receipt Type  | Client                | Client Refund  | Document Number |
		| <ReceiptType> | Client_Automation CMF | <ClientRefund> |                 |
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
	And I validate submit was successful

@e2eft
Examples:
	| OperatingUnit                  | ReceiptType | ClientRefund | BankAccount                        | Amount | TransactionType | Office         |
	| Dentons UK and Middle East LLP | CASH        | True         | London UKME - HSBC Off 1 Acc - GBP | 5000   | Credit Note     | London (UKIME) |
	
#Currently fails due to incorrect type of cheque used
@CancelProcess @e2eft
Scenario: 040 Void a Cheque
	Given I navigate to the cheque maintenance process
	Then I select an existing cheque
	And I void the cheque
		| VoidDate  | VoidReason |
		| {Today}+1 | Correction |
	When I update it
	And I submit it
	And I validate submit was successful

@CancelProcess
Scenario Outline: 050 Create a new New Voucher
	Given I add a new voucher with voucher default information
		| Operation Unit  | Invoice Number | Invoice Date |
		| <OperationUnit> | v_             | {Today}      |
	And I update the status to "Approved"
	And I add disbursement card details for voucher
		| Disbursement Type  | Narrative          | TaxCode   | InputTaxCode   | Voucher Amount |
		| <DisbursementType> | Automation Testing | <TaxCode> | <InputTaxCode> | 100            |
	And I validate the barrister fields are not mandatory
	And I update it
	And I add input amount "<InputAmount>" in voucher tax card
	When I submit the voucher

@e2eft
Examples:
	| OperationUnit                  | DisbursementType                                       | TaxCode                         | InputTaxCode                       | InputAmount |
	| Dentons UK and Middle East LLP | Registraton Fees - Issuance of SSCT - Anticipated (NT) | UK Output Domestic Standard 20% | AE ICB Input  Domestic Standard 5% | 0           |

@CancelProcess @e2eft
Scenario: 060 Create a Payment Selection Generation
	Given I navigate to the payment selection generation process
	Then I add a new payment selection generation record
	And I complete mandatory fields
		| Description | BankAccount                        | PaymentDate |
		| {Auto}+10   | London UKME - HSBC Off 1 Acc - GBP | {Today}     |
	When I add a new selection criteria
		| PaymentDate |
		| {Today}     |
	And I search for the voucher number
	And I test the selection
	And I verify the test result
	And I update it
	And I generate it
	And I verify that I'm on the payment selection edit page
	And I verify that the proposed child form is displayed
	And I allocate the payment
	And I process the payment
	And I submit it

