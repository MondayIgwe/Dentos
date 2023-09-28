
@release6 @frd058 @ReceiptsGlDetailDescription
Feature: ReceiptsGlDetailDescription

Scenario Outline: 010 Update Gl description
	Given I search for process 'GL Detail Description'
	And I advanced find and select
		| Search Column  | Search Operator  | Search Value  |
		| <SearchColumn> | <SearchOperator> | <SearchValue> |
	When I update both language '<Description>' and unit override '<Description>'

@ft @training @staging @canada @europe @uk @singapore @qa
Examples:
	| SearchColumn        | SearchOperator | SearchValue | Description                                                                                  |
	| Query (Description) | Equals         | Paid Cost   | <Auto Paid Time> Receipt Type: @ReceiptType@, Receipt Date: @RcptDate@, Invoice: @InvNumber@ |

Scenario Outline: 020 Create Matter and Charge Entry
	Given I create a user with details
		| UserName | DataRoleAlias | DefaultOperatingAlias   |
		| <User>   | Admin         | <DefaultOperatingAlias> |
	And I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	And I 'add' a workflow user '<User>' to fee earner '<FeeEarnerName>'
	And add a user '<User>' fee earner
	And I create the Payer with Api
		| PayerName   | Entity            |
		| <PayorName> | WhiteWater Entity |
	And I create a matter with details:
		| Client   | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| <Client> | <FeeEarnerName>   | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | Bill                 | <Office> | <Department> | <Section> | Desc_at_3e8glw7     | Desc_at_3e8glw7   | <PayorName> |
	When I add a charge entry
		| Charge Type  | Amount | Tax Code  |
		| <ChargeType> | 300.00 | <TaxCode> |
@qa
Examples:
	| User               | Client           | FeeEarnerName      | ChargeType                                              | TaxCode                         | Office         | Currency            | Department | Section | PayorName    | DefaultOperatingAlias          |
	| Johnny Westendsons | MarksAnd Spencer | Johnny Westendsons | Admin Charges (e.g. On-boarding fee, data storage etc.) | UK Output Domestic Standard 20% | London (UKIME) | GBP - British Pound | Default    | Default | Stacy Rogers | Dentons UK and Middle East LLP |
@ft
Examples:
	| User               | Client             | FeeEarnerName      | ChargeType                                              | TaxCode                         | Office         | Currency            | Department | Section | PayorName    | DefaultOperatingAlias          |
	| Johnny Westendsons | Johnny Westendsons | Johnny Westendsons | Admin Charges (e.g. On-boarding fee, data storage etc.) | UK Output Domestic Standard 20% | London (UKIME) | GBP - British Pound | Default    | Default | Stacy Rogers | Dentons UK and Middle East LLP |
@training @staging @uk
Examples:
	| User               | Client             | FeeEarnerName      | ChargeType                                              | TaxCode                 | Office         | Currency            | Department | Section | PayorName    | DefaultOperatingAlias          |
	| Johnny Westendsons | Johnny Westendsons | Johnny Westendsons | Admin Charges (e.g. On-boarding fee, data storage etc.) | UK Output Domestic Zero | London (UKIME) | GBP - British Pound | Default    | Default | Stacy Rogers | Dentons UK and Middle East LLP |
@canada
Examples:
	| User               | Client                        | FeeEarnerName      | ChargeType                                              | TaxCode                         | Office    | Currency              | Department          | Section           | PayorName    | DefaultOperatingAlias |
	| Johnny Westendsons | Client_Automation NumberThree | Johnny Westendsons | Admin Charges (e.g. On-boarding fee, data storage etc.) | CA Output Domestic Standard GST | Vancouver | CAD - Canadian Dollar | Banking and Finance | Banking & Finance | Stacy Rogers | Dentons Canada LLP    |
@europe
Examples:
	| User               | Client                        | FeeEarnerName      | ChargeType                                              | TaxCode                   | Office      | Currency   | Department | Section | PayorName    | DefaultOperatingAlias   |
	| Johnny Westendsons | Client_Automation NumberThree | Johnny Westendsons | Admin Charges (e.g. On-boarding fee, data storage etc.) | EU Output Conversion Code | London (EU) | EUR - Euro | Default    | Default | Stacy Rogers | Dentons Europe LLP (UK) |
@singapore
Examples:
	| User               | Client                        | FeeEarnerName      | ChargeType                                              | TaxCode                     | Office    | Currency               | Department | Section | PayorName    | DefaultOperatingAlias        |
	| Johnny Westendsons | Client_Automation NumberThree | Johnny Westendsons | Admin Charges (e.g. On-boarding fee, data storage etc.) | SG Output Domestic Standard | Singapore | SGD - Singapore Dollar | Default    | Default | Stacy Rogers | Dentons Rodyk & Davidson LLP |

Scenario Outline: 030 Disbursement Entry
	When I submit the disbursement modify
		| Work Date | Disbursement Type  | Work Currency  | Work Amount  | Narrative | Tax Code  |
		| {Today}-1 | <DisbursementType> | <WorkCurrency> | <WorkAmount> | {Auto}+36 | <TaxCode> |
	And I validate the disbursement is posted with no errors

@qa
Examples:
	| DisbursementType                   | WorkCurrency | WorkAmount | TaxCode                         |
	| Data Storage & Costs - Anticipated | GBP          | 500        | UK Output Domestic Standard 20% |
@ft
Examples:
	| DisbursementType    | WorkCurrency | WorkAmount | TaxCode                         |
	| Advertising Charges | GBP          | 500        | UK Output Domestic Standard 20% |
