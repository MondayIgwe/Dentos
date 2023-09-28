@smoke @Generateabill
Feature: Generateabill

@CancelProcess
Scenario Outline: 010 API Prep Data
	Given I create a user with details
		| UserName | DataRoleAlias | User Role List                                                              |
		| <User>   | Admin         | 0:AD:G:Common Authorisations,0:AD:G:System Administrator (read-only setups) |
	And I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	And I add a workflow user to a FeeEarner
		| User   | Name            |
		| <User> | <FeeEarnerName> |
	Given I search or create a client with fee earner
		| Entity Name | Fee Earner Full Name |
		| <Client>    | <FeeEarnerName>      |
	And  I create the Payer with Api
		| PayerName   | Entity        |
		| <PayorName> | SeasonsEntity |
	When I create a matter with details:
		| Client   | Status     | OpenDate   | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | Rate   | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| <Client> | Fully Open | {Today}-31 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <Rate> | <ChargeTypeGroup>   | <CostTypeGroup>   | <PayorName> |
	And I create a charge type with details
		| ChargeCode | Description  | CategoryInput | TransactionTypeAlias | Active |
		| {Auto}+10  | <ChargeType> | Other         | Miscellaneous Income | Yes    |
	And I add a charge entry
		| Charge Type  | Amount | Tax Code  |
		| <ChargeType> | 300.00 | <TaxCode> |

@ft
Examples:
	| User      | FeeEarnerName | Client          | Currency            | CurrencyMethod | Office         | Department | Section | Rate     | ChargeTypeGroup | CostTypeGroup   | ChargeType    | TaxCode                    | PayorName  | DefaultOperatingAlias          |
	| James Lee | James Lee     | JamesLee Client | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Standard | TestGroup3      | Desc_at_3e8glw7 | Miscellaneous | AE Output Domestic Zero 0% | Dave Peter | Dentons UK and Middle East LLP |
@training @staging
Examples:
| User      | FeeEarnerName | Client          | Currency | CurrencyMethod | Office         | Department | Section | Rate     | ChargeTypeGroup | CostTypeGroup   | ChargeType    | TaxCode                 | PayorName  | DefaultOperatingAlias          |
| James Lee | James Lee     | JamesLee Client | GBP      | Bill           | London (UKIME) | Default    | Default | Standard | TestGroup3      | Desc_at_3e8glw7 | Miscellaneous | UK Output Domestic Zero | Dave Peter | Dentons UK and Middle East LLP |
@qa
Examples:
	| User      | FeeEarnerName | Client          | Currency            | CurrencyMethod | Office         | Department | Section | Rate     | ChargeTypeGroup | CostTypeGroup | ChargeType                                              | TaxCode                    | PayorName  | DefaultOperatingAlias          |
	| James Lee | James Lee     | JamesLee Client | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Standard | TestGroup3      | TestGroup3    | Admin Charges (e.g. On-boarding fee, data storage etc.) | AE Output Domestic Zero 0% | Dave Peter | Dentons UK and Middle East LLP |
@europe
Examples:
	| User      | FeeEarnerName | Client          | Currency   | CurrencyMethod | Office      | Department | Section | Rate          | ChargeTypeGroup | CostTypeGroup | ChargeType    | TaxCode               | PayorName  | DefaultOperatingAlias   |
	| James Lee | James Lee     | JamesLee Client | EUR - Euro | Bill           | London (EU) | Default    | Default | Standard Rate | Desc_at_3e8glw7 | Auto__18Xeq   | Miscellaneous | ES Output Europe Zero | Dave Peter | Dentons Europe LLP (UK) |
@canada
Examples:
	| User      | FeeEarnerName | Client          | Currency              | CurrencyMethod | Office    | Department | Section           | Rate     | ChargeTypeGroup | CostTypeGroup | ChargeType    | TaxCode                         | PayorName  | DefaultOperatingAlias |
	| James Lee | James Lee     | JamesLee Client | CAD - Canadian Dollar | Bill           | Vancouver | Default    | Banking & Finance | Standard | Desc_at_3e8glw7 | Auto__18Xeq   | Miscellaneous | CA Output Domestic Standard GST | Dave Peter | Dentons Canada LLP    |
@uk
Examples:
	| User      | FeeEarnerName | Client          | Currency            | CurrencyMethod | Office         | Department | Section | Rate     | ChargeTypeGroup | CostTypeGroup | ChargeType    | TaxCode                 | PayorName  | DefaultOperatingAlias          |
	| James Lee | James Lee     | JamesLee Client | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Standard | Desc_at_3e8glw7 | Auto__18Xeq   | Miscellaneous | UK Output Domestic Zero | Dave Peter | Dentons UK and Middle East LLP |
@singapore
Examples:
| User      | FeeEarnerName | Client          | Currency               | CurrencyMethod | Office    | Department            | Section | Rate     | ChargeTypeGroup | CostTypeGroup | ChargeType    | TaxCode                     | PayorName  | DefaultOperatingAlias        |
| James Lee | James Lee     | JamesLee Client | SGD - Singapore Dollar | Bill           | Singapore | Corporate - Singapore | Default | Standard | Desc_at_3e8glw7 | Auto__18Xeq   | Miscellaneous | SG Output Domestic Standard | Dave Peter | Dentons Rodyk & Davidson LLP |

