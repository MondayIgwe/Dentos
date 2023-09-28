Feature: Matter Maintenance Modify

Update and maintain PTA Groups (Assignment to Matter)
Azure Link: https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/56624

@CancelProcess
Scenario: 001 Prepare Matter for the PTA Group test
	Given I create the Payer with Api
		| PayerName   | Entity       |
		| <PayorName> | CentralCoast |
	And I create a matter with details:
		| Client   | Status     | OpenDate   | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | Rate   | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| <Client> | Fully Open | {Today}-31 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <Rate> | <ChargeTypeGroup>   | <CostTypeGroup>   | <PayorName> |
	
@e2eft
Examples:
	| Client                | Currency            | CurrencyMethod | Office         | Department | Section | Rate     | ChargeTypeGroup | CostTypeGroup   | PayorName |
	| Client_Automation CMF | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Standard | TestGroup3      | Desc_at_3e8glw7 |James Mayor |

@CancelProcess @e2eft
Scenario: 010 Create a New Phase List
	Given I navigate to the phase list process
	And I search or create a new phase list
		| PhaseListCode | Description |
		| {Auto}+9      | {Auto}+10   |
	Then I add a phase list
		| Code   | Phase          |
		| PHASE1 | Identification |
	And I update it
	And I submit it
	And I validate submit was successful

@CancelProcess @e2eft
Scenario: 020 Add New Task List
	Given I navigate to the task list process
	And I create a new task list
		| TaskListCode | Description |
		| {Auto}+9     | {Auto}+10   |
	Then I add a task list
		| Code  | Task           | FirmTask   |
		| TASK1 | Identification | E2EFIRMTSK |
	And I update it
	And I submit it
	And I validate submit was successful

@CancelProcess @e2eft
Scenario: 030 Add New Activity List
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

@CancelProcess @e2eft
Scenario: 040 Add New PTA Group
	Given I navigate to the pta group process
	And I click add
	And I fill out the pta group fields
		| PTAGroupCode | Description |
		| {Auto}+11    | {Auto}+15   |
	Then I update it
	And I submit it

@CancelProcess @e2eft
Scenario: 050 Add PTA Group to the Matter
	Given I navigate to the matter maintenance process
	When I quick search by matter number
	And I add the pta group to the matter
	Then I update it
	And I submit it

@CancelProcess @e2eft
Scenario: 060 Verify PTA Group is added to the Matter
	Given I navigate to the matter maintenance process
	When I quick search by matter number
	And I verify the pta group is added to the matter

	

