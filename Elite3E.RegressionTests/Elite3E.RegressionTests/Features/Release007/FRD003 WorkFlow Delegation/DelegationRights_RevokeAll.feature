@release7 @frd003 @DelegationRights_RevokeAll @ProxyTest
Feature: DelegationRights - Revoke All
	Test 3: Delegation rights for all workflows can be removed from one fee earner who has had them delegated from another

# Prep TestData with API ToDos:
# 1. User / Role Management 
# 1.1 Create User with 2 Roles (DefaultWFTimeModifyApprovalRole_ccc, WFTimeModifyLondonOfficeApprover_ccc)
# 1.2. Create User without Roles
# Note, you'll need to use the featurecontext to store variables and you'll need to remove the clean up scenario.

# Adding delegation to be removed for test
Scenario Outline: 005 Create a General Journal Request
	Given I search and create a gj category with api
		| GJCategoryDescription | IsRequireApprovalCheckboxAlias |
		| <Category>            | Yes                            |
	And I add a new 'General Journal Request'
	When I add the general journal details:
		| Category   | Currency   | Journal   | GLType   | GLBook   |
		| <Category> | <Currency> | {Auto}+10 | <GLType> | <GLBook> |
@ft
Examples:
	| Category               | Currency | GLType                                   | GLBook                     |
	| Auto Requires Approval | GBP      | Local Adj - Dentons UK & Middle East LLP | Local UK & Middle East LLP |
@uk
Examples:
	| Category               | Currency | GLType             | GLBook |
	| Auto Requires Approval | GBP      | All Books GL Types |        |
@qa
Examples:
	| Category               | Currency | GLType             | GLBook |
	| Auto Requires Approval | AUD      | All Books GL Types |        |
@training @staging
Examples:
	| Category               | Currency | GLType                                                           | GLBook                                                           |
	| Auto Requires Approval | GBP      | Local Dentons Europe Consulting Real Estate and Tourism Advisory | Local Dentons Europe Consulting Real Estate and Tourism Advisory |
@singapore
Examples:
	| Category               | Currency | GLType                             | GLBook                             |
	| Auto Requires Approval | GBP      | Local Dentons Rodyk & Davidson LLP | Local Dentons Rodyk & Davidson LLP |
@europe
Examples:
	| Category               | Currency | GLType             | GLBook                                |
	| Auto Requires Approval | EUR      | All Books GL Types | Local Dentons Europe (London) Limited |
@canada
Examples:
	| Category               | Currency | GLType                           | GLBook |
	| Auto Requires Approval | CAD      | Canada Accrual as per Enterprise |        |

Scenario: 006 Create matter  and create a time modify
	Given I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	And I create the Payer with Api
		| PayerName   | Entity        |
		| <PayorName> | FrontPage Ltd |
	And I create a matter with details:
		| Client   | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department | Section | ChargeTypeGroupName | CostTypeGroupName | Rate     | PayorName   |
		| <Client> | <FeeEarnerName>   | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | Bill                 | <Office> | Default    | Default | Desc_at_3e8glw7     | Desc_at_3e8glw7   | Standard | <PayorName> |
	And I create a submatter 1 with details:
		| Client   | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department | Section | ChargeTypeGroupName | CostTypeGroupName | Rate     | PayorName   |
		| <Client> | <FeeEarnerName>   | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | Bill                 | <Office> | Default    | Default | Desc_at_3e8glw7     | Desc_at_3e8glw7   | Standard | <PayorName> |
	And I submit a time modify
		| Time Type  | Hours | Narrative | FeeEarnerName   | WorkRate | WorkAmount | WorkCurrency | TaxCode   |
		| <TimeType> | 0.1   | {Auto}+10 | <FeeEarnerName> | 1        | 0.1        | <Currency>   | <TaxCode> |
@europe
Examples:
	| Client                      | FeeEarnerName                  | TimeType | Hours1 | Hours2 | TaxCode                   | Currency | Office      | PayorName    |
	| SplitBillClient NumberThree | SplitBillFeeEarner NumberThree | FEES     | 0.10   | 0.20   | EU Output Conversion Code | EUR      | London (EU) | Emilio Price |

@ft @qa
Examples:
	| Client                      | FeeEarnerName                  | TimeType       | Hours1 | Hours2 | TaxCode                    | Currency            | Office         | PayorName    |
	| SplitBillClient NumberThree | SplitBillFeeEarner NumberThree | FEES (Default) | 0.01   | 0.20   | UK Output Domestic Zero 0% | GBP - British Pound | London (UKIME) | Emilio Price |
@training @staging
Examples:
	| Client                      | FeeEarnerName                  | TimeType | Hours1 | Hours2 | TaxCode                 | Currency            | Office      | PayorName    |
	| SplitBillClient NumberThree | SplitBillFeeEarner NumberThree | FEES     | 0.10   | 0.20   | AE Output Domestic Zero | GBP - British Pound | London (EU) | Emilio Price |
@uk
Examples:
	| Client                      | FeeEarnerName                  | TimeType       | Hours1 | Hours2 | TaxCode                 | Currency            | Office         | PayorName    |
	| SplitBillClient NumberThree | SplitBillFeeEarner NumberThree | FEES (Default) | 0.10   | 0.20   | UK Output Domestic Zero | GBP - British Pound | London (UKIME) | Emilio Price |
