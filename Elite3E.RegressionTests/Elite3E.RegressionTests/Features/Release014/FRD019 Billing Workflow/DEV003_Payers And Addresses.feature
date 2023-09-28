Feature: DEV003_Payers And Addresses

Verify Payors address is used rather than using the address on the matter or client
Verify When a proforma is generated the ‘Tax ID 1’ and ‘Tax ID 2’ fields are updated using the default payor details

@CancelProcess
Scenario Outline: 010 Create a new Matter and data preparation
	Given I create the Payer with Api
		| PayerName   | Entity       |
		| <PayorName> | CentralCoast |
	And I reopen an existing Payer
	And I update the payer tax information
		| TaxIDOne | TaxIDTwo |
		| {Auto}+5 | {Auto}+5 |
	And I submit it
	And I create a user with details
		| UserName | DataRoleAlias | UserRoleList   |
		| <User>   | Admin         | <UserRoleList> |
	Then I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	And I add a workflow user to a FeeEarner
		| User   | Name            |
		| <User> | <FeeEarnerName> |
	When I search or create a client
		| Entity Name | FeeEarnerFullName |
		| <Client>    | <FeeEarnerName>   |
	And I create a matter with details:
		| Client   | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | ChargeTypeGroupName | CostTypeGroupName | BillingOffice   | PayorName   |
		| <Client> | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <ChargeTypeGroup>   | <CostTypeGroup>   | <BillingOffice> | <PayorName> |

@ft @training @qa @uk
Examples:
	| FeeEarnerName | User       | Client       | Currency            | CurrencyMethod | Office  | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName   | DefaultOperatingAlias          | UserRoleList |
	| FRD19 John    | FRD19 John | FRD19 Client | GBP - British Pound | Bill           | Default | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | London (EU)   | James Mayer | Dentons UK and Middle East LLP |              |
@staging
Examples:
	| FeeEarnerName | User       | Client       | Currency            | CurrencyMethod | Office  | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName   | DefaultOperatingAlias          | UserRoleList                                   |
	| FRD19 John    | FRD19 John | FRD19 Client | GBP - British Pound | Bill           | Default | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | London (EU)   | James Mayer | Dentons UK and Middle East LLP | 0:AD:G:System Administrator (read-only setups) |
@europe
Examples:
	| FeeEarnerName | User       | Client       | Currency   | CurrencyMethod | Office              | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName   | DefaultOperatingAlias          | UserRoleList |
	| FRD19 John    | FRD19 John | FRD19 Client | EUR - Euro | Bill           | London Billing (EU) | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | London (EU)   | James Mayer | Dentons UK and Middle East LLP |              |
@canada
Examples:
	| FeeEarnerName | User       | Client       | Currency              | CurrencyMethod | Office   | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName   | DefaultOperatingAlias          | UserRoleList |
	| FRD19 John    | FRD19 John | FRD19 Client | CAD - Canadian Dollar | Bill           | Montreal | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | Montreal      | James Mayer | Dentons UK and Middle East LLP |              |
@singapore
Examples:
	| FeeEarnerName | User       | Client       | Currency               | CurrencyMethod | Office    | Department            | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName   | DefaultOperatingAlias          | UserRoleList |
	| FRD19 John    | FRD19 John | FRD19 Client | SGD - Singapore Dollar | Bill           | Singapore | Corporate - Singapore | Default | Auto_IncludeALL | Auto_IncludeALL | Singapore     | James Mayer | Dentons UK and Middle East LLP |              |

@CancelProcess
Scenario Outline: 020 Post Disbursement and Generate Proforma
	When I post the disbursement
		| Work Date | Disbursement Type  | Work Currency  | Work Amount  | Narrative | Tax Code  |
		| {Today}-1 | <DisbursementType> | <WorkCurrency> | <WorkAmount> | {Auto}+36 | <TaxCode> |
	And I validate the disbursement is posted with no errors
	Then I can generate the proforma
		| Description | Include Other Proformas | Proforma Status |
		| {Auto}+36   | <IncludeOtherProformas> | Draft           |

@ft
Examples:
	| DisbursementType    | WorkCurrency | WorkAmount | TaxCode                         | IncludeOtherProformas |
	| Advertising Charges | GBP          | 5000       | UK Output Domestic Standard 20% | No                    |
@uk
Examples:
	| DisbursementType        | WorkCurrency | WorkAmount | TaxCode                     | IncludeOtherProformas |
	| Automation Accomodation | GBP          | 5000       | UK Output Domestic Standard | No                    |
@qa
Examples:
	| DisbursementType | WorkCurrency | WorkAmount | TaxCode                         | IncludeOtherProformas |
	| Accommodation    | GBP          | 5000       | UK Output Domestic Standard 20% | No                    |
@training
Examples:
	| DisbursementType                                       | WorkCurrency | WorkAmount | TaxCode                     | IncludeOtherProformas |
	| Registraton Fees - Issuance of SSCT - Anticipated (NT) | GBP          | 5000       | UK Output Domestic Standard | No                    |

@staging
Examples:
	| DisbursementType            | WorkCurrency | WorkAmount | TaxCode                     | IncludeOtherProformas |
	| Bank & Finance Charges (NT) | GBP          | 5000       | UK Output Domestic Standard | No                    |
@singapore
Examples:
	| DisbursementType         | WorkCurrency | WorkAmount | TaxCode                     | IncludeOtherProformas |
	| Agency Registration (NT) | SGD          | 5000       | SG Output Domestic Standard | No                    |
@canada
Examples:
	| DisbursementType           | WorkCurrency | WorkAmount | TaxCode                   | IncludeOtherProformas |
	| Bank of Canada Certificate | CAD          | 5000       | CA Output BC Standard PST | No                    |
@europe
Examples:
	| DisbursementType        | WorkCurrency | WorkAmount | TaxCode               | IncludeOtherProformas |
	| Automation Accomodation | EUR          | 5000       | ES Output Europe Zero | No                    |

@CancelProcess
Scenario Outline: 030 Verify Payer information in Proforma
	Given I proxy as user '<User>'
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	And I open the proforma for submission
	Then I verify the payer information on a proforma
	And I close the proforma
	And I cancel proxy

@ft @training @staging @canada @europe @uk @singapore @qa
Examples:
	| User       |
	| FRD19 John |