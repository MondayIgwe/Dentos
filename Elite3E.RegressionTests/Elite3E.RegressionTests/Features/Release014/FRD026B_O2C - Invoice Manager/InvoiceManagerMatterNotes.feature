
Feature: InvoiceManagerMatterNotes

A short summary of the feature


Scenario Outline: 010 Create a new Matter
	Given I create a user with details
		| UserName | DataRoleAlias | DefaultOperatingAlias   | UserRoleList   |
		| <User>   | Admin         | <DefaultOperatingAlias> | <UserRoleList> |
	And I search or create a client
		| Entity Name | FeeEarnerFullName |
		| <Client>    | <FeeEarnerName>   |
	And I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	And I add a workflow user to a FeeEarner
		| User   | Name            |
		| <User> | <FeeEarnerName> |
	And I create the Payer with Api
		| PayerName   | Entity        |
		| <PayorName> | LimeLight Ltd |
	And I create a matter with details:
		| Client   | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department | Section | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| <Client> | <FeeEarnerName>   | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | Bill                 | <Office> | Default    | Default | Desc_at_3e8glw7     | Desc_at_3e8glw7   | <PayorName> |


@singapore
Examples:
	| User        | Client            | FeeEarnerName | Office    | ChargeType    | TaxCode                 | ChargeAmount | ChargeTypeGroup | Currency               | PayorName | DefaultOperatingAlias        | UserRoleList |
	| Mickey Rose | MickeyRose Client | Mickey Rose   | Singapore | Miscellaneous | SG Output Domestic Zero | 500.00       | at_BOA_Group    | SGD - Singapore Dollar | Lara Moon | Dentons Rodyk & Davidson LLP |              |
@ft @pipelineTest
Examples:
	| User        | Client            | FeeEarnerName | Office         | ChargeType    | TaxCode                    | ChargeAmount | ChargeTypeGroup | Currency            | PayorName | DefaultOperatingAlias          | UserRoleList |
	| Mickey Rose | MickeyRose Client | Mickey Rose   | London (UKIME) | Miscellaneous | UK Output Domestic Zero 0% | 500.00       | at_BOA_Group    | GBP - British Pound | Lara Moon | Dentons UK and Middle East LLP |              |
@staging
Examples:
	| User        | Client            | FeeEarnerName | Office         | ChargeType    | TaxCode                     | ChargeAmount | ChargeTypeGroup | Currency            | PayorName | DefaultOperatingAlias          | UserRoleList                                                                |
	| Mickey Rose | MickeyRose Client | Mickey Rose   | London (UKIME) | Miscellaneous | UK Output Domestic Standard | 500.00       | at_BOA_Group    | GBP - British Pound | Lara Moon | Dentons UK and Middle East LLP | 0:AD:G:Common Authorisations,0:AD:G:System Administrator (read-only setups) |


Scenario Outline: 020 Create a proforma Edit
	When I post the disbursement
		| Work Date | Disbursement Type  | Work Currency  | Work Amount  | Narrative | Tax Code  |
		| {Today}-1 | <DisbursementType> | <WorkCurrency> | <WorkAmount> | {Auto}+36 | <TaxCode> |
	And I validate the disbursement is posted with no errors
	Then I can generate the proforma
		| Description | Include Other Proformas | Proforma Status |
		| {Auto}+36   | <IncludeOtherProformas> | Draft           |
	And I cancel the process

@ft @training @canada @europe @uk
Examples:
	| DisbursementType    | WorkCurrency | WorkAmount | TaxCode                         | IncludeOtherProformas |
	| Advertising Charges | GBP          | 5000       | UK Output Domestic Standard 20% | No                    |
@staging
Examples:
	| DisbursementType            | WorkCurrency | WorkAmount | TaxCode                     | IncludeOtherProformas |
	| Bank & Finance Charges (NT) | GBP          | 5000       | UK Output Domestic Standard | No                    |
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
	| FeeEarnerName | TimeType          | Hours | Narrative         | TaxCode                         | Currency            |
	| Mickey        | Fixed-Capped Fees | 1     | test automation 1 | UK Output Domestic Standard 20% | GBP - British Pound |
@training @canada @europe @uk @singapore
Examples:
	| FeeEarnerName | TimeType          | Hours | Narrative         | TaxCode                     | Currency            |
	| Mickey        | Fixed-Capped Fees | 1     | test automation 1 | UK Output Domestic Standard | GBP - British Pound |
	
@staging
Examples:
	| FeeEarnerName | TimeType          | Hours | Narrative         | TaxCode                     | Currency            |
	| Mickey        | Fixed-Capped Fees | 1     | test automation 1 | UK Output Domestic Standard | GBP - British Pound |


@ft @training @staging @canada @europe @uk @singapore
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

@ft @training @staging @canada @europe @uk @singapore
Examples:
	| User        |
	| Mickey Rose |

@CancelProcess
Scenario: 035 Start Notification task
	Given I search for 'Notification Start Task'
	And I start a notification task '<NotificationTask>'

@ft @training @staging @canada @europe @uk @singapore
Examples:
	| NotificationTask |
	| 2_Collections    |



@qa @ft @canada @europe @singapore @uk @training @staging
Scenario: 040 I want to view Client and Matter notes using Invoice Manager Matter
	Given I search for process 'Invoice Manager' without add button
	When I search the results using 'Client' details
	Then I want to add a collection note
		| Comment   |
		| {Auto}+36 |
	And I want to view the matter notes