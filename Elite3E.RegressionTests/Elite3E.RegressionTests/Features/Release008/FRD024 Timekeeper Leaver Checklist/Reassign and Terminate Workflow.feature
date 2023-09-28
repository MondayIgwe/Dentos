@release8 @frd024.1 @ReassignAndTerminateWorkflow @ProxyTest
Feature: Reassign and Terminate Workflow
	Azure Test Cases: 
		CUST_306 - DEV001
		CUST_306 - DEV002


Scenario: 010 Manually Start Leaver Workflow
	Given I create a user with details
		| UserName | DataRoleAlias   | DefaultOperatingAlias   | UserRoleList   |
		| <User1>  | <DataRoleAlias> | <DefaultOperatingAlias> | <UserRoleList> |
	And I create a user with details
		| UserName | DataRoleAlias   | DefaultOperatingAlias   | UserRoleList   |
		| <User2>  | <DataRoleAlias> | <DefaultOperatingAlias> | <UserRoleList> |
	And I create a fee earner without details
	And I search for 'Timekeeper Leaver Checklist'
	When I start a timekeeper leaver workflow
		| LeaverDate | Lead Finance HR Clerk |
		| {Today}    | <User1>               |
	And I validate timekeeper leaver section 'Initial check by Finance' is readonly
	And I validate timekeeper leaver section 'Checks by Leaver or Legal Assistant' is readonly
	And I validate timekeeper leaver section 'Final Checks' is readonly
	Then I submit it
	
@ft @uk @qa
Examples:
	| User1        | User2        | DataRoleAlias | DefaultOperatingAlias     | UserRoleList                                         |
	| FRD042_User1 | FRD042_User2 | Admin         | Dentons Australia Limited | 0:AD:G:System Administrator,0:FM:W:GJ Approver: Firm |
@canada
Examples:
	| User1        | User2        | DataRoleAlias | DefaultOperatingAlias | UserRoleList                                         |
	| FRD042_User1 | FRD042_User2 | Admin         | Dentons Canada LLP    | 0:AD:G:System Administrator,0:FM:W:GJ Approver: Firm |
@singapore
Examples:
	| User1        | User2        | DataRoleAlias | DefaultOperatingAlias        | UserRoleList                                         |
	| FRD042_User1 | FRD042_User2 | Admin         | Dentons Rodyk & Davidson LLP | 0:AD:G:System Administrator,0:FM:W:GJ Approver: Firm |
@europe
Examples:
	| User1        | User2        | DataRoleAlias | DefaultOperatingAlias   | UserRoleList                                         |
	| FRD042_User1 | FRD042_User2 | Admin         | Dentons Europe LLP (UK) | 0:AD:G:System Administrator,0:FM:W:GJ Approver: Firm |
@training @staging
Examples:
	| User1        | User2        | DataRoleAlias | DefaultOperatingAlias           | UserRoleList                                         |
	| FRD042_User1 | FRD042_User2 | Admin         | Dentons Africa Holdings Limited | 0:AD:G:System Administrator,0:FM:W:GJ Approver: Firm |

		
Scenario: 020 Proxy as User1 and Fill Initial Checks
	Given I proxy as user '<User1>'
	When I search for 'Workflow Dashboard'
	Then I click on workflow inbox option 'Timekeeper Leave Checklist'
	And I click open on workflow inbox timekeeper leaver and at section 'Timekeeper Leaver Checklist Submitted'
	And I validate timekeeper leaver section 'Initial check by Finance' is editable
	And I input data into timekeeper leaver section
		| Section                  | LeaverDate | User    | NoFurtherActionRequired |
		| Initial check by Finance | {Today}    | <User1> | Yes                     |
	And I submit it
	
@ft @training @staging @canada @europe @uk @singapore @qa
Examples:
	| User1        | User2        |
	| FRD042_User1 | FRD042_User2 |

Scenario: 030 Perform Reassign from User1 to User2
	Then I click open on workflow inbox timekeeper leaver and at section 'No Further Action Required'
	When I perform timekeeper leaver reassign '<User2>'
	And I validate timekeeper is no longer present in workflow inbox
	Then I cancel proxy
		
@ft @training @staging @canada @europe @uk @singapore @qa
Examples:
	| User2        |
	| FRD042_User2 |

Scenario: 040 Proxy as User2 - Fill Final Checks and Save
	Given I proxy as user '<User2>'
	When I search for 'Workflow Dashboard'
	Then I click on workflow inbox option 'Timekeeper Leave Checklist'
	And I click open on workflow inbox timekeeper leaver and at section 'Timekeeper Leaver Checklist Reassigned'
	And I validate timekeeper leaver section 'Final Checks' is editable
	And I input data into timekeeper leaver section
		| Section      | LeaverDate | User    | LeaverReadyToDepart |
		| Final Checks | {Today}    | <User2> | No                  |
	And I perform timekeeper leaver ribbon option '<RibbonOption>'
	
@ft @training @staging @canada @europe @uk @singapore @qa
Examples:
	| User2        | RibbonOption |
	| FRD042_User2 | Save         |

Scenario: 050 Validate Workflow History and Terminate Workflow
	Given I click open on workflow inbox timekeeper leaver and at section 'Continue Timekeeper Leaver Checklist'
	When I validate timekeeper leaver history entires are '<WorkFlowHistoryEntries>'
	Then I perform timekeeper leaver ribbon option '<RibbonOption>'
	And I validate timekeeper is no longer present in workflow inbox
		
@ft @training @staging @canada @europe @uk @singapore @qa
Examples:
	| WorkFlowHistoryEntries | RibbonOption |
	| 4                      | Terminate    |
