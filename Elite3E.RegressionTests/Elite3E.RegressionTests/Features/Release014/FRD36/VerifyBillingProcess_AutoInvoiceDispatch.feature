
Feature: VerifyBillingProcess_AutoInvoiceDispatch

Invoice Distribution method - Auto Dispatch : Invoice generated automatically 
Invoice Type mapped to Matter attribute at matter level : Invoice prefix to be taken from invoice override value linked to Invoice Type (defect)
Email Body - When fee earner is updated with email body, the cover letter narrative is taken from it
Pres currency & proforma currency - same- Pres Currency, Exchange rates verified - Pres Date and Pres Invoice amount not  on Proforma Edit (defect)
Pres Exchange Rate not auto-populating 
Same currency remittance account - bank details not displayed in Invoice (defect)



@CancelProcess
Scenario Outline: 010  Invoice Distribution Method setup
	Given I search or create invoice override
		| Code                  | Description       | NextInvoiceNumber   | NextCreditNoteNumber   | NextTaxInvoiceNumber   | NextCreditNoteTaxNumber   |
		| <InvoiceOverrideCode> | <InvoiceOverride> | <NextInvoiceNumber> | <NextCreditNoteNumber> | <NextTaxInvoiceNumber> | <NextCreditNoteTaxNumber> |
	And I search for process 'Invoice Overrides'
	And I advanced find and select
		| Search Column | Search Operator | Search Value          |
		| Code          | Equals          | <InvoiceOverrideCode> |
	And I get the prefix for invoice override
	And I search or create invoice distribution method
		| Code      | Description        | DispatchOption |
		| Test_Auto | Test Auto Dispatch | Auto Dispatch  |
	And I search for process 'Office Configuration'
	And I advanced find and select
		| Search Column      | Search Operator | Search Value |
		| Office Description | Equals          | <Office>     |
	When I update office configuration
		| Office   | Language   | MatterAttribute       | CoverLetterNarrative                               | InvoiceNarrative                    | LegalName |
		| <Office> | <Language> | <MatterAttributeCode> | Auto Email @BillingTkprDisplayName@ @MatterNumber@ | AutoInvoiceNarrative @MatterNumber@ | <Office>  |

@ft @training @staging @qa @uk @europe @canada 
Examples:
	| InvoiceOverride                | InvoiceOverrideCode | MatterAttributeCode | Office         | Language                 | NextInvoiceNumber  | NextCreditNoteNumber | NextTaxInvoiceNumber | NextCreditNoteTaxNumber |
	| London (UKME) Nominal Sequence | 8054-NOM            | LondonAttribute     | London (UKIME) | English (United Kingdom) | 8054-N-2020-000063 | 8054-N-2020-900001   | 8054-N-2020-000063   | 8054-N-2020-900001      |
@singapore
Examples:
	| InvoiceOverride | InvoiceOverrideCode | MatterAttributeCode | Office    | Language                 | NextInvoiceNumber  | NextCreditNoteNumber | NextTaxInvoiceNumber | NextCreditNoteTaxNumber |
	| Miscellaneous   | 8054-NOM            | Client Billable     | Singapore | English (United Kingdom) | 8054-N-2020-000063 | 8054-N-2020-900001   | 8054-N-2020-000063   | 8054-N-2020-900001      |


Scenario Outline: 020 Create a new Matter
	Given I create a user with details
		| UserName | DataRoleAlias | UserRoleList   |
		| <User>   | Admin         | <UserRoleList> |  
	And I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	And I update the fee earner email body template
		| Language                 | CoverLetterNarrative                               |
		| English (United Kingdom) | Auto Email @BillingTkprDisplayName@ @MatterNumber@ |
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
	And I view an existing matter
	When I edit the existing matter
		| InvoiceDistributionMethod | InvoiceOverride   | PresentationCurrency   | RemittanceAccount   | Language                 | PresentationExchangeRate   |
		| Test Auto Dispatch        | <InvoiceOverride> | <PresentationCurrency> | <RemittanceAccount> | English (United Kingdom) | <PresentationExchangeRate> |
	And I add Client Reference child form
		| StartDate | ClientReference |
		| {Today}   | {Auto}+36       |
	Then I can submit the matter

@ft
Examples:
	| FeeEarnerName | User       | Client       | Currency            | CurrencyMethod | Office  | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice  | PayorName  | RemittanceAccount                  | PresentationCurrency | PresentationExchangeRate | InvoiceOverride                | UserRoleList |
	| FRD36 Pete    | FRD36 Pete | FRD36 Client | GBP - British Pound | Bill           | Default | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | London (UKIME) | James Matt | London UKME - HSBC Off 1 Acc - EUR | GBP                  | 1.00                     | London (UKME) Nominal Sequence |              |
