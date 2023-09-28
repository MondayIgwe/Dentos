@release7 @frd012 @CreateAUDF
Feature: Create a UDF
		Define (create) a UDF (now Client Output Field (COF)) record in the UDF (Client Output Fields (COF)) process

Scenario Outline: 010 Create a UDF record
	Given I navigate to the new UDF process
	And I add a new UDF record
	When I fill the UDF form record
		| Code     | Description | Label    | Type   |
		| {Auto}+5 | {Auto}+10   | {Auto}+8 | <Type> |
	Then all the UDF fields should exist along with the dropdown values
	And I submit the form

@ft @qa @training @staging  @canada @europe @uk @singapore
Examples:
	| Type   |
	| String |