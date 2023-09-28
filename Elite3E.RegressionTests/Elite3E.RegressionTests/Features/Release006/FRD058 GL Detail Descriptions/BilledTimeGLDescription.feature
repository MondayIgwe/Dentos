
@release6 @frd058 @BilledTimeGLDescription
Feature: BilledTimeGLDescription

Scenario Outline: 010 Update Gl description
	Given I search for process 'GL Detail Description'
	And I advanced find and select
		| Search Column  | Search Operator  | Search Value  |
		| <SearchColumn> | <SearchOperator> | <SearchValue> |
	When I update both language '<Description>' and unit override '<Description>'

@qa @training @staging @canada @europe @uk @singapore @ft
Examples:
	| SearchColumn        | SearchOperator | SearchValue | Description                                                                                 |
	| Query (Description) | Equals         | Billed Time | <Auto Billed Time> Invoice: @InvNumber@, Matter: @MatterNumber@, TimeKeeperName: @TkprName@ |

Scenario Outline: 020 create matter
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
		| Client       | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | Rate     | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| <ClientName> | <FeeEarnerName>   | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | Bill                 | <Office> | <Department> | <Section> | Standard | Desc_at_3e8glw7     | Desc_at_3e8glw7   | <PayorName> |
	When I create a charge type with details
		| ChargeCode | Description  | CategoryInput | TransactionTypeAlias | Active |
		| {Auto}+10  | <ChargeType> | Other         | Miscellaneous Income | Yes    |
	When I add a charge entry
		| Charge Type  | Amount | Tax Code  |
		| <ChargeType> | 300.00 | <TaxCode> |
@ft
Examples:
	| User               | ClientName         | FeeEarnerName      | ChargeType    | TaxCode                    | Office         | Currency            | Department | Section | PayorName    | DefaultOperatingAlias          |
	| Johnny Westendsons | Johnny Westendsons | Johnny Westendsons | Miscellaneous | UK Output Domestic Zero 0% | London (UKIME) | GBP - British Pound | Default    | Default | Stacy Rogers | Dentons UK and Middle East LLP |
@qa
Examples:
	| User          | ClientName         | FeeEarnerName      | ChargeType    | TaxCode                    | Office         | Currency            | Department | Section | PayorName    | DefaultOperatingAlias          |
	| Proforma User | Lena ClientSurname | Lena EarnerSurname | Miscellaneous | UK Output Domestic Zero 0% | London (UKIME) | GBP - British Pound | Default    | Default | Stacy Rogers | Dentons UK and Middle East LLP |
@europe
Examples:
	| User          | ClientName         | FeeEarnerName      | ChargeType    | TaxCode                 | Office      | Currency   | Department | Section | PayorName    | DefaultOperatingAlias   |
	| Proforma User | Lena ClientSurname | Lena EarnerSurname | Miscellaneous | AE Output Domestic Zero | London (EU) | EUR - Euro | Default    | Default | Stacy Rogers | Dentons Europe LLP (UK) |
@singapore
Examples:
	| User          | ClientName         | FeeEarnerName      | ChargeType    | TaxCode                     | Office    | Currency               | Department | Section | PayorName    | DefaultOperatingAlias        |
	| Proforma User | Lena ClientSurname | Lena EarnerSurname | Miscellaneous | SG Output Domestic Standard | Singapore | SGD - Singapore Dollar | Default    | Default | Stacy Rogers | Dentons Rodyk & Davidson LLP |
@canada
Examples:
	| User          | ClientName         | FeeEarnerName      | ChargeType    | TaxCode                         | Office    | Currency              | Department          | Section           | PayorName    | DefaultOperatingAlias |
	| Proforma User | Lena ClientSurname | Lena EarnerSurname | Miscellaneous | CA Output Domestic Standard GST | Vancouver | CAD - Canadian Dollar | Banking and Finance | Banking & Finance | Stacy Rogers | Dentons Canada LLP    |
@uk @training @staging
Examples:
	| User               | ClientName         | FeeEarnerName      | ChargeType    | TaxCode                 | Office         | Currency | Department | Section | PayorName    | DefaultOperatingAlias          |
	| Johnny Westendsons | Johnny Westendsons | Johnny Westendsons | Miscellaneous | UK Output Domestic Zero | London (UKIME) | GBP      | Default    | Default | Stacy Rogers | Dentons UK and Middle East LLP |

Scenario Outline: 030 Disbursement Entry
	Given I create a hard cost disbursement type with details
		| Code               | Description        |
		| <DisbursementType> | <DisbursementType> |
	When I submit the disbursement modify
		| Work Date | Disbursement Type  | Work Currency  | Work Amount  | Narrative | Tax Code  |
		| {Today}-1 | <DisbursementType> | <WorkCurrency> | <WorkAmount> | {Auto}+36 | <TaxCode> |
	And I validate the disbursement is posted with no errors

