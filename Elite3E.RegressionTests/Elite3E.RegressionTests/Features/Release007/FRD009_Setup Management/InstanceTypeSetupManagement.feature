@release7 @frd009 @InstanceSetupManagement
Feature: InstanceSetupManagement

Scenario Outline: 10 I want to create and verify if Instance Type process
	When I want to create a new instance type process
		| Code   | Description   | IsProduction   | IsActive   |
		| <Code> | <Description> | <IsProduction> | <IsActive> |
	Then I want to verify that the record has been added
	And I want to delete the it

@ft @training @staging  @canada @europe @uk @singapore
Examples:
	| Code        | Description        | IsProduction | IsActive |
	| Test_NonPro | Instance Type Test | false        | true     |
@qa
Examples:
	| Code      | Description        | IsProduction | IsActive |
	| Test_Prod | Instance Type Test | true         | true     |

Scenario Outline: 20 I want to create a second production instance type
	Given I have added a production instance type
		| Code   | Description   | IsProduction   | IsActive   |
		| <Code> | <Description> | <IsProduction> | <IsActive> |
	When I want to create another production instance type
		| Code   | Description   | IsProduction | IsActive   |
		| <Code> | <Description> | true         | <IsActive> |
	Then I verify that an error message 'Another record marked as Production exists' is generated
	And I want to delete the instance type i added initially

@ft @training @staging  @canada @europe @uk @singapore
Examples:
	| Code     | Description        | IsProduction | IsActive |
	| ProdTest | Instance Type Test | true         | true     |
@qa
Examples:
	| Code      | Description        | IsProduction | IsActive |
	| TestProd2 | Instance Type Test | true         | true     |