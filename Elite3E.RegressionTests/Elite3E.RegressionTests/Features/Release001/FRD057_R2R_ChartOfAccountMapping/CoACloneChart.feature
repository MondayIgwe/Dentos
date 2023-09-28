@release1 @frd057 @CoACloneChart
Feature: CoACloneChart
	In the COA Unit process, the Clone Chart button will be available where a region has been selected and there are no local accounts or mappings for the unit.

@ft @plmonitor @training @staging  @canada @europe @uk @singapore @us @qa
Scenario Outline: 010 Add CoA Values
	Given I add the coa legacy
		| Code         | Firm Description | Natural  |
		| CoALegacy_at | CoALegacy_at     | {Auto}+8 |
	And I add a new Region
		| Code         | Description  |
		| CoARegion_at | CoARegion_at |
	And I add a new CoA Natural Account
		| Natural       | Description   |
		| CoANatural_at | CoANatural_at |

@ft @plmonitor @training @staging  @canada @europe @uk @singapore @us @qa
#Ensure the CoA Units below similar from the newly created ones to the clone chart step
Scenario Outline: 020 CoA Clone Chart Button
	Given I add a new coa unit
		| Code   | Local Description |
		| CoAUn1 | CoAUn1            |
		| CoAUn2 | CoAUn2            |
	When I add the coa local account
		| Unit   | Firm Description      |
		| CoAUn1 | CoAUn1FirmDescription |
		| CoAUn2 | CoAUn2FirmDescription |
	And I click clone chart
		| Units  |
		| CoAUn1 |
		| CoAUn2 |
	And I submit the form
	Then the unit should be created and modelled
