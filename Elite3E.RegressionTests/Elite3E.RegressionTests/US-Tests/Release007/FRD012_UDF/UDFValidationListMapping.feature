@us
Feature: UDFValidationListMapping
Verify the elements on the UDF Validation List Mapping (now renamed as Client Output Fields Validation List Mapping or COF Validation List Mapping) process screen

Scenario Outline: 010 Data Prep 1 - UDF Validation list
	Given I create a udf validation list with child forms	
		| Code      | Description      | ValueCode  | ValueDescription  |
		| PListCode | Parent List Desc | PValueCode | Parent Value Desc |
		| CListCode | Child List Desc  | CValueCode | Child Value Desc  |

Scenario Outline: 020 Verify that a new UDF Validation List Mappingg can be added
	Given I navigate to the UDF Validation List Mapping process
	And I add a new UDF Validation List Mapping
	Then all the required fields should exist
	When I complete all the required fields
		| Code     | Description | Parent List  | Child List  |
		| {Auto}+7 | {Auto}+10   | <ParentList> | <ChildList> |


Examples:
	| ParentList       | ChildList       |
	| Parent List Desc | Child List Desc |

Scenario Outline: 030 Add Mapping
	When I add the Mapping on the child form and complete all the mandatory fields
		| Parent Value  | Child Value  |
		| <ParentValue> | <ChildValue> |
	And I submit the form
	Then I validate submit was successful

Examples:
	| ParentValue       | ChildValue       |
	| Parent Value Desc | Child Value Desc |