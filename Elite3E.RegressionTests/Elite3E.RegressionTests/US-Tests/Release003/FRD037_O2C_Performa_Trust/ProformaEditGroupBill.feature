@us
Feature: ProformaEditGroupBill

IF Disbursement Type does not auto populate, see fix: https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/42411
	
Scenario Outline: 010_Create Client Account Intended Use
	Given I create a new ClientAccount Intended Use APi
		| Code            | Description     |
		| Code_at_TowqCrF | Desc_at_YowqCrF |
			
Scenario Outline: 015_Prep Fee Earner Workflow User
	Given I remove workflow user from fee earner
		| Search Column      | Search Operator | Search Value | FeeEarnerUser |
		| Workflow User.Name | Equals          | null         |               |

Scenario Outline: 020_Create Matter
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
	And I create a matter with details:
		| Client   | FeeEarnerFullName | Status     | OpenDate   | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| <Client> | <FeeEarnerName>   | Fully Open | {Today}-20 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <ChargeTypeGroup>   | <CostTypeGroup>   | <PayorName> |

Examples:
	| User         | Client                | FeeEarnerName | Currency        | CurrencyMethod | Office            | Department | Section | ChargeTypeGroup | CostTypeGroup | PayorName  |
	| Josepha Miah | Josepha ClientSurname | Josepha Miah  | USD - US Dollar | Bill           | US Administration | Default    | Default | Desc_at_3e8glw7 | Auto__18Xeq   | Zelma Hood |

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

Examples:
	| TrustReceiptType | BankAcctTrust                         | Amount | ClientAccount | Reason             |
	| Cash             | FRANCIS KLEIN ESCROW SUB-ACCOUNT-800355605 | 50000  | Automation    | Automation Comment |

Scenario Outline: 050_Create a proforma Edit
	Given I create a hard cost disbursement type with details
		| Code               | Description        |
		| <DisbursementType> | <DisbursementType> |
	When I post the disbursement
		| Work Date | Disbursement Type  | Work Currency  | Work Amount  | Narrative   | Tax Code  | RefCurrency |
		| {Today}-1 | <DisbursementType> | <WorkCurrency> | <WorkAmount> | <Narrative> | <TaxCode> | GBP         |
	And I validate the disbursement is posted with no errors
	Then I can generate the proforma
		| Description | Include Other Proformas | Proforma Status |
		| {Auto}+36   | No                      | Action Required |
	And I can generate the group bill
		| Billing Group  | Office group  | Prof Date |
		| <BillingGroup> | <OfficeGroup> | {Today}   |
	And I validate submit was successful
			

Examples:
	| DisbursementType                      | WorkCurrency | WorkAmount | Narrative                 | TaxCode                          | IncludeOtherProformas | BillingGroup      | OfficeGroup | RefCurrency |
	| Court & Stamp Fees - Anticipated (NT) | USD          | 5000       | Automation Disbursement 1 | US Output Domestic Standard Rate | No                    | US15800787-007467 | Chicago     | USD         |

Scenario Outline: 060_Payee Maintenance
	Given I create a new Payee with the Api
		| PayeeName |
		| {Auto}+10 |

Scenario: 070_Generate the Fiscal Invoice from Proforma Edit
	Given I proxy as user '<User>'
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	And I open the proforma for submission
	When I add the apply client account child form intended uses
	Then I can verify the trust disbursement type
	And I cancel proxy

Examples:
	| User         |
	| Josepha Miah |
	
Scenario Outline: 080_Clean up Workflow user
	Given I remove workflow user from fee earner
		| Search Column      | Search Operator | Search Value | FeeEarnerUser |
		| Workflow User.Name | Equals          | null         |               |
	Then I 'remove' a workflow user '<User>' to fee earner '<FeeEarnerName>'

Examples:
	| FeeEarnerName | User         |
	| Josepha Miah  | Josepha Miah |

	