Scenario Outline: 020 Disbursement Entry
	Given I create a hard cost disbursement type with details
		| Code               | Description        | TransactionTypeAlias |
		| <DisbursementType> | <DisbursementType> | TestAlias            |
	Given I post the disbursement
		| Work Date | Disbursement Type  | Work Currency  | Work Amount  | Narrative | Tax Code  |
		| {Today}-1 | <DisbursementType> | <WorkCurrency> | <WorkAmount> | {Auto}+36 | <TaxCode> |
	When I validate the disbursement is posted with no errors

@ft
Examples:
	| DisbursementType    | WorkCurrency | WorkAmount | TaxCode                    |
	| Advertising Charges | GBP          | 500        | UK Output Domestic Zero 0% |
@singapore
Examples:
	| DisbursementType                    | WorkCurrency | WorkAmount | TaxCode                   |
	| DENSG-Dentons Cross Region Invoices | SGD          | 500        | SG Output Domestic Exempt |
@staging
Examples:
	| DisbursementType            | WorkCurrency | WorkAmount | TaxCode                 |
	| Bank & Finance Charges (NT) | GBP          | 500        | UK Output Domestic Zero |
@training
Examples:
	| DisbursementType          | WorkCurrency | WorkAmount | TaxCode                 |
	| Court Fees  - Anticipated | GBP          | 500        | UK Output Domestic Zero |
@qa
Examples:
	| DisbursementType | WorkCurrency | WorkAmount | TaxCode                 |
	| Agents Fees (NT) | GBP          | 500        | UK Output Domestic Zero |
@uk
Examples:
	| DisbursementType                    | WorkCurrency | WorkAmount | TaxCode                     |
	| DENUK-Dentons Cross Region Invoices | ZAR          | 500        | UK Output Domestic Standard |
@europe
Examples:
	| DisbursementType          | WorkCurrency | WorkAmount | TaxCode               |
	| Court Fees  - Anticipated | EUR          | 500        | ES Output Europe Zero |
@canada
Examples:
	| DisbursementType           | WorkCurrency          | WorkAmount | TaxCode                         |
	| Bank of Canada Certificate | CAD - Canadian Dollar | 500        | CA Output Domestic Standard GST |

Scenario Outline: 030 Create a time entry/modify
	And I submit a time modify
		| Time Type  | Hours   | Narrative   | FeeEarnerName   | WorkRate | WorkAmount | WorkCurrency | Tax Code  |
		| <TimeType> | <Hours> | <Narrative> | <FeeEarnerName> | 1        | 1          | <Currency>   | <TaxCode> |
	And I can generate the proforma
		| Description | Include Other Proformas | Proforma Status |
		| {Auto}+36   | <IncludeOtherProformas> | Draft           |
@ft
Examples:
	| FeeEarnerName | TimeType          | Hours | Narrative         | IncludeOtherProformas | Currency            | TaxCode                    |
	| James Lee     | Fixed-Capped Fees | 1     | test automation 1 | No                    | GBP - British Pound | UK Output Domestic Zero 0% |
@qa
Examples:
	| FeeEarnerName | TimeType          | Hours | Narrative         | IncludeOtherProformas | Currency            | TaxCode                    |
	| James Lee     | Fixed-Capped Fees | 1     | test automation 1 | No                    | GBP - British Pound | UK Output Domestic Zero 0% |
@uk
Examples:
	| FeeEarnerName | TimeType          | Hours | Narrative         | IncludeOtherProformas | Currency            | TaxCode                 |
	| James Lee     | Fixed-Capped Fees | 1     | test automation 1 | No                    | GBP - British Pound | AE Output Domestic Zero |
@europe
Examples:
	| FeeEarnerName | TimeType          | Hours | Narrative         | IncludeOtherProformas | Currency   | TaxCode               |
	| James Lee     | Fixed-Capped Fees | 1     | test automation 1 | No                    | EUR - Euro | ES Output Europe Zero |
@canada
Examples:
	| FeeEarnerName | TimeType          | Hours | Narrative         | IncludeOtherProformas | Currency              | TaxCode                         |
	| James Lee     | Fixed-Capped Fees | 1     | test automation 1 | No                    | CAD - Canadian Dollar | CA Output Domestic Standard GST |
@training @staging
Examples:
	| FeeEarnerName | TimeType          | Hours | Narrative         | IncludeOtherProformas | Currency | TaxCode                 |
	| James Lee     | Fixed-Capped Fees | 1     | test automation 1 | No                    | GBP      | UK Output Domestic Zero |
@singapore
Examples:
	| FeeEarnerName | TimeType          | Hours | Narrative         | IncludeOtherProformas | Currency               | TaxCode                     |
	| James Lee     | Fixed-Capped Fees | 1     | test automation 1 | No                    | SGD - Singapore Dollar | SG Output Domestic Standard |

@CancelProcess @ft @training @staging @canada @europe @uk @singapore @qa
Scenario: 040 Create a Proforma Edit and generate Bill
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
	And I search for 'Workflow Dashboard'
	And I open the Invoice Dispatch workflow task
	When I send the invoice routed to finance team when dispatch method not set

Examples:
	| User      |
	| James Lee |

@CancelProcess @ft @training @staging @canada @europe @uk @singapore @qa
Scenario: 050 Confirm the invoice is created
	When I view the invoices
	Then confirm invoice field exist
























