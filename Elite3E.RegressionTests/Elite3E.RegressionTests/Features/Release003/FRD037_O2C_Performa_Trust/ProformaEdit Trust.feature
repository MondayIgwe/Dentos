@release3 @frd037 @ProformaEditTrust
Feature: ProformaEdit Trust

@ft @training @staging @canada @europe @uk @singapore @qa
Scenario Outline: 001_Create Client Account Intended Use
	Given I create a new ClientAccount Intended Use APi
		| Code      | Description |
		| {Auto}+10 | {Auto}+16   |

@ft @training @staging @canada @europe @uk @singapore @qa
Scenario Outline: 002 Prep Fee Earner Workflow User
	Given I remove workflow user from fee earner
		| Search Column      | Search Operator | Search Value | FeeEarnerUser |
		| Workflow User.Name | Equals          | null         |               |

Scenario Outline: 003 Create Matter
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
	When I create a matter with details:
		| Client   | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| <Client> | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <ChargeTypeGroup>   | <CostTypeGroup>   | <PayorName> |

@ft @qa
Examples:
	| User         | Client                | FeeEarnerName | Currency            | CurrencyMethod | Office         | Department | Section | ChargeTypeGroup | CostTypeGroup | PayorName  | DefaultOperatingAlias          |
	| Josepha Miah | Josepha ClientSurname | Josepha Miah  | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Desc_at_3e8glw7 | Auto__18Xeq   | Zelma Hood | Dentons UK and Middle East LLP |
@training @staging
Examples:
	| User         | Client                | FeeEarnerName | Currency            | CurrencyMethod | Office         | Department | Section | ChargeTypeGroup | CostTypeGroup | PayorName  | DefaultOperatingAlias          |
	| Josepha Miah | Josepha ClientSurname | Josepha Miah  | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Desc_at_3e8glw7 | Auto__18Xeq   | Zelma Hood | Dentons UK and Middle East LLP |
@europe
Examples:
	| User         | Client                | FeeEarnerName | Currency   | CurrencyMethod | Office      | Department | Section | ChargeTypeGroup | CostTypeGroup | PayorName  | DefaultOperatingAlias   |
	| Josepha Miah | Josepha ClientSurname | Josepha Miah  | EUR - Euro | Bill           | London (EU) | Default    | Default | Desc_at_3e8glw7 | Auto__18Xeq   | Zelma Hood | Dentons Europe LLP (UK) |
@canada
Examples:
	| User         | Client                | FeeEarnerName | Currency              | CurrencyMethod | Office    | Department          | Section           | ChargeTypeGroup | CostTypeGroup | PayorName  | DefaultOperatingAlias |
	| Josepha Miah | Josepha ClientSurname | Josepha Miah  | CAD - Canadian Dollar | Bill           | Vancouver | Banking and Finance | Banking & Finance | Desc_at_3e8glw7 | Auto__18Xeq   | Zelma Hood | Dentons Canada LLP    |
@uk
Examples:
	| User         | Client                | FeeEarnerName | Currency            | CurrencyMethod | Office         | Department | Section | ChargeTypeGroup | CostTypeGroup | PayorName  | DefaultOperatingAlias          |
	| Josepha Miah | Josepha ClientSurname | Josepha Miah  | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Desc_at_3e8glw7 | Auto__18Xeq   | Zelma Hood | Dentons UK and Middle East LLP |
@singapore
Examples:
	| User         | Client                | FeeEarnerName | Currency               | CurrencyMethod | Office    | Department            | Section | ChargeTypeGroup | CostTypeGroup | PayorName  | DefaultOperatingAlias        |
	| Josepha Miah | Josepha ClientSurname | Josepha Miah  | SGD - Singapore Dollar | Bill           | Singapore | Corporate - Singapore | Default | Desc_at_3e8glw7 | Auto__18Xeq   | Zelma Hood | Dentons Rodyk & Davidson LLP |

@ft @training @staging @canada @europe @uk @singapore @qa
Scenario Outline: 004 Verify Intended use process billing is enabled
	When I open the client account intended use process
	And search client account intended use
	Then I can confirm the Intended use option is enabled.

Scenario Outline: 005 client account reciept process
	Given I view the client account reciept process
	And I add the required fields
		| Trust Receipt Type | Bank Acct Trust | DrawnBy  |
		| <TrustReceiptType> | <BankAcctTrust> | {Auto}+6 |
	And select Client Account Intendeded Use from client account reciept detail form
	Then I can add the amount and trust receipt type
		| Amount   | Trust Receipt Type | Reason   |
		| <Amount> | <TrustReceiptType> | <Reason> |
	
@ft
Examples:
	| TrustReceiptType | BankAcctTrust                    | Amount | ClientAccount | Reason             |
	| Cash             | Perth - ANZ Bank Trust Acc - AUD | 50000  | Automation    | Automation Comment |
@staging @qa
Examples:
	| TrustReceiptType | BankAcctTrust   | Amount | ClientAccount | Reason             |
	| Cash             | Test Automation | 50000  | Automation    | Automation Comment |
