@us
Feature: 3EInstancesSetupManagement

Scenario Outline: 10 I want to create a 3E Instance
	Given I have added a production instance type
		| Code           | Description    | IsProduction | IsActive   |
		| <InstanceType> | <InstanceType> | false        | <IsActive> |
	And I have an existing region
		| Code     | Description | DisableIntegration   | IsActive   |
		| <Region> | <Region>    | <DisableIntegration> | <IsActive> |
	When I want to create a three e instance
		| Code   | Description   | Region   | IsActive   | InstanceType   |
		| <Code> | <Description> | <Region> | <IsActive> | <InstanceType> |
	Then I want to delete the three e instance i added initially
	And I want to delete the instance type i added initially
	And I want to delete the region i added initially


Examples:
	| Code       | Description     | InstanceType   | Region        | IsActive |
	| CreateInst | Create Instance | CreateInstType | CreateInstReg | true     |

	
Scenario Outline: 20 I want to create and update a 3E Instance
	Given I have added a production instance type
		| Code           | Description    | IsProduction | IsActive   |
		| <InstanceType> | <InstanceType> | false        | <IsActive> |
	And I have an existing region
		| Code     | Description | DisableIntegration   | IsActive   |
		| <Region> | <Region>    | <DisableIntegration> | <IsActive> |
	When I want to create a three e instance
		| Code   | Description   | Region   | IsActive   | InstanceType   |
		| <Code> | <Description> | <Region> | <IsActive> | <InstanceType> |
	Then I want to edit and update the three e instance
	Then I want to verify and delete created three e instance
	And I want to delete the instance type i added initially
	And I want to delete the region i added initially

Examples:
	| Code       | Description     | InstanceType   | Region        | IsActive |
	| CreateInst | Create Instance | CreateInstType | CreateInstReg | true     |


	#Check with Siya why at staging we are getting error message  Enter a unique Code.
Scenario Outline: 30 I want to confirm Only one 3E instance with the production flag set to true exists.
	Given I have added a production instance type
		| Code     | Description | IsProduction | IsActive   |
		| {Auto}+6 | {Auto}+12   | true         | <IsActive> |
	And I have an existing region
		| Code     | Description | DisableIntegration   | IsActive   |
		| {Auto}+6 | {Auto}+12   | <DisableIntegration> | <IsActive> |
	And I have an existing three e instance
		| Code     | Description | IsActive   |
		| {Auto}+6 | {Auto}+12   | <IsActive> |
	When I want to create another three e instance
		| Code     | Description | IsActive   |
		| {Auto}+6 | {Auto}+12   | <IsActive> |
	Then I verify that an error message 'one record can be marked as production' is generated
	Then I want to delete the three e instance i added initially
	And I want to delete the instance type i added initially
	And I want to delete the region i added initially

Examples:
	| Region          | InstanceType    | IsActive |
	| Test Automation | Test Automation | true     |
