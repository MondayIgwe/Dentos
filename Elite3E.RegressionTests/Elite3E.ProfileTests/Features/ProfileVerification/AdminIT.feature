@profile
Feature: AdminIT
	Verify the roles for Profiles

Scenario: PROFILE: Report Development
	When I search the profile "PROFILE: Report Development"
	Then the below roles are available
		| Roles                                         |
		| 0:AD:G:Common Authorisations                  |
		| 0:AD:G:Deny All                               |
		| 0:CML:M:Records Reports Maintainer            |
		| 0:FM:M:General Ledger Reports Maintainer      |
		| 0:PP:M:Accounts Payable Reports Maintainer    |
		| 0:PP:M:Bank Reconciliation Reports Maintainer |
		| 0:WC:M:Billing Reports Maintainer             |
		| 0:WC:M:Client Account Reports Maintainer      |
		| 0:WC:M:Collections Reports Maintainer         |


Scenario: PROFILE: User Administration
	When I search the profile "PROFILE: User Administration"
	Then the below roles are available
		| Roles                        |
		| 0:AD:G:Common Authorisations |
		| 0:AD:G:Deny All              |
		| 0:AD:M:User Maintainer       |


Scenario: PROFILE: System Development
	When I search the profile "PROFILE: System Development"
	Then the below roles are available
		| Roles                        |
		| 0:AD:G:Common Authorisations |
		| 0:AD:G:Deny All              |



Scenario: PROFILE: HR Administration
	When I search the profile "PROFILE: HR Administration"
	Then the below roles are available
		| Roles                                        |
		| 0:AD:G:Common Authorisations                 |
		| 0:AD:G:Deny All                              |
		| 0:PA:M:Fee Earner Maintainer                 |
		| 0:PA:M:Fee Earner Maintainer - Global Change |
		| 0:PA:M:Work Calendar Maintainer              |
		| 0:WC:M:Fee Earner Rates Maintainer           |


Scenario: PROFILE: Revenue Administration
	When I search the profile "PROFILE: Revenue Administration"
	Then the below roles are available
		| Roles                                     |
		| 0:AD:G:Common Authorisations              |
		| 0:AD:G:Deny All                           |
		| 0:CML:M:Matter Maintainer - Global Change |
		| 0:CML:M:Client Maintainer - Global Change |


Scenario: PROFILE: Management
	When I search the profile "PROFILE: Management"
	Then the below roles are available
		| Roles                        |
		| 0:AD:G:Common Authorisations |
		| 0:AD:G:Deny All              |


Scenario: PROFILE: System Administration
	When I search the profile "PROFILE: System Administration"
	Then the below roles are available
		| Roles                                                   |
		| 0:AD:A:Audit Data Analyser                              |
		| 0:AD:G:Common Authorisations                            |
		| 0:AD:G:System Administrator                             |
		| 0:AD:M:Entity Maintainer                                |
		| 0:AD:M:Language Maintainer                              |
		| 0:AD:M:Role Maintainer                                  |
		| 0:AD:M:Row Level Security Access Maintainer             |
		| 0:AD:M:Workflow Delegation Maintainer                   |
		| 0:AD:P:Build BillSum Processor                          |
		| 0:AD:P:Post Queue Processor                             |
		| 0:AD:P:Posting Results Processor                        |
		| 0:AD:S:Active Directory Load Role                       |
		| 0:AD:S:Bank Statement Load Role                         |
		| 0:AD:S:Client Matter Load Role                          |
		| 0:AD:S:Cost Load Role                                   |
		| 0:AD:S:Credit Load Role                                 |
		| 0:AD:S:Currency Load Role                               |
		| 0:AD:S:Entity Load Role                                 |
		| 0:AD:S:General Journal Load Role                        |
		| 0:AD:S:GL Load Role                                     |
		| 0:AD:S:Rate Type Load Role                              |
		| 0:AD:S:Records Inventory Load Role                      |
		| 0:AD:S:Related Party Load Role                          |
		| 0:AD:S:Time Load Role                                   |
		| 0:AD:S:Voucher Load Role                                |
		| 0:FM:M:Financial Controls Maintainer                    |
		| 0:FM:M:Financial Period Maintainer                      |
		| 0:FM:M:GL Account Maintainer                            |
		| 0:FM:M:GL Book Maintainer                               |
		| 0:FM:M:GL Local Chart Maintainer                        |
		| 0:FM:M:GL Natural Maintainer                            |
		| 0:FM:M:GL Type Maintainer                               |
		| 0:FM:M:GL Unit Maintainer                               |
		| 0:FM:M:Tax Maintainer                                   |
		| 0:FM:P:Posting Results Processor                        |
		| 0:PP:M:Bank (Branch) Maintainer                         |
		| 0:PP:M:Bank Account Office Maintainer                   |
		| 0:PP:M:Notification Maintainer                          |
		| 0:PP:M:Vendor/Payee Maintainer                          |
		| 0:PP:M:Vendor/Payee Maintainer - Bank Details - Germany |
		| 0:WC:M:Collection Control Panel Maintainer              |
		| 0:WC:M:Collection Item Scheduler                        |
		| 0:WC:O:Purge Processor                                  |
		| 0:WC:U:WIP Uploader                                     |


Scenario: PROFILE: Business Analysis Services
	When I search the profile "PROFILE: Business Analysis Services"
	Then the are no roles available for this profile

Scenario: PROFILE: Support Analysis Services
	When I search the profile "PROFILE: Support Analysis Services"
	Then the below roles are available
		| Roles                        |
		| 0:AD:G:Common Authorisations |
		| 0:AD:G:Deny All              |
		| 0:AD:M:Currency Maintainer   |



