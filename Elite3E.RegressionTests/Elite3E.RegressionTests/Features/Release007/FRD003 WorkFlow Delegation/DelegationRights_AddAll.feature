@release7 @frd003 @DelegationRights_AddAll @ProxyTest
Feature: DelegationRights - Add All
	Test 1: Delegation rights for all workflows can be granted from one fee earner to another
	
	NOTE: When using role '0:FM:W:GJ Approver: Firm' we need to have a GJ Request created first.
	You should be able to run 'Release 6 FRD053 GJEntryApproval CategoryAmountValidation'
	for more info, see: https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/36206

	On QA, Go to Workflow Config, check the GJApproval section, check 'Routing for next workflow step'
		This will have a role, which your user must have in order for the GJ Requests to show up in your Workflow Dashboard.
		In QA, this is: GJApprover

	Also, to get Time Modify to show in the workflow dashboard:
	You should be able to run @release6 @frd022 @PurgeTypeWorkflow in order for there to be Time Modify in the workflow

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
	| Category               | Currency | GLType             | GLBook                     |
	| Auto Requires Approval | GBP      | All Books GL Types | Local UK & Middle East LLP |
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
	| FRD003_TwoRoles_AddAll  | FRD003_HasNoRoles          | true                         | Dentons UK and Middle East LLP |
@europe
Examples:
	| DelegationUserWithRoles | DelegationUserWithoutRoles | DelegateAllWorkflowsCheckbox | DefaultOperatingAlias   |
	| FRD003_TwoRoles_AddAll  | FRD003_HasNoRoles          | true                         | Dentons Europe LLP (UK) |
@canada
Examples:
	| DelegationUserWithRoles | DelegationUserWithoutRoles | DelegateAllWorkflowsCheckbox | DefaultOperatingAlias |
	| FRD003_TwoRoles_AddAll  | FRD003_HasNoRoles          | true                         | Dentons Canada LLP    |
@singapore
Examples:
	| DelegationUserWithRoles | DelegationUserWithoutRoles | DelegateAllWorkflowsCheckbox | DefaultOperatingAlias        |
	| FRD003_TwoRoles_AddAll  | FRD003_HasNoRoles          | true                         | Dentons Rodyk & Davidson LLP |


@CancelProcess
Scenario: 020 Validate Notification task Exists
	Given I search for process 'Notification Task Manager'
	When I perform quick find for '<NotificationTask>'
	Then I validate a notification task '<BusinessObject>'
	
@ft @training @staging @canada @europe @uk @singapore
Examples:
	| NotificationTask         | BusinessObject            |
	| Workflow Delegation Task | UpdateDelegationTasks_ccc |
@qa
Examples:
	| NotificationTask   | BusinessObject            |
	| ML Delegation Task | UpdateDelegationTasks_ccc |
		
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
		
Scenario: 030 Proxy as User and Validate Rights
	Given I proxy as user '<DelegationUserWithoutRoles>'
	When I search for 'Workflow Dashboard'
	Then I validate user workflow rights have been added '<RolesList>'
	And I cancel proxy

@ft @training @staging @canada @europe @uk @singapore @qa
Examples:
	| DelegationUserWithoutRoles | RolesList                                     |
	| FRD003_HasNoRoles          | General Journal Approval,Time Modify Workflow |

@CancelProcess
Scenario: 040 Clean up
	Given I add a new 'Update Delegation Rights'
	When I add the update delegation rights and revoke all workflows
		| DelegationUserWithRoles   | DelegationUserWithoutRoles   | EffectiveDate | DelegateAllWorkflowsCheckbox   |
		| <DelegationUserWithRoles> | <DelegationUserWithoutRoles> | {Today}       | <DelegateAllWorkflowsCheckbox> |
	Then I can submit the record
	And I search for 'Notification Start Task'
	And I start a notification task '<NotificationTask>'
	#And I proxy as user '<DelegationUserWithoutRoles>'
	#And I search for 'Workflow Dashboard'
	#And I validate user workflow rights have been removed
	
@ft @training @staging @canada @europe @uk @singapore
Examples:
	| DelegationUserWithRoles | DelegationUserWithoutRoles | DelegateAllWorkflowsCheckbox | NotificationTask         |
	| FRD003_TwoRoles_AddAll  | FRD003_HasNoRoles          | false                        | Workflow Delegation Task |
@qa
Examples:
	| DelegationUserWithRoles | DelegationUserWithoutRoles | DelegateAllWorkflowsCheckbox | NotificationTask   |
	| FRD003_TwoRoles_AddAll  | FRD003_HasNoRoles          | false                        | ML Delegation Task |
	