@canada
Examples:
	| Client                      | FeeEarnerName                  | TimeType | Hours1 | Hours2 | TaxCode                         | Currency              | Office    | PayorName    |
	| SplitBillClient NumberThree | SplitBillFeeEarner NumberThree | FEES     | 0.10   | 0.20   | CA Output Domestic Standard GST | CAD - Canadian Dollar | Vancouver | Emilio Price |
@singapore
Examples:
	| Client                      | FeeEarnerName                  | TimeType | Hours1 | Hours2 | TaxCode                     | Currency               | Office    | PayorName    |
	| SplitBillClient NumberThree | SplitBillFeeEarner NumberThree | FEES     | 0.10   | 0.20   | SG Output Domestic Standard | SGD - Singapore Dollar | Singapore | Emilio Price |

Scenario: 010 Create update delegation rights task
	Given I create a user with details
		| UserName                     | DataRoleAlias | DefaultOperatingAlias   |
		| <DelegationUserWithoutRoles> | Admin         | <DefaultOperatingAlias> |
	And I create a user with details
		| UserName                  | DataRoleAlias | DefaultOperatingAlias   | UserRoleList                                                  |
		| <DelegationUserWithRoles> | Admin         | <DefaultOperatingAlias> | DefaultWFTimeModifyApprovalRole_ccc, 0:FM:W:GJ Approver: Firm |
	And I remove existing workflow delegation records
		| UserName                     |
		| <DelegationUserWithRoles>    |
		| <DelegationUserWithoutRoles> |
	And I add a new 'Update Delegation Rights'
	When I add the update delegation rights and grant all workflows
		| DelegationUserWithRoles   | DelegationUserWithoutRoles   | EffectiveDate | DelegateAllWorkflowsCheckbox   |
		| <DelegationUserWithRoles> | <DelegationUserWithoutRoles> | {Today}       | <DelegateAllWorkflowsCheckbox> |
	Then I can submit the record
	
@ft @training @staging @uk @qa
Examples:
	| DelegationUserWithRoles | DelegationUserWithoutRoles | DelegateAllWorkflowsCheckbox | DefaultOperatingAlias          |
	| FRD003_TwoRoles         | FRD003_HasNoRoles          | true                         | Dentons UK and Middle East LLP |
@europe
Examples:
	| DelegationUserWithRoles | DelegationUserWithoutRoles | DelegateAllWorkflowsCheckbox | DefaultOperatingAlias   |
	| FRD003_TwoRoles         | FRD003_HasNoRoles          | true                         | Dentons Europe LLP (UK) |
@canada
Examples:
	| DelegationUserWithRoles | DelegationUserWithoutRoles | DelegateAllWorkflowsCheckbox | DefaultOperatingAlias |
	| FRD003_TwoRoles         | FRD003_HasNoRoles          | true                         | Dentons Canada LLP    |

@singapore
Examples:
	| DelegationUserWithRoles | DelegationUserWithoutRoles | DelegateAllWorkflowsCheckbox | DefaultOperatingAlias        |
	| FRD003_TwoRoles         | FRD003_HasNoRoles          | true                         | Dentons Rodyk & Davidson LLP |

@CancelProcess
Scenario: 025 Start Notification task
	Given I search for 'Notification Start Task'
	And I start a notification task '<NotificationTask>'
	And I search for 'Notification Task and Log Viewer'
	When I validate notification task '<NotificationTask>' is complete

@ft @training @staging @canada @europe @uk @singapore
Examples:
	| NotificationTask         |
	| Workflow Delegation Task |
@qa
Examples:
	| NotificationTask   |
	| ML Delegation Task |
		
Scenario: 030 Proxy as User and Validate Rights Added
	Given I proxy as user '<DelegationUserWithoutRoles>'
	When I search for 'Workflow Dashboard'
	Then I validate user workflow rights have been added '<RolesList>'
	And I cancel proxy

@ft @training @staging @canada @europe @uk @singapore @qa
Examples:
	| DelegationUserWithoutRoles | RolesList                                     |
	| FRD003_HasNoRoles          | General Journal Approval,Time Modify Workflow |

# Performing Test 3 - Revoking Rights
@CancelProcess
Scenario: 040 Revoke Rights
	Given I add a new 'Update Delegation Rights'
	When I add the update delegation rights and revoke all workflows
		| DelegationUserWithRoles   | DelegationUserWithoutRoles   | EffectiveDate | DelegateAllWorkflowsCheckbox   |
		| <DelegationUserWithRoles> | <DelegationUserWithoutRoles> | {Today}       | <DelegateAllWorkflowsCheckbox> |
	Then I can submit the record
	
@ft @training @staging @canada @europe @uk @singapore @qa
Examples:
	| DelegationUserWithRoles | DelegationUserWithoutRoles | DelegateAllWorkflowsCheckbox |
	| FRD003_TwoRoles         | FRD003_HasNoRoles          | false                        |
	
@CancelProcess
Scenario: 050 Start Notification task
	Given I search for 'Notification Start Task'
	And I start a notification task '<NotificationTask>'

@ft @training @staging @canada @europe @uk @singapore
Examples:
	| NotificationTask         |
	| Workflow Delegation Task |
@qa
Examples:
	| NotificationTask   |
	| ML Delegation Task |
		
Scenario: 060 Proxy as User and Validate Rights Removed
	Given I proxy as user '<DelegationUserWithoutRoles>'
	When I search for 'Workflow Dashboard'
	Then I validate user workflow rights have been removed
	And I cancel proxy

@ft @training @staging @canada @europe @uk @singapore @qa
Examples:
	| DelegationUserWithoutRoles |
	| FRD003_HasNoRoles          |