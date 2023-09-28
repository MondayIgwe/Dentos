@release8 @frd020 @MyBillableMattersAccessAndSearchParameters
Feature: MyBillableMattersAccessAndSearchParameters


@CancelProcess
Scenario Outline: 010 I want to add fee earner to use
	Given I create a user with details
		| UserName | DataRoleAlias |
		| <User>   | Admin         |
	And I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	Then I add a workflow user to a FeeEarner
		| User   | Name            |
		| <User> | <FeeEarnerName> |
	And the charge type group exists
		| Code        | Description       | ChargeTypeGroupExcludeOrIncludeList |
		| SG_BILLABLE | <ChargeTypeGroup> | IsIncludeList                       |
	And the below charge type added to the group
		| Code            | Description  | TransactionType | Category          |
		| Withholding Tax | <ChargeType> | BOA Fees        | Billed on Account |
	When I create the Payer with Api
		| PayerName   | Entity        |
		| <PayorName> | LimeLight Ltd |
	And I create a matter with details:
		| Client   | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department | Section | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| <Client> | <FeeEarnerName>   | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | Bill                 | <Office> | Default    | Default | Desc_at_3e8glw7     | Desc_at_3e8glw7   | <PayorName> |


@singapore
Examples:
	| User         | Client         | FeeEarnerName | Office    | ChargeType    | TaxCode                 | ChargeAmount | ChargeTypeGroup | Currency               | PayorName |
	| Cynthia Jose | Cynthia Client | Cynthia Jose  | Singapore | Miscellaneous | SG Output Domestic Zero | 500.00       | at_BOA_Group    | SGD - Singapore Dollar | Lara Moon |
@uk
Examples:
	| User         | Client         | FeeEarnerName | Office    | ChargeType    | TaxCode                         | ChargeAmount | ChargeTypeGroup | Currency              | PayorName |
	| Cynthia Jose | Cynthia Client | Cynthia Jose  | Vancouver | Miscellaneous | CA Output Domestic Standard GST | 500.00       | Desc_at_3e8glw7 | CAD - Canadian Dollar | Lara Moon |
@ft @pipelineTest
Examples:
	| User         | Client         | FeeEarnerName | Office         | ChargeType    | TaxCode                    | ChargeAmount | ChargeTypeGroup | Currency            | PayorName |
	| Cynthia Jose | Cynthia Client | Cynthia Jose  | London (UKIME) | Miscellaneous | UK Output Domestic Zero 0% | 500.00       | at_BOA_Group    | GBP - British Pound | Lara Moon |
@qa
Examples:
	| User         | Client         | FeeEarnerName | Office         | ChargeType    | TaxCode                        | ChargeAmount | ChargeTypeGroup | Currency            | PayorName |
	| Cynthia Jose | Cynthia Client | Cynthia Jose  | London (UKIME) | Miscellaneous | AE ICB Output Domestic Zero 0% | 500.00       | at_BOA_Group    | GBP - British Pound | Lara Moon |
@europe
Examples:
	| User         | Client         | FeeEarnerName | Office      | ChargeType    | TaxCode                   | ChargeAmount | ChargeTypeGroup | Currency | PayorName |
	| Cynthia Jose | Cynthia Client | Cynthia Jose  | London (EU) | Miscellaneous | EU Output Conversion Code | 500.00       | at_BOA_Group    | EUR      | Lara Moon |
@canada
Examples:
	| User         | Client         | FeeEarnerName | Office    | ChargeType    | TaxCode                         | ChargeAmount | ChargeTypeGroup | Currency              | PayorName |
	| Cynthia Jose | Cynthia Client | Cynthia Jose  | Vancouver | Miscellaneous | CA Output Domestic Standard GST | 500.00       | Desc_at_3e8glw7 | CAD - Canadian Dollar | Lara Moon |
@training @staging
Examples:
	| User         | Client         | FeeEarnerName | Office         | ChargeType    | TaxCode                     | ChargeAmount | ChargeTypeGroup | Currency            | PayorName |
	| Cynthia Jose | Cynthia Client | Cynthia Jose  | London (UKIME) | Miscellaneous | UK Output Domestic Standard | 500.00       | at_BOA_Group    | GBP - British Pound | Lara Moon |