@training
Examples:
	| TrustReceiptType | BankAcctTrust              | Amount | ClientAccount | Reason             |
	| Cash             | Canada Test Bank Account 1 | 50000  | Automation    | Automation Comment |
@canada
Examples:
	| TrustReceiptType | BankAcctTrust                         | Amount | ClientAccount | Reason             |
	| Cash             | Royal Bank of Canada U.S.-00970212404 | 50000  | Automation    | Automation Comment |
@europe
Examples:
	| TrustReceiptType | BankAcctTrust                                    | Amount | ClientAccount | Reason             |
	| Cash             | LON533 London Billing (EU)_Barclays Bank PLC_GBP | 50000  | Automation    | Automation Comment |
@uk
Examples:
	| TrustReceiptType | BankAcctTrust               | Amount | ClientAccount | Reason             |
	| Cash             | CA Client Account Bank Test | 50000  | Automation    | Automation Comment |
@singapore
Examples:
	| TrustReceiptType                | BankAcctTrust                      | Amount | ClientAccount | Reason             |
	| Cheque - Local Bank - Singapore | L&Y-Client-Dah Heng Bank (Current) | 50000  | Automation    | Automation Comment |


Scenario Outline: 006 Create a proforma Edit
	Given I create a hard cost disbursement type with details
		| Code               | Description        |
		| <DisbursementType> | <DisbursementType> |
	When I post the disbursement
		| Work Date | Disbursement Type  | Work Currency  | Work Amount  | Narrative   | Tax Code  |
		| {Today}-1 | <DisbursementType> | <WorkCurrency> | <WorkAmount> | <Narrative> | <TaxCode> |
	And I validate the disbursement is posted with no errors
	Then I can generate the proforma
		| Description | Include Other Proformas | Proforma Status |
		| {Auto}+36   | No                      | Draft           |
	
@ft
Examples:
	| DisbursementType                      | WorkCurrency | WorkAmount | Narrative                 | TaxCode                         | IncludeOtherProformas |
	| Court & Stamp Fees - Anticipated (NT) | GBP          | 5000       | Automation Disbursement 1 | UK Output Domestic Standard 20% | No                    |
@qa
Examples:
	| DisbursementType               | WorkCurrency | WorkAmount | Narrative                 | TaxCode                         | IncludeOtherProformas |
	| Automation Agency Registration | GBP          | 5000       | Automation Disbursement 1 | UK Output Domestic Standard 20% | No                    |
@staging
Examples:
	| DisbursementType                             | WorkCurrency | WorkAmount | Narrative                 | TaxCode                   | IncludeOtherProformas |
	| Registration Fees - Caveat -Anticipated (NT) | GBP          | 5000       | Automation Disbursement 1 | UK Output Domestic Exempt | No                    |
@training
Examples:
	| DisbursementType          | WorkCurrency | WorkAmount | Narrative                 | TaxCode                   | IncludeOtherProformas |
	| Court Fees  - Anticipated | GBP          | 5000       | Automation Disbursement 1 | UK Output Domestic Exempt | No                    |
@canada
Examples:
	| DisbursementType           | WorkCurrency | WorkAmount | Narrative                 | TaxCode                         | IncludeOtherProformas |
	| Bank of Canada Certificate | CAD          | 5000       | Automation Disbursement 1 | CA Output Domestic Standard GST | No                    |
@europe
Examples:
	| DisbursementType                    | WorkCurrency | WorkAmount | Narrative                 | TaxCode               | IncludeOtherProformas |
	| DENEU-Dentons Cross Region Invoices | EUR          | 5000       | Automation Disbursement 1 | ES Output Europe Zero | No                    |
@uk
Examples:
	| DisbursementType                    | WorkCurrency | WorkAmount | Narrative                 | TaxCode                 | IncludeOtherProformas |
	| DENUK-Dentons Cross Region Invoices | GBP          | 5000       | Automation Disbursement 1 | AE Output Domestic Zero | No                    |
@singapore
Examples:
	| DisbursementType      | WorkCurrency | WorkAmount | Narrative                 | TaxCode                     | IncludeOtherProformas |
	| Application Fees (NT) | SGD          | 5000       | Automation Disbursement 1 | SG Output Domestic Standard | No                    |


@ft @training @staging @canada @europe @uk @singapore @qa
Scenario Outline: 007_Payee Maintenance
	Given I create a new Payee with the Api
		| PayeeName   |
		| <PayeeName> |

@CancelProcess
#Update the Proforma Edit process as per the latest release
Scenario: 008 View Proforma and Confirm Client intendended use
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


Scenario Outline: 009_Clean up Workflow user
	Given I remove workflow user from fee earner
		| Search Column      | Search Operator | Search Value | FeeEarnerUser |
		| Workflow User.Name | Equals          | null         |               |
	Then I 'remove' a workflow user '<User>' to fee earner '<FeeEarnerName>'

@CancelProcess @ft @training @staging @canada @europe @uk @singapore @qa
Examples:
	| FeeEarnerName | User         |
	| Josepha Miah  | Josepha Miah |