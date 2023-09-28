@us
Feature: ProformaEdit Trust

Scenario Outline: 001_Create Client Account Intended Use
	Given I create a new ClientAccount Intended Use APi
		| Code      | Description |
		| {Auto}+10 | {Auto}+16   |

Scenario Outline: 002 Prep Fee Earner Workflow User
	Given I remove workflow user from fee earner
		| Search Column      | Search Operator | Search Value | FeeEarnerUser |
		| Workflow User.Name | Equals          | null         |               |

Scenario Outline: 003 Create Matter
	Given I create a user with details
		| UserName | DataRoleAlias |
		| <User>   | Admin         |
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

Examples:
	| User         | Client                | FeeEarnerName | Currency        | CurrencyMethod | Office            | Department | Section | ChargeTypeGroup | CostTypeGroup | PayorName  | DefaultOperatingAlias      |
	| Josepha Miah | Josepha ClientSurname | Josepha Miah  | USD - US Dollar | Bill           | US Administration | Default    | Default | Desc_at_3e8glw7 | Auto__18Xeq   | Zelma Hood | Dentons United States, LLP |

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
	

Examples:
	| TrustReceiptType | BankAcctTrust                                             | Amount | ClientAccount | Reason             |
	| Cash             | Dentons US LLP (IOLTA) Interest on Lawyer Trust-204968895 | 50000  | Automation    | Automation Comment |

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
	
Examples:
	| DisbursementType                      | WorkCurrency | WorkAmount | Narrative                 | TaxCode                          | IncludeOtherProformas |
	| Court & Stamp Fees - Anticipated (NT) | USD          | 5000       | Automation Disbursement 1 | US Output Domestic Standard Rate | No                    |


Scenario Outline: 007_Payee Maintenance
	Given I create a new Payee with the Api
		| PayeeName   |
		| <PayeeName> |

#Update the Proforma Edit process as per the latest release
Scenario: 008 View Proforma and Confirm Client intendended use
	Given I proxy as user '<User>'
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	And I open the proforma for submission
	When I add the apply client account child form intended uses
	#Then I can verify the trust disbursement type
	And I cancel proxy
Examples:
	| User         |
	| Josepha Miah |


Scenario Outline: 009_Clean up Workflow user
	Given I remove workflow user from fee earner
		| Search Column      | Search Operator | Search Value | FeeEarnerUser |
		| Workflow User.Name | Equals          | null         |               |
	Then I 'remove' a workflow user '<User>' to fee earner '<FeeEarnerName>'

Examples:
	| FeeEarnerName | User         |
	| Josepha Miah  | Josepha Miah |