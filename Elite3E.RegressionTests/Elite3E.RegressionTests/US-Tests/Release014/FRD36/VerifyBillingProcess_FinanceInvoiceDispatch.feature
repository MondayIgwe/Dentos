@us
Feature: VerifyBillingProcess_FinanceInvoiceDispatch

Invoice Distribution method - Finance Dispatch : Invoice  routed to Finance team
Invoice Type mapped to Matter attribute at matter level : Invoice prefix to be taken from invoice override value linked to Invoice Type (defect)
Pres currency & proforma currency - different- Pres Currency, Exchange rates verified - Pres Date and Pres Invoice amount not  on Proforma Edit
Pres Exchange Rate not auto-populating 
Different currency remittance account - bank details not displayed in Invoice (defect)



@CancelProcess
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
	| Code     | Description           | DispatchOption   |
	| Test_Fin | Test Finance Dispatch | Finance Dispatch |
		And I search or create invoice type
	| Code              | Description       | OverrideValue     |
	| <InvoiceTypeCode> | <InvoiceTypeCode> | <InvoiceOverride> |


		Examples: 
		| InvoiceOverride    | InvoiceOverrideCode | InvoiceTypeCode | NextInvoiceNumber  | NextCreditNoteNumber | NextTaxInvoiceNumber | NextCreditNoteTaxNumber |
		| US Sundry Sequence | 8054-USSUN          | Miscellaneous   | 5054-US-2020-00004 | 5054-US-2020-90001   | 5054-US-2020-00004   | 5054-US-2020-90001      |

	Scenario Outline: 020 Fee Earner and matter setup
	Given I create a user with details
		| UserName | DataRoleAlias | UserRoleList                                                                               |
		| <User>   | Admin         | 0:AD:G:Common Authorisations,0:WC:P:Proforma Processor,0:WC:P:Proforma Processor - Finance |
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
	And I view an existing matter
	When I edit the existing matter
		| InvoiceDistributionMethod | InvoiceOverride   | PresentationCurrency   | RemittanceAccount   | Language   |
		| Test Finance Dispatch     | <InvoiceOverride> | <PresentationCurrency> | <RemittanceAccount> | <Language> |
	And I add Client Reference child form
		| StartDate | ClientReference |
		| {Today}   | {Auto}+36       |
	Then I can submit the matter

Examples:
  | FeeEarnerName | User       | Client       | Currency        | CurrencyMethod | Office  | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName  | RemittanceAccount                                | DefaultOperatingAlias      | PresentationCurrency | Language                 | InvoiceOverride    |
  | FRD36 Pete    | FRD36 Pete | FRD36 Client | USD - US Dollar | Bill           | Chicago | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | Chicago       | James Matt | Dentons US, LLP - Application Off-Set Bank - USD | Dentons United States, LLP | AUD                  | English (United Kingdom) | US Sundry Sequence |



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
Scenario Outline: 040 Verify the generated invoice
	Given I proxy as user '<User>'
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	And I open the proforma for submission
	And I verify the client reference number
	And I verify the currency and bank details in proforma edit
	And I edit the proforma edit
	| BillingOffice   | AlternativeBankDetails   | FromTaxArea   |
	| <BillingOffice> | <AlternativeBankDetails> | <FromTaxArea> |
	Then I submit it
	And I cancel proxy
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	When I open the proforma for billing
	And I bill it without printing
	And the invoice number is generated
	And I search for 'Workflow Dashboard'
	And I open the Invoice Dispatch workflow task
	And I send the invoice routed to finance team

Examples:
	| User       | BillingOffice | AlternativeBankDetails                           | FromTaxArea   |
	| FRD36 Pete | Denver        | Dentons US, LLP - Application Off-Set Bank - USD | United States |
	

	Scenario Outline: 050 Enter receipt information
	Given I add a new receipt
		| Receipt Type  | Receipt Date | Document Number | Narrative |
		| <ReceiptType> | {Today}      | {Auto}+36       | {Auto}+36 |
	And change the operating unit "<OperatingUnit>"
	When add the invoice on the receipt
	#And I verify the presentation currency and invoice amount in receipts
	Then the payer is auto populated
	And I receipt the total amount
	And update the receipt
	And I can submit the receipt
	When I view the invoices
	Then I verify the presentation currency and invoice amount
	And I verify the prefix of the invoice matches the invoice override 

Examples:
		| ReceiptType | OperatingUnit   |
		| NFCITIUSD   | Dentons US, LLP |
	
	
