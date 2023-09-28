@release2 @frd041 @ReceiptWithoutFiscalInvoice
Feature: ReceiptWithoutFiscalInvoice
	A proforma Invoice can be receipted without making it a fiscal Invoice

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
	| User          | FeeEarnerName | Unit | UnitDescription                         | Client      | Currency   | CurrencyMethod | Office              | Department | Section | Rate     | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName | UserRoleList |
	| Tariq Patrick | Tariq Patrick | 3511 | Dentons Europe Studio Legale Tributario | Tim Mandela | EUR - Euro | Bill           | London Billing (EU) | Default    | Default | Standard | Auto_IncludeALL | Auto_IncludeALL | Milan         | James May |              |
@europe @qa
Examples:
	| User          | FeeEarnerName | Unit | UnitDescription                         | Client      | Currency            | CurrencyMethod | Office         | Department | Section | Rate     | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName | UserRoleList |
	| Tariq Patrick | Tariq Patrick | 3511 | Dentons Europe Studio Legale Tributario | Tim Mandela | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Standard | Auto_IncludeALL | Auto_IncludeALL | Milan         | James May |              |
@singapore
Examples:
	| User          | FeeEarnerName | Unit | UnitDescription              | Client      | Currency               | CurrencyMethod | Office    | Department            | Section | Rate     | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName | UserRoleList |
	| Tariq Patrick | Tariq Patrick | 1201 | Dentons Rodyk & Davidson LLP | Tim Mandela | SGD - Singapore Dollar | Bill           | Singapore | Corporate - Singapore | Default | Standard | Auto_IncludeALL | Auto_IncludeALL | Singapore     | James May |              |
@ft
Examples:
	| User          | FeeEarnerName | Unit | UnitDescription                         | Client      | Currency            | CurrencyMethod | Office         | Department | Section | Rate     | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName | UserRoleList |
	| Tariq Patrick | Tariq Patrick | 3511 | Dentons Europe Studio Legale Tributario | Tim Mandela | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Standard | Auto_IncludeALL | Auto_IncludeALL | Milan         | James May |              |

@CancelProcess
Scenario Outline: 020 Create a proforma Edit
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
@qa
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
@europe
Examples:
	| DisbursementType          | WorkCurrency | WorkAmount | TaxCode                   | IncludeOtherProformas |
	| Court Fees  - Anticipated | EUR          | 5000       | PL Output Europe Standard | No                    |
@singapore
Examples:
	| DisbursementType                      | WorkCurrency | WorkAmount | TaxCode                     | IncludeOtherProformas |
	| Court & Stamp Fees - Anticipated (NT) | SGD          | 5000       | SG Output Domestic Standard | No                    |


#Update the Proforma Edit process as per the latest release
Scenario: 030 I want to 'Bill No Print' the proforma
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
	Then the invoice number is generated
    
@CancelProcess @training @staging @europe @singapore @qa @ft
Examples:
	| User          |
	| Tariq Patrick |


@CancelProcess @training @staging @europe @singapore @qa @ft
Scenario: 040 I want to Send the Fiscal Invoice
	Given I search for 'Workflow Dashboard'
	And I open the Invoice Dispatch workflow task
	When I send the invoice routed to finance team when dispatch method not set

@CancelProcess
Scenario Outline: 050 Enter receipt information
	When I add a new receipt
		| Receipt Type  | Receipt Date | Document Number | Narrative |
		| <ReceiptType> | {Today}      | {Auto}+36       | {Auto}+36 |
	And change the operating unit "<OperatingUnit>"
	And add the invoice on the receipt
	Then the payer is auto populated
	When I receipt the total amount
	And update the receipt
	Then I can submit the receipt

@ft
Examples:
	| ReceiptType    | OperatingUnit            |
	| AEABUHSBC01AED | Dentons & Co - Abu Dhabi |
@qa
Examples:
	| ReceiptType    | OperatingUnit             |
	| AUSYDANZINVAUD | Dentons Australia Limited |
@training @staging
Examples:
	| ReceiptType  | OperatingUnit                         |
	| IGB_3000_GBP | 3000 - Dentons UK and Middle East LLP |
@europe
Examples:
	| ReceiptType    | OperatingUnit             |
	| AUSYDANZINVAUD | Dentons Australia Limited |
@singapore
Examples:
	| ReceiptType  | OperatingUnit                |
	| ICB_1201_SGD | Dentons Rodyk & Davidson LLP |

@CancelProcess @training @staging @europe @singapore @qa @ft
Scenario: 060 Create a Credit Note
	When I view the invoices
	Then the invoice is set as paid
	And the tax invoice number displayed on invoices