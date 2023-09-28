@release8 @frd028 @VerifyOptOutBillingRulesvalidationOnPerformaGeneration
Feature: VerifyOptOutBillingRulesvalidationOnPerformaGeneration
create a client with billing rules validation list
and create a matter with the client and optout validation, and add disbursment entry, and run proforma generation

@CancelProcess
Scenario Outline: 001 Prepare required data for the billing rule validation list
	Given I create a user with details
		| UserName | DataRoleAlias |
		| <User>   | Admin         |
	And I create a fee earner with details
		| EntityName          |
		| <FeeEarnerFullName> |
	Then I add a workflow user to a FeeEarner
		| User   | Name                |
		| <User> | <FeeEarnerFullName> |
	And I create the Payer with Api
		| PayerName   | Entity        |
		| <PayorName> | LimeLight Ltd |
	And I create a matter with details:
		| Client   | FeeEarnerFullName   | Status   | OpenDate   | MatterName   | Currency   | MatterCurrencyMethod   | Office   | Department   | Section   | Rate   | ChargeTypeGroupName   | CostTypeGroupName   | PayorName   |
		| <Client> | <FeeEarnerFullName> | <Status> | <OpenDate> | <MatterName> | <Currency> | <MatterCurrencyMethod> | <Office> | <Department> | <Section> | <Rate> | <ChargeTypeGroupName> | <CostTypeGroupName> | <PayorName> |

@ft @training @staging @uk @qa
Examples:
	| User         | Client         | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency            | MatterCurrencyMethod | Office         | Department | Section | Rate     | ChargeTypeGroupName | CostTypeGroupName | PayorName |
	| Johnson Rose | Johnson Client | Johnson Rose      | Fully Open | {Today}-1 | {Auto}+36  | GBP - British Pound | Bill                 | London (UKIME) | Default    | Default | Standard | FixedChargeType     | FixedCostType     | Lara Moon |
@canada
Examples:
	| User         | Client         | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency              | MatterCurrencyMethod | Office    | Department | Section | Rate     | ChargeTypeGroupName | CostTypeGroupName | PayorName |
	| Johnson Rose | Johnson Client | Johnson Rose      | Fully Open | {Today}-1 | {Auto}+36  | CAD - Canadian Dollar | Bill                 | Vancouver | Default    | Default | Standard | FixedChargeType     | FixedCostType     | Janie Key |
@europe
Examples:
	| User         | Client         | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency | MatterCurrencyMethod | Office      | Department | Section | Rate     | ChargeTypeGroupName | CostTypeGroupName | PayorName |
	| Johnson Rose | Johnson Client | Johnson Rose      | Fully Open | {Today}-1 | {Auto}+36  | EUR      | Bill                 | London (EU) | Default    | Default | Standard | FixedChargeType     | FixedCostType     | Janie Key |
@singapore
Examples:
	| User         | Client         | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency | MatterCurrencyMethod | Office    | Department            | Section | Rate     | ChargeTypeGroupName | CostTypeGroupName | PayorName |
	| Johnson Rose | Johnson Client | Johnson Rose      | Fully Open | {Today}-1 | {Auto}+36  | SGD      | Bill                 | Singapore | Corporate - Singapore | Default | Standard | FixedChargeType     | FixedCostType     | Janie Key |

Scenario Outline: 010 Create a billing rule validation list
	Given I search or create Cost and time billing rule validation list
		| Code                 | Description          |
		| Proforma Date period | Proforma Date period |
	And I search or create a client with billing rules
		| Entity Name | BillingRulesValidationList | FeeEarnerFullName   |
		| <Client>    | Proforma Date period       | <FeeEarnerFullName> |
	Then the charge type group exists
		| Code   | Description           | ChargeTypeGroupExcludeOrIncludeList |
		| at_BOA | <ChargeTypeGroupName> | IsIncludeList                       |
	And the below charge type added to the group
		| Code   | Description           | TransactionType | Category          |
		| at_BOA | <ChargeTypeGroupName> | Fees            | Billed on Account |
	
@ft @training @staging @uk @qa @CancelProcess
Examples:
	| Client       | FeeEarnerFullName | ChargeTypeGroupName |
	| Johnson Client | Johnson Rose      | FixedChargeType     |
@canada
Examples:
	| Client        | FeeEarnerFullName        | ChargeTypeGroupName |
	| Mister Client | RuleValidation FeeEarner | FixedChargeType     |
@singapore
Examples:
	| Client        | FeeEarnerFullName        | ChargeTypeGroupName |
	| Mister Client | RuleValidation FeeEarner | FixedChargeType     |
@europe
Examples:
	| Client        | FeeEarnerFullName        | ChargeTypeGroupName |
	| Mister Client | RuleValidation FeeEarner | FixedChargeType     |
@singapore
Examples:
	| Client        | FeeEarnerFullName        | ChargeTypeGroupName |
	| Mister Client | RuleValidation FeeEarner | FixedChargeType     |

@training @staging @canada @europe @uk @singapore @ft @qa
Scenario Outline: 020 Billing rules Optout
	When I search for the matter
	And I selct billing rules optout
	And I submit it

