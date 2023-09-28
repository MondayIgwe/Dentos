@ignore @us
Feature: ChequeDetailsGLDescriptions

#Defect https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/83219/


Scenario Outline: 005_Payee Maintenance
	Given I create a new Payee with the Api
		| PayeeName            |
		| Payee atVoucherMaint |

Scenario Outline: 010 Update Gl description
	Given I search for process 'GL Detail Description'
	And I advanced find and select
		| Search Column  | Search Operator  | Search Value  |
		| <SearchColumn> | <SearchOperator> | <SearchValue> |
	When I update both language '<Description>' and unit override '<Description>'

Examples:
	| SearchColumn        | SearchOperator | SearchValue | Description                                                                |
	| Query (Description) | Equals         | AP Check    | <Auto unit AP Check> Payee: @PayeeName@, Cheque: @CkNum@, Bank: @BankName@ |
	
#Scenario Outline: 015 Update Gl description
#	Given I search for process 'GL Detail Description'
#	And I advanced find and select
#		| Search Column  | Search Operator  | Search Value  |
#		| <SearchColumn> | <SearchOperator> | <SearchValue> |
#	When I update language '<Description>'
#@ft @training @staging  @canada @europe @uk @singapore @us @qa
#Examples:
#	| SearchColumn        | SearchOperator | SearchValue | Description                                                                |
#	| Query (Description) | Equals         | AP Check    | <Auto unit AP Check> Payee: @PayeeName@, Cheque: @CkNum@, Bank: @BankName@ |

Scenario Outline: 020 Enter Receipt Information
#	Given I create a receipt type with details:
#		 | Code      | Description | BankAccountDisplayName   | CurrencyTypeDescription   | ToleranceAmount | TolerancePercentage |
#		 | {Auto}+12 | {Auto}+21   | <BankAccountDisplayName> | <CurrencyTypeDescription> | 50.00           | 50.00               |
	Given I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	And I create the Payer with Api
		| PayerName   | Entity            |
		| <PayorName> | WhiteWater Entity |
	And I create a matter with details:
		| Client           | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department | Section | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| Blue CheckClient | <FeeEarnerName>   | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | Bill                 | <Office> | Default    | Default | Desc_at_3e8glw7     | Desc_at_3e8glw7   | <PayorName> |
	And I add a new receipt
		| Receipt Date | Receipt Type  |
		| {Today}      | <ReceiptType> |
	And change the operating unit "<OperatingUnit>"
	And I update the receipt
		| Receipt Type  | Narrative   |
		| <ReceiptType> | <Narrative> |
	When I add an unallocated child form
		| Unallocated Type  | Receipt Amount | Child Form  |
		| <UnallocatedType> | 5000           | Unallocated |
	And I receipt the total amount
	And I update the receipt
		| Document Number |
		| {Auto}+36       |
	Then the payer is auto populated
	And I can submit the receipt
	And I validate submit was successful

Examples:
	| FeeEarnerName    | OperatingUnit   | ReceiptType | Narrative         | UnallocatedType | Office  | BankAccountDisplayName | CurrencyTypeDescription         | Currency        | PayorName    |
	| Blue CheckEarner | Dentons US, LLP | NFCITIUSD   | Automation test 1 | Balance Held    | Chicago | Barclays BARCLAYS      | Daily - Azerbaijan Central Bank | USD - US Dollar | Stacy Rogers |

Scenario Outline: 030 Check Client Refund Checkbox And I add a new direct cheque
	Given I add a new direct cheque
	And I create a person entity
		| FirstName | LastName    |
		| Blue      | CheckClient |
	And I update the client refund
		| Receipt Type  | Document Number | Client Refund  |
		| <ReceiptType> | {Auto}+36       | <ClientRefund> |
	When I update the direct cheque
		| Bank Account  | Amount         | Transaction Type  | Office   | Cheque Template  | Cheque Printer  | ChequeNumber |
		| <BankAccount> | <ChequeAmount> | <TransactionType> | <Office> | <ChequeTemplate> | <ChequePrinter> | {Auto}+6     |
	And I verify client refund child form
		| Amount   | ClientRefundChildForm   | Office   |
		| <Amount> | <ClientRefundChildForm> | <Office> |
	Then I can submit the direct cheque
	#And I verify information message does not contain error
	And I validate submit was successful


Examples:
	| ClientRefund | BankAccount               | ChequeAmount | TransactionType | Office  | ChequeTemplate | ChequePrinter | Amount | ClientRefundChildForm | ReceiptType |
	| True         | Citibank Delaware CITI-CH | 20.00        | Credit Note     | Chicago | TE_AP Check    | NO_PRINTER    | 20.00  | Client Refund         | NFCITIUSD   |


Scenario Outline: 040 Search for direct cheque
	Given I search for process 'Direct Cheque'
	When I advanced find and select direct Cheque
		| Search Column   | Search Operator |
		| Document Number | Equals          |
	Then I verify the direct cheque number


Scenario: 050 verify the GL Detail Subledger enquiry for cheque
	Given I search for a process 'GL Detail Subledger enquiry' and select a chart 'GL Detail Subledger enquiry (GLDetailSubledgerInq)'
	When I create GL Detail Subledger 'Cheques' report
	Then I verify the cheque report description
