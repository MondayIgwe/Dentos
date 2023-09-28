@release3 @frd037 @ProformaEditGroupBill
Feature: ProformaEditGroupBill

IF Disbursement Type does not auto populate, see fix: https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/42411
	
@ft @training @staging @canada @europe @uk @singapore @qa
Scenario Outline: 010_Create Client Account Intended Use
	Given I create a new ClientAccount Intended Use APi
		| Code            | Description     |
		| Code_at_TowqCrF | Desc_at_YowqCrF |
			
@ft @training @staging @canada @europe @uk @singapore @qa
Scenario Outline: 015_Prep Fee Earner Workflow User
	Given I remove workflow user from fee earner
		| Search Column      | Search Operator | Search Value | FeeEarnerUser |
		| Workflow User.Name | Equals          | null         |               |

Scenario Outline: 020_Create Matter
	Given I create a user with details
		| UserName | DataRoleAlias | DefaultOperatingAlias   |
		| <User>   | Admin         | <DefaultOperatingAlias> |
	And I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
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
		| Client   | FeeEarnerFullName | Status     | OpenDate   | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| <Client> | <FeeEarnerName>   | Fully Open | {Today}-20 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <ChargeTypeGroup>   | <CostTypeGroup>   | <PayorName> |

@ft @qa
Examples:
	| User         | Client                | FeeEarnerName | Currency            | CurrencyMethod | Office         | Department | Section | ChargeTypeGroup | CostTypeGroup | PayorName  | DefaultOperatingAlias          |
	| Josepha Miah | Josepha ClientSurname | Josepha Miah  | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Desc_at_3e8glw7 | Auto__18Xeq   | Zelma Hood | Dentons UK and Middle East LLP |
@training @staging
Examples:
	| User         | Client                | FeeEarnerName | Currency            | CurrencyMethod | Office         | Department | Section | ChargeTypeGroup | CostTypeGroup | PayorName  | DefaultOperatingAlias          |
	| Josepha Miah | Josepha ClientSurname | Josepha Miah  | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Desc_at_3e8glw7 | Auto__18Xeq   | Zelma Hood | Dentons UK and Middle East LLP |
@canada
Examples:
	| User         | Client                | FeeEarnerName | Currency              | CurrencyMethod | Office  | Department          | Section           | ChargeTypeGroup | CostTypeGroup | PayorName  | DefaultOperatingAlias          |
	| Josepha Miah | Josepha ClientSurname | Josepha Miah  | CAD - Canadian Dollar | Bill           | Calgary | Banking and Finance | Banking & Finance | Desc_at_3e8glw7 | Auto__18Xeq   | Zelma Hood | Dentons UK and Middle East LLP |
@europe
Examples:
	| User         | Client                | FeeEarnerName | Currency   | CurrencyMethod | Office              | Department | Section | ChargeTypeGroup | CostTypeGroup | PayorName  | DefaultOperatingAlias          |
	| Josepha Miah | Josepha ClientSurname | Josepha Miah  | EUR - Euro | Bill           | London Billing (EU) | Default    | Default | Desc_at_3e8glw7 | Auto__18Xeq   | Zelma Hood | Dentons UK and Middle East LLP |
@uk
Examples:
	| User         | Client                | FeeEarnerName | Currency            | CurrencyMethod | Office         | Department | Section | ChargeTypeGroup | CostTypeGroup                    | PayorName  | DefaultOperatingAlias          |
	| Josepha Miah | Josepha ClientSurname | Josepha Miah  | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Desc_at_3e8glw7 | automation_cost_type_group_37ygu | Zelma Hood | Dentons UK and Middle East LLP |
@singapore
Examples:
	| User         | Client                | FeeEarnerName | Currency               | CurrencyMethod | Office    | Department            | Section | ChargeTypeGroup | CostTypeGroup | PayorName  | DefaultOperatingAlias          |
	| Josepha Miah | Josepha ClientSurname | Josepha Miah  | SGD - Singapore Dollar | Bill           | Singapore | Corporate - Singapore | Default | Desc_at_3e8glw7 | Auto__18Xeq   | Zelma Hood | Dentons UK and Middle East LLP |


