@release10 @frd065 @DOARoles
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

@ft @qa
Examples:
	| Unit                      | Office   | Department | Role                           |
	| Dentons Australia Limited | Aberdeen | Default    | 0:FM:W:GJ Approver: 1001: 8006 |

@uk @staging @training
Examples:
	| Unit | Office   | Department | Role                     |
	| Firm | Aberdeen | Default    | 0:FM:W:GJ Approver: Firm |
		
@singapore
Examples:
	| Unit                         | Office    | Department            | Role                     |
	| Dentons Rodyk & Davidson LLP | Singapore | Corporate - Singapore | 0:FM:W:GJ Approver: Firm |

@europe
Examples:
	| Unit | Office      | Department | Role                     |
	| Firm | London (EU) | Default    | 0:FM:W:GJ Approver: Firm |

@canada
Examples:
	| Unit | Office    | Department | Role                     |
	| Firm | Vancouver | Default    | 0:FM:W:GJ Approver: Firm |

	
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

@ft @qa @training @staging  @canada @europe @uk @singapore
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

@ft @qa 
Examples:
	| Unit                      | Office   | Department | Role                           |
	| Dentons Australia Limited | Aberdeen | Default    | 0:FM:W:GJ Approver: 1001: 8006 |
	
@singapore
Examples:
	| Unit                         | Office    | Department            | Role                     |
	| Dentons Rodyk & Davidson LLP | Singapore | Corporate - Singapore | 0:FM:W:GJ Approver: Firm |

@uk @training @staging
Examples:
	| Unit | Office   | Department | Role                     |
	| Firm | Aberdeen | Default    | 0:FM:W:GJ Approver: Firm |
@europe
Examples:
	| Unit | Office      | Department | Role                     |
	| Firm | London (EU) | Default    | 0:FM:W:GJ Approver: Firm |
@canada
Examples:
	| Unit | Office    | Department | Role                     |
	| Firm | Vancouver | Default    | 0:FM:W:GJ Approver: Firm |


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
	
@ft @qa 
Examples:
	| Unit                      | Office   | Department | Role                           |
	| Dentons Australia Limited | Aberdeen | Default    | 0:FM:W:GJ Approver: 1001: 8006 |
	
@singapore
Examples:
	| Unit                         | Office    | Department            | Role                     |
	| Dentons Rodyk & Davidson LLP | Singapore | Corporate - Singapore | 0:FM:W:GJ Approver: Firm |

@uk @training @staging 
Examples:
	| Unit | Office   | Department | Role                     |
	| Firm | Aberdeen | Default    | 0:FM:W:GJ Approver: Firm |
@europe
Examples:
	| Unit | Office      | Department | Role                     |
	| Firm | London (EU) | Default    | 0:FM:W:GJ Approver: Firm |
@canada
Examples:
	| Unit | Office    | Department | Role                     |
	| Firm | Vancouver | Default    | 0:FM:W:GJ Approver: Firm |


