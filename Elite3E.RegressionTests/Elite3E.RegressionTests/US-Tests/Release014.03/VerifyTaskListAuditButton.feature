@us
Feature: VerifyTaskListAuditButton

@CancelProcess
Scenario: 005 Add New Firm Task List
	Given I navigate to the firm task list process
	And I search or create a new firm task list
		| FirmListCode | FirmListDescription |
		| E2EFIRMTSK   | {Auto}+10           |
	And I update it
	And I submit it
	And I validate submit was successful

@CancelProcess
Scenario: 010 Add New Task List
	Given I navigate to the task list process
	And I create a new task list
		| TaskListCode | Description |
		| {Auto}+9     | {Auto}+10   |
	Then I add a task list
		| Code  | Task           | FirmTask   |
		| TASK1 | Identification | E2EFIRMTSK |
		| TASK2 | Preservation   | E2EFIRMTSK |
		| TASK3 | Collection     | E2EFIRMTSK |
	And I update it
	And I submit it
	And I validate submit was successful

@CancelProcess
Scenario: 020 Verify that the Added Task List is saved with the Firm Task Lists
	Given I navigate to the task list process
	Then I select a task list to modify
	And I verify the audit button in the task childform
	And I cancel the process
