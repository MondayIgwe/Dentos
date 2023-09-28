@us
Feature: UDFValidationList
Verify that a validation list can be created in the UDF Validation List (Now renamed as Client Output Field (COF) Validation List) process

Scenario Outline: 010 Verify that a new UDF Validation list can be added
	Given I navigate to the UDF Validation List process
	And I add a new UDF Validation List
	Then  I complete all the mandatory fields
		| Code     | Description |
		| {Auto}+8 | {Auto}+11   |

Scenario Outline: 020 Add List items
	When I add the List items child form and complete all the mandatory fields
		| Code   | Description   |
		| <Code> | <Description> |
		| <Code> | <Description> |
	And I submit the form

	Examples:
		| Code     | Description |
		| {Auto}+7 | {Auto}+10   |
	