@CancelProcess
Scenario Outline: 020 Disbursement Entry
	Given I create a hard cost disbursement type with details
		| Code               | Description        | TransactionTypeAlias  |
		| <DisbursementType> | <DisbursementType> | Anticipated Hard Cost |
	And I post the disbursement
		| Work Date | Disbursement Type  | Work Currency  | Work Amount  | Narrative | Tax Code  |
		| {Today}-1 | <DisbursementType> | <WorkCurrency> | <WorkAmount> | {Auto}+36 | <TaxCode> |
	Then I validate the disbursement is posted with no errors

@ft
Examples:
	| DisbursementType | WorkCurrency | WorkAmount | TaxCode                    |
	| Advertising Charges         | GBP          | 500        | UK Output Domestic Zero 0% |
@europe
Examples:
	| DisbursementType                    | WorkCurrency | WorkAmount | TaxCode                   |
	| DENEU-Dentons Cross Region Invoices | EUR          | 500        | EU Output Conversion Code |
@qa
Examples:
	| DisbursementType               | WorkCurrency | WorkAmount | TaxCode                    |
	| Automation Agency Registration | ZAR          | 500        | AE Output Domestic Zero 0% |
@singapore
Examples:
	| DisbursementType      | WorkCurrency | WorkAmount | TaxCode                     |
	| Application Fees (NT) | SGD          | 500        | SG Output Domestic Standard |
@canada
Examples:
	| DisbursementType           | WorkCurrency          | WorkAmount | TaxCode                         |
	| Bank of Canada Certificate | CAD - Canadian Dollar | 500        | CA Output Domestic Standard GST |
@uk
Examples:
	| DisbursementType                    | WorkCurrency | WorkAmount | TaxCode                     |
	| DENUK-Dentons Cross Region Invoices | GBP          | 500        | UK Output Domestic Standard |
@training
Examples:
	| DisbursementType          | WorkCurrency | WorkAmount | TaxCode                     |
	| Court Fees  - Anticipated | GBP          | 500        | UK Output Domestic Standard |
@staging
Examples:
	| DisbursementType | WorkCurrency | WorkAmount | TaxCode                     |
	| Advertising      | GBP          | 500        | UK Output Domestic Standard |

@CancelProcess
Scenario Outline: 030 Create a time entry/modify
	Given I submit a time modify
		| Time Type  | Hours   | Narrative   | FeeEarnerName   | WorkRate | WorkAmount | WorkCurrency | Tax Code  |
		| <TimeType> | <Hours> | <Narrative> | <FeeEarnerName> | 1        | 1          | <Currency>   | <TaxCode> |

@ft
Examples:
	| FeeEarnerName | TimeType          | Hours | Narrative         | TaxCode                    | Currency            |
	| Cynthia Jose  | Fixed-Capped Fees | 1     | test automation 1 | UK Output Domestic Zero 0% | GBP - British Pound |
@qa
Examples:
	| FeeEarnerName         | TimeType          | Hours | Narrative         | TaxCode                    | Currency            |
	| DentonsAPI FeeEarner3 | Fixed-Capped Fees | 1     | test automation 1 | UK Output Domestic Zero 0% | GBP - British Pound |
@singapore
Examples:
	| FeeEarnerName         | TimeType | Hours | Narrative         | TaxCode                     | Currency               |
	| DentonsAPI FeeEarner3 | FEES     | 1     | test automation 1 | SG Output Domestic Standard | SGD - Singapore Dollar |
@uk
Examples:
	| FeeEarnerName         | TimeType       | Hours | Narrative         | TaxCode                     | Currency            |
	| DentonsAPI FeeEarner3 | FEES (Default) | 0.01  | test automation 1 | UK Output Domestic Standard | GBP - British Pound |
@training @staging
Examples:
	| FeeEarnerName | TimeType | Hours | Narrative         | TaxCode                 | Currency            |
	| Cynthia Jose  | FEES     | 1     | test automation 1 | UK Output Domestic Zero | GBP - British Pound |
