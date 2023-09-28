@profile
Feature: Records
	Verify the roles for Profiles

Scenario: PROFILE: Records Services
	When I search the profile "PROFILE: Records Services"
	Then the below roles are available
		| Roles                                          |
		| 0:AD:G:Common Authorisations                   |
		| 0:AD:G:Deny All                                |
		| 0:CML:A:Records Analyser                       |
		| 0:CML:M:Records Container Maintainer           |
		| 0:CML:M:Records Disposition Project Maintainer |
		| 0:CML:M:Records Maintainer                     |
		| 0:CML:M:Records Reports Maintainer             |
		| 0:CML:P:Records Processor                      |
		| 0:CML:V:Records Viewer                         |


Scenario: PROFILE: Records Supervision
	When I search the profile "PROFILE: Records Supervision"
	Then the below roles are available
		| Roles                                          |
		| 0:AD:G:Common Authorisations                   |
		| 0:AD:G:Deny All                                |
		| 0:CML:A:Records Analyser                       |
		| 0:CML:M:Records Container Maintainer           |
		| 0:CML:M:Records Disposition Project Maintainer |
		| 0:CML:M:Records Maintainer                     |
		| 0:CML:M:Records Reports Maintainer             |
		| 0:CML:P:Records Processor                      |
		| 0:CML:V:Records Viewer                         |


