@us
Feature: DOAReport

CUST_400 - DEV001 - DOA Reporting (ID: 26562)

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
	
Examples:
	| Unit | Office  | Department | Role                     |
	| Firm | Chicago | Default    | 0:FM:W:GJ Approver: 5000 |

Scenario Outline: 002 Create a DoA Report
	Given I navigate to the DoA Report process
	Then all the report  fields should exist
	When I provide some information and run the Report
		| Unit   | Office   | Department   | Role   |
		| <Unit> | <Office> | <Department> | <Role> |
	And all the respective columns are shown with correct values
	
Examples:
	| Unit | Office  | Department | Role                     |
	| Firm | Chicago | Default    | 0:FM:W:GJ Approver: 5000 |