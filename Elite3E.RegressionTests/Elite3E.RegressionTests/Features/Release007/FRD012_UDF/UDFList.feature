@release7 @frd012 @CreateAUDFList
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
@ft @europe @uk @qa
Examples:
	| Code     | Description | UDF         | NewValue           | OldValue        |
	| {Auto}+6 | {Auto}+10   | Claim Value | Print w/ Templates | IsPrintTemplate |
@training @singapore @canada
Examples:
	| Code     | Description | UDF        | NewValue           | OldValue        |
	| {Auto}+6 | {Auto}+10   | at_3l7x3wL | Print w/ Templates | IsPrintTemplate |
@staging
Examples:
	| Code     | Description | UDF        | NewValue           | OldValue        |
	| {Auto}+6 | {Auto}+10   | at_SRKRulj | Print w/ Templates | IsPrintTemplate |

@ft @training @staging @canada @europe @uk @singapore @qa
Scenario Outline: 020 Submit the UDF List created
	When I submit the UDF list
	Then the udf details should be saved correctly
		
@ft @training @staging @canada @europe @uk @singapore @qa
 Scenario Outline: 030 Add UDF list using Client Maintenance process
	Given I search for process 'Client Maintenance'
	Then I add a UDF list
	And Text should be updated from '<NewValue>' to '<OldValue>'
Examples:
	| NewValue           | OldValue        |
	| Print w/ Templates | IsPrintTemplate |