@europe
Examples:
	| FeeEarnerName | TimeType | Hours | Narrative         | TaxCode                   | Currency |
	| Cynthia Jose  | FEES     | 1     | test automation 1 | EU Output Conversion Code | EUR      |
@canada
Examples:
	| FeeEarnerName         | TimeType | Hours | Narrative         | TaxCode                         | Currency              |
	| DentonsAPI FeeEarner3 | FEES     | 1     | test automation 1 | CA Output Domestic Standard GST | CAD - Canadian Dollar |
	
@CancelProcess @ft @training @staging @canada @europe @uk @singapore @qa
Scenario: 031 I run the My Matters Metric
	Given I navigate to my matters metric process
	And I search or add new my matters metric
		| Matter Number Index | Search Value | TableName           | Description                    | Search Criteria |
		| 1                   | 1            | MyMattersSource_ccc | My Matters Metric - Automation | Not Equal To    |
	Then I submit it

@ft @training @staging @canada @europe @uk @singapore @qa
Scenario: 033 Verify that the user can search within the My Billable Matters process using Matter with WIP
	When I navigate to my billable matters process
	Then I enter details for the matter
	And I want to search for matters with 'WIP' and see results displayed

@ft @training @staging @canada @europe @uk @singapore @qa
Scenario: 035 Proforma generate Bill
	And I can generate the proforma
		| Description | Include Other Proformas | Proforma Status |
		| {Auto}+36   | No                      | Draft           |
	And I cancel the process

@CancelProcess
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

@training @staging @canada @europe @uk @singapore @qa @ft
Examples:
	| User         |
	| Cynthia Jose |

@CancelProcess @ft @training @staging @canada @europe @uk @singapore @qa
Scenario: 041 I run the My Matters Metric
	Given I navigate to my matters metric process
	And I search or add new my matters metric
		| Matter Number Index | Search Value | TableName           | Description                    | Search Criteria |
		| 1                   | 1            | MyMattersSource_ccc | My Matters Metric - Automation | Not Equal To    |
	Then I submit it

@CancelProcess @ft @training @staging @canada @europe @uk @singapore @qa
Scenario: 042 Verify that in the My Billable Matters process there are separate buttons for 'Search', 'Proforma Options' and 'Info Only Options'
	When I navigate to my billable matters process
	Then I verify that the process has separate buttons for search,proforma options and info only options

@CancelProcess @ft @training @staging @canada @europe @uk @singapore @qa
Scenario: 050 Verify that the user can search within the My Billable Matters process using Billable Clients
	When I navigate to my billable matters process
	Then I enter details for the client
	And I want to search and see results displayed

@CancelProcess @ft @training @staging @canada @europe @uk @singapore @qa
Scenario: 060 Verify that the user can search within the My Billable Matters process using Billable Fee Earner
	When I navigate to my billable matters process
	Then I enter details for the fee earner
	And I want to search and see results displayed

@CancelProcess @ft @training @staging @canada @europe @uk @singapore @qa
Scenario: 070 Verify that the user can search within the My Billable Matters process using Responsible Fee Earner
	When I navigate to my billable matters process
	Then I enter details for the fee earner
	And I want to search and see results displayed for 'Responsible' Fee Earner

@CancelProcess @ft @training @staging @canada @europe @uk @singapore @qa
Scenario: 080 Verify that the user can search within the My Billable Matters process using Supervising Fee Earner
	When I navigate to my billable matters process
	Then I enter details for the fee earner
	And I want to search and see results displayed for 'Supervising' Fee Earner

@CancelProcess @ft @training @staging @canada @europe @uk @singapore @qa
Scenario: 090 Verify that the user can search within the My Billable Matters process using Billable Matter
	When I navigate to my billable matters process
	Then I enter details for the matter
	And I want to search and see results displayed

@CancelProcess @ft @training @staging @canada @europe @uk @singapore @qa
Scenario: 092 Verify that the user can search within the My Billable Matters process using Billable Fee Earner with AR
	When I navigate to my billable matters process
	Then I enter details for the matter
	And I want to search for matters with 'AR' and see results displayed
