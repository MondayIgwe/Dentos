@profile
Feature: External
	Verify the roles for Profiles

Scenario: PROFILE: Third Party Accountant
	When I search the profile "PROFILE: Third Party Accountant"
	Then the below roles are available
		| Roles                        |
		| 0:AD:G:Common Authorisations |
		| 0:AD:G:Deny All              |


Scenario: PROFILE: External Auditor
	When I search the profile "PROFILE: External Auditor"
	Then the below roles are available
		| Roles                        |
		| 0:AD:G:Common Authorisations |
		| 0:AD:G:Deny All              |



