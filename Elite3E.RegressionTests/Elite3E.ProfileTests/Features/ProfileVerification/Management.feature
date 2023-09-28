@profile
Feature: Management
	Verify the roles for Profiles

Scenario: PROFILE: Global Finance Director
	When I search the profile "PROFILE: Global Finance Director"
	Then the below roles are available
		| Roles                        |
		| 0:AD:G:Common Authorisations |
		| 0:AD:G:Deny All              |
		| 0:FM:W:GL Account Approver   |


Scenario: PROFILE: Regional Finance Director
	When I search the profile "PROFILE: Regional Finance Director"
	Then the below roles are available
		| Roles                              |
		| 0:AD:G:Common Authorisations       |
		| 0:AD:G:Deny All                    |
		| 0:FM:W:GJ Approver[: Unit: Office] |



