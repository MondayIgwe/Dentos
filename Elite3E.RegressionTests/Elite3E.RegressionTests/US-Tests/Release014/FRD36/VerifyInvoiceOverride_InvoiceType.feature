@ignore @us
Feature:VerifyInvoiceOverride_InvoiceType

Invoice Distribution method - Auto Dispatch : Invoice generated automatically 
Invoice Type mapped to Matter attribute at matter level : Invoice prefix to be taken from invoice override value linked to Invoice Type (defect)
Invoice Narrative where Language,Billing office and matter attribute matches 
Cover Letter Narrative - to be taken from Office configuration as email body set in fee earner maintenance 
Matter and Client notes verified 
Government System Upload - On adding template in office config, not able to open government system upload record (defect)

https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/65237
https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/67493


	@CancellProcess
	Scenario Outline:010  Invoice type and Invoice distribution method setup
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
		And I search or create a matter attribute
		| Code                  | Description           | InvoiceType       |
		| <MatterAttributeCode> | <MatterAttributeCode> | <InvoiceTypeCode> |
		When I search or create invoice distribution method
		| Code      | Description        | DispatchOption |
		| Test_Auto | Test Auto Dispatch | Auto Dispatch  |
		And I search for process 'Office Configuration'
		And I advanced find and select
		| Search Column      | Search Operator | Search Value |
		| Office Description | Equals          | <Office>     |
		And I update office configuration
		| Office   | Language   | MatterAttribute       | CoverLetterNarrative                                       | InvoiceNarrative                    | LegalName | GovtSysTemplate   |
		| <Office> | <Language> | <MatterAttributeCode> | AutoCoverNarrative @BillingTkprDisplayName@ @MatterNumber@ | AutoInvoiceNarrative @MatterNumber@ | <Office>  | <GovtSysTemplate> |
		Then I verify the note type '<NoteType>' has proforma flag set to true
		
		Examples: 
		| InvoiceOverride    | InvoiceOverrideCode | InvoiceTypeCode | MatterAttributeCode        | Unit            | Office  | Language                 | GovtSysTemplate | NextInvoiceNumber  | NextCreditNoteNumber | NextTaxInvoiceNumber | NextCreditNoteTaxNumber | NoteType |
		| US Sundry Sequence | 8054-USSUN          | Miscellaneous   | Dentons United States, LLP | Client Billable | Chicago | English (United Kingdom) | TE_DG_3E_LEDES  | 5054-US-2020-00004 | 5054-US-2020-90001   | 5054-US-2020-00004   | 5054-US-2020-90001      | WIP      |

	
	Scenario Outline: 020 Create  Matter
	Given I create a user with details
		| UserName | DataRoleAlias | DefaultOperatingAlias   | UserRoleList                                                                               |
		| <User>   | Admin         | <DefaultOperatingAlias> | 0:AD:G:Common Authorisations,0:WC:P:Proforma Processor,0:WC:P:Proforma Processor - Finance |
	And I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	And I remove the email body in fee earner maintenance
	And I add a workflow user to a FeeEarner
	| User   | Name            |
	| <User> | <FeeEarnerName> |
	When I retrieve the current username
	And I search or create a client
		| Entity Name | FeeEarnerFullName |
		| <Client>    | <FeeEarnerName>   |
	And I view an existing client
	Then I add a note to a client
		| DateEntered | NoteType | ActionOwner | ActionDate | Note      |
		| {Today}     | WIP      | <User>      | {Today}    | {Auto}+36 |
	And I submit it
	And I verify the client notes in Client Notes process
	And I create the Payer with Api
		| PayerName   | Entity   |
		| <PayorName> | <Client> |
	And I create a matter with details:
		| Client   | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | ChargeTypeGroupName | CostTypeGroupName | BillingOffice   | PayorName   |
		| <Client> | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <ChargeTypeGroup>   | <CostTypeGroup>   | <BillingOffice> | <PayorName> |
	Given I view an existing matter
	When I edit the existing matter
		| InvoiceDistributionMethod | MatterAttribute | Language                 |
		| Test Auto Dispatch        | LondonAttribute | English (United Kingdom) |
	Then I add a note to a matter
		| DateEntered | NoteType | ActionOwner | ActionDate | Note      |
		| {Today}     | WIP      | <User>      | {Today}    | {Auto}+36 |
	Then I can submit the matter
	And I verify the matter notes in Matter Notes process


Examples:
  | FeeEarnerName | User       | Client       | Currency        | CurrencyMethod | Office  | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName  | RemittanceAccount                                |
  | FRD36 Pete    | FRD36 Pete | FRD36 Client | USD - US Dollar | Bill           | Chicago | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | Chicago       | James Matt | Dentons US, LLP - Application Off-Set Bank - USD |


Scenario Outline: 030 Post Disbursement and Generate Proforma
	Given I post the disbursement
		| Work Date | Disbursement Type  | Work Currency  | Work Amount  | Narrative | Tax Code  |
		| {Today}-1 | <DisbursementType> | <WorkCurrency> | <WorkAmount> | {Auto}+36 | <TaxCode> |
	And I validate the disbursement is posted with no errors
	And I submit a time modify
		| Time Type  | Hours | Narrative | FeeEarnerName   | WorkRate | WorkCurrency   | TaxCode   |
		| <TimeType> | 0.1   | {Auto}+10 | <FeeEarnerName> | 100      | <WorkCurrency> | <TaxCode> |
	When I add a charge entry
		| Charge Type | Amount       | Tax Code  |
		| <Charge>    | <WorkAmount> | <TaxCode> |
	And I validate the post all was successful
	Then I can generate the proforma
		| Description | Include Other Proformas | Proforma Status |
		| {Auto}+36   | <IncludeOtherProformas> | Draft           |

Examples:
	| DisbursementType              | WorkCurrency | WorkAmount | TaxCode                          | IncludeOtherProformas | FeeEarnerName | TimeType       | Charge        |
	| Dentons United States Funding | USD          | 5000       | US Output Domestic Standard Rate | No                    | Matt Thomas   | FEES (Default) | Sundry Income |

@CancelProcess
Scenario Outline: 040 Verification of invoice generated
	Given I proxy as user '<User>'
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	And I open the proforma for submission
	And I verify the narratives populated in proforma edit
	And I mark the cards as non-chargeable and ensure the bill amount reduces to '0.00'
	And I verify the client and matter notes
	Then I submit it
	And I cancel proxy
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	When I open the proforma for billing
	And I bill it without printing
	And the invoice number is generated


Examples:
	| User       |
	| FRD36 Pete |
	

	Scenario: Verify Invoice Dispatch workflow
    Given I generate the government system upload for the invoice
	When I view the invoices
	Then I verify the narratives populated in Invoices
	And I verify the government system uplaod date child form details
	And I verify the prefix of the invoice matches the invoice override 
