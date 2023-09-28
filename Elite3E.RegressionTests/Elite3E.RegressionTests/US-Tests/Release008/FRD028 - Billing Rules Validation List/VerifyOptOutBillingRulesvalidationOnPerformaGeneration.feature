@us
Feature: VerifyOptOutBillingRulesvalidationOnPerformaGeneration
create a client with billing rules validation list
and create a matter with the client and optout validation, and add disbursment entry, and run proforma generation

@CancelProcess
Scenario Outline: 001 Prepare required data for the billing rule validation list
	Given I create a user with details
		| UserName | DataRoleAlias | UserRoleList                                                                               |
		| <User>   | Admin         | 0:AD:G:Common Authorisations,0:WC:P:Proforma Processor,0:WC:P:Proforma Processor - Finance |
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

Examples:
	| User         | Client         | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency        | MatterCurrencyMethod | Office  | Department | Section | Rate     | ChargeTypeGroupName | CostTypeGroupName | PayorName |
	| Johnson Rose | Johnson Client | Johnson Rose      | Fully Open | {Today}-1 | {Auto}+36  | USD - US Dollar | Bill                 | Chicago | Default    | Default | Standard | FixedChargeType     | FixedCostType     | Janie Key |

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
	
Examples:
	| Client        | FeeEarnerFullName        | ChargeTypeGroupName |
	| Mister Client | RuleValidation FeeEarner | FixedChargeType     |


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

Examples:
	| ChargeType    | DisbursementType                      | WorkCurrency | WorkAmount | TaxCode                          |
	| Miscellaneous | Court & Stamp Fees - Anticipated (NT) | USD          | 500        | US Output Domestic Standard Rate |

Scenario Outline: 040 Create a time modify
	When I submit a time modify
		| Time Type  | Hours | Narrative | FeeEarnerName   | WorkRate | WorkAmount | WorkCurrency | Tax Code  |
		| <TimeType> | 1     | {Auto}+10 | <FeeEarnerName> | 1        | 1          | <Currency>   | <TaxCode> |
	Then I can post all the time entries
	And I can generate the proforma
		| Description | Include Other Proformas | Proforma Status |
		| {Auto}+36   | <IncludeOtherProformas> | Draft           |

Examples:
	| TimeType          | TaxCode                          | IncludeOtherProformas | FeeEarnerName            | Currency        |
	| Fixed-Capped Fees | US Output Domestic Standard Rate | No                    | RuleValidation FeeEarner | USD - US Dollar |

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
	And bill it without printing
	Then the invoice is generated

Examples:
	| User         |
	| Johnson Rose |
	
