Feature: PropagationSetUpManagment

Scenario: 10 I want to propagate records
	Given I search for process 'Setup Propagation'
	And I use advanced find to search and check propagation exists
	| Search Column  | Search Operator | Search Value  |
	| Process (Code) | Equals          | <ProcessCode> |
	When I add new Setup Propagation
	| Process       | Role                        | Action |
	| <ProcessCode> | 0:AD:G:System Administrator | Create |
	And I add 3E instances
	| Instance List                                                            | Control Source | Include List |
	| EU DEV, Canada Dev, Singapore Dev, US Dev, UKIME Development Environment | <Source>       | true         |
	Then I want to submit
Examples:
		 | ProcessCode              | ProcessName          | Source              |
		 | CurrencyType_srv_ccc     | CurrencyType         | Global and Regional |
		 | NxUnit                   | Unit                 | Partial Global      |
		 | UnitGroup_srv_ccc        | UnitGroup            | Global              |
		 | GLUnit                   | GLUnit               | Partial Global      |
		 | Office                   | Office               | Partial Global      |
		 | Officegroup_srv_ccc      | OfficeGroup          | Global              |
		 | GLOffice                 | GLOffice             | Partial Global      |
		 | Department_srv_ccc       | Department           | Global              |
		 | DepartmentGroup_srv_ccc  | DepartmentGroup      | Global              |
		 | GLDepartment             | GLDepartment         | Partial Global      |
		 | Section_srv_ccc          | Section              | Global              |
		 | SectionGroup_srv_ccc     | SectionGroup         | Global              |
		 | GLSection                | GLSection            | Partial Global      |
		 | TaxJurisdiction_srv_ccc  | TaxJurisdiction      | Global              |
		 | PhaseList_srv_ccc        | PhaseList            | Global and Regional |
		 | TaskList_srv_ccc         | TaskList             | Global and Regional |
		 | ActivityList_srv_ccc     | ActivityList         | Global and Regional |
		 | PTAGroup_srv_ccc         | PTAGroup             | Global and Regional |
		 | CliAttribute_srv_ccc     | CliAttribute         | Global              |
		 | IndustryCode_srv_ccc     | Industry             | Global              |
		 | IndustryGroup_srv_ccc    | Industry Group       | Global              |
		 | RateClass_srv_ccc        | RateClass            | Global              |
		 | TitleGroup_srv_ccc       | TitleGroup           | Global              |
		 | GLGroup_srv_ccc          | GLGroup              | Global              |
		 | GLNatural                | GLNatural            | Partial Global      |
		 | GLType                   | GLType               | Partial Global      |
		 | GLProject_srv_ccc        | GLProject            | Global and Regional |
		 | NonBillMattType_srv_ccc  | NonBillMatType       | Global              |
		 | Proformastatus_srv_ccc   | Profstatus           | Global              |
		 | GlobalTimekeeper_srv_ccc | GlobalTimekeeper_ccc | Global              |
		 | CurrencyCode_srv_ccc     | Currency             | Global              |


