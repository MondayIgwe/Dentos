@ignore
Feature: InvoiceManagerDisputed

A short summary of the feature
Bug: https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/58372

Scenario Outline: 010 Create a new Matter

	Given I create a user with details
		| UserName | DataRoleAlias | DefaultOperatingAlias   |
		| <User>   | Admin         | <DefaultOperatingAlias> |
	And I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	And I add a workflow user to a FeeEarner
		| User   | Name            |
		| <User> | <FeeEarnerName> |
	Given the charge type group exists
		| Code        | Description       | ChargeTypeGroupExcludeOrIncludeList |
		| SG_BILLABLE | <ChargeTypeGroup> | IsIncludeList                       |
	And the below charge type added to the group
		| Code            | Description  | TransactionType | Category          |
		| Withholding Tax | <ChargeType> | BOA Fees        | Billed on Account |
	And I create the Payer with Api
		| PayerName   | Entity        |
		| <PayorName> | LimeLight Ltd |
	And I create a matter with details:
		| Client   | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department | Section | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| <Client> | <FeeEarnerName>   | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | Bill                 | <Office> | Default    | Default | Desc_at_3e8glw7     | Desc_at_3e8glw7   | <PayorName> |


@singapore
Examples:
	| User      | Client        | FeeEarnerName         | Office    | ChargeType    | TaxCode                 | ChargeAmount | ChargeTypeGroup | Currency               | PayorName | DefaultOperatingAlias        |
	| Ann Water | Mister Client | DentonsAPI FeeEarner3 | Singapore | Miscellaneous | SG Output Domestic Zero | 500.00       | at_BOA_Group    | SGD - Singapore Dollar | Lara Moon | Dentons Rodyk & Davidson LLP |
@ft @pipelineTest
Examples:
	| User     | Client   | FeeEarnerName     | Office         | ChargeType    | TaxCode                    | ChargeAmount | ChargeTypeGroup | Currency            | PayorName | DefaultOperatingAlias          |
	| Ann Rose | Ann Rose | Doctor FeeEarner1 | London (UKIME) | Miscellaneous | UK Output Domestic Zero 0% | 500.00       | at_BOA_Group    | GBP - British Pound | ghgggggggg| Dentons UK and Middle East LLP |


Scenario Outline: 020 Create a proforma Edit
	When I post the disbursement
		| Work Date | Disbursement Type  | Work Currency  | Work Amount  | Narrative | Tax Code  |
		| {Today}-1 | <DisbursementType> | <WorkCurrency> | <WorkAmount> | {Auto}+36 | <TaxCode> |
	And I validate the disbursement is posted with no errors
	Then I can generate the proforma
		| Description | Include Other Proformas | Proforma Status |
		| {Auto}+36   | <IncludeOtherProformas> | Draft           |
	And I cancel the process

@ft
Examples:
	| DisbursementType    | WorkCurrency | WorkAmount | TaxCode                         | IncludeOtherProformas |
	| Advertising Charges | GBP          | 5000       | UK Output Domestic Standard 20% | No                    |

@singapore
Examples:
	| DisbursementType | WorkCurrency | WorkAmount | TaxCode                     | IncludeOtherProformas |
	| Court Fees (NT)  | SGD          | 5000       | SG Output Domestic Standard | No                    |

Scenario Outline: 030 Create a time entry/modify
	Given I submit a time modify
		| Time Type  | Hours   | Narrative   | FeeEarnerName   | WorkRate | WorkAmount | WorkCurrency | Tax Code  |
		| <TimeType> | <Hours> | <Narrative> | <FeeEarnerName> | 1        | 1          | <Currency>   | <TaxCode> |

@ft
Examples:
	| FeeEarnerName         | TimeType          | Hours | Narrative         | TaxCode                    | Currency            |
	| Doctor FeeEarner1 | Fixed-Capped Fees | 1     | test automation 1 | UK Output Domestic Zero 0% | GBP - British Pound |


Scenario: 031 Proforma Edit
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
@ft @singapore
Examples:
	| User     |
	| Ann Rose |

@CancelProcess
Scenario: 035 Start Notification task
	Given I search for 'Notification Start Task'
	And I start a notification task '<NotificationTask>'

@ft @training @staging @canada @europe @uk @singapore
Examples:
	| NotificationTask |
	| 2_Collections    |

@qa @ft @canada @europe @singapore @uk @training @staging
Scenario: 040 I want to view Doubtful invoices using Invoice Manager Matter
	Given I search for process 'Invoice Manager' without add button
	When I search the results using 'Matter' details
	And I want to open it
	Then I set status
		| Status       | Office         | Email                   |
		| Disputed - 1 | London (UKIME) | Disputed@dentons.global |
	And I can submit the record
	Then I want to view only 'Disputed' invoices
	And I want to see only 'Disputed' invoices
	 

