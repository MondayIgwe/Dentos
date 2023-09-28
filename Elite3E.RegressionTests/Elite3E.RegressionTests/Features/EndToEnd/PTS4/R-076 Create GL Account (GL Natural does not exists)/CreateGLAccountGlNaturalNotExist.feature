@ignore
Feature: CreateGLAccountGlNaturalNotExist

A short summary of the feature
R-076 - R2R - Create GL Account (GL Natural does not exists - control/aggregate account with Local Natural required)
https://dev.azure.com/dentonsglobal/GFT%203E/_testPlans/execute?view=_TestManagement&planId=12845&suiteId=38475

030 I want add a GL Unit is unable to submit due to error when selecting Unit


@e2eft
Scenario: 010 I want to Confirm GL Natural does not exist
	Given I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	When I open gl natural process
	Then I search for an non existing gl natural
		| GlNatural |
		| NoExistGl |
	Then I cancel the process

Examples:
	| FeeEarnerName         |
	| DentonsAPI FeeEarner3 |


@e2eft
Scenario: 020 I want add a GL Natural
	Given I open gl natural process
	When I want to add a gl natural
		| GlNatural | Description    | ValueType | AccountCategory | IsControlAccount | IsAggregate | IsAutoAdd |
		| Test12    | This is a test | Rollup    | Revenue         | true             | true        | true      |
	Then I submit it
	And I validate submit was successful

@e2eft
Scenario: 030 I want add a GL Unit
	Given I open gl unit process
	When I want to add a gl unit
		| GlValue | Description    | ValueType | Unit                             | IsUseLocalAccount | GlLocalChart            |
		| 1234    | This is a test | Normal    | 1001 - Dentons Australia Limited | true              | Dentons Australia Local |
	Then I want to add unit local account child details
	Then I submit it
	And I validate submit was successful


@e2eft
Scenario: 040 I want add a GL Account
	Given I open gl account process
	When I want to add a gl account
		| GL Unit                   | Natural Account               | GLUnitLocal | GLDepartment | GLSection | GLOffice | Description |
		| Dentons Australia Limited | PP&E Construction in progress | Default     | Default      | Default   | Default  |             |
	Then I submit it
	And I validate submit was successful

