Feature: CashReceipt

A requestor notifies a collector of misallocation and the collector requests for the receipt to be reversed 
The Cashier Team reviews the request, accept and reverses the receipt accordingly. 
The fund is then transferred to client account for other types of transaction.

Azure link: https://dev.azure.com/dentonsglobal/GFT%203E/_testPlans/execute?view=_TestManagement&planId=12845&suiteId=38476

@CancelProcess
Scenario: 010 Prepare data for Receipts
	Given I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	And I create a user with details
		| UserName | DataRoleAlias | DefaultOperatingAlias   | UserRoleList          |
		| <User>   | Admin         | <DefaultOperatingAlias> | DEFAULT_WORKFLOW_ROLE |
	And add a user '<User>' fee earner
	And I create the Payer with Api
		| PayerName   | Entity   |
		| <PayorName> | <Client> |
	And I create a matter with details:
		| Client   | Status     | OpenDate   | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | Rate   | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| <Client> | Fully Open | {Today}-31 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <Rate> | <ChargeTypeGroup>   | <CostTypeGroup>   | <PayorName> |
	And I post the disbursement
		| Work Date | Disbursement Type  | Work Currency | Work Amount | Narrative | Tax Code  |
		| {Today}-1 | <DisbursementType> | <Currency>    | 5000        | {Auto}+36 | <TaxCode> |
	When I validate the disbursement is posted with no errors
	And I can generate the proforma
		| Description | Include Other Proformas | Proforma Status |
		| {Auto}+36   | No                      | Draft           |

@e2eft
Examples:
	| FeeEarnerName | Currency            | Client        | CurrencyMethod | Office         | Department | Section | Rate     | ChargeTypeGroup | CostTypeGroup   | DisbursementType        | TaxCode                    | PayorName  | User     | DefaultOperatingAlias          |
	| Mike Thanks   | GBP - British Pound | Mikel Mandela | Bill           | London (UKIME) | Default    | Default | Standard | TestGroup3      | Desc_at_3e8glw7 | Automation Accomodation | UK Output Domestic Zero 0% | James Matt | Ann Rose | Dentons UK and Middle East LLP |

@CancelProcess
Scenario Outline: 020 Generate Invoice number
	Given I proxy as user '<User>'
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	And I open the proforma for submission
	Then I submit it
	And I cancel proxy
	And I search for 'Workflow Dashboard'
	When I open the proforma workflow task
	And I open the proforma for billing
	And I bill it without printing
	And the invoice number is generated

@e2eft
Examples:
	| User        |
	| Mike Thanks |

@CancelProcess
Scenario Outline: 030 Create a Receipt with Invoice
	When I add a new receipt
		| Receipt Type  | Receipt Date | Cheque Date | Document Number | Narrative | Amount   | Deposit Number |
		| <ReceiptType> | {Today}      | {Today}-1   | {Auto}+36       | {Auto}+36 | <Amount> | {Auto}+12      |
	And change the operating unit "<OperatingUnit>"
	And add the invoice on the receipt
	And I add the receipt amount '<Amount>' and total it
	Then I can submit the receipt
	And I validate submit was successful

@e2eft
Examples:
	| ReceiptType | OperatingUnit                  | Amount  |
	| Cash        | Dentons UK and Middle East LLP | 4000.00 |

@CancelProcess
Scenario: 040 Reverse and Reallocate Receipt
	Given I locate and search for a receipt to reverse
	And I perform the reversal
		| ReversalDate | Reason | Comment                       | ReAllocate |
		| {Today}+1    | Error  | Reversed due to misallocation | true       |
	And change the operating unit "<OperatingUnit>"
	Then I can submit the reciept allocation
	And I remove the invoice for the receipt
	When add the invoice on the receipt
	And I receipt the total amount
	And update the receipt
	And submit it
	And I validate submit was successful

@e2eft
Examples:
	| OperatingUnit                  |
	| Dentons UK and Middle East LLP |

@CancelProcess @e2eft
Scenario: 050 Verify Receipt is posted
	Given I locate a submitted receipt
	And I can verify that the receipt is reversed

