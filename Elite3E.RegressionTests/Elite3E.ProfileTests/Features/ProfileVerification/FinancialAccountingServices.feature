@profile
Feature: FinancialAccounting
	Verify the roles for Profiles

Scenario: PROFILE: Financial Accounting Services
	When I search the profile "PROFILE: Financial Accounting Services"
	Then the below roles are available
		| Roles                                              |
		| 0:AD:A:Exchange Rate Analyser                      |
		| 0:AD:A:Notifications Analyser                      |
		| 0:AD:G:Common Authorisations                       |
		| 0:AD:G:Deny All                                    |
		| 0:FM:A:General Ledger Analyser                     |
		| 0:FM:A:System Balancing Analyser                   |
		| 0:FM:A:Tax Analyser                                |
		| 0:FM:M:GL Allocation Definition Maintainer         |
		| 0:FM:M:GL Unit Maintainer                          |
		| 0:FM:M:Revaluation Group Maintainer                |
		| 0:FM:M:Revaluation Maintainer                      |
		| 0:FM:P:Auto Reverse Journal Processor              |
		| 0:FM:P:GJ Processor                                |
		| 0:FM:P:GJ Reversal Processor                       |
		| 0:FM:P:GL Allocation Generator                     |
		| 0:FM:P:Recurring Journal Definer                   |
		| 0:FM:P:Recurring Journal Generator                 |
		| 0:FM:P:Revaluation Generator                       |
		| 0:FM:R:GL Processor - Financial Accounting[: Unit] |
		| 0:FM:V:General Ledger Viewer                       |
		| 0:PP:P:Bank Reconciliation Processor               |
		| 0:PP:P:Bank Statement Creator                      |
		| 0:PP:P:Quick Bank Reconciliation Processor         |
		| 0:PP:U:Bank Statement Uploader                     |
		| 0:PP:V:Accounts Payable Viewer                     |
		| 0:PP:V:Bank Reconciliation Viewer                  |
		| 0:WC:P:Cash Receipt Processor                      |
		| 0:WC:P:WIP Provision Processor                     |

Scenario: PROFILE: Financial Accounting Supervision
	When I search the profile "PROFILE: Financial Accounting Supervision"
	Then the below roles are available
		| Roles                                              |
		| 0:AD:A:Exchange Rate Analyser                      |
		| 0:AD:A:Notifications Analyser                      |
		| 0:AD:G:Common Authorisations                       |
		| 0:AD:G:Deny All                                    |
		| 0:FM:A:General Ledger Analyser                     |
		| 0:FM:A:System Balancing Analyser                   |
		| 0:FM:A:Tax Analyser                                |
		| 0:FM:M:GL Allocation Definition Maintainer         |
		| 0:FM:M:GL Project Maintainer                       |
		| 0:FM:M:GL Unit Maintainer                          |
		| 0:FM:M:Revaluation Group Maintainer                |
		| 0:FM:M:Revaluation Maintainer                      |
		| 0:FM:P:Recurring Journal Definer                   |
		| 0:FM:P:Recurring Journal Generator                 |
		| 0:FM:P:Revaluation Generator                       |
		| 0:FM:R:GL Processor - Financial Accounting[: Unit] |
		| 0:FM:R:Period Override - All                       |
		| 0:FM:V:General Ledger Viewer                       |
		| 0:PP:P:Bank Statement Creator                      |
		| 0:PP:P:Quick Bank Reconciliation Processor         |
		| 0:PP:U:Bank Statement Uploader                     |
		| 0:PP:V:Accounts Payable Viewer                     |
		| 0:PP:V:Bank Reconciliation Viewer                  |
		| 0:WC:M:Charge Type Maintainer                      |

Scenario: PROFILE: Partner Accounting Services
	When I search the profile "PROFILE: Partner Accounting Services"
	Then the below roles are available
		| Roles                                          |
		| 0:AD:A:Exchange Rate Analyser                  |
		| 0:AD:A:Notifications Analyser                  |
		| 0:AD:G:Common Authorisations                   |
		| 0:AD:G:Deny All                                |
		| 0:FM:A:General Ledger Analyser                 |
		| 0:FM:A:System Balancing Analyser               |
		| 0:FM:A:Tax Analyser                            |
		| 0:FM:R:GL Processor - Partner Accounts         |
		| 0:PA:M:Fee Earner Maintainer - Partner Points  |
		| 0:PA:M:Fee Earner Maintainer - Partner Credits |


Scenario: PROFILE: Partner Accounting Supervision
	When I search the profile "PROFILE: Partner Accounting Supervision"
	Then the below roles are available
		| Roles                                          |
		| 0:AD:A:Exchange Rate Analyser                  |
		| 0:AD:A:Notifications Analyser                  |
		| 0:AD:G:Common Authorisations                   |
		| 0:AD:G:Deny All                                |
		| 0:FM:A:General Ledger Analyser                 |
		| 0:FM:A:System Balancing Analyser               |
		| 0:FM:A:Tax Analyser                            |
		| 0:FM:R:GL Processor - Partner Accounts         |
		| 0:PA:M:Fee Earner Maintainer - Partner Points  |
		| 0:PA:M:Fee Earner Maintainer - Partner Credits |

