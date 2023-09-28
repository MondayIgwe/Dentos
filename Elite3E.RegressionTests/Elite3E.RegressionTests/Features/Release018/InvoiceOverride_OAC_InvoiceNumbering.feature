Feature: InvoiceOverride_OAC_InvoiceNumbering

Scenario Outline: 010 Verify that a new  flag to the UDF List has been added
Given I search or create invoice override
	| Code                  | Description       | NextInvoiceNumber   | NextCreditNoteNumber   | NextTaxInvoiceNumber   | NextCreditNoteTaxNumber   |
	| <InvoiceOverrideCode> | <InvoiceOverride> | <NextInvoiceNumber> | <NextCreditNoteNumber> | <NextTaxInvoiceNumber> | <NextCreditNoteTaxNumber> |
	And I search for process 'Invoice Overrides'
	And I advanced find and select
		| Search Column | Search Operator | Search Value          |
		| Code          | Equals          | <InvoiceOverrideCode> |
	And I get the prefix for invoice override
	And I search or create invoice type
	| Code              | Description       | Unit   | OverrideValue     |
	| <InvoiceTypeCode> | <InvoiceTypeCode> | <Unit> | <InvoiceOverride> |

@ft @training @staging @qa @uk @europe @canada @singapore
Examples: 
	| InvoiceOverride                   | InvoiceOverrideCode | InvoiceTypeCode | Unit                           | Office         | NextInvoiceNumber | NextCreditNoteNumber | NextTaxInvoiceNumber | NextCreditNoteTaxNumber |
	| London (UKME) On Account Sequence | 8054-OAC            | OAC             | Dentons UK and Middle East LLP | London (UKIME) | 8054-O-2020-00024 | 8054-O-2020-90001    | 8054-O-2020-00024    | 8054-O-2020-90001       |



	Scenario Outline: 020 Create a new Matter
	Given I create a user with details
		| UserName | DataRoleAlias |  
		| <User>   | Admin         | 
	And I create a fee earner with details
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
	And I create a matter with details:
		| Client   | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | ChargeTypeGroupName | CostTypeGroupName | BillingOffice   | PayorName   |
		| <Client> | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <ChargeTypeGroup>   | <CostTypeGroup>   | <BillingOffice> | <PayorName> |

@ft @training @staging @qa @uk
Examples:
	| FeeEarnerName | User       | Client       | Currency            | CurrencyMethod | Office  | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice  | PayorName  | RemittanceAccount                     |
	| FRD36 Pete    | FRD36 Pete | FRD36 Client | GBP - British Pound | Bill           | Default | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | London (UKIME) | James Matt | UKME - Application Off-Set Bank - GBP |
	
@europe
Examples:
	| FeeEarnerName | User       | Client       | Currency   | CurrencyMethod | Office              | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName  | RemittanceAccount               |
	| FRD36 Pete    | FRD36 Pete | FRD36 Client | EUR - Euro | Bill           | London Billing (EU) | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | London (EU)   | James Matt | Citibank Europe plc (BUDEURESC) |
@canada
Examples:
	 | FeeEarnerName | User       | Client       | Currency              | CurrencyMethod | Office   | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName  | RemittanceAccount                   |
	 | FRD36 Pete    | FRD36 Pete | FRD36 Client | CAD - Canadian Dollar | Bill           | Montreal | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | Montreal      | James Matt | Bank of Montreal IBA Trust-1248-814 |
@singapore
Examples:
  | FeeEarnerName | User       | Client       | Currency               | CurrencyMethod | Office    | Department            | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName  | RemittanceAccount                             |
  | FRD36 Pete    | FRD36 Pete | FRD36 Client | SGD - Singapore Dollar | Bill           | Singapore | Corporate - Singapore | Default | Auto_IncludeALL | Auto_IncludeALL | Singapore     | James Matt | Singapore - OCBC (LnL & DrnD) Trust Acc - SGD |


Scenario Outline: 030 Post Disbursement and Generate Proforma
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
	| DisbursementType                                       | WorkCurrency | WorkAmount | TaxCode                     | IncludeOtherProformas |
	| Registraton Fees - Issuance of SSCT - Anticipated (NT) | GBP          | 5000       | UK Output Domestic Standard | No                    |
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
Scenario Outline: 040 Verification on invoice generated
	Given I proxy as user '<User>'
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	And I open the proforma for submission
	When I submit it
	And I cancel proxy
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	When I open the proforma for billing
	And I edit the proforma edit
	| InvoiceType   |
	| <InvoiceType> |
	And I bill it without printing
	And the invoice number is generated
	And I search for 'Workflow Dashboard'
	And I open the Invoice Dispatch workflow task
	And I send the invoice routed to finance team when dispatch method not set
	And I view the invoices
	Then I verify the prefix of the invoice matches the invoice override
	And I verify the invoice type

@ft @training @staging @qa @uk @europe @canada @singapore
Examples:
	| User       | InvoiceType |
	| FRD36 Pete | OAC         |

