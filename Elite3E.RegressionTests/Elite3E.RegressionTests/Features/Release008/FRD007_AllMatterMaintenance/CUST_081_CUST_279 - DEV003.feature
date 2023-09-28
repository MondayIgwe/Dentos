@release8 @frd007 @ValidateGlobalTimekeeper
Feature: Validate Global Timekeeper
		Verify the Global timekeeper process

@ft @training @staging  @canada @europe @uk @singapore @qa
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
		
@europe
Examples:
	| TimekeeperNumber | TimeKeeperName | TimeKeeperOffice | TimeKeeperTitle       |
	| {Auto}+9         | {Auto}+10      | Default          | Full Interest Partner |
@ft @qa
Examples:
	| TimekeeperNumber | TimeKeeperName | TimeKeeperOffice | TimeKeeperTitle |
	| {Auto}+9         | {Auto}+10      | Default          | Partner         |
@canada
Examples:
	| TimekeeperNumber | TimeKeeperName | TimeKeeperOffice | TimeKeeperTitle     |
	| {Auto}+9         | {Auto}+10      | Default          | CA Partner (Income) |
@uk
Examples:
	| TimekeeperNumber | TimeKeeperName | TimeKeeperOffice | TimeKeeperTitle          |
	| {Auto}+9         | {Auto}+10      | Default          | UK Full Interest Partner |
@training @staging
Examples:
	| TimekeeperNumber | TimeKeeperName | TimeKeeperOffice | TimeKeeperTitle |
	| {Auto}+9         | {Auto}+10      | Default          | SG Partner       |
	@singapore
Examples:
	| TimekeeperNumber | TimeKeeperName | TimeKeeperOffice | TimeKeeperTitle |
	| {Auto}+9         | {Auto}+10      | Default          | Manager      |
