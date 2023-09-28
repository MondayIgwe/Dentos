@us
Feature: Create a UDF with an existing Validation List
Verify that a newly created validation list created in the UDF Validation List  (Now renamed as Client Output Field (COF) Validation List) process,
can be used in the UDF (COF) process


Scenario Outline: 010 Data Prep - UDF Validation list
	Given I navigate to the UDF Validation List process
	And I add a new UDF Validation List
	Then I complete all the mandatory fields
		| Code     | Description |
		| {Auto}+8 | {Auto}+11   |
	Then I submit it
	And I validate submit was successful
	
Scenario Outline: 020 Create a UDF record with a newly created Validation List
	Given I navigate to the new UDF process
	And I add a new UDF record
	When I fill the UDF form record
		| Code     | Description | Label    | Type |
		| {Auto}+10 | {Auto}+10   | {Auto}+8 | List |
	And I select the Validation List from the dropdown
	And I submit the form
	And I validate submit was successful
