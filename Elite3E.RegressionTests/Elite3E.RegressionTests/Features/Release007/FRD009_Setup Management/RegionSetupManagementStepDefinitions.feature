@release7 @frd009 @RegionSetupManagement
Feature: RegionSetupManagement


Scenario Outline: 10 I want to create and verify if Region process
	When I want to create a new region process
		| Code   | Description   | DisableIntegration   | IsActive   |
		| <Code> | <Description> | <DisableIntegration> | <IsActive> |
	Then I want to verify that the region has been added
	And I want to delete the it

@ft @training @staging  @canada @europe @uk @singapore @qa
Examples:
	| Code        | Description | DisableIntegration | IsActive |
	| Region_Test | Region Test | true               | true     |


Scenario Outline: 20 I want to update and verify  Region process
	Given I have an existing region
		| Code   | Description   | DisableIntegration   | IsActive   |
		| <Code> | <Description> | <DisableIntegration> | <IsActive> |
	Then I want to update the region
		| Description           | DisableIntegration | IsActive   |
		| . updated description | false              | <IsActive> |
	And I want to verify that the region has been updated
	Then I want to delete the it

@ft @training @staging  @canada @europe @uk @singapore @qa
Examples:
	| Code        | Description | DisableIntegration | IsActive |
	| Region_Test | Region Test | true               | true     |