@ft @training @staging @canada @europe @uk @singapore @qa
Scenario Outline: 030_Verify Intended use process billing is enabled
	When I open the client account intended use process
	And search client account intended use
	Then I can confirm the Intended use option is enabled.

Scenario Outline: 040_client account reciept process
	Given I view the client account reciept process
	And I add the required fields
		| Trust Receipt Type | Bank Acct Trust | DrawnBy  |
		| <TrustReceiptType> | <BankAcctTrust> | {Auto}+6 |
	And select Client Account Intendeded Use from client account reciept detail form
	Then I can add the amount and trust receipt type
		| Amount   | Trust Receipt Type | Reason   |
		| <Amount> | <TrustReceiptType> | <Reason> |
	And I validate submit was successful
@ft
Examples:
	| TrustReceiptType | BankAcctTrust      | Amount | ClientAccount | Reason             |
	| Cash             | Portia Testing 123 | 50000  | Automation    | Automation Comment |
@training
Examples:
	| TrustReceiptType | BankAcctTrust              | Amount | ClientAccount | Reason             |
	| Cash             | Canada Test Bank Account 1 | 50000  | Automation    | Automation Comment |
@staging
Examples:
	| TrustReceiptType | BankAcctTrust   | Amount | ClientAccount | Reason             |
	| Cash             | Test Automation | 50000  | Automation    | Automation Comment |
@qa
Examples:
	| TrustReceiptType | BankAcctTrust   | Amount | ClientAccount | Reason             |
	| Cash             | Test Automation | 50000  | Automation    | Automation Comment |
@canada
Examples:
	| TrustReceiptType | BankAcctTrust                         | Amount | ClientAccount | Reason             |
	| Cash             | Royal Bank of Canada U.S.-00970212404 | 50000  | Automation    | Automation Comment |
@europe
Examples:
	| TrustReceiptType | BankAcctTrust              | Amount | ClientAccount | Reason             |
	| Cash             | Barclays Bank PLC (LON230) | 50000  | Automation    | Automation Comment |
@uk
Examples:
	| TrustReceiptType | BankAcctTrust                                | Amount | ClientAccount | Reason             |
	| Cash             | C40 London (UKIME)_UK Client Account TBC_AUD | 50000  | Automation    | Automation Comment |
@singapore
Examples:
	| TrustReceiptType | BankAcctTrust                      | Amount | ClientAccount | Reason             |
	| Cash             | L&Y-Client-Dah Heng Bank (Current) | 50000  | Automation    | Automation Comment |


Scenario Outline: 050_Create a proforma Edit
	Given I create a hard cost disbursement type with details
		| Code               | Description        |
		| <DisbursementType> | <DisbursementType> |
	When I post the disbursement
		| Work Date  | Disbursement Type  | Work Currency  | Work Amount  | Narrative   | Tax Code  | RefCurrency |
		| {Today}-20 | <DisbursementType> | <WorkCurrency> | <WorkAmount> | <Narrative> | <TaxCode> | GBP         |
	And I validate the disbursement is posted with no errors
	Then I can generate the proforma
		| Description | Include Other Proformas | Proforma Status |
		| {Auto}+36   | No                      | Action Required |
	And I can generate the group bill
		| Billing Group  | Office group  | Prof Date |
		| <BillingGroup> | <OfficeGroup> | {Today}   |
	And I validate submit was successful
			
@ft
Examples:
	| DisbursementType                      | WorkCurrency | WorkAmount | Narrative                 | TaxCode                         | IncludeOtherProformas | BillingGroup                   | OfficeGroup | RefCurrency |
	| Court & Stamp Fees - Anticipated (NT) | GBP          | 5000       | Automation Disbursement 1 | UK Output Domestic Standard 20% | No                    | ICB - Unit 3000 owes Unit 3021 | Aberdeen    | GBP         |
