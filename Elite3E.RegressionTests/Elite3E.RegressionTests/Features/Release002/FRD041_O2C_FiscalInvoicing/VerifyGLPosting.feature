@release2 @frd041 @VerifyGLPosting
Feature: VerifyGLPosting
	Verify the GL Posting

	Please note: This test has been removed for all the regions except europe and singapore as all the other regions do 
not use Fiscal invoice renumbering process. 
https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/42490

@CancelProcess
Scenario Outline: 010 Create a new Matter
	Given there exists a fiscal invoice setup
		| Unit   | Unit Description  |
		| <Unit> | <UnitDescription> |
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
	And I create the Payer with Api
		| PayerName   | Entity   |
		| <PayorName> | <Client> |
	When I create a matter with details:
		| Client   | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | Rate   | ChargeTypeGroupName | CostTypeGroupName | BillingOffice   | PayorName   |
		| <Client> | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <Rate> | <ChargeTypeGroup>   | <CostTypeGroup>   | <BillingOffice> | <PayorName> |

@training @staging
Examples:
	| FeeEarnerName | User          | Client         | Unit | UnitDescription                         | Currency   | CurrencyMethod | Office              | Department | Section | Rate     | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName | UserRoleList |
	| Sabelo McGill | Sabelo McGill | Sabelo Mandela | 3511 | Dentons Europe Studio Legale Tributario | EUR - Euro | Bill           | London Billing (EU) | Default    | Default | Standard | Auto_IncludeALL | Auto_IncludeALL | Milan         | James May |              |
@qa
Examples:
	| FeeEarnerName | User          | Client         | Unit | UnitDescription                         | Currency            | CurrencyMethod | Office         | Department | Section | Rate     | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName | UserRoleList |
	| Sabelo McGill | Sabelo McGill | Sabelo Mandela | 3511 | Dentons Europe Studio Legale Tributario | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Standard | Auto_IncludeALL | Auto_IncludeALL | Milan         | James May |              |
@ft
Examples:
	| FeeEarnerName | User          | Client         | Unit | UnitDescription                         | Currency            | CurrencyMethod | Office         | Department | Section | Rate     | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName | UserRoleList                                             |
	| Sabelo McGill | Sabelo McGill | Sabelo Mandela | 3511 | Dentons Europe Studio Legale Tributario | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Standard | Auto_IncludeALL | Auto_IncludeALL | Milan         | James May | 0:AD:G:Common Authorisations,0:AD:G:System Administrator |
@europe
Examples:
	| FeeEarnerName | User          | Client         | Unit | UnitDescription                         | Currency   | CurrencyMethod | Office      | Department | Section | Rate     | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName | UserRoleList |
	| Sabelo McGill | Sabelo McGill | Sabelo Mandela | 3511 | Dentons Europe Studio Legale Tributario | EUR - Euro | Bill           | London (EU) | Default    | Default | Standard | Auto_IncludeALL | Auto_IncludeALL | Milan         | James May |              |
@singapore
Examples:
	| FeeEarnerName | User          | Client         | Unit | UnitDescription              | Currency               | CurrencyMethod | Office    | Department            | Section | Rate     | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName | UserRoleList |
	| Sabelo McGill | Sabelo McGill | Sabelo Mandela | 1201 | Dentons Rodyk & Davidson LLP | SGD - Singapore Dollar | Bill           | Singapore | Corporate - Singapore | Default | Standard | Auto_IncludeALL | Auto_IncludeALL | Singapore     | James May |              |

@CancelProcess
Scenario Outline: 011 Create a soft disbursement
	When I post the disbursement
		| Work Date | Disbursement Type  | Work Currency  | Work Amount  | Narrative | Tax Code  |
		| {Today}-1 | <DisbursementType> | <WorkCurrency> | <WorkAmount> | {Auto}+36 | <TaxCode> |
	And I validate the disbursement is posted with no errors

@qa
Examples:
	| DisbursementType    | WorkCurrency | WorkAmount | TaxCode                         |
	| Agency Registration | GBP          | 5000       | UK Output Domestic Standard 20% |
@ft
Examples:
	| DisbursementType    | WorkCurrency | WorkAmount | TaxCode                         |
	| Agency Registration | GBP          | 5000       | UK Output Domestic Standard 20% |