@europe
Examples:
	| DisbursementType        | WorkCurrency | WorkAmount | TaxCode                   |
	| Automation Accomodation | EUR - Euro   | 500        | EU Output Conversion Code |

@canada
Examples:
	| DisbursementType                      | WorkCurrency | WorkAmount | TaxCode                         |
	| Court & Stamp Fees - Anticipated (NT) | CAD          | 500        | CA Output Domestic Standard GST |
@uk
Examples:
	| DisbursementType        | WorkCurrency | WorkAmount | TaxCode                 |
	| Automation Accomodation | GBP          | 500        | UK Output Domestic Zero |
@training @staging
Examples:
	| DisbursementType | WorkCurrency | WorkAmount | TaxCode                 |
	| Agents Fees      | GBP          | 500        | UK Output Domestic Zero |
@singapore
Examples:
	| DisbursementType          | WorkCurrency           | WorkAmount | TaxCode                     |
	| Court Fees  - Anticipated | SGD - Singapore Dollar | 500        | SG Output Domestic Standard |

Scenario Outline: 040 Create a time modify
	When I submit a time modify
		| Time Type  | Hours | Narrative | FeeEarnerName | WorkRate | WorkCurrency | Tax Code  |
		| <TimeType> | 0.1   | {Auto}+10 | <FeeEarner>   | 1        | <Currency>   | <TaxCode> |
	And I can generate the proforma
		| Description | Include Other Proformas | Proforma Status |
		| {Auto}+36   | <IncludeOtherProformas> | Draft           |
@ft @qa
Examples:
	| FeeEarner          | TimeType          | Hours | Narrative         | TaxCode                    | IncludeOtherProformas | Currency            |
	| Johnny Westendsons | Fixed-Capped Fees | 0.10  | test automation 1 | AE Output Domestic Zero 0% | No                    | GBP - British Pound |
@training @staging
Examples:
	| FeeEarner          | TimeType | Hours | Narrative         | TaxCode                 | IncludeOtherProformas | Currency            |
	| Johnny Westendsons | FEES     | 0.10  | test automation 1 | UK Output Domestic Zero | No                    | GBP - British Pound |
@europe
Examples:
	| FeeEarner          | TimeType | Hours | Narrative         | TaxCode                   | IncludeOtherProformas | Currency |
	| Johnny Westendsons | FEES     | 0.10  | test automation 1 | EU Output Conversion Code | No                    | EUR      |

@canada
Examples:
	| FeeEarner          | TimeType | Hours | Narrative         | TaxCode                         | IncludeOtherProformas | Currency              |
	| Johnny Westendsons | FEES     | 0.10  | test automation 1 | CA Output Domestic Standard GST | No                    | CAD - Canadian Dollar |
@uk
Examples:
	| FeeEarner          | TimeType       | Hours | Narrative         | TaxCode                 | IncludeOtherProformas | Currency            |
	| Johnny Westendsons | FEES (Default) | 0.10  | test automation 1 | UK Output Domestic Zero | No                    | GBP - British Pound |
@singapore
Examples:
	| FeeEarner          | TimeType          | Hours | Narrative         | TaxCode                     | IncludeOtherProformas | Currency               |
	| Johnny Westendsons | Fixed-Capped Fees | 0.10  | test automation 1 | SG Output Domestic Standard | No                    | SGD - Singapore Dollar |

@CancelProcess @ft @training @staging @canada @europe @uk @singapore @qa
Scenario: 050 Create a Proforma Edit and generate Bill
	Given I proxy as user '<User>'
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	And I open the proforma for submission
	Then I submit it
	And I cancel proxy
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	When I open the proforma for billing
	And I bill it without printing
	And the invoice number is generated

@training @staging @canada @europe @uk @singapore @qa @ft
Examples:
	| User               |
	| Johnny Westendsons |


@CancelProcess @ft @training @staging @canada @europe @uk @singapore @qa
Scenario: 055 Validate Invoice GL has been posted
	When I search for 'Workflow Dashboard'
	And I open the Invoice Dispatch workflow task
	And I send the invoice routed to finance team when dispatch method not set
	Given I search for process 'Invoices' without add button
	When I quick search by invoice number
	And I view the gl postings
	Then I validate gl postings status
	And I close the gl postings

Scenario Outline: 060 Enter receipt information
	When I add a new receipt
		| ReceiptType   | Receipt Date | Document Number | Narrative |
		| <ReceiptType> | {Today}      | {Auto}+36       | {Auto}+36 |
	And change the operating unit "<OperatingUnit>"
	And add the invoice on the receipt
	And I receipt the total amount
	Then I can submit the receipt

@qa @staging @uk
Examples:
	| ReceiptType    | OperatingUnit                  |
	| AEABUHSBC01AED | Dentons UK and Middle East LLP |

@ft @training
Examples:
	| ReceiptType  | OperatingUnit                  |
	| IGB_3000_GBP | Dentons UK and Middle East LLP |
@canada
Examples:
	| ReceiptType | OperatingUnit      |
	| CRC-01      | Dentons Canada LLP |
@europe
Examples:
	| ReceiptType      | OperatingUnit                              |
	| GBDCABARCOP01EUR | Dentons Europe (Central Asia) Limited (UK) |
@singapore
Examples:
	| ReceiptType  | OperatingUnit                |
	| ICB_1201_SGD | Dentons Rodyk & Davidson LLP |

@ft @training @staging @qa @canada @europe @uk @singapore
Scenario: 070 verify the GL Detail Subledger enquiry for receipts
	Given I search for a process 'GL Detail Subledger enquiry' and select a chart 'GL Detail Subledger enquiry (GLDetailSubledgerInq)'
	When I create GL Detail Subledger 'Receipts' report
	Then I verify the receipt report description

