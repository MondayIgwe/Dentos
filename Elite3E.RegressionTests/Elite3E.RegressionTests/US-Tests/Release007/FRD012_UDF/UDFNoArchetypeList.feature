@us
Feature: Create a UDF with no Archetype supplied
		Verify that when the value 'Archetype' is selected in the Type field in the UDF (now renamed COF) process,
		the Archetype field becomes available and mandatory

Scenario Outline:  010 Create a UDF record with no Archetype
	Given I navigate to the new UDF process
	And I add a new UDF record
	When I fill the UDF form record
		| Code     | Description | Label    | Type   |
		| {Auto}+5 | {Auto}+10   | {Auto}+8 | <Type> |
	And I submit the form
	Then the error should occur about the archetype field
	And I provide the archetype
		| Archetype   |
		| <Archetype> |
	And I submit the form

	Examples:
		| Type      | Archetype |
		| Archetype | Activity  |
