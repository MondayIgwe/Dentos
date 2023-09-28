@ignore
Feature: BillingRule_OAC_Proforma

Billing Rule - OAC - warning message on submitting proforma (defect)
UDF records to be verified in Proforma Edit (defect)
#Defect - https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/67276

Scenario Outline: 005 Verify that a new  flag to the UDF List has been added
	Given I navigate to the UDF List process
	When I add a new UDF List and fill all the mandatory fields
		| Code   | Description   | UDF   |
		| <Code> | <Description> | <UDF> |
	And I add a new Attributes record providing the '<UDF>'
	And I submit it
@ft @training @staging @qa @uk @europe @canada
Examples:
	| Code     | Description | UDF         |
	| {Auto}+6 | {Auto}+10   | Claim Value | 
@singapore
Examples:
	| Code     | Description | UDF      |
	| {Auto}+6 | {Auto}+10   | CLAIMVAL |

@ft @training @staging @qa @uk @europe @canada @singapore
Scenario Outline:010  Invoice Distribution Method setup
	Given I search or create invoice distribution method
	| Code      | Description        | DispatchOption |
	| Test_Auto | Test Auto Dispatch | Auto Dispatch  |


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
	And I view an existing matter
	When I edit the existing matter
		| InvoiceDistributionMethod | UDFValue |
		| Test Auto Dispatch        | 100.00   |
	Then I can submit the matter
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
	When I update the invoice type '<InvoiceType>'
	Then I submit it
	#And I verify the warning '<Message>' in proforma edit



@ft @training @staging @qa @uk @europe @canada @singapore
Examples:
	| User       | InvoiceType | Message                                                                  |
	| FRD36 Pete | OAC         | Warning: Proformas with Invoice Type OAC cannot have Time or Cost cards. |

	