@training
Examples:
	| DisbursementType   | WorkCurrency | WorkAmount | TaxCode                     |
	| Administration Fee | GBP          | 5000       | UK Output Domestic Standard |
@staging
Examples:
	| DisbursementType | WorkCurrency | WorkAmount | TaxCode                     |
	| Accomodation     | GBP          | 5000       | UK Output Domestic Standard |
@singapore
Examples:
	| DisbursementType         | WorkCurrency | WorkAmount | TaxCode                     |
	| Agency Registration (NT) | SGD          | 5000       | SG Output Domestic Standard |
@europe
Examples:
	| DisbursementType         | WorkCurrency | WorkAmount | TaxCode                   |
	| Agency Registration (NT) | EUR          | 5000       | NL Output Europe Standard |

@CancelProcess
Scenario Outline: 020 Create a proforma Edit
	When I post the disbursement
		| Work Date | Disbursement Type  | Work Currency  | Work Amount  | Narrative | Tax Code  |
		| {Today}-1 | <DisbursementType> | <WorkCurrency> | <WorkAmount> | {Auto}+36 | <TaxCode> |
	And I validate the disbursement is posted with no errors
	Then I can generate the proforma
		| Description | Include Other Proformas | Proforma Status |
		| {Auto}+36   | <IncludeOtherProformas> | Draft           |
@qa
Examples:
	| DisbursementType    | WorkCurrency | WorkAmount | TaxCode                         | IncludeOtherProformas |
	| Advertising Charges | GBP          | 5000       | UK Output Domestic Standard 20% | No                    |
@ft
Examples:
	| DisbursementType    | WorkCurrency | WorkAmount | TaxCode                         | IncludeOtherProformas |
	| Advertising Charges | GBP          | 5000       | UK Output Domestic Standard 20% | No                    |
@training
Examples:
	| DisbursementType                                       | WorkCurrency | WorkAmount | TaxCode                     | IncludeOtherProformas |
	| Registraton Fees - Issuance of SSCT - Anticipated (NT) | GBP          | 5000       | UK Output Domestic Standard | No                    |
@staging
Examples:
	| DisbursementType | WorkCurrency | WorkAmount | TaxCode                     | IncludeOtherProformas |
	| Accomodation     | GBP          | 5000       | UK Output Domestic Standard | No                    |
@singapore
Examples:
	| DisbursementType                      | WorkCurrency | WorkAmount | TaxCode                     | IncludeOtherProformas |
	| Court & Stamp Fees - Anticipated (NT) | SGD          | 5000       | SG Output Domestic Standard | No                    |
@europe
Examples:
	| DisbursementType        | WorkCurrency | WorkAmount | TaxCode                   | IncludeOtherProformas |
	| Automation Accomodation | EUR          | 5000       | NL Output Europe Standard | No                    |

#Update the Proforma Edit process as per the latest release
Scenario: 030 Generate the Fiscal Invoice
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

@CancelProcess @ft @training @staging @canada @europe @uk @singapore @qa
Examples:
	| User          |
	| Sabelo McGill |



@CancelProcess @ft @training @staging @canada @europe @uk @singapore  @qa
Scenario: 040 I want to Send the Fiscal Invoice
	Given I search for 'Workflow Dashboard'
	And I open the Invoice Dispatch workflow task
	When I send the invoice routed to finance team when dispatch method not set


@ft @training @staging @europe @singapore @qa
Scenario: 050 Enter receipt information
	When I view the invoices
	And select the invoice details
	Then soft disbursement and taxes are part of the Invoice

@CancelProcess @ft @training @staging @europe @singapore @qa
Scenario: 060 Verify GL postings
	When I view the gl postings
	Then gl type is suspense gl type set on the fiscal invoice setup
	And I navigate to the home page

@CancelProcess @ft @training @staging @europe @singapore @qa
Scenario Outline: 070 Create the fiscal invoice
	Given I create a fiscal invoice from fiscal invoice create
		| Tax Date | GL Date | Currency Date |
		| {Today}  | {Today} | {Today}       |
	And submit it
	And print the invoice

@CancelProcess @ft @training @staging @europe @singapore @qa
Scenario: 080 View the gl postings on
	When I view the invoices
	And view the gl postings
	Then gl type is bill gl type set on the fiscal invoice setup
	And the tax invoice number starts with the prefix set on the fiscal invoice setup