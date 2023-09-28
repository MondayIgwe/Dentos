@us
Feature: DOARoles

CUST_400 - DEV001 - DOA Roles (ID: 26561)

Scenario Outline: 001 Create a DoA Role Type
	Given I navigate to the DoA Role Type process
	And I add a new DoA Role Type
	Then all the relevant fields should exist
	When I provide all the required data
		| Code     | Description |
		| {Auto}+8 | {Auto}+15   |
	And I add the DoA Roles
		| Unit   | Office   | Department   | Role   |
		| <Unit> | <Office> | <Department> | <Role> |
	And I submit it
	And all the data is saved correctly


Examples:
	| Unit | Office  | Department | Role                     |
	| Firm | Chicago | Default    | 0:FM:W:GJ Approver: 5000 |
	
Scenario Outline: 002 Create a Workflow Rule Set Routing
	Given I navigate to the Worflow Rule Set Routing process
	Then all the correct fields should exist
	When I provide all rule set  data
		| RuleSet   |
		| <RuleSet> |
	And I submit it
	And the rules must be saved correctly
		| RuleSet   |
		| <RuleSet> |

Examples:
	| RuleSet           |
	| Approval Required |
	
Scenario Outline: 003 Delete a DoA Role Type
	Given I navigate to the DoA Role Type process
	And I add a new DoA Role Type
	When I provide all the required data
		| Code     | Description |
		| {Auto}+8 | {Auto}+15   |
	And I add the DoA Roles
		| Unit   | Office   | Department   | Role   |
		| <Unit> | <Office> | <Department> | <Role> |
	And I submit it
	And I delete the DoA Role
	And I submit it
	Then I verify that the record has been deleted

Examples:
	| Unit | Office  | Department | Role                     |
	| Firm | Chicago | Default    | 0:FM:W:GJ Approver: 5000 |

@CancelProcess
Scenario Outline: 004 Create a duplicate DoA Role Type
	Given I navigate to the DoA Role Type process
	And I add a new DoA Role Type
	When I provide all the required data
		| Code     | Description |
		| {Auto}+8 | {Auto}+15   |
	And I add the duplicate DoA Roles
		| Unit   | Office   | Department   | Role   |
		| <Unit> | <Office> | <Department> | <Role> |
	Then I should get an error
	

Examples:
	| Unit | Office  | Department | Role                     |
	| Firm | Chicago | Default    | 0:FM:W:GJ Approver: 5000 |


