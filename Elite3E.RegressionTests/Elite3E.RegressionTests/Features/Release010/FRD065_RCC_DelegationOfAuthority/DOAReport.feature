@release10 @frd065 @DOAReport
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
	
@ft @qa 
Examples:
	| Unit                      | Office   | Department | Role                           |
	| Dentons Australia Limited | Aberdeen | Default    | 0:FM:W:GJ Approver: 1001: 8006 |

@singapore
Examples:
	| Unit                         | Office    | Department            | Role                     |
	| Dentons Rodyk & Davidson LLP | Singapore | Corporate - Singapore | 0:FM:W:GJ Approver: Firm |
@europe @staging
Examples:
	| Unit | Office      | Department | Role                     |
	| Firm | London (EU) | Default    | 0:FM:W:GJ Approver: Firm |
@uk @training
Examples:
	| Unit | Office   | Department | Role                     |
	| Firm | Aberdeen | Default    | 0:FM:W:GJ Approver: Firm |
@canada
Examples:
	| Unit | Office    | Department | Role                     |
	| Firm | Vancouver | Default    | 0:FM:W:GJ Approver: Firm |


Scenario Outline: 002 Create a DoA Report
	Given I navigate to the DoA Report process
	Then all the report  fields should exist
	When I provide some information and run the Report
		| Unit   | Office   | Department   | Role   |
		| <Unit> | <Office> | <Department> | <Role> |
	And all the respective columns are shown with correct values
	
@ft @qa @uk
Examples:
	| Unit                      | Office   | Department | Role                           |
	| Dentons Australia Limited | Aberdeen | Default    | 0:FM:W:GJ Approver: 1001: 8006 |

@singapore
Examples:
	| Unit                         | Office    | Department            | Role                     |
	| Dentons Rodyk & Davidson LLP | Singapore | Corporate - Singapore | 0:FM:W:GJ Approver: Firm |

@europe @staging
Examples:
	| Unit | Office      | Department | Role                     |
	| Firm | London (EU) | Default    | 0:FM:W:GJ Approver: Firm |
	
@uk
Examples:
	| Unit | Office   | Department | Role                     |
	| Firm | Aberdeen | Default    | 0:FM:W:GJ Approver: Firm |
@canada
Examples:
	| Unit | Office    | Department | Role                     |
	| Firm | Vancouver | Default    | 0:FM:W:GJ Approver: Firm |