@training @staging @qa @uk
Examples:
	| FeeEarnerName | User       | Client       | Currency            | CurrencyMethod | Office  | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice  | PayorName  | RemittanceAccount                  | PresentationCurrency | PresentationExchangeRate | InvoiceOverride                | UserRoleList |
	| FRD36 Pete    | FRD36 Pete | FRD36 Client | GBP - British Pound | Bill           | Default | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | London (UKIME) | James Matt | London UKME - HSBC Off 1 Acc - EUR | GBP                  | 1.00                     | London (UKME) Nominal Sequence |              |

@europe
Examples:
	| FeeEarnerName | User       | Client       | Currency   | CurrencyMethod | Office              | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName  | RemittanceAccount               | PresentationCurrency | PresentationExchangeRate | InvoiceOverride                | UserRoleList |
	| FRD36 Pete    | FRD36 Pete | FRD36 Client | EUR - Euro | Bill           | London Billing (EU) | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | London (EU)   | James Matt | Citibank Europe plc (BUDEURESC) | EUR                  | 1.00                     | London (UKME) Nominal Sequence |              |
@canada
Examples:
	| FeeEarnerName | User       | Client       | Currency              | CurrencyMethod | Office   | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName  | RemittanceAccount                   | PresentationCurrency | PresentationExchangeRate | InvoiceOverride                | UserRoleList |
	| FRD36 Pete    | FRD36 Pete | FRD36 Client | CAD - Canadian Dollar | Bill           | Montreal | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | Montreal      | James Matt | Bank of Montreal IBA Trust-1248-814 | CAD                  | 1.00                     | London (UKME) Nominal Sequence |              |
@singapore
Examples:
	| FeeEarnerName | User       | Client       | Currency               | CurrencyMethod | Office    | Department            | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName  | RemittanceAccount                | PresentationCurrency | PresentationExchangeRate | InvoiceOverride | UserRoleList |
	| FRD36 Pete    | FRD36 Pete | FRD36 Client | SGD - Singapore Dollar | Bill           | Singapore | Corporate - Singapore | Default | Auto_IncludeALL | Auto_IncludeALL | Singapore     | James Matt | Singapore-SCB-Office CA No.2-SGD | SGD                  | 1.00                     | Miscellaneous   |              |


Scenario Outline: 030 Post Disbursement and Generate Proforma
	Given I post the disbursement
		| Work Date | Disbursement Type  | Work Currency  | Work Amount  | Narrative | Tax Code  |
		| {Today}-1 | <DisbursementType> | <WorkCurrency> | <WorkAmount> | {Auto}+36 | <TaxCode> |
	And I validate the disbursement is posted with no errors
	Then I can generate the proforma
		| Description | Include Other Proformas | Proforma Status |
		| {Auto}+36   | <IncludeOtherProformas> | Draft           |
@ft
Examples:
	| DisbursementType    | WorkCurrency | WorkAmount | TaxCode                    | IncludeOtherProformas |
	| Advertising Charges | GBP          | 5000       | UK Output Domestic Zero 0% | No                    |
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
	And I verify the client reference number
	And I verify the currency and bank details in proforma edit
	And I edit the proforma edit
		| InvoiceType   |
		| <InvoiceType> |
	And I verify the cover letter narrative
	Then I submit it
	And I cancel proxy
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	Then I open the proforma for billing
	And I bill it without printing
	And the invoice number is generated


@ft @training @staging @qa @uk @europe @canada @singapore
Examples:
	| User       | InvoiceType |
	| FRD36 Pete | OAC         |


Scenario Outline: 050 Enter receipt information
	Given I add a new receipt
		| Receipt Type  | Receipt Date | Document Number | Narrative |
		| <ReceiptType> | {Today}      | {Auto}+36       | {Auto}+36 |
	And change the operating unit "<OperatingUnit>"
	When add the invoice on the receipt
	And the payer is auto populated
	And I receipt the total amount
	And update the receipt
	Then I can submit the receipt
	When I view the invoices
	Then I verify the presentation currency and invoice amount
	And I verify the prefix of the invoice matches the invoice override

@ft @training @staging @qa @uk @europe @canada @singapore
Examples:
	| ReceiptType | OperatingUnit                  |
	| CASH        | Dentons UK and Middle East LLP |
	
	
