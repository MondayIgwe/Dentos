@release9 @frd016 @ChargeModifyAuditPostedBy @ProxyTest
Feature: ChargeModifyAuditPostedBy

FRD 016 - TransactionAuditing
Test 6 : Charge Modify contains the "Posted By" field which will contain the identity of the user who posted the charge
Test 7 : Where the user is proxied in as another user and creates a charge the "Posted By" field in charge modify will contain both the identity of the user and the proxied in user.


Scenario Outline: 010 Charge Modify Posted By
	Given I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	And I create the Payer with Api
		| PayerName   | Entity       |
		| <PayorName> | BrightEntity |
	And I create a matter with details:
		| Client   | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department | Section | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| <Client> | <FeeEarnerName>   | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | Bill                 | <Office> | Default    | Default | Desc_at_3e8glw7     | Desc_at_3e8glw7   | <PayorName> |
	And the below Charge types are available
		| Code   | Description             | TransactionType   | Category          |
		| at_AAC | <ChargeTypeDescription> | <TransactionType> | Billed on Account |
	When I retrieve the current username
	And I try to update the charge modify
		| Work Date | Charge Type             | Work Amount | WorkCurrency   |
		| {Today}-1 | <ChargeTypeDescription> | 500.00      | <WorkCurrency> |
	And submit it
	And I validate submit was successful
	Then I search for process 'Charge Modify'
	And I quick search by matter number
	And I validate entry is posted by
	And I cancel the process

@ft
Examples:
	| Client       | FeeEarnerName   | Office         | ChargeTypeDescription | TransactionType            | WorkCurrency | Currency            | PayorName    |
	| Audit Client | Audit FeeEarner | London (UKIME) | Sundry Income         | auto_Acticipated_Hard_Cost |              | GBP - British Pound | Susan Wilson |
	
@singapore
Examples:
	| Client       | FeeEarnerName   | Office    | ChargeTypeDescription | TransactionType       | WorkCurrency | Currency               | PayorName    |
	| Audit Client | Audit FeeEarner | Singapore | Sundry Income         | Anticipated Hard Cost |              | SGD - Singapore Dollar | Susan Wilson |

@europe
Examples:
	| Client       | FeeEarnerName   | Office      | ChargeTypeDescription | TransactionType            | WorkCurrency | Currency            | PayorName    |
	| Audit Client | Audit FeeEarner | London (EU) | Sundry Income         | auto_Acticipated_Hard_Cost | EUR          | GBP - British Pound | Susan Wilson |

@uk
Examples:
	| Client       | FeeEarnerName   | Office         | ChargeTypeDescription | TransactionType       | WorkCurrency | Currency            | PayorName    |
	| Audit Client | Audit FeeEarner | London (UKIME) | Sundry Income         | Anticipated Hard Cost |              | GBP - British Pound | Susan Wilson |
@qa
Examples:
	| Client       | FeeEarnerName   | Office         | ChargeTypeDescription | TransactionType            | WorkCurrency | Currency            | PayorName    |
	| Audit Client | Audit FeeEarner | London (UKIME) | Sundry Income         | auto_Acticipated_Hard_Cost |              | GBP - British Pound | Susan Wilson |
@us
Examples:
	| Client       | FeeEarnerName   | Office  | ChargeTypeDescription | TransactionType            | WorkCurrency | Currency        | PayorName    |
	| Audit Client | Audit FeeEarner | Chicago | Sundry Income         | auto_Acticipated_Hard_Cost |              | USD - US Dollar | Ganesh Kumar |
@canada
Examples:
	| Client       | FeeEarnerName   | Office  | ChargeTypeDescription | TransactionType            | WorkCurrency | Currency | PayorName    |
	| Audit Client | Audit FeeEarner | Calgary | Sundry Income         | auto_Acticipated_Hard_Cost |              | CAD      | Susan Wilson |

