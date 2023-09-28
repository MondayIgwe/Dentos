Feature: DEV002 - GL Unit Group

Verify that in the GL unit  group process only one unit can be a lead unit
https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/60865

@CancelProcess
Scenario: 010 Verify that in the GL unit  group process only one unit can be a lead unit
	Given I search for process 'GL Unit Group'
	And I add new gl unit group
		| Code      | Description |
		| {Auto}+10 | {Auto}+20   |
	When I add a unit to the group and mark it as lead unit
		| Unit    |
		| <Unit1> |
	And I add another unit to the group and mark it as lead unit
		| Unit    |
		| <Unit2> |
	And I update it
	Then I get an error regarding the lead unit allowed
		| Error Message                                  |
		| Only one row must be selected as the Lead Unit |

@ft @training @staging @qa @uk @europe @canada @singapore
Examples:
	| Unit1 | Unit2 |
	| 1101  | 1221  |


@CancelProcess
Scenario: 020 Verify that a unit can only be linked to one group
	Given I search for process 'GL Unit Group'
	And I add new gl unit group
		| Code      | Description |
		| {Auto}+10 | {Auto}+20   |
	When I add a unit to the group and mark it as lead unit
		| Unit    |
		| <Unit1> |
	And I add another unit similar to the previous one
		| Unit    |
		| <Unit1> |
	And I update it
	Then I get an error regarding the lead unit allowed
		| Error Message                                                                                          |
		| GLUnit (1221) cannot be assigned to a group because there are some records for the unit in GLUnitLocal |

@ft @training @staging @qa @uk @europe @canada @singapore
Examples:
	| Unit1 |
	| 1221  |


@CancelProcess
Scenario: 030 Verify that a unit can have only units without local units 
	Given I search for process 'GL Unit Group'
	And I add new gl unit group
		| Code      | Description |
		| {Auto}+10 | {Auto}+20   |
	When I add a unit to the group and mark it as lead unit
		| Unit    |
		| <Unit1> |
	And I add  a unit to the group
		| Unit    |
		| <Unit2> |
	And I update it
	Then I verify that an error message '<ErrorMessage>' is generated


@ft @training @staging @qa @uk @europe @canada @singapore
Examples:
	| Unit1 | Unit2 | ErrorMessage                                                                                           |
	| 1221  | 1251  | GLUnit (1251) cannot be assigned to a group because there are some records for the unit in GLUnitLocal |
	
@CancelProcess
Scenario: 040 Verify that Once a Lead Unit has been assigned to a Group and local accounts and mappings have been copied to other units, the Lead Unit cannot be changed
	Given I search for process 'GL Unit'
	When I perform quick find for '<Unit1>'
	Then I verify the lead unit group has been checked and is read only

@ft @training @staging @qa @uk @europe @canada
Examples:
	| Unit1     |
	| 3000 |

@singapore
Examples:
	| Unit1     |
	| 1201 |