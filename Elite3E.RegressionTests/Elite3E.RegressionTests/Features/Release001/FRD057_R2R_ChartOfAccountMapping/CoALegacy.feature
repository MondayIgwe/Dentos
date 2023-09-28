@release1 @frd057 @COALegacy
Feature: COA Legacy

@ft @training @staging  @canada @europe @uk @singapore @us @qa @plmonitor
Scenario: 010_Add CoA Legacy
	When I add the coa legacy
		| Code         | Firm Description | Natural  |
		| CoALegacy_at | CoALegacy_at     | {Auto}+8 |
	Then the coa legacy data is saved

@ft @training @staging  @canada @europe @uk @singapore @us @qa @plmonitor
Scenario: 020_Add Local Description to CoA Legacy
	When I add local desrciption to coa legacy
	Then the local description is saved for coa legacy

@ft @training @staging  @canada @europe @uk @singapore @us @qa @plmonitor
Scenario: 030_Field Name is correct
	Then the local description field is correctly named in coa legacy


	
