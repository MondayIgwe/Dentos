@release9 @frd016 @TimeModifyAuditPostedBy @ProxyTest
Feature: TimeModifyAuditPostedBy

FRD 016 - TransactionAuditing
Test 2 - Time Modify contains the "Posted By" field which will contain the identity of the user who posted the timecard
Test 3 - Where the user is proxied in as another user and created a timecard the "Posted By" field in time modify will contain both the identity of the user and the proxied in user.


Scenario Outline: 010 Time Modify Posted By
	Given I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	And I create the Payer with Api
		| PayerName   | Entity       |
		| <PayorName> | BrightEntity |
	And I create a matter with details:
		| Client   | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department | Section | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| <Client> | <FeeEarnerName>   | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | Bill                 | <Office> | Default    | Default | Desc_at_3e8glw7     | Desc_at_3e8glw7   | <PayorName> |
	When I retrieve the current username
	And I submit a time modify
		| Time Type  | Hours | Narrative | FeeEarnerName   | WorkRate | WorkCurrency |
		| <TimeType> | 0.1   | {Auto}+10 | <FeeEarnerName> | 100      | <Currency>   |
	Then I search for process 'Time Modify'
	And I quick search by matter number
	And I validate entry is posted by

@ft
Examples:
	| Client       | FeeEarnerName   | Office         | TaxCodeDescription      | Currency | TimeType       | PayorName    |
	| Audit Client | Audit FeeEarner | London (UKIME) | UK Output Domestic Zero | GBP      | FEES (Default) | Susan Wilson |
@singapore
Examples:
	| Client       | FeeEarnerName   | Office    | TaxCodeDescription          | Currency | TimeType | PayorName    |
	| Audit Client | Audit FeeEarner | Singapore | SG Output Domestic Standard | SGD      | FEES     | Susan Wilson |
@canada
Examples:
	| Client       | FeeEarnerName   | Office  | TaxCodeDescription              | Currency | TimeType | PayorName    |
	| Audit Client | Audit FeeEarner | Calgary | CA Output Domestic Standard GST | CAD      | FEES     | Susan Wilson |
@us
Examples:
	| Client       | FeeEarnerName   | Office  | TaxCodeDescription               | Currency | TimeType | PayorName      |
	| Audit Client | Audit FeeEarner | Chicago | US Output Domestic Standard Rate | USD      | FEES     | Doctor strange |
@qa
Examples:
	| Client       | FeeEarnerName   | Office         | TaxCodeDescription      | Currency | TimeType | PayorName    |
	| Audit Client | Audit FeeEarner | London (UKIME) | UK Output Domestic Zero | GBP      | FEES     | Susan Wilson |
@uk
Examples:
	| Client       | FeeEarnerName   | Office         | TaxCodeDescription          | Currency | TimeType       | PayorName    |
	| Audit Client | Audit FeeEarner | London (UKIME) | UK Output Domestic Standard | GBP      | FEES (Default) | Susan Wilson |
@training @staging
Examples:
	| Client       | FeeEarnerName   | Office         | TaxCodeDescription          | Currency | TimeType | PayorName    |
	| Audit Client | Audit FeeEarner | London (UKIME) | UK Output Domestic Standard | GBP      | FEES     | Susan Wilson |
@europe
Examples:
	| Client       | FeeEarnerName   | Office      | TaxCodeDescription          | Currency | TimeType | PayorName    |
	| Audit Client | Audit FeeEarner | London (EU) | UK Output Domestic Standard | EUR      | FEES     | Susan Wilson |
	
@CancelProcess
Scenario Outline: 020 Proxy Time Modify Posted By
	Given I create a user with details
		| UserName   | DataRoleAlias | DefaultOperatingAlias     |
		| <UserName> | Admin         | Dentons Australia Limited |
	And I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	And I create the Payer with Api
		| PayerName   | Entity       |
		| <PayorName> | BrightEntity |
	And I create a matter with details:
		| Client   | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department | Section | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| <Client> | <FeeEarnerName>   | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | Bill                 | <Office> | Default    | Default | Desc_at_3e8glw7     | Desc_at_3e8glw7   | <PayorName> |
	When I retrieve the current username
	And I proxy as user '<UserName>'
	And I submit a time modify
		| Time Type  | Hours | Narrative | FeeEarnerName   | WorkRate | WorkCurrency |
		| <TimeType> | 0.1   | {Auto}+10 | <FeeEarnerName> | 100      | <Currency>   |
	Then I search for process 'Time Modify'
	And I quick search by matter number
	And I validate entry is posted by '<UserName>'

@singapore
Examples:
	| UserName  | Client       | FeeEarnerName   | Office    | TaxCodeDescription          | Currency | TimeType | PayorName    |
	| AuditUser | Audit Client | Audit FeeEarner | Singapore | SG Output Domestic Standard | SGD      | FEES     | Susan Wilson |
@europe
Examples:
	| UserName  | Client       | FeeEarnerName   | Office      | TaxCodeDescription          | Currency | TimeType | PayorName    |
	| AuditUser | Audit Client | Audit FeeEarner | London (EU) | AE Output Domestic Standard | AED      | FEES     | Susan Wilson |
@uk
Examples:
	| UserName  | Client       | FeeEarnerName   | Office         | TaxCodeDescription          | Currency | TimeType       | PayorName    |
	| AuditUser | Audit Client | Audit FeeEarner | London (UKIME) | UK Output Domestic Standard | GBP      | FEES (Default) | Susan Wilson |
@canada
Examples:
	| UserName  | Client       | FeeEarnerName   | Office  | TaxCodeDescription              | Currency | TimeType | PayorName    |
	| AuditUser | Audit Client | Audit FeeEarner | Calgary | CA Output Domestic Standard GST | CAD      | FEES     | Susan Wilson |
@us
Examples:
	| UserName  | Client       | FeeEarnerName   | Office  | TaxCodeDescription               | Currency | TimeType | PayorName  |
	| AuditUser | Audit Client | Audit FeeEarner | Chicago | US Output Domestic Standard Rate | USD      | FEES     | Tony Stark |
@training @staging
Examples:
	| UserName  | Client       | FeeEarnerName   | Office         | TaxCodeDescription          | Currency | TimeType | PayorName    |
	| AuditUser | Audit Client | Audit FeeEarner | London (UKIME) | UK Output Domestic Standard | GBP      | FEES     | Susan Wilson |
@qa
Examples:
	| UserName  | Client       | FeeEarnerName   | Office         | TaxCodeDescription      | Currency | TimeType       | PayorName    |
	| AuditUser | Audit Client | Audit FeeEarner | London (UKIME) | UK Output Domestic Zero | GBP      | FEES (Default) | Susan Wilson |
@ft
Examples:
	| UserName  | Client       | FeeEarnerName   | Office         | TaxCodeDescription      | Currency | TimeType       | PayorName    |
	| AuditUser | Audit Client | Audit FeeEarner | London (UKIME) | UK Output Domestic Zero | GBP      | FEES (Default) | Susan Wilson |


	
