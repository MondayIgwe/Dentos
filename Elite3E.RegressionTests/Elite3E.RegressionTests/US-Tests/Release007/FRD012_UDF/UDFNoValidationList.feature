@us
Feature: Create a UDF with no Validation List supplied
		Verify that when the value 'List' is selected in the Type field in the UDF  (Now Client Output Field (COF)) process, 
		the Validation List field becomes available and mandatory


Scenario Outline: 010 Data Prep - UDF Validation list
	Given I navigate to the UDF Validation List process
	And I add a new UDF Validation List
	Then I complete all the mandatory fields
		| Code     | Description |
		| {Auto}+8 | {Auto}+11   |
	Then I submit it


Scenario Outline: 020 Create a UDF record with no Validation List
	Given I navigate to the new UDF process
	And I add a new UDF record
	When I fill the UDF form record
		| Code     | Description | Label    | Type |
		| {Auto}+5 | {Auto}+10   | {Auto}+8 | List |
	And I submit the form
	Then the error should occur about the validation list field
	When I select the Validation List from the dropdown
	And I submit the form
