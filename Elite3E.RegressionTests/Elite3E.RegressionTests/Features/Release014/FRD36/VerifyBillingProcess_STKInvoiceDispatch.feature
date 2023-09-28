Feature: VerifyBillingProcess_STKInvoiceDispatch

Invoice Distribution method - RTK Dispatch : Invoice approval routed to timekeeper
Invoice Type added to Proforma Edit: Invoice prefix to be taken from invoice override value linked to Invoice Type 
Invoice narrative - Proforma matches the language and billing office and not matter attribute 



@training @staging @qa @uk @europe @canada @singapore
Scenario Outline:010  Invoice Distribution Method setup
	Given I search or create invoice override
	| Code                  | Description       | NextInvoiceNumber   | NextCreditNoteNumber   | NextTaxInvoiceNumber   | NextCreditNoteTaxNumber   |
	| <InvoiceOverrideCode> | <InvoiceOverride> | <NextInvoiceNumber> | <NextCreditNoteNumber> | <NextTaxInvoiceNumber> | <NextCreditNoteTaxNumber> |
	And I search for process 'Invoice Overrides'
	And I advanced find and select
		| Search Column | Search Operator | Search Value          |
		| Code          | Equals          | <InvoiceOverrideCode> |
	And I get the prefix for invoice override
	And I search or create invoice distribution method
	| Code     | Description       | DispatchOption |
	| TestRTK  | Test RTK Dispatch | RTK Dispatch   |
	And I search or create invoice type
	| Code              | Description       | Unit   | OverrideValue     |
	| <InvoiceTypeCode> | <InvoiceTypeCode> | <Unit> | <InvoiceOverride> |
	And I search for process 'Office Configuration'
	And I advanced find and select
		| Search Column      | Search Operator | Search Value |
		| Office Description | Equals          | <Office>     |
	When I update office configuration
		| Office   | Language   | MatterAttribute       | CoverLetterNarrative                                       | InvoiceNarrative                    | LegalName |
		| <Office> | <Language> | <MatterAttributeCode> | AutoCoverNarrative @BillingTkprDisplayName@ @MatterNumber@ | AutoInvoiceNarrative @MatterNumber@ | <Office>  |

		@ft
		Examples: 
		| InvoiceOverride               | InvoiceOverrideCode | InvoiceTypeCode | Unit                           | MatterAttributeCode | Office         | Language                 |NextInvoiceNumber  | NextCreditNoteNumber | NextTaxInvoiceNumber | NextCreditNoteTaxNumber |
		| London (UKME) Sundry Sequence | 8054-SUN            | LondonSequence  | Dentons UK and Middle East LLP | LondonAttribute     | London (UKIME) | English (United Kingdom) | 8054-S-2020-00004 | 8054-S-2020-90001    | 8054-S-2020-00004    | 8054-S-2020-90001       |

	Scenario Outline: 020 Create a new Matter
	Given I create a user with details
		| UserName | DataRoleAlias | DefaultOperatingAlias   | UserRoleList   |
		| <User>   | Admin         | <DefaultOperatingAlias> | <UserRoleList> |  
	And I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	And I update the fee earner email body template
		| Language                 | CoverLetterNarrative      |
		| English (United Kingdom) | Auto Email @MatterNumber@ |
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
	Given I view an existing matter
	When I edit the existing matter
		| InvoiceDistributionMethod | InvoiceOverride   | RemittanceAccount   |
		| Test RTK Dispatch         | <InvoiceOverride> | <RemittanceAccount> |
	And I add Client Reference child form
		| StartDate | ClientReference |
		| {Today}   | {Auto}+36       |
	Then I can submit the matter


@ft
Examples:
	| FeeEarnerName | User       | Client       | Currency            | CurrencyMethod | Office  | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice  | PayorName  | RemittanceAccount                     | InvoiceOverride               | UserRoleList          |
	| FRD36 Pete    | FRD36 Pete | FRD36 Client | GBP - British Pound | Bill           | Default | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | London (UKIME) | James Matt | UKME - Application Off-Set Bank - GBP | London (UKME) Sundry Sequence | DEFAULT_WORKFLOW_ROLE |
@training @staging @qa @uk
Examples:
	| FeeEarnerName | User       | Client       | Currency            | CurrencyMethod | Office  | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice  | PayorName  | RemittanceAccount                     | InvoiceOverride               | UserRoleList |
	| FRD36 Pete    | FRD36 Pete | FRD36 Client | GBP - British Pound | Bill           | Default | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | London (UKIME) | James Matt | UKME - Application Off-Set Bank - GBP | London (UKME) Sundry Sequence |              | 
@europe
Examples:
	| FeeEarnerName | User       | Client       | Currency   | CurrencyMethod | Office              | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName  | RemittanceAccount               | InvoiceOverride | UserRoleList |
	| FRD36 Pete    | FRD36 Pete | FRD36 Client | EUR - Euro | Bill           | London Billing (EU) | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | London (EU)   | James Matt | Citibank Europe plc (BUDEURESC) | Miscellaneous   |              |
@canada
Examples:
	 | FeeEarnerName | User       | Client       | Currency              | CurrencyMethod | Office   | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName  | RemittanceAccount                   | InvoiceOverride | UserRoleList |
	 | FRD36 Pete    | FRD36 Pete | FRD36 Client | CAD - Canadian Dollar | Bill           | Montreal | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | Montreal      | James Matt | Bank of Montreal IBA Trust-1248-814 | Miscellaneous   |              |
@singapore
Examples:
  | FeeEarnerName | User       | Client       | Currency               | CurrencyMethod | Office    | Department            | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName  | RemittanceAccount                             | InvoiceOverride | UserRoleList |
  | FRD36 Pete    | FRD36 Pete | FRD36 Client | SGD - Singapore Dollar | Bill           | Singapore | Corporate - Singapore | Default | Auto_IncludeALL | Auto_IncludeALL | Singapore     | James Matt | Singapore - OCBC (LnL & DrnD) Trust Acc - SGD | Miscellaneous   |              |

	


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
	And I proxy as user '<User>'
	And I search for 'Workflow Dashboard'
	And I open the Invoice Dispatch workflow task
	And I send the invoice routed to the timekeeper
	And I cancel proxy
	And I view the invoices
	Then I verify the prefix of the invoice matches the invoice override
	And I verify the invoice type

@ft @training @staging @qa @uk @europe @canada @singapore
Examples:
	| User       | InvoiceType    |
	| FRD36 Pete | LondonSequence |


@CancelProcess @ft @training @staging @canada @europe @uk @singapore @qa
Scenario Outline: 050 Verify invoice type in the Credit Note process
	Given I search for 'Partial Credit Notes'
	And I add invoice to partial credit notes
	Then I verify the invoice type

	
	