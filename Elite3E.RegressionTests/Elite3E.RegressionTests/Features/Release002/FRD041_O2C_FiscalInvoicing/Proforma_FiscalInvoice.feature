@release2 @frd041 @Proforma_FiscalInvoice
Feature: Proforma_FiscalInvoice
	Validating the Fiscal Invoice field is disabled.

	
@CancelProcess
Scenario Outline: 010 Create a new Matter
	Given I create the Payer with Api
		| PayerName   | Entity       |
		| <PayorName> | CentralCoast |
	And I create a user with details
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

@training @staging @qa @uk
Examples:
	| FeeEarnerName | User     | Client    | Currency            | CurrencyMethod | Office  | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName | UserRoleList |
	| Kate Lee      | Kate Lee | Kate Team | GBP - British Pound | Bill           | Default | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | London (EU)   | James May |              |
@ft
Examples:
	| FeeEarnerName | User     | Client    | Currency            | CurrencyMethod | Office  | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice  | PayorName | UserRoleList |
	| Kate Lee      | Kate Lee | Kate Team | GBP - British Pound | Bill           | Default | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | London (UKIME) | James May |              |
@europe
Examples:
	| FeeEarnerName | User     | Client    | Currency   | CurrencyMethod | Office              | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName | UserRoleList |
	| Kate Lee      | Kate Lee | Kate Team | EUR - Euro | Bill           | London Billing (EU) | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | London (EU)   | James May |              |
@canada
Examples:
	| FeeEarnerName | User     | Client    | Currency              | CurrencyMethod | Office   | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName | UserRoleList |
	| Kate Lee      | Kate Lee | Kate Team | CAD - Canadian Dollar | Bill           | Montreal | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | Montreal      | James May |              |
 
@singapore
Examples:
	| FeeEarnerName | User     | Client    | Currency               | CurrencyMethod | Office    | Department            | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName | UserRoleList |
	| Kate Lee      | Kate Lee | Kate Team | SGD - Singapore Dollar | Bill           | Singapore | Corporate - Singapore | Default | Auto_IncludeALL | Auto_IncludeALL | Singapore     | James May |              |


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
@training @staging
Examples:
	| DisbursementType | WorkCurrency | WorkAmount | TaxCode                     | IncludeOtherProformas |
	| Accomodation     | GBP          | 5000       | UK Output Domestic Standard | No                    |
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
Scenario Outline: 030 Verification on inactive fiscal record
	Given I proxy as user '<User>'
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	And I open the proforma for submission
	Then I submit it
	And I cancel proxy
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	When I open the proforma for billing
	Then I can verify the Fiscal Invoice field is disabled

@ft @training @staging @canada @europe @uk @singapore @qa
Examples:
	| User     |
	| Kate Lee |

	
