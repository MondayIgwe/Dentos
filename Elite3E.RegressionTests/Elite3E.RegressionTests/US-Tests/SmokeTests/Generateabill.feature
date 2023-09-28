@us
Feature: Generateabill

Scenario Outline: 010 create matter
nario Outline: 010 API Prep Data
	Given I create a user with details
		| UserName | DataRoleAlias | UserRoleList                                                                               |
		| <User>   | Admin         | 0:AD:G:Common Authorisations,0:WC:P:Proforma Processor,0:WC:P:Proforma Processor - Finance |
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


Examples:
	| User      | FeeEarnerName | Client          | Currency        | CurrencyMethod | Office  | Department | Section | Rate     | ChargeTypeGroup | CostTypeGroup | ChargeType    | TaxCode                          | PayorName  | DefaultOperatingAlias      |
	| James Lee | James Lee     | JamesLee Client | USD - US Dollar | Bill           | Chicago | Default    | Default | Standard | Desc_at_3e8glw7 | Auto__18Xeq   | Miscellaneous | US Output Domestic Standard Rate | Dave Peter | Dentons United States, LLP |

Scenario Outline: 020 Disbursement Entry
	Given I create a hard cost disbursement type with details
		| Code               | Description        | TransactionTypeAlias |
		| <DisbursementType> | <DisbursementType> | TestAlias            |
	Given I post the disbursement
		| Work Date | Disbursement Type  | Work Currency  | Work Amount  | Narrative | Tax Code  |
		| {Today}-1 | <DisbursementType> | <WorkCurrency> | <WorkAmount> | {Auto}+36 | <TaxCode> |
	When I validate the disbursement is posted with no errors

Examples:
	| DisbursementType              | WorkCurrency    | WorkAmount | TaxCode                          |
	| Dentons United States Funding | USD - US Dollar | 500        | US Output Domestic Standard Rate |

Scenario Outline: 030 Create a time entry/modify
	And I submit a time modify
		| Time Type  | Hours   | Narrative   | FeeEarnerName   | WorkRate | WorkAmount | WorkCurrency | Tax Code  |
		| <TimeType> | <Hours> | <Narrative> | <FeeEarnerName> | 1        | 1          | <Currency>   | <TaxCode> |
	And I can generate the proforma
		| Description | Include Other Proformas | Proforma Status |
		| {Auto}+36   | <IncludeOtherProformas> | Draft           |

Examples:
	| FeeEarnerName | TimeType          | Hours | Narrative         | IncludeOtherProformas | Currency        | TaxCode                          |
	| James Lee     | Fixed-Capped Fees | 1     | test automation 1 | No                    | USD - US Dollar | US Output Domestic Standard Rate |

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
	And I search for 'Workflow Dashboard'
	And I open the Invoice Dispatch workflow task
	When I send the invoice routed to finance team when dispatch method not set

Examples:
	| User      |
	| James Lee |

@CancelProcess
Scenario: 050 Confirm the invoice is created
	When I view the invoices
	Then confirm invoice field exist
























