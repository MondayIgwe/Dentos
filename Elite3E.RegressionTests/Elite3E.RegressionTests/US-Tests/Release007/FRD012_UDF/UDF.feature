@us @ignore

#Defect https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/52501
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

Examples:
	| Type   |
	| String |