@ft
Examples:
	| DisbursementType    | WorkCurrency | WorkAmount | TaxCode                    |
	| Advertising Charges | GBP          | 500        | UK Output Domestic Zero 0% |
@qa
Examples:
	| DisbursementType               | WorkCurrency | WorkAmount | TaxCode                             |
	| Automation Agency Registration | GBP          | 500        | AE Output Domestic Standard Rate 5% |
@singapore
Examples:
	| DisbursementType                    | WorkCurrency | WorkAmount | TaxCode                     |
	| DENSG-Dentons Cross Region Invoices | SGD          | 500        | SG Output Domestic Standard |
@canada
Examples:
	| DisbursementType           | WorkCurrency | WorkAmount | TaxCode                         |
	| Bank of Canada Certificate | CAD          | 500        | CA Output Domestic Standard GST |
@staging
Examples:
	| DisbursementType | WorkCurrency | WorkAmount | TaxCode                 |
	| Advertising      | GBP          | 500        | UK Output Domestic Zero |
@training
Examples:
	| DisbursementType          | WorkCurrency        | WorkAmount | TaxCode                 |
	| Court Fees  - Anticipated | GBP - British Pound | 500        | UK Output Domestic Zero |
@uk
Examples:
	| DisbursementType                    | WorkCurrency        | WorkAmount | TaxCode                 |
	| DENUK-Dentons Cross Region Invoices | GBP - British Pound | 500        | UK Output Domestic Zero |
@europe
Examples:
	| DisbursementType                    | WorkCurrency | WorkAmount | TaxCode                 |
	| DENEU-Dentons Cross Region Invoices | EUR - Euro   | 500        | UK Output Domestic Zero |

Scenario Outline: 040 Create a time modify
	When I submit a time modify
		| Time Type  | Hours | Narrative | FeeEarnerName   | WorkRate | WorkCurrency | Tax Code  |
		| <TimeType> | 0.1   | {Auto}+10 | <FeeEarnerName> | 100      | <Currency>   | <TaxCode> |
	And I can generate the proforma
		| Description | Include Other Proformas | Proforma Status |
		| {Auto}+36   | <IncludeOtherProformas> | Draft           |
@ft
Examples:
	| TimeType          | Narrative         | TaxCode                    | IncludeOtherProformas | FeeEarnerName      | Currency            |
	| Fixed-Capped Fees | test automation 1 | UK Output Domestic Zero 0% | No                    | Johnny Westendsons | GBP - British Pound |
@qa
Examples:
	| TimeType | Narrative         | TaxCode                    | IncludeOtherProformas | FeeEarnerName      | Currency            |
	| Fees     | test automation 1 | UK Output Domestic Zero 0% | No                    | Lena EarnerSurname | GBP - British Pound |
@europe
Examples:
	| TimeType          | Narrative         | TaxCode                 | IncludeOtherProformas | FeeEarnerName      | Currency                |
	| Fixed-Capped Fees | test automation 1 | AE Output Domestic Zero | No                    | Lena EarnerSurname | AUD - Australian Dollar |
@singapore
Examples:
	| TimeType          | Narrative         | TaxCode                     | IncludeOtherProformas | FeeEarnerName      | Currency               |
	| Fixed-Capped Fees | test automation 1 | SG Output Domestic Standard | No                    | Lena EarnerSurname | SGD - Singapore Dollar |
@canada
Examples:
	| TimeType          | Narrative         | TaxCode                         | IncludeOtherProformas | FeeEarnerName      | Currency              |
	| Fixed-Capped Fees | test automation 1 | CA Output Domestic Standard GST | No                    | Lena EarnerSurname | CAD - Canadian Dollar |
@uk @training @staging
Examples:
	| TimeType | Narrative         | TaxCode                 | IncludeOtherProformas | FeeEarnerName      | Currency |
	| Fees     | test automation 1 | UK Output Domestic Zero | No                    | Johnny Westendsons | GBP      |
	   
@CancelProcess
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
	And remove a fee earner '<User>' from the user

@training @staging @canada @europe @uk @singapore @qa @ft
Examples:
	| User               |
	| Johnny Westendsons |

@CancelProcess @training @staging @canada @europe @uk @singapore @qa @ft
Scenario: 055 Validate Invoice GL has been posted
	Given I search for 'Workflow Dashboard'
	And I open the Invoice Dispatch workflow task
	And I send the invoice routed to finance team when dispatch method not set
	When I view the invoices
	And I view the gl postings
	Then I validate gl postings status
	And I close the gl postings

@training @staging @canada @europe @uk @singapore @qa @ft
Scenario: 060 verify the GL Detail Subledger enquiry for billing invoice
	Given I search for a process 'GL Detail Subledger enquiry' and select a chart 'GL Detail Subledger enquiry (GLDetailSubledgerInq)'
	When I create GL Detail Subledger 'BillingInvoice' report
	Then I verify the billing invoice report description

