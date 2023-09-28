Feature: Modify Phase Task

The Administrator needs to modify a Phase List, Under the Phase Child, 
link a Firm Phase to a Phase Code - Do this for a couple of items.  
Also, link the same Firm Phase code to two phase code's in the same phase list.
Azure link: https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/46370

@CancelProcess @e2eft
Scenario: 010 Create a new Phase List
	Given I navigate to the phase list process
	And I search or create a new phase list
		| PhaseListCode | Description |
		| {Auto}+9      | {Auto}+10   |
	Then I add a phase list
		| Code   | Phase          |
		| PHASE1 | Identification |
		| PHASE2 | Preservation   |
		| PHASE3 | Collection     |
	And I update it
	And I submit it
	And I validate submit was successful

@CancelProcess @e2eft
Scenario: 020 Create a Firm Phase List
	Given I navigate to the firm phase list process
	And I search or create a new firm phase list
		| FirmListCode | FirmListDescription |
		| E2EFIRMPHA   | {Auto}+10           |
	And I update it
	And I submit it
	And I validate submit was successful

@CancelProcess
Scenario: 030 Modify the Phase List
	Given I navigate to the phase list process
	Then I select a phase list to modify
	And I update the '<Description>' of the phase list
	And I submit it
	And I validate submit was successful

@e2eft
Examples:
	| Description             |
	| New Updated Description |

@CancelProcess @e2eft
Scenario: 040 Modify the Phase List Firm
	Given I navigate to the phase list process
	Then I select a phase list to modify
	And I validate that the initial modification is saved
	When I update the firm phase as per the requirement
	And I update it
	And I submit it
	And I validate submit was successful

@CancelProcess @e2eft
Scenario: 050 Confirm the Phase List contains the same Firm Phase
	Given I navigate to the phase list process
	Then I select a phase list to modify
	And I verify that the phases contain the same firm codes
	And I submit it
	And I validate submit was successful

@CancelProcess @e2eft
Scenario: 060 Update the Firm Code
	Given I navigate to the phase list process
	Then I select a phase list to modify
	When I update the first phase with a different firm code
		| FirmCode |
		| ABC1     |
	And I submit it
	And I validate submit was successful

@CancelProcess @e2eft
Scenario: 070 Verify that the Updated Firm Code was saved
	Given I navigate to the phase list process
	Then I select a phase list to modify
	When I verify that the firm code is updated
	And I submit it
	And I validate submit was successful
		
@CancelProcess @e2eft
Scenario: 080 Add New Firm Task List
	Given I navigate to the firm task list process
	And I search or create a new firm task list
		| FirmListCode | FirmListDescription |
		| E2EFIRMTSK   | {Auto}+10           |
	And I update it
	And I submit it
	And I validate submit was successful

@CancelProcess @e2eft
Scenario: 090 Add New Task List
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

@e2eft
Scenario: 100 Verify that the Added Task List is saved with the Firm Task Lists
	Given I navigate to the task list process
	Then I select a task list to modify
	And I verify that data is saved correctly
	And I cancel the process

@CancelProcess @e2eft
Scenario: 110 Add New Firm Activity List
	Given I navigate to the firm activity list process
	And I search or create a new firm activity list
		| FirmListCode | FirmListDescription |
		| E2EFIRMACT   | {Auto}+10           |
	And I update it
	And I submit it
	And I validate submit was successful

@CancelProcess @e2eft
Scenario: 120 Add New Activity List
	Given I navigate to the activity list process
	And I create a new activity list
		| ActivityListCode | Description |
		| {Auto}+9         | {Auto}+10   |
	Then I add an activity list
		| Code      | Task           | FirmActivity |
		| ACTIVITY1 | Identification | E2EFIRMACT   |
	And I update it
	And I submit it
	And I validate submit was successful

@e2eft
Scenario: 130 Verify that the added Activity List exists
	Given I navigate to the activity list process
	Then I select an activity list to modify
	And I verify that activity list data is saved correctly
	And I cancel the process

