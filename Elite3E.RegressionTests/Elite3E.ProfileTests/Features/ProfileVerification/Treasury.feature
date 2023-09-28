@profile
Feature: Treasury
	Verify the roles for Profiles

Scenario: PROFILE: Treasury Services
	When I search the profile "PROFILE: Treasury Services"
	Then the below roles are available
		| Roles                             |
		| 0:AD:A:Exchange Rate Analyser     |
		| 0:AD:G:Common Authorisations      |
		| 0:AD:G:Deny All                   |
		| 0:FM:A:General Ledger Analyser    |
		| 0:PP:V:Accounts Payable Viewer    |
		| 0:PP:V:Bank Reconciliation Viewer |
		| 0:WC:P:Cash Receipt Processor     |
		

Scenario: PROFILE: Treasury Supervision
	When I search the profile "PROFILE: Treasury Supervision"
	Then the below roles are available
		| Roles                             |
		| 0:AD:A:Exchange Rate Analyser     |
		| 0:AD:G:Common Authorisations      |
		| 0:AD:G:Deny All                   |
		| 0:FM:A:General Ledger Analyser    |
		| 0:PP:V:Accounts Payable Viewer    |
		| 0:PP:V:Bank Reconciliation Viewer |
		| 0:WC:P:Cash Receipt Processor     |


