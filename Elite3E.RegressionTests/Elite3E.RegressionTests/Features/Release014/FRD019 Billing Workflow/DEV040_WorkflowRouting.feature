Feature: DEV040_WorkflowRouting

Verify that a proforma can be generated via My billable matters process

@CancelProcess
Scenario Outline: 010 Create a new Matter and data preparation
	Given I create the Payer with Api
		| PayerName   | Entity       |
		| <PayorName> | CentralCoast |
	When I create a user with details
		| UserName | DataRoleAlias | UserRoleList   |
		| <User>   | Admin         | <UserRoleList> |
	Then I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	And I add a workflow user to a FeeEarner
		| User   | Name            |
		| <User> | <FeeEarnerName> |
	And I search or create a client
		| Entity Name | FeeEarnerFullName |
		| <Client>    | <FeeEarnerName>   |
	And I create a matter with details:
		| Client   | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | ChargeTypeGroupName | CostTypeGroupName | BillingOffice   | PayorName   |
		| <Client> | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <ChargeTypeGroup>   | <CostTypeGroup>   | <BillingOffice> | <PayorName> |

@ft @training @qa @uk
Examples:
	| FeeEarnerName | User       | Client       | Currency            | CurrencyMethod | Office  | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName | DefaultOperatingAlias          | UserRoleList |
	| FRD19 John    | FRD19 John | FRD19 Client | GBP - British Pound | Bill           | Default | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | London (EU)   | James May | Dentons UK and Middle East LLP |              |
@staging
Examples:
	| FeeEarnerName | User       | Client       | Currency            | CurrencyMethod | Office  | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName | DefaultOperatingAlias          | UserRoleList                                                                |
	| FRD19 John    | FRD19 John | FRD19 Client | GBP - British Pound | Bill           | Default | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | London (EU)   | James May | Dentons UK and Middle East LLP | 0:AD:G:Common Authorisations,0:AD:G:System Administrator (read-only setups) |
@europe
Examples:
	| FeeEarnerName | User       | Client       | Currency   | CurrencyMethod | Office              | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName | DefaultOperatingAlias          | UserRoleList |
	| FRD19 John    | FRD19 John | FRD19 Client | EUR - Euro | Bill           | London Billing (EU) | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | London (EU)   | James May | Dentons UK and Middle East LLP |              |
@canada
Examples:
	| FeeEarnerName | User       | Client       | Currency              | CurrencyMethod | Office   | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName | DefaultOperatingAlias          | UserRoleList |
	| FRD19 John    | FRD19 John | FRD19 Client | CAD - Canadian Dollar | Bill           | Montreal | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | Montreal      | James May | Dentons UK and Middle East LLP |              |
@singapore
Examples:
	| FeeEarnerName | User       | Client       | Currency               | CurrencyMethod | Office    | Department            | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName | DefaultOperatingAlias          | UserRoleList |
	| FRD19 John    | FRD19 John | FRD19 Client | SGD - Singapore Dollar | Bill           | Singapore | Corporate - Singapore | Default | Auto_IncludeALL | Auto_IncludeALL | Singapore     | James May | Dentons UK and Middle East LLP |              |

@CancelProcess
Scenario Outline: 020 Post Disbursement
	When I post the disbursement
		| Work Date | Disbursement Type  | Work Currency  | Work Amount  | Narrative | Tax Code  |
		| {Today}-1 | <DisbursementType> | <WorkCurrency> | <WorkAmount> | {Auto}+36 | <TaxCode> |
	And I validate the disbursement is posted with no errors

@ft
Examples:
	| DisbursementType    | WorkCurrency | WorkAmount | TaxCode                         |
	| Advertising Charges | GBP          | 5000       | UK Output Domestic Standard 20% |
@uk
Examples:
	| DisbursementType        | WorkCurrency | WorkAmount | TaxCode                     |
	| Automation Accomodation | GBP          | 5000       | UK Output Domestic Standard |
@qa
Examples:
	| DisbursementType | WorkCurrency | WorkAmount | TaxCode                         |
	| Accommodation    | GBP          | 5000       | UK Output Domestic Standard 20% |
@training
Examples:
	| DisbursementType                                       | WorkCurrency | WorkAmount | TaxCode                     |
	| Registraton Fees - Issuance of SSCT - Anticipated (NT) | GBP          | 5000       | UK Output Domestic Standard |
@staging
Examples:
	| DisbursementType            | WorkCurrency | WorkAmount | TaxCode                     |
	| Bank & Finance Charges (NT) | GBP          | 5000       | UK Output Domestic Standard |
@singapore
Examples:
	| DisbursementType         | WorkCurrency | WorkAmount | TaxCode                     |
	| Agency Registration (NT) | SGD          | 5000       | SG Output Domestic Standard |
@canada
Examples:
	| DisbursementType           | WorkCurrency | WorkAmount | TaxCode                   |
	| Bank of Canada Certificate | CAD          | 5000       | CA Output BC Standard PST |
@europe
Examples:
	| DisbursementType        | WorkCurrency | WorkAmount | TaxCode               |
	| Automation Accomodation | EUR          | 5000       | ES Output Europe Zero |


Scenario Outline: 030 Generate the Proforma via My Billable Matters
	Given I proxy as user '<User>'
	And I search for 'My Billable Matters'
	When I search or filter by matter
	And I verify proforma has been generated
	Then I close my billable matter

@ft @training @staging @canada @europe @uk @singapore @qa
Examples:
	| User       |
	| FRD19 John |

@CancelProcess
Scenario Outline: 040 Verification on Proforma Workflow
	Given I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	When I open the proforma for submission
	And I add apply adjustment details
		| Percentage |
		| 25         |
	And I submit it
	And I cancel proxy
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	Then I open the proforma for billing
	And I bill it without printing
	And the invoice number is generated

@ft @training @staging @canada @europe @uk @singapore @qa
Examples:
	| User       |
	| FRD19 John |
