@profile
Feature: Collections
	Verify the roles for Profiles

Scenario: PROFILE: Collections Services
	When I search the profile "PROFILE: Collections Services"
	Then the below roles are available
		| Roles                                    |
		| 0:AD:A:Exchange Rate Analyser            |
		| 0:AD:G:Common Authorisations             |
		| 0:AD:G:Deny All                          |
		| 0:CML:M:Client Maintainer                |
		| 0:CML:M:Client Maintainer - Close        |
		| 0:CML:M:Client Maintainer - Contacts     |
		| 0:CML:M:Matter Maintainer                |
		| 0:CML:M:Matter Maintainer - Close        |
		| 0:FM:A:Client Account Analyser           |
		| 0:PP:V:Accounts Payable Viewer           |
		| 0:WC:A:Billing Analyser                  |
		| 0:WC:A:Client Account Analyser           |
		| 0:WC:A:Collections Analyser              |
		| 0:WC:P:Cash Receipt Reversal Processor   |
		| 0:WC:P:Collection Item Processor         |
		| 0:WC:P:eBilling Invoice Status Processor |
		| 0:WC:P:Write Off Processor               |
		| 0:WC:V:Billing Viewer                    |
		| 0:WC:V:Client Account Viewer             |
		| 0:WC:V:Collections Viewer                |

Scenario: PROFILE: Collections Supervision
	When I search the profile "PROFILE: Collections Supervision"
	Then the below roles are available
		| Roles                                    |
		| 0:AD:A:Exchange Rate Analyser            |
		| 0:AD:G:Common Authorisations             |
		| 0:AD:G:Deny All                          |
		| 0:CML:M:Client Maintainer                |
		| 0:CML:M:Client Maintainer - Close        |
		| 0:CML:M:Client Maintainer - Contacts     |
		| 0:CML:M:Client Maintainer - Credit Terms |
		| 0:CML:M:Matter Maintainer                |
		| 0:CML:M:Matter Maintainer - Close        |
		| 0:FM:A:Client Account Analyser           |
		| 0:PP:V:Accounts Payable Viewer           |
		| 0:WC:A:Billing Analyser                  |
		| 0:WC:A:Client Account Analyser           |
		| 0:WC:A:Collections Analyser              |
		| 0:WC:M:Collection Group Link Maintainer  |
		| 0:WC:P:Cash Receipt Reversal Processor   |
		| 0:WC:P:Collection Item Processor         |
		| 0:WC:P:eBilling Invoice Status Processor |
		| 0:WC:P:Write Off Processor               |
		| 0:WC:V:Billing Viewer                    |
		| 0:WC:V:Client Account Viewer             |
		| 0:WC:V:Collections Viewer                |


Scenario: PROFILE: Collections Services (EU)
	When I search the profile "PROFILE: Collections Services (EU)"
	Then the below roles are available
		| Roles                                    |
		| 0:AD:A:Exchange Rate Analyser            |
		| 0:AD:G:Common Authorisations             |
		| 0:AD:G:Deny All                          |
		| 0:CML:M:Client Maintainer                |
		| 0:CML:M:Client Maintainer - Close        |
		| 0:CML:M:Matter Maintainer                |
		| 0:CML:M:Matter Maintainer - Close        |
		| 0:FM:A:Client Account Analyser           |
		| 0:PP:V:Accounts Payable Viewer           |
		| 0:WC:A:Billing Analyser                  |
		| 0:WC:A:Client Account Analyser           |
		| 0:WC:A:Collections Analyser              |
		| 0:WC:P:Cash Receipt Reversal Processor   |
		| 0:WC:P:Collection Item Processor         |
		| 0:WC:P:eBilling Invoice Status Processor |
		| 0:WC:P:Write Off Processor               |
		| 0:WC:V:Billing Viewer                    |
		| 0:WC:V:Client Account Viewer             |
		| 0:WC:V:Collections Viewer                |

Scenario: PROFILE: Collections Supervision (EU)
	When I search the profile "PROFILE: Collections Supervision (EU)"
	Then the below roles are available
		| Roles                                    |
		| 0:AD:A:Exchange Rate Analyser            |
		| 0:AD:G:Common Authorisations             |
		| 0:AD:G:Deny All                          |
		| 0:CML:M:Client Maintainer                |
		| 0:CML:M:Client Maintainer - Close        |
		| 0:CML:M:Client Maintainer - Credit Terms |
		| 0:CML:M:Matter Maintainer                |
		| 0:CML:M:Matter Maintainer - Close        |
		| 0:FM:A:Client Account Analyser           |
		| 0:PP:V:Accounts Payable Viewer           |
		| 0:WC:A:Billing Analyser                  |
		| 0:WC:A:Client Account Analyser           |
		| 0:WC:A:Collections Analyser              |
		| 0:WC:M:Collection Group Link Maintainer  |
		| 0:WC:P:Cash Receipt Reversal Processor   |
		| 0:WC:P:Collection Item Processor         |
		| 0:WC:P:eBilling Invoice Status Processor |
		| 0:WC:P:Write Off Processor               |
		| 0:WC:V:Billing Viewer                    |
		| 0:WC:V:Client Account Viewer             |
		| 0:WC:V:Collections Viewer                |



