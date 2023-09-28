@profile
Feature: ClientAccounting
	Verify the roles for Profiles

Scenario: PROFILE: Client Accounting Services
	When I search the profile "PROFILE: Client Accounting Services"
	Then the below roles are available
		| Roles                                                    |
		| 0:AD:A:Exchange Rate Analyser                            |
		| 0:AD:G:Common Authorisations                             |
		| 0:AD:G:Deny All                                          |
		| 0:CML:M:Client Maintainer                                |
		| 0:CML:M:Client Maintainer - Close                        |
		| 0:CML:M:Client Maintainer - Contacts                     |
		| 0:CML:M:Matter Maintainer                                |
		| 0:CML:M:Matter Maintainer - Close                        |
		| 0:CML:M:Matter Maintainer - Finance                      |
		| 0:FM:A:Client Account Analyser                           |
		| 0:PP:V:Accounts Payable Viewer                           |
		| 0:WC:A:Billing Analyser                                  |
		| 0:WC:A:Client Account Analyser                           |
		| 0:WC:A:Collections Analyser                              |
		| 0:WC:P:Client Account Adjustment Processor               |
		| 0:WC:P:Client Account Cheque Processor                   |
		| 0:WC:P:Client Account Disbursement Processor             |
		| 0:WC:P:Client Account Payment Processor                  |
		| 0:WC:P:Client Account Receipt Processor                  |
		| 0:WC:P:Client Account Receipt Processor - Days to Clear  |
		| 0:WC:P:Client Account Transfer Processor                 |
		| 0:WC:P:Client Account Transfer Processor - Days to Clear |
		| 0:WC:R:Client Account Processor[: Unit: Office]          |
		| 0:WC:V:Billing Viewer                                    |
		| 0:WC:V:Client Account Viewer                             |
		| 0:WC:V:Collections Viewer                                |

Scenario: PROFILE: Client Accounting Supervision
	When I search the profile "PROFILE: Client Accounting Supervision"
	Then the below roles are available
		| Roles                                                 |
		| 0:AD:A:Exchange Rate Analyser                         |
		| 0:AD:G:Common Authorisations                          |
		| 0:AD:G:Deny All                                       |
		| 0:CML:M:Client Maintainer                             |
		| 0:CML:M:Client Maintainer - Close                     |
		| 0:CML:M:Client Maintainer - Contacts                  |
		| 0:CML:M:Matter Maintainer                             |
		| 0:CML:M:Matter Maintainer - Close                     |
		| 0:CML:M:Matter Maintainer - Finance                   |
		| 0:FM:A:Client Account Analyser                        |
		| 0:PP:V:Accounts Payable Viewer                        |
		| 0:WC:A:Billing Analyser                               |
		| 0:WC:A:Client Account Analyser                        |
		| 0:WC:A:Collections Analyser                           |
		| 0:WC:M:Client Bank Account Maintainer                 |
		| 0:WC:P:Client Account Adjustment Processor            |
		| 0:WC:P:Client Account Cheque Void Processor           |
		| 0:WC:P:Client Account Disbursement Reversal Processor |
		| 0:WC:P:Client Account Interest Calculation Processor  |
		| 0:WC:P:Client Account Receipt Reversal Processor      |
		| 0:WC:P:Client Account Transfer Reversal Processor     |
		| 0:WC:R:Client Account Processor[: Unit: Office]       |
		| 0:WC:V:Billing Viewer                                 |
		| 0:WC:V:Client Account Viewer                          |
		| 0:WC:V:Collections Viewer                             |

Scenario: PROFILE: Client Accounting Services (EU)
	When I search the profile "PROFILE: Client Accounting Services (EU)"
	Then the below roles are available
		| Roles                                            |
		| 0:AD:A:Exchange Rate Analyser                    |
		| 0:AD:G:Common Authorisations                     |
		| 0:AD:G:Deny All                                  |
		| 0:CML:M:Client Maintainer                        |
		| 0:CML:M:Client Maintainer - Close                |
		| 0:CML:M:Matter Maintainer                        |
		| 0:CML:M:Matter Maintainer - Close                |
		| 0:CML:M:Matter Maintainer - Finance              |
		| 0:FM:A:Client Account Analyser                   |
		| 0:PP:V:Accounts Payable Viewer                   |
		| 0:WC:A:Billing Analyser                          |
		| 0:WC:A:Client Account Analyser                   |
		| 0:WC:A:Collections Analyser                      |
		| 0:WC:P:Client Account Adjustment Processor       |
		| 0:WC:P:Client Account Cheque Processor           |
		| 0:WC:P:Client Account Disbursement Processor     |
		| 0:WC:P:Client Account Payment Processor          |
		| 0:WC:P:Client Account Receipt Processor          |
		| 0:WC:P:Client Account Transfer Processor         |
		| 0:WC:R:Client Account Processor[: Unit: Office]  |
		| 0:WC:V:Billing Viewer                            |
		| 0:WC:V:Client Account Viewer                     |
		| 0:WC:V:Collections Viewer                        |

Scenario: PROFILE: Client Accounting Supervision (EU)
	When I search the profile "PROFILE: Client Accounting Supervision (EU)"
	Then the below roles are available
		| Roles                                                    |
		| 0:AD:A:Exchange Rate Analyser                            |
		| 0:AD:G:Common Authorisations                             |
		| 0:AD:G:Deny All                                          |
		| 0:CML:M:Client Maintainer                                |
		| 0:CML:M:Client Maintainer - Close                        |
		| 0:CML:M:Matter Maintainer                                |
		| 0:CML:M:Matter Maintainer - Close                        |
		| 0:CML:M:Matter Maintainer - Finance                      |
		| 0:FM:A:Client Account Analyser                           |
		| 0:WC:A:Billing Analyser                                  |
		| 0:WC:A:Client Account Analyser                           |
		| 0:WC:A:Collections Analyser                              |
		| 0:WC:M:Client Bank Account Maintainer                    |
		| 0:WC:P:Client Account Adjustment Processor               |
		| 0:WC:P:Client Account Cheque Void Processor              |
		| 0:WC:P:Client Account Disbursement Reversal Processor    |
		| 0:WC:P:Client Account Interest Calculation Processor     |
		| 0:WC:P:Client Account Receipt Processor - Days to Clear  |
		| 0:WC:P:Client Account Receipt Reversal Processor         |
		| 0:WC:P:Client Account Transfer Processor - Days to Clear |
		| 0:WC:P:Client Account Transfer Reversal Processor        |
		| 0:WC:R:Client Account Processor[: Unit: Office]          |
		| 0:WC:V:Billing Viewer                                    |
		| 0:WC:V:Client Account Viewer                             |
		| 0:WC:V:Collections Viewer                                |


