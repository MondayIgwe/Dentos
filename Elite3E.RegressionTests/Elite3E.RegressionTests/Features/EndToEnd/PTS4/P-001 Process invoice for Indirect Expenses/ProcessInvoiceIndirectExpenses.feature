Feature: ProcessInvoiceIndirectExpenses

Process invoice for Indirect Expenses
Create Voucher & Validate it in 'AP Detailed Invoice Transactional Report' and GL Postings in Voucher maintanenace
Testcase URL: https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/56005

@e2eft
Scenario Outline: 010 Create a Voucher
	Given I create the Payer with Api
		| PayerName   | Entity       |
		| <PayorName> | CentralCoast |
	And I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	And I create a matter with details:
		| Client   | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department | Section | ChargeTypeGroupName | CostTypeGroupName | InputAmount   | PayorName   |
		| <Client> | <FeeEarnerName>   | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | Bill                 | <Office> | Default    | Default | Desc_at_3e8glw7     | Desc_at_3e8glw7   | <InputAmount> | <PayorName> |
	And I create a new Payee with the Api
		| PayeeName            |
		| Payee atVoucherMaint |
	And I add a new voucher with voucher default information
		| Operation Unit  | Invoice Number | Invoice Date | Transaction Type  | Voucher Status  |
		| <OperationUnit> | v_             | {Today}      | <TransactionType> | <VoucherStatus> |
	And I add disbursement card details for voucher
		| Disbursement Type  | Narrative          | InputTaxCode   | Voucher Amount |
		| <DisbursementType> | Automation Testing | <InputTaxCode> | 100.00         |
	And I click on Tax button in disbursement card and validate tax record is added in voucher taxes
	And I update it
	When I submit the voucher
	Then I verify the voucher is created

Examples:
	| Client                        | FeeEarnerName         | Office         | OperationUnit | DisbursementType | InputTaxCode                   | Currency            | TransactionType | VoucherStatus | PayorName |
	| Client_Automation NumberThree | DentonsAPI FeeEarner4 | London (UKIME) | Firm          | Court Fees       | UK Input Domestic Standard 20% | GBP - British Pound | Direct Debit    | Direct Debit  | James May |


@e2eft
Scenario: 020 Verify in AP Detailed Invoice Transactional Report
	Given I search for process 'AP Detailed Invoice Transactional Report' without add button
	When I run report by searching with invoice number
	Then I verify the voucher details in AP Detailed Invoice Transactional Report


@e2eft
Scenario: 030 Validate Voucher GL has been posted
	Given I search for process 'Voucher Maintenance' without add button
	When I quick search by voucher number
	And I view the gl postings
	Then I validate gl postings status
	And I validate tax amount in gl postings
	And I close the gl postings