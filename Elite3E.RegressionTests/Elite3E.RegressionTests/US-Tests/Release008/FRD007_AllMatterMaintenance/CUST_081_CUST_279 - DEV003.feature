@us
Feature: Validate Global Timekeeper
		Verify the Global timekeeper process


Scenario Outline: 010 Validate that the Global Timekeeper process exists
	Given I navigate to the Global Timekeeper process
	Then the process should exist
	

Scenario Outline: 020 Validate that the Global Timekeeper fields exists
	Given I navigate to the Global Timekeeper process
	Then I add a new record with all the details
		| Timekeeper Number  | TImekeeper Name  | Timekeeper Office  | Timekeeper Title  |
		| <TimekeeperNumber> | <TimeKeeperName> | <TimeKeeperOffice> | <TimeKeeperTitle> |
	And I submit the form
	When I reopen the record the information is stored correctly
		
Examples:
	| TimekeeperNumber | TimeKeeperName | TimeKeeperOffice | TimeKeeperTitle |
	| {Auto}+9         | {Auto}+10      | Default          | Accounts Clerk  |