@CancelProcess
Scenario: 060 Prepare data for Cash Receipt
	Given I create a user with details
		| UserName        | DataRoleAlias | DefaultOperatingAlias   |
		| <FeeEarnerName> | Admin         | <DefaultOperatingAlias> |
	And I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	And I search or create a client
		| Entity Name | FeeEarnerFullName |
		| <Client>    | <FeeEarnerName>   |
	And I create the Payer with Api
		| PayerName   | Entity   |
		| <PayorName> | <Client> |
	And I 'add' a workflow user '<FeeEarnerName>' to fee earner '<FeeEarnerName>'
	And I create a matter with details:
		| Client   | Status     | OpenDate   | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | Rate   | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| <Client> | Fully Open | {Today}-31 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <Rate> | <ChargeTypeGroup>   | <CostTypeGroup>   | <PayorName> |
	And I create a new Payee with the Api
		| PayeeName          |
		| Payee_ CashReceipt |

@e2eft
Examples:
	| Currency            | CurrencyMethod | Office         | Department | Section | Rate     | ChargeTypeGroup | CostTypeGroup   | PayorName | Client        | FeeEarnerName | DefaultOperatingAlias          |
	| GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Standard | TestGroup3      | Desc_at_3e8glw7 | James May | Mikel Mandela | Mike Thanks   | Dentons UK and Middle East LLP |

@CancelProcess
Scenario: 070 Create a New Cash Receipt
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
	| Cash        | 5000          | Sydney - Domestic Client - Head office | Unidentified    | Dentons UK and Middle East LLP |

@CancelProcess
Scenario: 080 Create a New Direct Cheque
	Given I add a new direct cheque
	And change the operating unit "<OperatingUnit>"
	And I update the client refund
		| Receipt Type  | Client        | Client Refund  | Document Number |
		| <ReceiptType> | Mikel Mandela | <ClientRefund> |                 |
	When I update the direct cheque
		| Bank Account  | Amount   | Transaction Type  | Office   | Cheque Template | Cheque Printer | ChequeNumber |
		| <BankAccount> | <Amount> | <TransactionType> | <Office> | TE_AP Check     | NO_PRINTER     | {Auto}+7     |
	And I verify that the cheque number and the cheque date have been auto populated
	Then I update the amount for the client refund
		| Amount   |
		| <Amount> |
	And submit it
	And I verify that the direct cheque has been created

@e2eft
Examples:
	| OperatingUnit                  | ReceiptType | ClientRefund | BankAccount                        | Amount | TransactionType | Office         |
	| Dentons UK and Middle East LLP | CASH        | True         | London UKME - HSBC Off 1 Acc - GBP | 5000   | Credit Note     | London (UKIME) |


@CancelProcess @e2eft
Scenario Outline: 090 Create a Client Account Receipt
	Given I search for process 'Client Account Receipt'
	And I add a new client account receipt
		| TransactionDate | ClientAccountReceiptType | ClientAccountAcct                    | DocumentNumber |
		| {Today}-1       | Cash                     | Singapore - SCB Bank Trust Acc - GBP | {Auto}+10      |
	When I add client account receipt detail child form data
		| Amount | IntendedUse | Reason                 |
		| 100    | General     | Client Account Receipt |
	Then I update it
	And submit it
	And I validate submit was successful
	And I remove workflow user from fee earner
		| Search Column      | Search Operator | Search Value | FeeEarnerUser |
		| Workflow User.Name | Equals          | null         | Mike Thanks   |

@CancelProcess @e2eft
Scenario Outline: 100 Approve the Client Account Receipt
	Given I search for 'Workflow Dashboard'
	And I navigate to the client account receipt section
	Then I open the client account receipt record
	When I tick the aml checks checkbox and approve
	And I submit it

#ignored since the template options are not available,
#waiting for Rashpal feedback
@CancelProcess @e2eft @ignore
Scenario Outline: 110 Print the Client Account Receipt
	Given I search for process 'Client Account Receipt'
	And I select existing receipt
	Then I print the receipt
		| PrintJobName                   | Template |
		| Client Account Receipt PRint01 | Test     |
	And I submit it
	And I validate submit was successful
