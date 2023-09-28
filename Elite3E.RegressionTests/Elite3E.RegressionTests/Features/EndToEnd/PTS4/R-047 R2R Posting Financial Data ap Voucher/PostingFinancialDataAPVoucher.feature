Feature: PostingFinancialDataAPVoucher

AP Voucher is posted and verify it accordingly
@e2eft
Scenario Outline: 010 Create a Voucher
	Given I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	And I create the Payer with Api
		| PayerName   | Entity   |
		| <PayorName> | <Client> |
	And I create a matter with details:
		| Client   | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department | Section | ChargeTypeGroupName | CostTypeGroupName | InputAmount   | PayorName   |
		| <Client> | <FeeEarnerName>   | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | Bill                 | <Office> | Default    | Default | Desc_at_3e8glw7     | Desc_at_3e8glw7   | <InputAmount> | <PayorName> |
	And I create a new Payee with the Api
		| PayeeName            |
		| Payee atVoucherMaint |
	And I add a new voucher with voucher default information
		| Operation Unit  | Invoice Number | Invoice Date | Transaction Type  | Voucher Status  | Amount |
		| <OperationUnit> | v_             | {Today}      | <TransactionType> | <VoucherStatus> | 100.00 |
	And I add voucher direct-gl information
		| GL Account  | Amount | VoucherGLTaxCode   |
		| <GLAccount> | 100.00 | <VoucherGLTaxCode> |
	And I update it
	And I add input amount "<InputAmount>" in voucher tax card
	When I submit the voucher
	Then I verify the voucher is created

Examples:
	| Client                        | FeeEarnerName         | Office         | OperationUnit | DisbursementType | InputTaxCode                   | Currency            | TransactionType | VoucherStatus | GLAccount                              | VoucherGLTaxCode               | InputAmount | PayorName  |
	| Client_Automation NumberThree | DentonsAPI FeeEarner4 | London (UKIME) | Firm          | Court Fees       | UK Input Domestic Standard 20% | GBP - British Pound | Direct Debit    | Approved      | 3000-101010-1000-0000-0000-0000-000000 | UA Input Domestic Standard 20% | 100.00      | James Matt |

@e2eft
Scenario: 020 Validate Voucher GL has been posted
	Given I search for process 'Voucher Maintenance' without add button
	When I quick search by voucher number
	And I view the gl postings
	Then I validate the gl postings for operating unit '<OperatingUnit>'
	
@e2eft
Examples:
	| OperatingUnit |
	| 3000          |

@e2eft
Scenario: 030 Verify Post queue failed records that voucher is not displayed
	Given I search for process 'Post Queue' without add button
	When I quick search by voucher number and verify its not present

@e2eft
Scenario: 040 Verify in Posting Results
	Given I search for the entry in posting results process 
	Then verify the posting status is posted

@e2eft
Scenario: 050 Verify in Journal Register
	Given I search for the entry in journal register
	Then verify the posting details in journal register
            | Search Column   | Search Operator |
            | Journal Manager | Equals          |