@CancelProcess
Scenario Outline: 020 Proxy Charge Modify Posted By
	Given I create a user with details
		| UserName   | DataRoleAlias | DefaultOperatingAlias   | UserRoleList                |
		| <UserName> | Admin         | <DefaultOperatingAlias> | 0:AD:G:System Administrator |
	And I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	And I create the Payer with Api
		| PayerName   | Entity       |
		| <PayorName> | BrightEntity |
	And I create a matter with details:
		| Client   | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department | Section | Rate     | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| <Client> | <FeeEarnerName>   | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | Bill                 | <Office> | Default    | Default | Standard | Desc_at_3e8glw7     | Desc_at_3e8glw7   | <PayorName> |
	And the below Charge types are available
		| Code   | Description             | Category          | TransactionType   |
		| at_AAC | <ChargeTypeDescription> | Billed on Account | <TransactionType> |
	When I retrieve the current username
	And I proxy as user '<UserName>'
	And I try to update the charge modify
		| Work Date | Charge Type             | Work Amount | WorkCurrency   |
		| {Today}-1 | <ChargeTypeDescription> | 500.00      | <WorkCurrency> |
	And submit it
	And I validate submit was successful
	Then I search for process 'Charge Modify'
	And I quick search by matter number
	And I validate entry is posted by '<UserName>'

@singapore
Examples:
	| UserName  | Client       | FeeEarnerName   | Office    | ChargeTypeDescription | DefaultOperatingAlias        | TransactionType       | WorkCurrency | Currency               | PayorName    |
	| AuditUser | Audit Client | Audit FeeEarner | Singapore | Sundry Income         | Dentons Rodyk & Davidson LLP | Anticipated Hard Cost |              | SGD - Singapore Dollar | Susan Wilson |

@europe
Examples:
	| UserName  | Client       | FeeEarnerName   | Office      | ChargeTypeDescription | DefaultOperatingAlias   | TransactionType            | WorkCurrency | Currency            | PayorName    |
	| AuditUser | Audit Client | Audit FeeEarner | London (EU) | Sundry Income         | Dentons Europe LLP (UK) | auto_Acticipated_Hard_Cost | EUR          | GBP - British Pound | Susan Wilson |
@ft
Examples:
	| UserName  | Client       | FeeEarnerName   | Office         | ChargeTypeDescription | DefaultOperatingAlias     | TransactionType            | WorkCurrency | Currency            | PayorName    |
	| AuditUser | Audit Client | Audit FeeEarner | London (UKIME) | Sundry Income         | Dentons Australia Limited | auto_Acticipated_Hard_Cost |              | GBP - British Pound | Susan Wilson |

@uk
Examples:
	| UserName  | Client       | FeeEarnerName   | Office         | ChargeTypeDescription | DefaultOperatingAlias          | TransactionType       | WorkCurrency | Currency            | PayorName    |
	| AuditUser | Audit Client | Audit FeeEarner | London (UKIME) | Sundry Income         | Dentons UK and Middle East LLP | Anticipated Hard Cost |              | GBP - British Pound | Susan Wilson |

@us
Examples:
	| UserName  | Client       | FeeEarnerName   | Office  | ChargeTypeDescription | DefaultOperatingAlias      | TransactionType       | WorkCurrency | Currency        | PayorName  |
	| AuditUser | Audit Client | Audit FeeEarner | Chicago | Sundry Income         | Dentons United States, LLP | Anticipated Hard Cost |              | USD - US Dollar | Jon Miller |
@training @staging
Examples:
	| UserName  | Client       | FeeEarnerName   | Office         | ChargeTypeDescription | DefaultOperatingAlias          | TransactionType            | WorkCurrency | Currency            | PayorName    |
	| AuditUser | Audit Client | Audit FeeEarner | London (UKIME) | Sundry Income         | Dentons UK and Middle East LLP | auto_Acticipated_Hard_Cost |              | GBP - British Pound | Susan Wilson |
@qa
Examples:
	| UserName  | Client       | FeeEarnerName   | Office         | ChargeTypeDescription | DefaultOperatingAlias     | TransactionType       | WorkCurrency | Currency | PayorName    |
	| AuditUser | Audit Client | Audit FeeEarner | London (UKIME) | Sundry Income         | Dentons Australia Limited | Anticipated Hard Cost |              | GBP      | Susan Wilson |
	
@canada
Examples:
	| UserName  | Client       | FeeEarnerName   | Office  | ChargeTypeDescription | DefaultOperatingAlias | TransactionType            | WorkCurrency | Currency | PayorName    |
	| AuditUser | Audit Client | Audit FeeEarner | Calgary | Sundry Income         | Dentons Canada LLP    | auto_Acticipated_Hard_Cost |              | CAD      | Susan Wilson |
