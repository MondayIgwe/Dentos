@release8 @frd024.1 @MultiUserLeaverWorkflow @ProxyTest
Feature: Multi User Leaver Workflow
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
	
@ft @staging @uk @qa
Examples:
	| User1  | User2  | DataRoleAlias | DefaultOperatingAlias        | UserRoleList                                         |
	| Kasper | Aparna | Admin         | Dentons Rodyk & Davidson LLP | 0:AD:G:System Administrator,0:FM:W:GJ Approver: Firm |
@canada
Examples:
	| User1  | User2  | DataRoleAlias | DefaultOperatingAlias | UserRoleList                                         |
	| Kasper | Aparna | Admin         | Dentons Canada LLP    | 0:AD:G:System Administrator,0:FM:W:GJ Approver: Firm |
@singapore
Examples:
	| User1  | User2  | DataRoleAlias | DefaultOperatingAlias        | UserRoleList                                         |
	| Kasper | Aparna | Admin         | Dentons Rodyk & Davidson LLP | 0:AD:G:System Administrator,0:FM:W:GJ Approver: Firm |
@europe @training
Examples:
	| User1  | User2  | DataRoleAlias | DefaultOperatingAlias   | UserRoleList                                         |
	| Kasper | Aparna | Admin         | Dentons Europe LLP (UK) | 0:AD:G:System Administrator,0:FM:W:GJ Approver: Firm |

			
Scenario: 020 Proxy as User1 and Fill Initial Checks
	Given I proxy as user '<User1>'
	When I search for 'Workflow Dashboard'
	Then I click on workflow inbox option 'Timekeeper Leave Checklist'
	And I click open on workflow inbox timekeeper leaver and at section 'Timekeeper Leaver Checklist Submitted'
	And I validate timekeeper leaver section 'Initial check by Finance' is editable
	And I input data into timekeeper leaver section
		| Section                  | LeaverDate | User    | NextActionUser |
		| Initial check by Finance | {Today}    | <User1> | <User2>        |
	And I submit it
	And I validate timekeeper is no longer present in workflow inbox
	And I cancel proxy

@ft @training @staging @canada @europe @uk @singapore @qa
Examples:
	| User1  | User2  |
	| Kasper | Aparna |

Scenario: 030 Proxy as User2 and Fill Checks by Leaver or Legal Assistant
	Given I proxy as user '<User2>'
	When I search for 'Workflow Dashboard'
	Then I click on workflow inbox option 'Timekeeper Leave Checklist'
	And I click open on workflow inbox timekeeper leaver and at section 'Further Action Required'
	And I validate timekeeper leaver section 'Checks by Leaver or Legal Assistant' is editable
	And I input data into timekeeper leaver section
		| Section                             | LeaverDate | User    |
		| Checks by Leaver or Legal Assistant | {Today}    | <User2> |
	And I submit it
	And I validate timekeeper is no longer present in workflow inbox
	And I cancel proxy

@ft @training @staging @canada @europe @uk @singapore @qa
Examples:
	| User2  |
	| Aparna |
			
Scenario: 040 Proxy as User1 and Fill Final Checks
	Given I proxy as user '<User1>'
	When I search for 'Workflow Dashboard'
	Then I click on workflow inbox option 'Timekeeper Leave Checklist'
	And I click open on workflow inbox timekeeper leaver and at section 'Lead Finance HR Clerk Review'
	And I validate timekeeper leaver section 'Final Checks' is editable
	And I input data into timekeeper leaver section
		| Section      | LeaverDate | User    | LeaverReadyToDepart |
		| Final Checks | {Today}    | <User1> | Yes                 |
	And I submit it
	And I validate timekeeper is no longer present in workflow inbox
	And I cancel proxy

@ft @training @staging @canada @europe @uk @singapore @qa
Examples:
	| User1  |
	| Kasper |
