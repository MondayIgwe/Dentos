@us
Feature: TimekeeperFeature

Verify that Timekeeper Number has been re labeled as 'Timekeeper Index'

Scenario Outline: 010 Validate that the Global Timekeeper process exists
	Given I navigate to the Global Timekeeper process
	Then I add a new record with all the details
		| Timekeeper Number  | TImekeeper Name  | Timekeeper Office  | Timekeeper Title  |
		| <TimekeeperNumber> | <TimeKeeperName> | <TimeKeeperOffice> | <TimeKeeperTitle> |
	And I verify that the timekeeper index exists
	And I submit the form
	When I reopen the record the information is stored correctly

Examples:
	| TimekeeperNumber | TimeKeeperName | TimeKeeperOffice | TimeKeeperTitle |
	| {Auto}+9         | {Auto}+10      | Default          | Partner         |