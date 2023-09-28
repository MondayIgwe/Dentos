@us @ignore
# Test ignored because of workflow configuration not set for the operating unit 
Feature: DelegationRights - Revoke Some
	Test 4: Delegation rights for one or a specified number of (but not all) workflows can be removed from one fee earner where they are delegated from another.

# Prep TestData with API ToDos:
# 1. User / Role Management 
# 1.1 Create User with 2 Roles (DefaultWFTimeModifyApprovalRole_ccc, WFTimeModifyLondonOfficeApprover_ccc)
# 1.2. Create User without Roles
# Note, you'll need to use the featurecontext to store variables and you'll need to remove the clean up scenario.


Scenario Outline: 005 Create a General Journal Request
	Given I search and create a gj category with api
		| GJCategoryDescription | IsRequireApprovalCheckboxAlias |
		| <Category>            | Yes                            |
	And I add a new 'General Journal Request'
	When I add the general journal details:
		| Category   | Currency   | Journal   | GLType   | GLBook   |
		| <Category> | <Currency> | {Auto}+10 | <GLType> | <GLBook> |

Examples:
	| Category               | Currency | GLType                | GLBook                |
	| Auto Requires Approval | USD      | Local Dentons US, LLP | Local Dentons US, LLP |


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

Examples:
	| Client                      | FeeEarnerName                  | TimeType | Hours1 | Hours2 | TaxCode                          | Currency        | Office  | PayorName    |
	| SplitBillClient NumberThree | SplitBillFeeEarner NumberThree | FEES     | 0.10   | 0.20   | US Output Domestic Standard Rate | USD - US Dollar | Chicago | Emilio Price |


Scenario: 010 Create update delegation rights task
	Given I create a user with details
		| UserName                     | DataRoleAlias | DefaultOperatingAlias   |
		| <DelegationUserWithoutRoles> | Admin         | <DefaultOperatingAlias> |
	And I create a user with details
		| UserName                  | DataRoleAlias | DefaultOperatingAlias   | UserRoleList                                                  |
		| <DelegationUserWithRoles> | Admin         | <DefaultOperatingAlias> | DefaultWFTimeModifyApprovalRole_ccc, 0:FM:W:GJ Approver: 5000 |
	And I remove existing workflow delegation records
		| UserName                     |
		| <DelegationUserWithRoles>    |
		| <DelegationUserWithoutRoles> |
	And I add a new 'Update Delegation Rights'
	When I add the update delegation rights and grant some workflows
		| DelegationUserWithRoles   | DelegationUserWithoutRoles   | EffectiveDate | DelegateAllWorkflowsCheckbox   | WorkflowsToGrantAccessTo   |
		| <DelegationUserWithRoles> | <DelegationUserWithoutRoles> | {Today}       | <DelegateAllWorkflowsCheckbox> | <WorkflowsToGrantAccessTo> |
	Then I can submit the record
	
Examples:
	| DelegationUserWithRoles | DelegationUserWithoutRoles | DelegateAllWorkflowsCheckbox | WorkflowsToGrantAccessTo                      | DefaultOperatingAlias      |
	| FRD003_TwoRoles         | FRD003_NoRoles             | false                        | General Journal Approval,Time Modify Workflow | Dentons United States, LLP |

@CancelProcess
Scenario: 025 Start Notification task
	Given I search for 'Notification Start Task'
	And I start a notification task '<NotificationTask>'
	And I search for 'Notification Task and Log Viewer'
	When I validate notification task '<NotificationTask>' is complete

Examples:
	| NotificationTask         |
	| Workflow Delegation Task |

		
Scenario: 030 Proxy as User and Validate Rights Added
	Given I proxy as user '<DelegationUserWithoutRoles>'
	When I search for 'Workflow Dashboard'
	Then I validate user workflow rights have been added '<RolesList>'
	And I cancel proxy

Examples:
	| DelegationUserWithoutRoles | RolesList                                     |
	| FRD003_NoRoles             | General Journal Approval,Time Modify Workflow |

# Performing Test 4 - Revoking some Rights
@CancelProcess
Scenario: 040 Revoke Rights
	Given I add a new 'Update Delegation Rights'
	When I add the update delegation rights and revoke all workflows
		| DelegationUserWithRoles   | DelegationUserWithoutRoles   | EffectiveDate | DelegateAllWorkflowsCheckbox   | WorkflowsToRevokeAccessTo   |
		| <DelegationUserWithRoles> | <DelegationUserWithoutRoles> | {Today}       | <DelegateAllWorkflowsCheckbox> | <WorkflowsToRevokeAccessTo> |
	Then I can submit the record
	

Examples:
	| DelegationUserWithRoles | DelegationUserWithoutRoles | DelegateAllWorkflowsCheckbox | WorkflowsToRevokeAccessTo |
	| FRD003_TwoRoles         | FRD003_NoRoles             | false                        | General Journal Approval  |
	
@CancelProcess
Scenario: 050 Start Notification task
	Given I search for 'Notification Start Task'
	And I start a notification task '<NotificationTask>'

Examples:
	| NotificationTask         |
	| Workflow Delegation Task |

		
Scenario: 060 Proxy as User and Validate Rights Removed
	Given I proxy as user '<DelegationUserWithoutRoles>'
	When I search for 'Workflow Dashboard'
	Then I validate user workflow rights have been updated '<RolesList>'
	And I cancel proxy

Examples:
	| DelegationUserWithoutRoles | RolesList            |
	| FRD003_NoRoles             | Time Modify Workflow |
		
@CancelProcess
Scenario: 070 Clean up
	Given I add a new 'Update Delegation Rights'
	When I add the update delegation rights and revoke all workflows
		| DelegationUserWithRoles   | DelegationUserWithoutRoles   | EffectiveDate | DelegateAllWorkflowsCheckbox   | WorkflowsToRevokeAccessTo   |
		| <DelegationUserWithRoles> | <DelegationUserWithoutRoles> | {Today}       | <DelegateAllWorkflowsCheckbox> | <WorkflowsToRevokeAccessTo> |
	Then I can submit the record
	And I search for 'Notification Start Task'
	And I start a notification task '<NotificationTask>'
	
Examples:
	| DelegationUserWithRoles | DelegationUserWithoutRoles | DelegateAllWorkflowsCheckbox | NotificationTask         | WorkflowsToRevokeAccessTo |
	| FRD003_TwoRoles         | FRD003_NoRoles             | false                        | Workflow Delegation Task | Time Modify Workflow      |
