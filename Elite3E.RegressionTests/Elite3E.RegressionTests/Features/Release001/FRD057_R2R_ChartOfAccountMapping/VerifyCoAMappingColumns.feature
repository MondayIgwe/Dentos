@release1 @frd057 @VerifyCOAMappingColumns
Feature: Verify COA Mapping Columns
	When querying for records in the COA Local process, the Natural Account Code will be displayed


@ft @plmonitor @training @staging  @canada @europe @uk @singapore @us @qa
Scenario Outline: 001 Add CoA Values
	Given I add the coa legacy
		| Code         | Firm Description | Natural  |
		| CoALegacy_at | CoALegacy_at     | {Auto}+8 |
	And I add a new Region
		| Code         | Description  |
		| CoARegion_at | CoARegion_at |

@ft @plmonitor @training @staging  @canada @europe @uk @singapore @us @qa
Scenario Outline: 002 I Add a new CoA Unit
	Given I add a new coa unit
		| Code   | Local Description |
		| CoAUn1 | CoAUn1            |

Scenario Outline: 003 Add CoA Mapping
	Given I add a CoA mapping
	Then the correct columns are dispalyed in mapping detail
		| Grid Columns |
		| <Column1>    |
		| <Column2>    |
		| <Column3>    |
		| <Column4>    |
		| <Column5>    |
		| <Column6>    |
		| <Column7>    |
		| <Column8>    |
		| <Column9>    |
		| <Column10>   |
		| <Column11>   |
		| <Column12>   |

@ft @plmonitor
Examples:
	| Column1        | Column2                         | Column3                          | Column4         | Column5                     | Column6       | Column7                   | Column8                         | Column9 | Column10          | Column11                           | Column12                            |
	| Legacy Account | Legacy Account Firm Description | Legacy Account Local Description | Natural Account | Natural Account Description | Local Account | Local Account Description | Local Account Local Description | Actions | Statutory Account | Statutory Account Firm Description | Statutory Account Local Description |
@training @staging  @canada @europe @uk @singapore @us
Examples:
	| Column1        | Column2                         | Column3                          | Column4         | Column5                     | Column6       | Column7                   | Column8                         | Column9 | Column10          | Column11                           | Column12                            |
	| Legacy Account | Legacy Account Firm Description | Legacy Account Local Description | Natural Account | Natural Account Description | Local Account | Local Account Description | Local Account Local Description | Actions | Statutory Account | Statutory Account Firm Description | Statutory Account Local Description |
@qa
Examples:
	| Column1        | Column2                         | Column3                          | Column4         | Column5                     | Column6       | Column7                   | Column8                         | Column9 | Column10          | Column11                           | Column12                            |
	| Legacy Account | Legacy Account Firm Description | Legacy Account Local Description | Natural Account | Natural Account Description | Local Account | Local Account Description | Local Account Local Description | Actions | Statutory Account | Statutory Account Firm Description | Statutory Account Local Description |
	
	
	
