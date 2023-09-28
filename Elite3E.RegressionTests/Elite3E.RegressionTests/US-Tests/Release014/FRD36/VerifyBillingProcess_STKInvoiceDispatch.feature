@us
Feature: VerifyBillingProcess_STKInvoiceDispatch

Invoice Distribution method - STK Dispatch : Invoice approval routed to timekeeper
Invoice Type added to Proforma Edit: Invoice prefix to be taken from invoice override value linked to Invoice Type 
Invoice narrative - Proforma matches the language and billing office and not matter attribute 



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
	| Test_STK | Test STK Dispatch | STK Dispatch   |
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

	
		Examples: 
		| InvoiceOverride    | InvoiceOverrideCode | InvoiceTypeCode | Unit                       | MatterAttributeCode | Office  | Language                 | NextInvoiceNumber  | NextCreditNoteNumber | NextTaxInvoiceNumber | NextCreditNoteTaxNumber |
		| US Sundry Sequence | 8054-USSUN          | Miscellaneous   | Dentons United States, LLP | Client Billable     | Chicago | English (United Kingdom) | 5054-US-2020-00004 | 5054-US-2020-90001   | 5054-US-2020-00004   | 5054-US-2020-90001      |

	Scenario Outline: 020 Create a new Matter
	Given I create a user with details
		| UserName | DataRoleAlias | DefaultOperatingAlias   | UserRoleList                                                                               |
		| <User>   | Admin         | <DefaultOperatingAlias> | 0:AD:G:Common Authorisations,0:WC:P:Proforma Processor,0:WC:P:Proforma Processor - Finance |
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
		| Test STK Dispatch         | <InvoiceOverride> | <RemittanceAccount> |
	And I add Client Reference child form
		| StartDate | ClientReference |
		| {Today}   | {Auto}+36       |
	Then I can submit the matter


Examples:
  | FeeEarnerName | User       | Client       | Currency        | CurrencyMethod | Office  | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName  | RemittanceAccount                                | InvoiceOverride    |
  | FRD36 Pete    | FRD36 Pete | FRD36 Client | USD - US Dollar | Bill           | Chicago | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | Chicago       | James Matt | Dentons US, LLP - Application Off-Set Bank - USD | US Sundry Sequence |



Scenario Outline: 030 Post Disbursement and Generate Proforma
	Given I post the disbursement
		| Work Date | Disbursement Type  | Work Currency  | Work Amount  | Narrative | Tax Code  |
		| {Today}-1 | <DisbursementType> | <WorkCurrency> | <WorkAmount> | {Auto}+36 | <TaxCode> |
	And I validate the disbursement is posted with no errors
	Then I can generate the proforma
		| Description | Include Other Proformas | Proforma Status |
		| {Auto}+36   | <IncludeOtherProformas> | Draft           |

Examples:
	| DisbursementType              | WorkCurrency | WorkAmount | TaxCode                          | IncludeOtherProformas |
	| Dentons United States Funding | USD          | 5000       | US Output Domestic Standard Rate | No                    |


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
	Given I proxy as user '<User>'
	And I search for 'Workflow Dashboard'
	And I open the Invoice Dispatch workflow task
	And I send the invoice routed to the timekeeper
	And I cancel proxy
	And I view the invoices
	Then I verify the prefix of the invoice matches the invoice override 

Examples:
	| User       | InvoiceType   |
	| FRD36 Pete | Miscellaneous |

	
