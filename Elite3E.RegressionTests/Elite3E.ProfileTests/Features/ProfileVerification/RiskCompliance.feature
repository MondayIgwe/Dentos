@profile
Feature: RiskCompliance
	Verify the roles for Profiles

Scenario: PROFILE: Risk Services
	When I search the profile "PROFILE: Risk Services"
	Then the below roles are available
		| Roles                               |
		| 0:AD:G:Common Authorisations        |
		| 0:AD:G:Deny All                     |
		| 0:AD:M:Entity Maintainer            |
		| 0:CML:M:Client Maintainer           |
		| 0:CML:M:Client Maintainer - Address |
		| 0:CML:M:Client Maintainer - Create  |
		| 0:CML:M:Client Maintainer - Risk    |
		| 0:CML:M:Matter Maintainer           |
		| 0:CML:M:Matter Maintainer - Address |
		| 0:CML:M:Matter Maintainer - Create  |
		| 0:CML:M:Matter Maintainer - Risk    |
		| 0:CML:M:Payor Maintainer            |
		| 0:CML:Q:Records Retention Reviewer  |
		| 0:PP:M:Vendor/Payee Maintainer      |


Scenario: PROFILE: Risk Supervision
	When I search the profile "PROFILE: Risk Supervision"
	Then the below roles are available
		| Roles                               |
		| 0:AD:G:Common Authorisations        |
		| 0:AD:G:Deny All                     |
		| 0:AD:M:Entity Maintainer            |
		| 0:CML:M:Client Maintainer           |
		| 0:CML:M:Client Maintainer - Address |
		| 0:CML:M:Client Maintainer - Create  |
		| 0:CML:M:Client Maintainer - Risk    |
		| 0:CML:M:Matter Maintainer           |
		| 0:CML:M:Matter Maintainer - Address |
		| 0:CML:M:Matter Maintainer - Create  |
		| 0:CML:M:Matter Maintainer - Risk    |
		| 0:CML:M:Payor Maintainer            |
		| 0:CML:Q:Records Retention Reviewer  |
		| 0:PP:M:Vendor/Payee Maintainer      |


Scenario: PROFILE: Risk Services (EU)
	When I search the profile "PROFILE: Risk Services (EU)"
	Then the below roles are available
		| Roles                                |
		| 0:AD:G:Common Authorisations         |
		| 0:AD:G:Deny All                      |
		| 0:AD:M:Entity Maintainer             |
		| 0:CML:M:Client Maintainer            |
		| 0:CML:M:Client Maintainer - Address  |
		| 0:CML:M:Client Maintainer - Contacts |
		| 0:CML:M:Client Maintainer - Create   |
		| 0:CML:M:Client Maintainer - Risk     |
		| 0:CML:M:Matter Maintainer            |
		| 0:CML:M:Matter Maintainer - Address  |
		| 0:CML:M:Matter Maintainer - Create   |
		| 0:CML:M:Matter Maintainer - Risk     |
		| 0:CML:M:Payor Maintainer             |
		| 0:CML:Q:Records Retention Reviewer   |
		| 0:PP:M:Vendor/Payee Maintainer       |



Scenario: PROFILE: Risk Supervision (EU)
	When I search the profile "PROFILE: Risk Supervision (EU)"
	Then the below roles are available
		| Roles                                |
		| 0:AD:G:Common Authorisations         |
		| 0:AD:G:Deny All                      |
		| 0:AD:M:Entity Maintainer             |
		| 0:CML:M:Client Maintainer            |
		| 0:CML:M:Client Maintainer - Address  |
		| 0:CML:M:Client Maintainer - Contacts |
		| 0:CML:M:Client Maintainer - Create   |
		| 0:CML:M:Client Maintainer - Risk     |
		| 0:CML:M:Matter Maintainer            |
		| 0:CML:M:Matter Maintainer - Address  |
		| 0:CML:M:Matter Maintainer - Create   |
		| 0:CML:M:Matter Maintainer - Risk     |
		| 0:CML:M:Payor Maintainer             |
		| 0:CML:Q:Records Retention Reviewer   |
		| 0:PP:M:Vendor/Payee Maintainer       |

Scenario: PROFILE: Internal Auditor 
	When I search the profile "PROFILE: Internal Auditor"
	Then the below roles are available
		| Roles                               |
		| 0:AD:A:Audit Data Analyser          |
		| 0:AD:A:Exchange Rate Analyser       |
		| 0:AD:G:Common Authorisations        |
		| 0:AD:G:Deny All                     |
		| 0:CML:A:Records Analyser            |
		| 0:CML:V:Records Viewer              |
		| 0:FM:A:Client Account Analyser      |
		| 0:FM:A:General Ledger Analyser      |
		| 0:FM:A:System Balancing Analyser    |
		| 0:FM:A:Tax Analyser                 |
		| 0:FM:V:General Ledger Viewer        |
		| 0:PP:A:Accounts Payable Analyser    |
		| 0:PP:A:Bank Reconciliation Analyser |
		| 0:PP:V:Accounts Payable Viewer      |
		| 0:PP:V:Bank Reconciliation Viewer   |
		| 0:WC:A:Billing Analyser             |
		| 0:WC:A:Client Account Analyser      |
		| 0:WC:A:Collections Analyser         |
		| 0:WC:V:Billing Viewer               |
		| 0:WC:V:Client Account Viewer        |
		| 0:WC:V:Collections Viewer           |
