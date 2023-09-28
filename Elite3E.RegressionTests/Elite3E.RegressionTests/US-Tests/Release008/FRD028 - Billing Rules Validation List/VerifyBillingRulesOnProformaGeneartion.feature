@ignore @us
Feature: VerifyBillingRulesOnProformaGeneartion
create a client with billing rules validation list
and create a matter with the client, and add disbursment entry, and run proforma generation
defect: https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/39303/
@CancelProcess
Scenario Outline: 001 Prepare required data for the billing rule validation list
	Given I create a user with details
		| UserName | DataRoleAlias |
		| <User>   | Admin         |
	And I create a fee earner with details
		| EntityName          |
		| <FeeEarnerFullName> |
	Then I add a workflow user to a FeeEarner
		| User   | Name            |
		| <User> | <FeeEarnerName> |
	And I create the Payer with Api
		| PayerName   | Entity              |
		| <PayorName> | <FeeEarnerFullName> |
	Then I create a matter with details:
		| Client   | FeeEarnerFullName   | Status   | OpenDate   | MatterName   | Currency   | MatterCurrencyMethod   | Office   | Department   | Section   | Rate   | ChargeTypeGroupName   | CostTypeGroupName   | PayorName   |
		| <Client> | <FeeEarnerFullName> | <Status> | <OpenDate> | <MatterName> | <Currency> | <MatterCurrencyMethod> | <Office> | <Department> | <Section> | <Rate> | <ChargeTypeGroupName> | <CostTypeGroupName> | <PayorName> |

Examples:
	| User         | Client         | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency        | MatterCurrencyMethod | Office  | Department | Section | Rate     | ChargeTypeGroupName | CostTypeGroupName | PayorName |
	| Cynthia Jose | Cynthia Client | Cynthia Jose      | Fully Open | {Today}-1 | {Auto}+36  | USD - US Dollar | Bill                 | Chicago | Default    | Default | Standard | FixedChargeType     | FixedCostType     | Janie Key |

@CancelProcess
Scenario Outline: 010 Create a billing rule validation list
	Given I search or create Cost and time billing rule validation list
		| Code                 | Description          |
		| Proforma Date period | Proforma Date period |
	And I search or create a client with billing rules
		| Entity Name | BillingRulesValidationList | FeeEarnerFullName   |
		| <Client>    | Proforma Date period       | <FeeEarnerFullName> |
	And the charge type group exists
		| Code   | Description       | ChargeTypeGroupExcludeOrIncludeList |
		| at_BOA | <ChargeTypeGroup> | IsIncludeList                       |
	And the below charge type added to the group
		| Code   | Description       | TransactionType | Category          |
		| at_BOA | <ChargeTypeGroup> | Fees            | Billed on Account |

Examples:
	| Client        | FeeEarnerFullName        | ChargeTypeGroup |
	| Mister Client | RuleValidation FeeEarner | FixedChargeType |

		
@CancelProcess
Scenario Outline: 020 Charge and Disbursment Entry
	When I create a charge type with details
		| ChargeCode | Description  | CategoryInput | TransactionTypeAlias | Active |
		| {Auto}+10  | <ChargeType> | Other         | Miscellaneous Income | Yes    |
	And I add a charge entry
		| Charge Type  | Amount | Tax Code  |
		| <ChargeType> | 300.00 | <TaxCode> |
	Given I create a hard cost disbursement type with details
		| Code               | Description        |
		| <DisbursementType> | <DisbursementType> |
	And I post the disbursement
		| Work Date | Disbursement Type  | Work Currency  | Work Amount  | Narrative | Tax Code  |
		| {Today}-1 | <DisbursementType> | <WorkCurrency> | <WorkAmount> | {Auto}+36 | <TaxCode> |
	Then I validate the disbursement is posted with no errors

Examples:
	| ChargeType    | DisbursementType                    | WorkCurrency | WorkAmount | TaxCode                          |
	| Miscellaneous | DENUS-Dentons Cross Region Invoices | USD          | 500        | US Output Domestic Standard Rate |

		
@CancelProcessc
Scenario Outline: 030 Create a time modify
	When I submit a time modify
		| Time Type  | Hours | Narrative | FeeEarnerName   | WorkRate | WorkAmount | WorkCurrency | Tax Code  |
		| <TimeType> | 1     | {Auto}+10 | <FeeEarnerName> | 1        | 1          | <Currency>   | <TaxCode> |
	And I can generate the proforma
		| Description | Include Other Proformas | Proforma Status |
		| {Auto}+36   | <IncludeOtherProformas> | Draft           |

Examples:
	| TimeType          | TaxCode                          | IncludeOtherProformas | FeeEarnerName            | Currency        |
	| Fixed-Capped Fees | US Output Domestic Standard Rate | No                    | RuleValidation FeeEarner | USD - US Dollar |

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
	When bill it without printing
	Then Proforma edit should show following errors:
		| Error                                                                |
		| Billing Rule Validation Errors were found from the Bill Error stage. |
		| Error: Cards exist before or after the proforma period interval.     |
		| Error: Cards exist before or after the proforma period interval.     |

Examples:
	| User         |
	| Cynthia Jose |