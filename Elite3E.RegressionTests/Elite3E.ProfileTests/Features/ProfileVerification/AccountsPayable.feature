
@profile
Feature: AccountsPayable
	Verify the roles for Profiles

Scenario: PROFILE: Accounts Payable Services
	When I search the profile "PROFILE: Accounts Payable Services"
	Then the below roles are available
		| Roles                                         |
		| 0:AD:A:Exchange Rate Analyser                 |
		| 0:AD:G:Common Authorisations                  |
		| 0:AD:G:Deny All                               |
		| 0:PP:A:Accounts Payable Analyser              |
		| 0:PP:A:Bank Reconciliation Analyser           |
		| 0:PP:M:Positive Pay Maintainer                |
		| 0:PP:P:1099 Processor                         |
		| 0:PP:P:AP Voucher Processor                   |
		| 0:PP:P:AP Voucher Request Processor           |
		| 0:PP:P:Cheque Cancellation Processor          |
		| 0:PP:P:Direct Cheque Processor                |
		| 0:PP:P:Payment Proposal Processor             |
		| 0:PP:P:Reclamation Processor                  |
		| 0:PP:R:Bank Account Processor[: Unit: Office] |
		| 0:PP:U:Invoice Uploader                       |
		| 0:PP:V:Accounts Payable Viewer                |
		| 0:PP:V:Bank Reconciliation Viewer             |
		| 0:PP:W:Payment Proposal Generator             |
		


Scenario: PROFILE: Accounts Payable Supervision
	When I search the profile "PROFILE: Accounts Payable Supervision"
	Then the below roles are available
		| Roles                                         |
		| 0:AD:A:Exchange Rate Analyser                 |
		| 0:AD:G:Common Authorisations                  |
		| 0:AD:G:Deny All                               |
		| 0:PP:A:Accounts Payable Analyser              |
		| 0:PP:A:Bank Reconciliation Analyser           |
		| 0:PP:M:Bank Account Office Maintainer         |
		| 0:PP:M:Positive Pay Maintainer                |
		| 0:PP:P:1099 Processor                         |
		| 0:PP:P:AP Voucher Request Approver            |
		| 0:PP:P:AP Voucher Status Processor            |
		| 0:PP:P:Direct Cheque Processor                |
		| 0:PP:P:Payment Processor                      |
		| 0:PP:P:Reclamation Processor                  |
		| 0:PP:R:Bank Account Processor[: Unit: Office] |
		| 0:PP:U:Invoice Uploader                       |
		| 0:PP:V:Accounts Payable Viewer                |
		| 0:PP:V:Bank Reconciliation Viewer             |
		| 0:PP:W:Payment Proposal Approver              |
		| 0:PP:W:Payment Proposal Generator             |


Scenario: PROFILE: AP Master Files Services
	When I search the profile "PROFILE: AP Master Files Services"
	Then the below roles are available
		| Roles                                 |
		| 0:AD:A:Exchange Rate Analyser         |
		| 0:AD:G:Common Authorisations          |
		| 0:AD:G:Deny All                       |
		| 0:AD:M:Entity Maintainer              |
		| 0:PP:A:Accounts Payable Analyser      |
		| 0:PP:M:Bank (Branch) Maintainer       |
		| 0:PP:M:Bank Account Office Maintainer |
		| 0:PP:M:Positive Pay Maintainer        |
		| 0:PP:M:Vendor/Payee Maintainer        |
		| 0:PP:V:Accounts Payable Viewer        |
		| 0:PP:V:Bank Reconciliation Viewer     |



Scenario: PROFILE: AP Master Files Supervision
	When I search the profile "PROFILE: AP Master Files Supervision"
	Then the below roles are available
		| Roles                             |
		| 0:AD:A:Exchange Rate Analyser     |
		| 0:AD:G:Common Authorisations      |
		| 0:AD:G:Deny All                   |
		| 0:PP:A:Accounts Payable Analyser  |
		| 0:PP:M:Vendor/Payee Approver      |
		| 0:PP:V:Accounts Payable Viewer    |
		| 0:PP:V:Bank Reconciliation Viewer |



