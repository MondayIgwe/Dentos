@us

Feature: VoucherDetailGLDescription

Scenario Outline: 010 Update Gl description
	Given I search for process 'GL Detail Description'
	And I advanced find and select
		| Search Column  | Search Operator  | Search Value  |
		| <SearchColumn> | <SearchOperator> | <SearchValue> |
	When I update both language '<Description>' and unit override '<Description>'

Examples:
	| SearchColumn        | SearchOperator | SearchValue | Description                                                                           |
	| Query (Description) | Equals         | AP Voucher  | <Auto language AP Voucher> Name: @Name@, Voucher: @VchrAutoNumber@, Invoice: @InvNum@ |

Scenario Outline: 020 Create Matter and Voucher Entry
	Given I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	And I create the Payer with Api
		| PayerName   | Entity            |
		| <PayorName> | WhiteWater Entity |
	And I create a matter with details:
		| Client   | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department | Section | ChargeTypeGroupName | CostTypeGroupName | InputAmount   | PayorName   |
		| <Client> | <FeeEarnerName>   | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | Bill                 | <Office> | Default    | Default | Desc_at_3e8glw7     | Desc_at_3e8glw7   | <InputAmount> | <PayorName> |
	And I create a new Payee with the Api
		| PayeeName            |
		| Payee atVoucherMaint |
	And I add a new voucher with voucher default information
		| Operation Unit  | Invoice Number | Invoice Date |
		| <OperationUnit> | v_             | {Today}      |
	And I add disbursement card details for voucher
		| Disbursement Type  | Narrative          | TaxCode   | InputTaxCode   | Voucher Amount |
		| <DisbursementType> | Automation Testing | <TaxCode> | <InputTaxCode> | 100            |
	And I update it
	And I add input amount "<InputAmount>" in voucher tax card
	When I submit the voucher
	Then I verify the voucher is created

Examples:
	| Client                        | FeeEarnerName         | Office                     | OperationUnit              | DisbursementType | InputTaxCode                    | TaxCode                          | Currency        | InputAmount | PayorName    |
	| Client_Automation NumberThree | DentonsAPI FeeEarner4 | Dentons United States, LLP | Dentons United States, LLP | Bank Fees        | US Input Domestic Standard Rate | US Output Domestic Standard Rate | USD - US Dollar | 0           | Stacy Rogers |

@CancelProcess
Scenario: 025 Validate Voucher GL has been posted
	Given I search for process 'Voucher Maintenance' without add button
	When I quick search by voucher number
	And I view the gl postings
	Then I validate gl postings status
	And I close the gl postings

Scenario: 030 verify the GL Detail Subledger enquiry for voucher
	Given I search for a process 'GL Detail Subledger enquiry' and select a chart 'GL Detail Subledger enquiry (GLDetailSubledgerInq)'
	When I create GL Detail Subledger 'Voucher' report
	Then I verify the voucher report description

