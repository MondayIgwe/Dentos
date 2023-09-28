Feature: GLNaturalDoesNotExists

Search for GL Natural account and verify its not present in the list
Also validate that Add button is greyed out

@e2eft
Scenario: 010 Search GL account and verify its not present
	Given I search for process 'GL Natural'
	When I quick search by "<Account>" and verify its not present

@e2eft
Examples:
	| Account   |
	| {Auto}+10 |
