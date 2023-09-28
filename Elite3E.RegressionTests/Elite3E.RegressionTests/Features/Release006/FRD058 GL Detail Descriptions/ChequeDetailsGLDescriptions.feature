 @release6 @frd058 @ChequeDetailsGLDescriptions
Feature: ChequeDetailsGLDescriptions

@training @staging @canada @europe @uk @singapore @qa @ft
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
@training @staging @canada @europe @uk @singapore @qa @ft
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
@ft
Examples:
	| FeeEarnerName    | OperatingUnit                  | ReceiptType   | Narrative         | UnallocatedType | Office         | BankAccountDisplayName             | CurrencyTypeDescription | Currency            | PayorName    |
	| Blue CheckEarner | Dentons UK and Middle East LLP | UKMEHSBC01GBP | Automation test 1 | Unidentified    | London (UKIME) | London UKME - HSBC Off 1 Acc - GBP | Daily Rate Xe           | GBP - British Pound | Stacy Rogers |
@training @staging @uk @qa
Examples:
	| FeeEarnerName    | OperatingUnit                  | ReceiptType  | Narrative         | UnallocatedType | Office         | BankAccountDisplayName | CurrencyTypeDescription         | Currency            | PayorName    |
	| Blue CheckEarner | Dentons UK and Middle East LLP | IGB_3000_GBP | Automation test 1 | Balance Held    | London (UKIME) | Barclays BARCLAYS      | Daily - Azerbaijan Central Bank | GBP - British Pound | Stacy Rogers |
@singapore
Examples:
	| FeeEarnerName    | OperatingUnit                | ReceiptType  | Narrative         | UnallocatedType | Office    | BankAccountDisplayName                  | CurrencyTypeDescription         | Currency               | PayorName    |
	| Blue CheckEarner | Dentons Rodyk & Davidson LLP | IGB_3000_GBP | Automation test 1 | Balance Held    | Singapore | ICB - 1201 - Singapore - ICB Bank - SGD | Daily - Azerbaijan Central Bank | SGD - Singapore Dollar | Stacy Rogers |
@canada
Examples:
	| FeeEarnerName    | OperatingUnit      | ReceiptType | Narrative         | UnallocatedType | Office    | BankAccountDisplayName | CurrencyTypeDescription         | Currency              | PayorName    |
	| Blue CheckEarner | Dentons Canada LLP | CRC-01      | Automation test 1 | Unallocated     | Vancouver | Barclays BARCLAYS      | Daily - Azerbaijan Central Bank | CAD - Canadian Dollar | Stacy Rogers |
@europe
Examples:
	| FeeEarnerName    | OperatingUnit           | ReceiptType      | Narrative         | UnallocatedType | Office      | BankAccountDisplayName | CurrencyTypeDescription         | Currency   | PayorName    |
	| Blue CheckEarner | Dentons Europe LLP (UK) | GBDCABARCOP01EUR | Automation test 1 | Balance Held    | London (EU) | Barclays BARCLAYS      | Daily - Azerbaijan Central Bank | EUR - Euro | Stacy Rogers |

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

@ft
Examples:
	| ClientRefund | BankAccount                        | ChequeAmount | TransactionType | Office         | ChequeTemplate | ChequePrinter | Amount | ClientRefundChildForm | ReceiptType   |
	| True         | London UKME - HSBC Off 1 Acc - GBP | 20.00        | Credit Note     | London (UKIME) | TE_AP Check    | NO_PRINTER    | 20.00  | Client Refund         | UKMEHSBC01GBP |

@qa @uk
Examples:
	| ClientRefund | BankAccount                        | ChequeAmount | TransactionType | Office         | ChequeTemplate | ChequePrinter | Amount | ClientRefundChildForm | ReceiptType   |
	| True         | London UKME - HSBC Off 1 Acc - GBP | 20.00        | Credit Note     | London (UKIME) | TE_AP Check    | NO_PRINTER    | 20.00  | Client Refund         | AUSYDANZ01AUD |
@singapore
Examples:
	| ClientRefund | BankAccount                             | ChequeAmount | TransactionType | Office    | ChequeTemplate | ChequePrinter | Amount | ClientRefundChildForm | ReceiptType |
	| True         | ICB - 1201 - Singapore - ICB Bank - SGD | 20.00        | Credit Note     | Singapore | TE_AP Check    | NO_PRINTER    | 20.00  | Client Refund         | SGOKBCM4MMK |
@training
Examples:
	| ClientRefund | BankAccount       | ChequeAmount | TransactionType | Office         | ChequeTemplate | ChequePrinter | Amount | ClientRefundChildForm | ReceiptType  |
	| True         | Barclays BARCLAYS | 20.00        | Credit Note     | London (UKIME) | TE_AP Check    | NO_PRINTER    | 20.00  | Client Refund         | IGB_3000_GBP |
@staging
Examples:
	| ClientRefund | BankAccount       | ChequeAmount | TransactionType | Office         | ChequeTemplate | ChequePrinter | Amount | ClientRefundChildForm | ReceiptType   |
	| True         | Barclays BARCLAYS | 20.00        | Credit Note     | London (UKIME) | TE_AP Check    | NO_PRINTER    | 20.00  | Client Refund         | AUSYDANZ01AUD |
@europe
Examples:
	| ClientRefund | BankAccount                                                      | ChequeAmount | TransactionType | Office      | ChequeTemplate | ChequePrinter | Amount | ClientRefundChildForm | ReceiptType      |
	| True         | Dentons Europe (AZ) - Bank of Azerbaijan - Operating Acc 1 - EUR | 20.00        | Credit Note     | London (EU) | TE_AP Check    | NO_PRINTER    | 20.00  | Client Refund         | GBDCABARCOP01EUR |
@canada
Examples:
	| ClientRefund | BankAccount                           | ChequeAmount | TransactionType | Office    | ChequeTemplate | ChequePrinter | Amount | ClientRefundChildForm | ReceiptType |
	| True         | NAT - Foreign Withholding Taxes - CAD | 20.00        | Credit Note     | Vancouver | TE_AP Check    | NO_PRINTER    | 20.00  | Client Refund         | CRC-01      |

@training @staging @canada @europe @uk @singapore @qa @ft
Scenario Outline: 040 Search for direct cheque
	Given I search for process 'Direct Cheque'
	When I advanced find and select direct Cheque
		| Search Column   | Search Operator |
		| Document Number | Equals          |
	Then I verify the direct cheque number

@training @staging @canada @europe @uk @singapore @qa @ft
Scenario: 050 verify the GL Detail Subledger enquiry for cheque
	Given I search for a process 'GL Detail Subledger enquiry' and select a chart 'GL Detail Subledger enquiry (GLDetailSubledgerInq)'
	When I create GL Detail Subledger 'Cheques' report
	Then I verify the cheque report description
