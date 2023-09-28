@us
Feature: PropagationSetUpManagment


Scenario: 10 I want to propagate records
	Given I have added a production instance type
		| Code   | Description   | IsProduction | IsActive   |
		| <Code> | <Description> | false        | <IsActive> |
	And I have an existing region
		| Code   | Description   | DisableIntegration   | IsActive   |
		| <Code> | <Description> | <DisableIntegration> | <IsActive> |
	And I have an existing three e instance
		| Code   | Description   | Region   | IsActive   | InstanceType   |
		| <Code> | <Description> | <Region> | <IsActive> | <InstanceType> |
	And I have a added 'NxUnit' process
	And I have a process to select 'NxUnit'
	When I add a child three e instance
		| Process   | Instance   | ControlSource   |
		| <Process> | <Instance> | <ControlSource> |
	Then I want to 'include' items having 'System Required - No Transactions'
	And I add another child three e instance
		| Process   | Instance   | ControlSource   |
		| <Process> | <Instance> | <ControlSource> |
	And I want to 'exclude' items having 'System Required - No Transactions'
	Then I want to submit

Examples:
	| Process     | Instance        | ControlSource  | Code           | Description     | InstanceType    | Region          | IsActive |
	| Region_Test | Test Automation | Partial Global | TestAutomation | Test Automation | Test Automation | Test Automation | true     |

Scenario: 20 I want to get an error when i add another propagate records
	Given I have added a production instance type
		| Code   | Description   | IsProduction | IsActive   |
		| <Code> | <Description> | false        | <IsActive> |
	And I have an existing region
		| Code   | Description   | DisableIntegration   | IsActive   |
		| <Code> | <Description> | <DisableIntegration> | <IsActive> |
	And I have an existing three e instance
		| Code   | Description   | Region   | IsActive   | InstanceType   |
		| <Code> | <Description> | <Region> | <IsActive> | <InstanceType> |
	And I have a process to select 'NxUnit'
	When I add a child three e instance
		| Process   | Instance   | ControlSource   |
		| <Process> | <Instance> | <ControlSource> |
	Then I want to 'include' items having 'System Required - No Transactions'
	Then I add another child three e instance
		| Process   | Instance   | ControlSource   |
		| <Process> | <Instance> | <ControlSource> |
	And I want to 'exclude' items having 'System Required - No Transactions'
	Then I want to submit and get an error message
	And I want to delete the it
	And I want to delete the existing record

Examples:
	| Process     | Instance        | ControlSource  | Code           | Description     | InstanceType    | Region          | IsActive |
	| Region_Test | Test Automation | Partial Global | TestAutomation | Test Automation | Test Automation | Test Automation | true     |
