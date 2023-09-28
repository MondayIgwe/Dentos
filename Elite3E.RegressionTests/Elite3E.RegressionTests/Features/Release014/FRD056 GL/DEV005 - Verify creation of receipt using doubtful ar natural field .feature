@ignore
Feature: DEV005 - Verify creation of receipt using doubtful ar natural field

Defect: https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/64025

https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/60986
https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/60987
https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/60989
https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/60990

Scenario Outline: 010 Create a new Matter
	Given I create a user with details
		| UserName | DataRoleAlias |
		| <User>   | Admin         |
	And I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	And I update the fee earner email body template
		| Language                 | CoverLetterNarrative                               |
		| English (United Kingdom) | Auto Email @BillingTkprDisplayName@ @MatterNumber@ |
	Then I add a workflow user to a FeeEarner
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
		| InvoiceDistributionMethod | InvoiceOverride                | PresentationCurrency   | RemittanceAccount   | Language                 | PresentationExchangeRate   |
		| Test Auto Dispatch        | London (UKME) Nominal Sequence | <PresentationCurrency> | <RemittanceAccount> | English (United Kingdom) | <PresentationExchangeRate> |
	And I add Client Reference child form
		| StartDate | ClientReference |
		| {Today}   | {Auto}+36       |
	Then I can submit the matter

@ft @training @staging @qa @uk
Examples:
	| FeeEarnerName | User      | Client       | Currency            | CurrencyMethod | Office  | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice  | PayorName  | RemittanceAccount                  | PresentationCurrency | PresentationExchangeRate |
	| FRD56 Tom     | FRD56 Tom | FRD56 Client | GBP - British Pound | Bill           | Default | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | London (UKIME) | James Matt | London UKME - HSBC Off 1 Acc - EUR | GBP                  | 1.00                     |
@europe
Examples:
	| FeeEarnerName | User      | Client       | Currency   | CurrencyMethod | Office              | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName  | RemittanceAccount               | PresentationCurrency | PresentationExchangeRate |
	| FRD56 Tom     | FRD56 Tom | FRD56 Client | EUR - Euro | Bill           | London Billing (EU) | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | London (EU)   | James Matt | Citibank Europe plc (BUDEURESC) | EUR                  | 1.00                     |
@canada
Examples:
	 | FeeEarnerName | User      | Client       | Currency              | CurrencyMethod | Office   | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName  | RemittanceAccount                   | PresentationCurrency | PresentationExchangeRate |
	 | FRD56 Tom     | FRD56 Tom | FRD56 Client | CAD - Canadian Dollar | Bill           | Montreal | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | Montreal      | James Matt | Bank of Montreal IBA Trust-1248-814 | CAD                  | 1.00                     |
@singapore
Examples:
  | FeeEarnerName | User      | Client       | Currency               | CurrencyMethod | Office    | Department            | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName  | RemittanceAccount                             | PresentationCurrency | PresentationExchangeRate |
  | FRD56 Tom     | FRD56 Tom | FRD56 Client | SGD - Singapore Dollar | Bill           | Singapore | Corporate - Singapore | Default | Auto_IncludeALL | Auto_IncludeALL | Singapore     | James Matt | Singapore - OCBC (LnL & DrnD) Trust Acc - SGD | GBP                  | 1.00                     |


Scenario Outline: 020 Post Disbursement and Generate Proforma
	Given I post the disbursement
		| Work Date | Disbursement Type  | Work Currency  | Work Amount  | Narrative | Tax Code  |
		| {Today}-1 | <DisbursementType> | <WorkCurrency> | <WorkAmount> | {Auto}+36 | <TaxCode> |
	And I validate the disbursement is posted with no errors
	Then I can generate the proforma
		| Description | Include Other Proformas | Proforma Status |
		| {Auto}+36   | <IncludeOtherProformas> | Draft           |
@ft @training @staging @qa @uk
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
Scenario Outline: 030 Verification on invoice generated
	Given I proxy as user '<User>'
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	And I open the proforma for submission
	And I verify the client reference number
	And I verify the currency and bank details in proforma edit
	And I edit the proforma edit
		| InvoiceType   |
		| <InvoiceType> |
	Then I submit it
	And I cancel proxy
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	Then I open the proforma for billing
	And I bill it without printing
	And the invoice number is generated

@ft @training @staging @qa @uk @europe @canada @singapore
Examples:
	| User      | InvoiceType |
	| FRD56 Tom | OAC         |

@ft @training @staging @qa @uk @europe @canada @singapore
Scenario: 040 Verify doubtful A/R natural field is present in Receipt Type
	Given I add a new 'Receipt Type'
	Then Verify the given fields are present
		| FieldName               |
		| Doubtful A/R GL Natural |
	And I verify the doubtful A/R natural field is readonly and editable
	And I cancel the process

@CancelProcess
Scenario: 050 Verify creation receipt and GL postings
	Given I search for process 'Receipt Type'
	And I perform quick find for '<ReceiptType>'
	And I update the doubtful A/R GL natural value to '<DoubtfulARNatural>'
	And I submit it
	And I validate submit was successful
	When I add a new receipt
		| Receipt Type  | Receipt Date | Document Number | Narrative |
		| <ReceiptType> | {Today}      | {Auto}+36       | {Auto}+36 |
	And change the operating unit "<OperatingUnit>"
	And add the invoice on the receipt
	And I receipt the total amount
	And update the receipt
	Then I can submit the receipt
	And I locate the submitted receipt
	And I view the gl postings
	And I validate gl postings status
	And I validate doubtful AR GL natural '<DoubtfulARNatural>' is shown
	And I close the gl postings

@ft @training @staging @qa @uk
Examples:
	| ReceiptType | DoubtfulARNatural | OperatingUnit                  |
	| DOUBT_PRVN  | 202060            | Dentons UK and Middle East LLP |
@europe
Examples:
| ReceiptType | DoubtfulARNatural | OperatingUnit           |
| PAY_3501EUR | 202060            | Dentons Europe LLP (UK) |
@canada
Examples:
| ReceiptType                      | DoubtfulARNatural | OperatingUnit      |
| Credit Card / Interactions - CAD | 202060            | Dentons Canada LLP |
@singapore
Examples:
| ReceiptType | DoubtfulARNatural | OperatingUnit                |
| SGPRMBLYHKD | 202060            | Dentons Rodyk & Davidson LLP |

@CancelProcess @ft @training @staging @qa @uk @europe @canada @singapore
Scenario: 060 Receipt reverse
	Given I locate the submitted receipt
	When I perform the reversal
		| ReversalDate | Reason    | Comment          | ReAllocate |
		| {Today}+1    | BILL_PAID | Receipt Reversal | false      |
	And I update it
	And I submit it
	Then I validate submit was successful