Scenario Outline: 030 Charge and Disbursment Entry
	Given I create a charge type with details
		| ChargeCode | Description  | CategoryInput | TransactionTypeAlias | Active |
		| {Auto}+10  | <ChargeType> | Other         | Miscellaneous Income | Yes    |
	And I add a charge entry
		| Charge Type  | Amount | Tax Code  |
		| <ChargeType> | 300.00 | <TaxCode> |
	When I create a hard cost disbursement type with details
		| Code               | Description        |
		| <DisbursementType> | <DisbursementType> |
	And I post the disbursement
		| Work Date | Disbursement Type  | Work Currency  | Work Amount  | Narrative | Tax Code  |
		| {Today}-1 | <DisbursementType> | <WorkCurrency> | <WorkAmount> | {Auto}+36 | <TaxCode> |
	Then I validate the disbursement is posted with no errors

@ft
Examples:
	| ChargeType    | DisbursementType | WorkCurrency | WorkAmount | TaxCode                    |
	| Miscellaneous | Agency Registration         | GBP          | 500        | AE Output Domestic Exempt 0% |
@singapore
Examples:
	| ChargeType    | DisbursementType      | WorkCurrency           | WorkAmount | TaxCode                 |
	| Miscellaneous | Application Fees (NT) | SGD - Singapore Dollar | 500        | SG Output Domestic Zero |
@canada
Examples:
	| ChargeType    | DisbursementType           | WorkCurrency | WorkAmount | TaxCode                         |
	| Miscellaneous | Bank of Canada Certificate | CAD          | 500        | CA Output Domestic Standard GST |
@training @staging
Examples:
	| ChargeType    | DisbursementType | WorkCurrency        | WorkAmount | TaxCode                 |
	| Miscellaneous | Advertising      | GBP - British Pound | 500        | UK Output Domestic Zero |
@uk
Examples:
	| ChargeType    | DisbursementType                    | WorkCurrency        | WorkAmount | TaxCode                 |
	| Miscellaneous | DENUK-Dentons Cross Region Invoices | GBP - British Pound | 500        | UK Output Domestic Zero |
@qa
Examples:
	| ChargeType    | DisbursementType               | WorkCurrency        | WorkAmount | TaxCode                    |
	| Miscellaneous | Automation Agency Registration | GBP - British Pound | 500        | UK Output Domestic Zero 0% |
@europe
Examples:
	| ChargeType    | DisbursementType                    | WorkCurrency | WorkAmount | TaxCode                   |
	| Miscellaneous | DENEU-Dentons Cross Region Invoices | EUR          | 500        | EU Output Conversion Code |

Scenario Outline: 040 Create a time modify
	When I submit a time modify
		| Time Type  | Hours | Narrative | FeeEarnerName   | WorkRate | WorkAmount | WorkCurrency | Tax Code  |
		| <TimeType> | 1     | {Auto}+10 | <FeeEarnerName> | 1        | 1          | <Currency>   | <TaxCode> |
	Then I can post all the time entries
	And I can generate the proforma
		| Description | Include Other Proformas | Proforma Status |
		| {Auto}+36   | <IncludeOtherProformas> | Draft           |
@ft
Examples:
	| TimeType          | TaxCode                    | IncludeOtherProformas | FeeEarnerName               | Currency            |
	| Fixed-Capped Fees | UK Output Domestic Zero 0% | No                    | Johnson Rose| GBP - British Pound |
@qa
Examples:
	| TimeType          | TaxCode                    | IncludeOtherProformas | FeeEarnerName            | Currency            |
	| Fixed-Capped Fees | UK Output Domestic Zero 0% | No                    | RuleValidation FeeEarner | GBP - British Pound |
@singapore
Examples:
	| TimeType          | TaxCode                 | IncludeOtherProformas | FeeEarnerName            | Currency               |
	| Fixed-Capped Fees | SG Output Domestic Zero | No                    | RuleValidation FeeEarner | SGD - Singapore Dollar |
@canada
Examples:
	| TimeType          | TaxCode                         | IncludeOtherProformas | FeeEarnerName            | Currency |
	| Fixed-Capped Fees | CA Output Domestic Standard GST | No                    | RuleValidation FeeEarner | CAD      |
@uk @training @staging
Examples:
	| TimeType | TaxCode                 | IncludeOtherProformas | FeeEarnerName | Currency            |
	| Fees     | UK Output Domestic Zero | No                    | Johnson Rose  | GBP - British Pound |
@europe
Examples:
	| TimeType          | TaxCode                   | IncludeOtherProformas | FeeEarnerName            | Currency |
	| Fixed-Capped Fees | EU Output Conversion Code | No                    | RuleValidation FeeEarner | EUR      |

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
	And bill it without printing
	Then the invoice is generated
	And I remove workflow user from fee earner
		| Search Column      | Search Operator | Search Value | FeeEarnerUser |
		| Workflow User.Name | Equals          | null         | <User>        |

@training @staging @canada @europe @uk @singapore @qa @ft
Examples:
	| User         |
	| Johnson Rose |
	
