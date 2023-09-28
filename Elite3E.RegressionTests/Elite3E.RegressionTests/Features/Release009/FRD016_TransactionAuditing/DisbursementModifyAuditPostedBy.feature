@release9 @frd016 @DisbursementModifyAuditPostedBy @ProxyTest
Feature: DisbursementModifyAuditPostedBy

FRD 016 - TransactionAuditing
Test 4 - Disbursement Modify contains the "Posted By" field which will contain the identity of the user who posted the costcard
Test 5 - Where the user is proxied in as another user and creates a disbursement the "Posted By" field in disbursement modify will contain both the identity of the user and the proxied in user.


Scenario Outline: 010 Disbursement Modify Posted By
	Given I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	And I create the Payer with Api
		| PayerName   | Entity       |
		| <PayorName> | BrightEntity |
	And I create a matter with details:
		| Client   | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency       | MatterCurrencyMethod | Office   | Department | Section | Rate     | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| <Client> | <FeeEarnerName>   | Fully Open | {Today}-1 | {Auto}+36  | <WorkCurrency> | Bill                 | <Office> | Default    | Default | Standard | Desc_at_3e8glw7     | Desc_at_3e8glw7   | <PayorName> |
	When I retrieve the current username
	And I create a hard cost disbursement type with details
		| Code               | Description        | TransactionTypeAlias  |
		| <DisbursementType> | <DisbursementType> | Anticipated Hard Cost |
	And I submit the disbursement modify
		| Work Date | Disbursement Type  | Work Currency  | Work Amount  | Narrative | Tax Code  |
		| {Today}   | <DisbursementType> | <WorkCurrency> | <WorkAmount> | {Auto}+36 | <TaxCode> |
	And I search for process 'Disbursement Modify'
	And I quick search by matter number
	Then I validate entry is posted by

@europe
Examples:
	| Client       | FeeEarnerName   | Office      | DisbursementType                    | WorkCurrency | WorkAmount | TaxCode                   | PayorName    |
	| Audit Client | Audit FeeEarner | London (EU) | DENEU-Dentons Cross Region Invoices | EUR          | 5000       | EU Output Conversion Code | Susan Wilson |
@ft
Examples:
	| Client       | FeeEarnerName   | Office         | DisbursementType | WorkCurrency | WorkAmount | TaxCode                         | PayorName    |
	| Audit Client | Audit FeeEarner | London (UKIME) |Agency Registration    | GBP          | 5000       | UK Output Domestic Standard 20% | Susan Wilson |
@qa
Examples:
	| Client       | FeeEarnerName   | Office         | DisbursementType               | WorkCurrency | WorkAmount | TaxCode                         | PayorName    |
	| Audit Client | Audit FeeEarner | London (UKIME) | Automation Agency Registration | GBP          | 5000       | UK Output Domestic Standard 20% | Susan Wilson |
@training
Examples:
	| Client       | FeeEarnerName   | Office         | DisbursementType          | WorkCurrency | WorkAmount | TaxCode                     | PayorName    |
	| Audit Client | Audit FeeEarner | London (UKIME) | Court Fees  - Anticipated | GBP          | 5000       | UK Output Domestic Standard | Susan Wilson |
@uk
Examples:
	| Client       | FeeEarnerName   | Office         | DisbursementType                    | WorkCurrency | WorkAmount | TaxCode                     | PayorName    |
	| Audit Client | Audit FeeEarner | London (UKIME) | DENUK-Dentons Cross Region Invoices | GBP          | 5000       | UK Output Domestic Standard | Susan Wilson |
@staging
Examples:
	| Client       | FeeEarnerName   | Office         | DisbursementType            | WorkCurrency | WorkAmount | TaxCode                     | PayorName    |
	| Audit Client | Audit FeeEarner | London (UKIME) | Bank & Finance Charges (NT) | GBP          | 5000       | UK Output Domestic Standard | Susan Wilson |
@canada
Examples:
	| Client       | FeeEarnerName   | Office  | DisbursementType           | WorkCurrency | WorkAmount | TaxCode                         | PayorName    |
	| Audit Client | Audit FeeEarner | Calgary | Bank of Canada Certificate | CAD          | 5000       | CA Output Domestic Standard GST | Susan Wilson |
@us
Examples:
	| Client       | FeeEarnerName   | Office  | DisbursementType                    | WorkCurrency | WorkAmount | TaxCode                          | PayorName  |
	| Audit Client | Audit FeeEarner | Chicago | DENUS-Dentons Cross Region Invoices | USD          | 5000       | US Output Domestic Standard Rate | James Bond |
@singapore
Examples:
	| Client       | FeeEarnerName   | Office    | DisbursementType      | WorkCurrency | WorkAmount | TaxCode                     | PayorName    |
	| Audit Client | Audit FeeEarner | Singapore | Application Fees (NT) | SGD          | 5000       | SG Output Domestic Standard | Susan Wilson |

