@ignore
Feature: BillingWorkflow_GroupProforma

#Defect https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/69647

# Need to be revisited. Group Proforma workflow defect


	Scenario Outline: 020 Create a new Matter
	Given I create a user with details
		| UserName | DataRoleAlias | DefaultOperatingAlias   | UserRoleList          |
		| <User>   | Admin         | <DefaultOperatingAlias> | DEFAULT_WORKFLOW_ROLE |
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
		| Client   | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | ChargeTypeGroupName | CostTypeGroupName | BillingOffice   | PayorName   | BillingGroupDescription   |
		| <Client> | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <ChargeTypeGroup>   | <CostTypeGroup>   | <BillingOffice> | <PayorName> | <BillingGroupDescription> |
		

@ft @training @staging @qa @uk
Examples:
	| FeeEarnerName | User       | Client       | Currency            | CurrencyMethod | Office  | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice  | PayorName  | RemittanceAccount                  | PresentationCurrency | PresentationExchangeRate | BillingGroupDescription                   |
	| FRD36 Pete    | FRD36 Pete | FRD36 Client | GBP - British Pound | Bill           | Default | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | London (UKIME) | James Matt | London UKME - HSBC Off 1 Acc - EUR | GBP                  | 1.00                     | ICB - Unit 1002 owes Unit 3000 GBP ALL 10 |
@europe
Examples:
	| FeeEarnerName | User       | Client       | Currency   | CurrencyMethod | Office              | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName  | RemittanceAccount               | PresentationCurrency | PresentationExchangeRate | BillingGroupDescription |
	| FRD36 Pete    | FRD36 Pete | FRD36 Client | EUR - Euro | Bill           | London Billing (EU) | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | London (EU)   | James Matt | Citibank Europe plc (BUDEURESC) | GBP                  | 1.00                     | EBRD EURO               |
@canada
Examples:
	 | FeeEarnerName | User       | Client       | Currency              | CurrencyMethod | Office   | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName  | RemittanceAccount                   | PresentationCurrency | PresentationExchangeRate | BillingGroupDescription |
	 | FRD36 Pete    | FRD36 Pete | FRD36 Client | CAD - Canadian Dollar | Bill           | Montreal | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | Montreal      | James Matt | Bank of Montreal IBA Trust-1248-814 | GBP                  | 1.00                     | TDAM - TRANSACTIONAL    |
@singapore
Examples:
  | FeeEarnerName | User       | Client       | Currency               | CurrencyMethod | Office    | Department            | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName  | RemittanceAccount                             | PresentationCurrency | PresentationExchangeRate | BillingGroupDescription        |
  | FRD36 Pete    | FRD36 Pete | FRD36 Client | SGD - Singapore Dollar | Bill           | Singapore | Corporate - Singapore | Default | Auto_IncludeALL | Auto_IncludeALL | Singapore     | James Matt | Singapore - OCBC (LnL & DrnD) Trust Acc - SGD | GBP                  | 1.00                     | ICB - Unit 3000 owes Unit 3021 |

	


Scenario Outline: 030 Post Disbursement and Generate Proforma
	Given I post the disbursement
		| Work Date | Disbursement Type  | Work Currency  | Work Amount  | Narrative | Tax Code  |
		| {Today}-1 | <DisbursementType> | <WorkCurrency> | <WorkAmount> | {Auto}+36 | <TaxCode> |
	And I validate the disbursement is posted with no errors
	Then I can generate the proforma
		| Description | Include Other Proformas | Proforma Status | BillingGroup   |
		| {Auto}+36   | <IncludeOtherProformas> | Draft           | <BillingGroup> |
@ft
Examples:
	| DisbursementType    | WorkCurrency | WorkAmount | TaxCode                    | IncludeOtherProformas | BillingGroup                              |
	| Advertising Charges | GBP          | 5000       | UK Output Domestic Zero 0% | No                    | ICB - Unit 1002 owes Unit 3000 GBP ALL 10 |
@uk
Examples:
	| DisbursementType        | WorkCurrency | WorkAmount | TaxCode                     | IncludeOtherProformas | BillingGroup |
	| Automation Accomodation | GBP          | 5000       | UK Output Domestic Standard | No                    | CIC          |
@qa
Examples:
	| DisbursementType | WorkCurrency | WorkAmount | TaxCode                         | IncludeOtherProformas | BillingGroup                   |
	| Accommodation    | GBP          | 5000       | UK Output Domestic Standard 20% | No                    | ICB - Unit 3000 owes Unit 3021 |
@training @staging
Examples:
	| DisbursementType                                       | WorkCurrency | WorkAmount | TaxCode                     | IncludeOtherProformas | BillingGroup                   |
	| Registraton Fees - Issuance of SSCT - Anticipated (NT) | GBP          | 5000       | UK Output Domestic Standard | No                    | ICB - Unit 3000 owes Unit 3021 |
@singapore
Examples:
	| DisbursementType         | WorkCurrency | WorkAmount | TaxCode                     | IncludeOtherProformas | BillingGroup                   |
	| Agency Registration (NT) | SGD          | 5000       | SG Output Domestic Standard | No                    | ICB - Unit 3000 owes Unit 3021 |
@canada
Examples:
	| DisbursementType           | WorkCurrency | WorkAmount | TaxCode                   | IncludeOtherProformas | BillingGroup         |
	| Bank of Canada Certificate | CAD          | 5000       | CA Output BC Standard PST | No                    | TDAM - TRANSACTIONAL |
@europe
Examples:
	| DisbursementType        | WorkCurrency | WorkAmount | TaxCode               | IncludeOtherProformas | BillingGroup |
	| Automation Accomodation | EUR          | 5000       | ES Output Europe Zero | No                    | EBRD EURO    |

@CancelProcess
Scenario Outline: 040 Verification on invoice generated
	Given I proxy as user '<User>'
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	And I open the proforma for submission
	When I submit it
	And I cancel proxy
	And I bill the group proforma
	And the invoice number is generated
	And I proxy as user '<User>'
	And I search for 'Workflow Dashboard'
	And I open the Invoice Dispatch workflow task
	And I send the invoice routed to the timekeeper
	And I cancel proxy
	Then I view the invoices
@ft @training @staging @qa @uk @europe @canada @singapore
Examples:
	| User       |
	| FRD36 Pete |