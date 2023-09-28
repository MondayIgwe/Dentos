@us
Feature: Single User Leaver Workflow
Azure Test Cases: 
		CUST_306 - DEV001
		CUST_306 - DEV002


Scenario: 010 Manually Start Leaver Workflow
	Given I create a user with details
		| UserName | DataRoleAlias   | DefaultOperatingAlias   | UserRoleList   |
		| <User1>  | <DataRoleAlias> | <DefaultOperatingAlias> | <UserRoleList> |
	And I create a fee earner without details
	And I search for 'Timekeeper Leaver Checklist'
	When I start a timekeeper leaver workflow
		| LeaverDate | Lead Finance HR Clerk |
		| {Today}    | <User1>               |
	And I validate timekeeper leaver section 'Initial check by Finance' is readonly
	And I validate timekeeper leaver section 'Checks by Leaver or Legal Assistant' is readonly
	And I validate timekeeper leaver section 'Final Checks' is readonly
	Then I submit it
	
Examples:
	| User1        | DataRoleAlias | DefaultOperatingAlias      | UserRoleList                                                            |
	| FRD042_User1 | Admin         | Dentons United States, LLP | 0:AD:G:System Administrator (read-only setups),0:FM:W:GJ Approver: 5000 |

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

Examples:
	| User1        |
	| FRD042_User1 |

Scenario: 030 Finalize Checklist and Submit
	Given I click open on workflow inbox timekeeper leaver and at section 'No Further Action Required'
	When I validate timekeeper leaver section 'Checks by Leaver or Legal Assistant' is readonly
	And I validate timekeeper leaver section 'Final Checks' is editable
	Then I input data into timekeeper leaver section
		| Section      | LeaverDate | User    | LeaverReadyToDepart |
		| Final Checks | {Today}    | <User1> | Yes                 |
	And I submit it
	And I validate timekeeper is no longer present in workflow inbox

Examples:
	| User1        |
	| FRD042_User1 |
