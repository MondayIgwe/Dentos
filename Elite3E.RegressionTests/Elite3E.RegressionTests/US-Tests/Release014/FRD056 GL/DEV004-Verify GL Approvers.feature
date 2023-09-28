@us
Feature: DEV004-Verify GL Approvers

Verify GL Approvers process
Verify three new fields in GL Account, Fee Earner Maintenance, GL Unit processes

https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/60938
https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/60939
https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/60940
https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/61562

@CancelProcess
Scenario: 010 Verify GL Approvers process
	Given I search for process 'GL Approvers'
	And I click add
	Then Verify the given fields are present
		| FieldName   |
		| Code        |
		| Description |
		| Active      |
		| Start Date  |
		| End Date    |
	And I add a new GL approver
		| Code      | Description |
		| {Auto}+10 | {Auto}+20   |
	And I update it
	And I submit it
	And I validate submit was successful
	When I search for process 'GL Approvers'
	Then I validate GL Approver is created
	And I cancel the process

@CancelProcess
Scenario: 020 Verify Approvers in the given processes
	Given I verify approver details in the following processes
		| ProcessName            |
		| GL Account             |
		| Fee Earner Maintenance |
		| GL Unit                |		