@qa
Examples:
	| DisbursementType               | WorkCurrency | WorkAmount | Narrative                 | TaxCode                         | IncludeOtherProformas | BillingGroup                   | OfficeGroup | RefCurrency |
	| Automation Agency Registration | GBP          | 5000       | Automation Disbursement 1 | UK Output Domestic Standard 20% | No                    | ICB - Unit 3000 owes Unit 3021 | Aberdeen    | GBP         |
@staging
Examples:
	| DisbursementType                             | WorkCurrency | WorkAmount | Narrative                 | TaxCode                   | IncludeOtherProformas | BillingGroup       | OfficeGroup | RefCurrency |
	| Registration Fees - Caveat -Anticipated (NT) | GBP          | 5000       | Automation Disbursement 1 | UK Output Domestic Exempt | No                    | FABG Billing Group | Aberdeen    | GBP         |
@training
Examples:
	| DisbursementType          | WorkCurrency | WorkAmount | Narrative                 | TaxCode                   | IncludeOtherProformas | BillingGroup              | OfficeGroup | RefCurrency |
	| Court Fees  - Anticipated | GBP          | 5000       | Automation Disbursement 1 | UK Output Domestic Exempt | No                    | AE Output Domestic Exempt | Aberdeen    | GBP         |
@canada
Examples:
	| DisbursementType           | WorkCurrency | WorkAmount | Narrative                 | TaxCode                         | IncludeOtherProformas | BillingGroup         | OfficeGroup | RefCurrency |
	| Bank of Canada Certificate | CAD          | 5000       | Automation Disbursement 1 | CA Output Domestic Standard GST | No                    | TDAM - TRANSACTIONAL | Vancouver   | CAD         |
@europe
Examples:
	| DisbursementType                    | WorkCurrency | WorkAmount | Narrative                 | TaxCode               | IncludeOtherProformas | BillingGroup | OfficeGroup         | RefCurrency |
	| DENEU-Dentons Cross Region Invoices | EUR          | 5000       | Automation Disbursement 1 | ES Output Europe Zero | No                    | EBRD EURO    | London Billing (EU) | EUR         |
@uk
Examples:
	| DisbursementType                    | WorkCurrency | WorkAmount | Narrative                 | TaxCode                     | IncludeOtherProformas | BillingGroup | OfficeGroup    | RefCurrency |
	| DENUK-Dentons Cross Region Invoices | GBP          | 5000       | Automation Disbursement 1 | UK Output Domestic Standard | No                    | CIC          | London (UKIME) | GBP         |
@singapore
Examples:
	| DisbursementType      | WorkCurrency | WorkAmount | Narrative                 | TaxCode                     | IncludeOtherProformas | BillingGroup                   | OfficeGroup | RefCurrency |
	| Application Fees (NT) | SGD          | 5000       | Automation Disbursement 1 | SG Output Domestic Standard | No                    | ICB - Unit 3000 owes Unit 3021 | Singapore   | GBP         |

@ft @training @staging @canada @europe @uk @singapore @qa
Scenario Outline: 060_Payee Maintenance
	Given I create a new Payee with the Api
		| PayeeName |
		| {Auto}+10 |

@CancelProcess
Scenario: 070_Generate the Fiscal Invoice from Proforma Edit
	Given I proxy as user '<User>'
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	And I open the proforma for submission
	When I add the apply client account child form intended uses
	Then I can verify the trust disbursement type
	And I cancel proxy

@ft @training @staging @canada @europe @uk @singapore @qa
Examples:
	| User         |
	| Josepha Miah |
	
Scenario Outline: 080_Clean up Workflow user
	Given I remove workflow user from fee earner
		| Search Column      | Search Operator | Search Value | FeeEarnerUser |
		| Workflow User.Name | Equals          | null         |               |
	Then I 'remove' a workflow user '<User>' to fee earner '<FeeEarnerName>'

@CancelProcess @ft @training @staging @canada @europe @uk @singapore @qa
Examples:
	| FeeEarnerName | User         |
	| Josepha Miah  | Josepha Miah |

	
