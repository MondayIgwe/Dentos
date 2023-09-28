@us
Feature: Create a UDF with a duplicate Code
Verify that the UDF (now Client Output Fields (COF)) process Code field does not accept duplicate codes

Scenario Outline:  010 Create a UDF record
	Given I navigate to the new UDF process
	And I add a new UDF record
	When I fill the UDF form record
		| Code   | Description | Label    | Type   |
		| <Code> | {Auto}+10   | {Auto}+8 | <Type> |
	And I submit the form
	Then I get error message about the duplicate code

	Examples:
		| Code     | Type   |
		| CLAIMVAL | String |
