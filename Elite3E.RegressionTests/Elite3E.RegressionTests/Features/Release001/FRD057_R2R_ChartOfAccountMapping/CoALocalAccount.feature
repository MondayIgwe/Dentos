@release1 @frd057 @COALocalAccount
Feature: COA Local Account

@ft @qa @plmonitor @training @staging  @canada @europe @uk @singapore @us
Scenario Outline: 001 I Add a new CoA Unit
	Given I add the coa legacy
		| Code         | Firm Description | Natural  |
		| CoALegacy_at | CoALegacy_at     | {Auto}+8 |
	And I add a new Region
		| Code         | Description  |
		| CoARegion_at | CoARegion_at |
	And I add a new coa unit
		| Code   | Local Description |
		| CoAUn1 | CoAUn1            |

@ft @qa @plmonitor @training @staging  @canada @europe @uk @singapore @us
Scenario Outline: 002 Add CoA Local account
	Given I add a new CoA Natural Account
		| Natural       | Description   |
		| CoANatural_at | CoANatural_at |
	When I add the coa local account
		| Unit   | Firm Description      |
		| CoAUn1 | CoAUn1FirmDescription |
	Then the data is saved

@ft @training @staging  @canada @europe @uk @singapore @us @qa @plmonitor
Scenario: 003 Add Local Description
	When I add local description
	Then the local description is saved

@CancelProcess @ft @training @staging  @canada @europe @uk @singapore @us @qa @plmonitor
Scenario: 004 Field Name is correct
	Then the local description field is correctly named

@CancelProcess @ft @training @staging  @canada @europe @uk @singapore @us @qa @plmonitor
Scenario Outline: 005 Verify Natural Column is displayed
	When I search the local account
	Then the natural code is displayed

