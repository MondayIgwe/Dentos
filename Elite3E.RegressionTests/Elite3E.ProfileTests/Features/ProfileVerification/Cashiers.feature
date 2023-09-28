@profile
Feature: Cashiers
	Verify the roles for Profiles

Scenario: PROFILE: Cashiers Services
	When I search the profile "PROFILE: Cashiers Services"
	Then the below roles are available
		| Roles                                       |
		| 0:AD:A:Exchange Rate Analyser               |
		| 0:AD:G:Common Authorisations                |
		| 0:AD:G:Deny All                             |
		| 0:FM:A:Client Account Analyser              |
		| 0:FM:P:Intercompany Receipt Processor       |
		| 0:PP:P:Direct Cheque Processor              |
		| 0:PP:V:Accounts Payable Viewer              |
		| 0:PP:V:Bank Reconciliation Viewer           |
		| 0:WC:A:Billing Analyser                     |
		| 0:WC:A:Client Account Analyser              |
		| 0:WC:A:Collections Analyser                 |
		| 0:WC:P:Cash Receipt Processor               |
		| 0:WC:P:Cash Receipt Processor - BOA         |
		| 0:WC:P:Cash Receipt Processor - Unallocated |
		| 0:WC:P:eBilling Invoice Status Processor    |
		| 0:WC:V:Billing Viewer                       |
		| 0:WC:V:Client Account Viewer                |
		| 0:WC:V:Collections Viewer                   |
		| 0:WC:W:Cash Receipt Processor               |

Scenario: PROFILE: Cashiers Supervision
	When I search the profile "PROFILE: Cashiers Supervision"
	Then the below roles are available
		| Roles                                             |
		| 0:AD:A:Exchange Rate Analyser                     |
		| 0:AD:G:Common Authorisations                      |
		| 0:AD:G:Deny All                                   |
		| 0:FM:A:Client Account Analyser                    |
		| 0:FM:N:ICB Receipt Notification Recipient[: Unit] |
		| 0:PP:M:Bank Account Office Maintainer             |
		| 0:PP:V:Accounts Payable Viewer                    |
		| 0:PP:V:Bank Reconciliation Viewer                 |
		| 0:WC:A:Billing Analyser                           |
		| 0:WC:A:Client Account Analyser                    |
		| 0:WC:A:Collections Analyser                       |
		| 0:WC:P:Cash Receipt Processor                     |
		| 0:WC:P:Cash Receipt Processor - BOA               |
		| 0:WC:P:Cash Receipt Processor - Unallocated       |
		| 0:WC:P:Cash Receipt Reversal Processor            |
		| 0:WC:P:eBilling Invoice Status Processor          |
		| 0:WC:V:Billing Viewer                             |
		| 0:WC:V:Client Account Viewer                      |
		| 0:WC:V:Collections Viewer                         |
		| 0:WC:W:Cash Receipt Processor                     |

Scenario: PROFILE: Cashiers Services (CA)
	When I search the profile "PROFILE: Cashiers Services (CA)"
	Then the below roles are available
		| Roles                                       |
		| 0:AD:A:Exchange Rate Analyser               |
		| 0:AD:G:Common Authorisations                |
		| 0:AD:G:Deny All                             |
		| 0:FM:A:Client Account Analyser              |
		| 0:FM:P:Intercompany Receipt Processor       |
		| 0:PP:P:Direct Cheque Processor              |
		| 0:PP:V:Accounts Payable Viewer              |
		| 0:PP:V:Bank Reconciliation Viewer           |
		| 0:WC:A:Billing Analyser                     |
		| 0:WC:A:Client Account Analyser              |
		| 0:WC:A:Collections Analyser                 |
		| 0:WC:P:Cash Receipt Processor               |
		| 0:WC:P:Cash Receipt Processor - BOA         |
		| 0:WC:P:Cash Receipt Processor - Unallocated |
		| 0:WC:P:eBilling Invoice Status Processor    |
		| 0:WC:V:Billing Viewer                       |
		| 0:WC:V:Client Account Viewer                |
		| 0:WC:V:Collections Viewer                   |
		| 0:WC:W:Cash Receipt Processor               |

Scenario: PROFILE: Cashiers Supervision (CA)
	When I search the profile "PROFILE: Cashiers Supervision (CA)"
	Then the below roles are available
		| Roles                                             |
		| 0:AD:A:Exchange Rate Analyser                     |
		| 0:AD:G:Common Authorisations                      |
		| 0:AD:G:Deny All                                   |
		| 0:FM:A:Client Account Analyser                    |
		| 0:FM:N:ICB Receipt Notification Recipient[: Unit] |
		| 0:PP:M:Bank Account Office Maintainer             |
		| 0:PP:V:Accounts Payable Viewer                    |
		| 0:PP:V:Bank Reconciliation Viewer                 |
		| 0:WC:A:Billing Analyser                           |
		| 0:WC:A:Client Account Analyser                    |
		| 0:WC:A:Collections Analyser                       |
		| 0:WC:P:Cash Receipt Processor                     |
		| 0:WC:P:Cash Receipt Processor - BOA               |
		| 0:WC:P:Cash Receipt Processor - Unallocated       |
		| 0:WC:P:Cash Receipt Reversal Processor            |
		| 0:WC:P:eBilling Invoice Status Processor          |
		| 0:WC:V:Billing Viewer                             |
		| 0:WC:V:Client Account Viewer                      |
		| 0:WC:V:Collections Viewer                         |
		| 0:WC:W:Cash Receipt Processor                     |
