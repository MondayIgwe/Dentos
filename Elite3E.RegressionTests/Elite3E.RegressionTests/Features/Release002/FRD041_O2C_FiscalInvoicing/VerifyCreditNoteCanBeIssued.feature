
@release2 @frd041 @VerifyCreditNoteCanBeIssued
Feature: VerifyCreditNoteCanBeIssued
	Verify that credit note can be issued and verify credit not is disabled on Proforma edit

Please note: This test has been removed for all the regions except europe and singapore as all the other regions do 
not use Fiscal invoice renumbering process. 
https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/42490

@CancelProcess
Scenario Outline: 010 Create a new Matter
	Given there exists a fiscal invoice setup
		| Unit   | Unit Description  |
		| <Unit> | <UnitDescription> |
	And I create a user with details
		| UserName | DataRoleAlias | DefaultOperatingAlias   |
		| <User>   | Admin         | <DefaultOperatingAlias> |
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

@training @staging @europe
Examples:
	| DefaultOperatingAlias          | FeeEarnerName | User          | Client         | Unit | UnitDescription                         | Currency   | CurrencyMethod | Office              | Department | Section | Rate     | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName |
	| Dentons UK and Middle East LLP | Sabelo McGill | Sabelo McGill | Sabelo Mandela | 3511 | Dentons Europe Studio Legale Tributario | EUR - Euro | Bill           | London Billing (EU) | Default    | Default | Standard | Auto_IncludeALL | Auto_IncludeALL | Milan         | James May |
@ft @qa
Examples:
	| DefaultOperatingAlias          | FeeEarnerName | User          | Client         | Unit | UnitDescription                         | Currency            | CurrencyMethod | Office         | Department | Section | Rate     | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName |
	| Dentons UK and Middle East LLP | Sabelo McGill | Sabelo McGill | Sabelo Mandela | 3511 | Dentons Europe Studio Legale Tributario | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Standard | Auto_IncludeALL | Auto_IncludeALL | Milan         | James May |
@singapore
Examples:
	| DefaultOperatingAlias          | FeeEarnerName | User          | Client         | Unit | UnitDescription              | Currency               | CurrencyMethod | Office    | Department            | Section | Rate     | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName |
	| Dentons UK and Middle East LLP | Sabelo McGill | Sabelo McGill | Sabelo Mandela | 1201 | Dentons Rodyk & Davidson LLP | SGD - Singapore Dollar | Bill           | Singapore | Corporate - Singapore | Default | Standard | Auto_IncludeALL | Auto_IncludeALL | Singapore     | James May |
			
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
	| DisbursementType | WorkCurrency | WorkAmount | TaxCode                         | IncludeOtherProformas |
	| Accommodation    | GBP          | 5000       | UK Output Domestic Standard 20% | No                    |
@ft @staging
Examples:
	| DisbursementType    | WorkCurrency | WorkAmount | TaxCode                         | IncludeOtherProformas |
	| Advertising Charges | GBP          | 5000       | UK Output Domestic Standard 20% | No                    |
@training 
Examples:
	| DisbursementType                                       | WorkCurrency | WorkAmount | TaxCode                     | IncludeOtherProformas |
	| Registraton Fees - Issuance of SSCT - Anticipated (NT) | GBP          | 5000       | UK Output Domestic Standard | No                    |
@singapore
Examples:
	| DisbursementType                      | WorkCurrency | WorkAmount | TaxCode                     | IncludeOtherProformas |
	| Court & Stamp Fees - Anticipated (NT) | SGD          | 5000       | SG Output Domestic Standard | No                    |
@europe
Examples:
	| DisbursementType        | WorkCurrency | WorkAmount | TaxCode               | IncludeOtherProformas |
	| Automation Accomodation | EUR          | 5000       | ES Output Europe Zero | No                    |

#Update the Proforma Edit process as per the latest release
Scenario: 030 Proforma Edit
	Given I proxy as user '<User>'
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	And I open the proforma for submission
	Then I submit it
	And I cancel proxy
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	When I open the proforma for billing
	Then the full credit note is disabled
	Then I set create fiscal invoice
	And bill it without printing
	Then the tax invoice number is generated
	

@CancelProcess @ft @training @staging @europe @singapore @qa
Examples:
	| User          |
	| Sabelo McGill |
 
@CancelProcess @ft @training @staging @europe @singapore @qa
Scenario: 040 I want to Send the Fiscal Invoice
	Given I search for 'Workflow Dashboard'
	And I open the Invoice Dispatch workflow task
	When I send the invoice routed to finance team when dispatch method not set
	And I navigate to the home page

@CancelProcess
Scenario: 050 Create a Credit Note
	When I view the invoices
	And submit the invoice with the full credit note set with '<Invoices>'
	Then the credit note is generated

@staging
Examples:
	| Invoices |
	| Invoices |
@ft @qa
Examples:
	| Invoices |
	| Invoices |
@training
Examples:
	| Invoices     |
	| Nominal bill |
@europe @singapore
Examples:
	| Invoices     |
	| Nominal bill |

@CancelProcess @ft @training @staging @europe @singapore @qa
Scenario: 060 Print the creditnote
	Given I print the invoice

@CancelProcess @ft @training @staging @europe @singapore @qa
Scenario: 070 View the Credit Note and Verify
	When I search and select all the invoices
	Then the credit note is available