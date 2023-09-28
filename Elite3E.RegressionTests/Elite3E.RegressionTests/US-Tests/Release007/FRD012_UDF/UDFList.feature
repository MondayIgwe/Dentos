@us
Feature: Create a UDF List
		Verify that a new  flag to the UDF List (COF List) with label “Print w/Template” has been added
		And that the details are saved correctly

Scenario Outline: 010 Verify that a new  flag to the UDF List has been added
	Given I navigate to the UDF List process
	When I add a new UDF List and fill all the mandatory fields
		| Code   | Description   | UDF   |
		| <Code> | <Description> | <UDF> |
	And I add a new Attributes record providing the '<UDF>'
	Then the isPrintTemplate column should exist
	And Text should be updated from '<NewValue>' to '<OldValue>'

Examples:
	| Code     | Description | UDF      | NewValue           | OldValue        |
	| {Auto}+6 | {Auto}+10   | CLAIMVAL | Print w/ Templates | IsPrintTemplate |



Scenario Outline: 020 Submit the UDF List created
	When I submit the UDF list
	Then the udf details should be saved correctly
		

Scenario Outline: 030 Add UDF list using Client Maintenance process
	Given I search for process 'Client Maintenance'
	Then I add a UDF list
	And Text should be updated from '<NewValue>' to '<OldValue>'
Examples:
	| NewValue           | OldValue        |
	| Print w/ Templates | IsPrintTemplate |