@CancelProcess
Scenario Outline: 020 Proxy Disbursement Modify Posted By
	Given I create a user with details
		| UserName   | DataRoleAlias | DefaultOperatingAlias |
		| <UserName> | Admin         | Default               |
	And I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	And I create the Payer with Api
		| PayerName   | Entity       |
		| <PayorName> | BrightEntity |
	And I create a matter with details:
		| Client   | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency       | MatterCurrencyMethod | Office   | Department | Section | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| <Client> | <FeeEarnerName>   | Fully Open | {Today}-1 | {Auto}+36  | <WorkCurrency> | Bill                 | <Office> | Default    | Default | Desc_at_3e8glw7     | Desc_at_3e8glw7   | <PayorName> |
	When I retrieve the current username
	And I create a hard cost disbursement type with details
		| Code               | Description        | TransactionTypeAlias  |
		| <DisbursementType> | <DisbursementType> | Anticipated Hard Cost |
	And I proxy as user '<UserName>'
	And I submit the disbursement modify
		| Work Date | Disbursement Type  | Work Currency  | Work Amount  | Narrative | Tax Code  |
		| {Today}   | <DisbursementType> | <WorkCurrency> | <WorkAmount> | {Auto}+36 | <TaxCode> |
	And I validate submit was successful
	And I search for process 'Disbursement Modify'
	And I quick search by matter number
	Then I validate entry is posted by '<UserName>'

@ft
Examples:
	| UserName  | Client       | FeeEarnerName   | Office         | DisbursementType | WorkCurrency | WorkAmount | TaxCode                         | PayorName    |
	| AuditUser | Audit Client | Audit FeeEarner | London (UKIME) | Auditors Tea     | GBP          | 5000       | UK Output Domestic Standard 20% | Susan Wilson |
@staging
Examples:
	| UserName  | Client       | FeeEarnerName   | Office         | DisbursementType            | WorkCurrency | WorkAmount | TaxCode                     | PayorName    |
	| AuditUser | Audit Client | Audit FeeEarner | London (UKIME) | Bank & Finance Charges (NT) | GBP          | 5000       | UK Output Domestic Standard | Susan Wilson |
@training
Examples:
	| UserName  | Client       | FeeEarnerName   | Office         | DisbursementType          | WorkCurrency | WorkAmount | TaxCode                     | PayorName    |
	| AuditUser | Audit Client | Audit FeeEarner | London (UKIME) | Court Fees  - Anticipated | GBP          | 5000       | UK Output Domestic Standard | Susan Wilson |
@uk
Examples:
	| UserName  | Client       | FeeEarnerName   | Office         | DisbursementType                    | WorkCurrency | WorkAmount | TaxCode                     | PayorName    |
	| AuditUser | Audit Client | Audit FeeEarner | London (UKIME) | DENUK-Dentons Cross Region Invoices | GBP          | 5000       | UK Output Domestic Standard | Susan Wilson |
@us
Examples:
	| UserName  | Client       | FeeEarnerName   | Office  | DisbursementType   | WorkCurrency | WorkAmount | TaxCode                          | PayorName |
	| AuditUser | Audit Client | Audit FeeEarner | Chicago | Administration Fee | USD          | 5000       | US Output Domestic Standard Rate | Jon Bayer |
@singapore
Examples:
	| UserName  | Client       | FeeEarnerName   | Office    | DisbursementType      | WorkCurrency | WorkAmount | TaxCode                     | PayorName    |
	| AuditUser | Audit Client | Audit FeeEarner | Singapore | Application Fees (NT) | SGD          | 5000       | SG Output Domestic Standard | Susan Wilson |
@canada
Examples:
	| UserName  | Client       | FeeEarnerName   | Office  | DisbursementType           | WorkCurrency | WorkAmount | TaxCode                         | PayorName    |
	| AuditUser | Audit Client | Audit FeeEarner | Calgary | Bank of Canada Certificate | CAD          | 5000       | CA Output Domestic Standard GST | Susan Wilson |
@qa
Examples:
	| UserName  | Client       | FeeEarnerName   | Office         | DisbursementType               | WorkCurrency | WorkAmount | TaxCode                         | PayorName    |
	| AuditUser | Audit Client | Audit FeeEarner | London (UKIME) | Automation Agency Registration | GBP          | 5000       | UK Output Domestic Standard 20% | Susan Wilson |
@europe
Examples:
	| UserName  | Client       | FeeEarnerName   | Office      | DisbursementType                    | WorkCurrency | WorkAmount | TaxCode                   | PayorName    |
	| AuditUser | Audit Client | Audit FeeEarner | London (EU) | DENEU-Dentons Cross Region Invoices | EUR          | 5000       | EU Output Conversion Code | Susan Wilson |
	