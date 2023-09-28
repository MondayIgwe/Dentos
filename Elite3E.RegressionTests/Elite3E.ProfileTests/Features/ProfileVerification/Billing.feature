@profile
Feature: Billing
	Verify the roles for Profiles

Scenario: PROFILE: Billing Services
	When I search the profile "PROFILE: Billing Services"
	Then the below roles are available
		| Roles                                              |
		| 0:AD:A:Exchange Rate Analyser                      |
		| 0:AD:G:Common Authorisations                       |
		| 0:AD:G:Deny All                                    |
		| 0:CML:M:Client Maintainer                          |
		| 0:CML:M:Client Maintainer - Close                  |
		| 0:CML:M:Client Maintainer - Contacts               |
		| 0:CML:M:Client Maintainer - Finance                |
		| 0:CML:M:Matter Maintainer                          |
		| 0:CML:M:Matter Maintainer - Close                  |
		| 0:CML:M:Matter Maintainer - Finance                |
		| 0:FM:A:Client Account Analyser                     |
		| 0:FM:P:Intercompany Bill Processor                 |
		| 0:PP:V:Accounts Payable Viewer                     |
		| 0:WC:A:Billing Analyser                            |
		| 0:WC:A:Client Account Analyser                     |
		| 0:WC:A:Collections Analyser                        |
		| 0:WC:M:Billing Group Maintainer                    |
		| 0:WC:P:Bill Modification Processor                 |
		| 0:WC:P:Charge Entry Processor                      |
		| 0:WC:P:Charge Modification Processor               |
		| 0:WC:P:Client Account Bill Processor               |
		| 0:WC:P:Client Account Disbursement Processor       |
		| 0:WC:P:Client Account Transfer Processor           |
		| 0:WC:P:Cost Entry Processor                        |
		| 0:WC:P:Cost Modification Processor                 |
		| 0:WC:P:Flat Fee Close Processor                    |
		| 0:WC:P:Group Billing Processor                     |
		| 0:WC:P:Matter Split Reversal Processor             |
		| 0:WC:P:Matter Splits Processor                     |
		| 0:WC:P:Proforma Generation Processor               |
		| 0:WC:P:Proforma Processor                          |
		| 0:WC:P:Proforma Processor - Finance                |
		| 0:WC:P:Proforma Processor - Apply BOA              |
		| 0:WC:P:Proforma Processor - Apply Trust            |
		| 0:WC:P:Proforma Processor - Apply Unallocated      |
		| 0:WC:P:Quick Billing Proforma Processor            |
		| 0:WC:U:WIP Uploader                                |
		| 0:WC:V:Billing Viewer                              |
		| 0:WC:V:Client Account Viewer                       |
		| 0:WC:V:Collections Viewer                          |
		| 0:WC:W:Client Account Processor[: Unit: Office]    |
		| 3EProformaEditorRole                               |
		| 0:WC:W:Group Billing Processor[: Unit: Office]     |
		| 0:WC:W:Proforma Billing Team[: Unit: Office]       |
		| 0:WC:W:Proforma Finance Support[: Unit: Office]    |
		| 0:WC:W:Proforma Local Finance Team[: Unit: Office] |


Scenario: PROFILE: Billing Supervision
	When I search the profile "PROFILE: Billing Supervision"
	Then the below roles are available
		| Roles                                                    |
		| 0:AD:A:Exchange Rate Analyser                            |
		| 0:AD:G:Common Authorisations                             |
		| 0:AD:G:Deny All                                          |
		| 0:CML:M:Client Maintainer                                |
		| 0:CML:M:Client Maintainer - Close                        |
		| 0:CML:M:Client Maintainer - Contacts                     |
		| 0:CML:M:Client Maintainer - Finance                      |
		| 0:CML:M:Matter Maintainer                                |
		| 0:CML:M:Matter Maintainer - Close                        |
		| 0:CML:M:Matter Maintainer - Finance                      |
		| 0:FM:A:Client Account Analyser                           |
		| 0:PP:V:Accounts Payable Viewer                           |
		| 0:WC:A:Billing Analyser                                  |
		| 0:WC:A:Client Account Analyser                           |
		| 0:WC:A:Collections Analyser                              |
		| 0:WC:M:Billing Group Maintainer                          |
		| 0:WC:P:Bill Modification Processor                       |
		| 0:WC:P:Billing Statement Processor                       |
		| 0:WC:P:Cash Receipt Processor                            |
		| 0:WC:P:Charge Entry Processor                            |
		| 0:WC:P:Client Account Bill Processor                     |
		| 0:WC:P:Client Account Disbursement Processor             |
		| 0:WC:P:Client Account Transfer Processor                 |
		| 0:WC:P:Credit Note Processor                             |
		| 0:WC:P:Flat Fee Close Processor                          |
		| 0:WC:P:Group Billing Processor                           |
		| 0:WC:P:Matter Split Reversal Processor                   |
		| 0:WC:P:Matter Splits Processor                           |
		| 0:WC:P:Proforma Generation Processor                     |
		| 0:WC:P:Proforma Processor                                |
		| 0:WC:P:Proforma Processor - Finance                      |
		| 0:WC:P:Proforma Processor - Apply BOA                    |
		| 0:WC:P:Proforma Processor - Apply Trust                  |
		| 0:WC:P:Proforma Processor - Apply Unallocated            |
		| 0:WC:P:Quick Billing Proforma Processor                  |
		| 0:WC:U:WIP Uploader                                      |
		| 0:WC:V:Billing Viewer                                    |
		| 0:WC:V:Client Account Viewer                             |
		| 0:WC:V:Collections Viewer                                |
		| 0:WC:W:Client Account Processor[: Unit: Office]          |
		| 3EProformaEditorRole                                     |
		| 0:WC:W:Group Billing Processor[: Unit: Office]           |
		| 0:WC:W:Proforma Billing Team[: Unit: Office]             |
		| 0:WC:W:Proforma Finance Support[: Unit: Office]          |
		| 0:WC:W:Proforma Local Finance Team[: Unit: Office]       |
		| 0:WC:W:Write Off Approver L1[: Unit: Office: Department] |
		| 0:WC:W:Write Off Approver L2[: Unit: Office: Department] |
		| 0:WC:W:Write Off Approver L3[: Unit: Office: Department] |


Scenario: PROFILE: Billing Services (EU)
	When I search the profile "PROFILE: Billing Services (EU)"
	Then the below roles are available
		| Roles                                              |
		| 0:AD:A:Exchange Rate Analyser                      |
		| 0:AD:G:Common Authorisations                       |
		| 0:AD:G:Deny All                                    |
		| 0:CML:M:Client Maintainer                          |
		| 0:CML:M:Client Maintainer - Close                  |
		| 0:CML:M:Client Maintainer - Finance                |
		| 0:CML:M:Matter Maintainer                          |
		| 0:CML:M:Matter Maintainer - Close                  |
		| 0:CML:M:Matter Maintainer - Finance                |
		| 0:FM:A:Client Account Analyser                     |
		| 0:FM:P:Intercompany Bill Processor                 |
		| 0:PP:V:Accounts Payable Viewer                     |
		| 0:WC:A:Billing Analyser                            |
		| 0:WC:A:Client Account Analyser                     |
		| 0:WC:A:Collections Analyser                        |
		| 0:WC:M:Billing Group Maintainer                    |
		| 0:WC:P:Bill Modification Processor                 |
		| 0:WC:P:Charge Entry Processor                      |
		| 0:WC:P:Charge Modification Processor               |
		| 0:WC:P:Client Account Bill Processor               |
		| 0:WC:P:Client Account Disbursement Processor       |
		| 0:WC:P:Client Account Transfer Processor           |
		| 0:WC:P:Cost Entry Processor                        |
		| 0:WC:P:Cost Modification Processor                 |
		| 0:WC:P:Flat Fee Close Processor                    |
		| 0:WC:P:Group Billing Processor                     |
		| 0:WC:P:Matter Split Reversal Processor             |
		| 0:WC:P:Matter Splits Processor                     |
		| 0:WC:P:Proforma Generation Processor               |
		| 0:WC:P:Proforma Processor                          |
		| 0:WC:P:Proforma Processor - Finance                |
		| 0:WC:P:Proforma Processor - Apply BOA              |
		| 0:WC:P:Proforma Processor - Apply Trust            |
		| 0:WC:P:Proforma Processor - Apply Unallocated      |
		| 0:WC:P:Quick Billing Proforma Processor            |
		| 0:WC:U:WIP Uploader                                |
		| 0:WC:V:Billing Viewer                              |
		| 0:WC:V:Client Account Viewer                       |
		| 0:WC:V:Collections Viewer                          |
		| 0:WC:W:Client Account Processor[: Unit: Office]    |
		| 3EProformaEditorRole                               |
		| 0:WC:W:Group Billing Processor[: Unit: Office]     |
		| 0:WC:W:Proforma Billing Team[: Unit: Office]       |
		| 0:WC:W:Proforma Finance Support[: Unit: Office]    |
		| 0:WC:W:Proforma Local Finance Team[: Unit: Office] |

Scenario: PROFILE: Billing Supervision (EU)
	When I search the profile "PROFILE: Billing Supervision (EU)"
	Then the below roles are available
		| Roles                                                    |
		| 0:AD:A:Exchange Rate Analyser                            |
		| 0:AD:G:Common Authorisations                             |
		| 0:AD:G:Deny All                                          |
		| 0:CML:M:Client Maintainer                                |
		| 0:CML:M:Client Maintainer - Close                        |
		| 0:CML:M:Client Maintainer - Finance                      |
		| 0:CML:M:Matter Maintainer                                |
		| 0:CML:M:Matter Maintainer - Close                        |
		| 0:CML:M:Matter Maintainer - Finance                      |
		| 0:FM:A:Client Account Analyser                           |
		| 0:PP:V:Accounts Payable Viewer                           |
		| 0:WC:A:Billing Analyser                                  |
		| 0:WC:A:Client Account Analyser                           |
		| 0:WC:A:Collections Analyser                              |
		| 0:WC:M:Billing Group Maintainer                          |
		| 0:WC:P:Bill Modification Processor                       |
		| 0:WC:P:Billing Statement Processor                       |
		| 0:WC:P:Cash Receipt Processor                            |
		| 0:WC:P:Charge Entry Processor                            |
		| 0:WC:P:Client Account Bill Processor                     |
		| 0:WC:P:Client Account Disbursement Processor             |
		| 0:WC:P:Client Account Transfer Processor                 |
		| 0:WC:P:Credit Note Processor                             |
		| 0:WC:P:Flat Fee Close Processor                          |
		| 0:WC:P:Group Billing Processor                           |
		| 0:WC:P:Matter Split Reversal Processor                   |
		| 0:WC:P:Matter Splits Processor                           |
		| 0:WC:P:Proforma Generation Processor                     |
		| 0:WC:P:Proforma Processor                                |
		| 0:WC:P:Proforma Processor - Finance                      |
		| 0:WC:P:Proforma Processor - Apply BOA                    |
		| 0:WC:P:Proforma Processor - Apply Trust                  |
		| 0:WC:P:Proforma Processor - Apply Unallocated            |
		| 0:WC:P:Quick Billing Proforma Processor                  |
		| 0:WC:U:WIP Uploader                                      |
		| 0:WC:V:Billing Viewer                                    |
		| 0:WC:V:Client Account Viewer                             |
		| 0:WC:V:Collections Viewer                                |
		| 0:WC:W:Client Account Processor[: Unit: Office]          |
		| 3EProformaEditorRole                                     |
		| 0:WC:W:Group Billing Processor[: Unit: Office]           |
		| 0:WC:W:Proforma Billing Team[: Unit: Office]             |
		| 0:WC:W:Proforma Finance Support[: Unit: Office]          |
		| 0:WC:W:Proforma Local Finance Team[: Unit: Office]       |
		| 0:WC:W:Write Off Approver L1[: Unit: Office: Department] |
		| 0:WC:W:Write Off Approver L2[: Unit: Office: Department] |
		| 0:WC:W:Write Off Approver L3[: Unit: Office: Department] |
		
Scenario: PROFILE: Billing Coordinator
	When I search the profile "PROFILE: Billing Coordinator"
	Then the below roles are available
		| Roles                                           |
		| 0:AD:A:Exchange Rate Analyser                   |
		| 0:AD:G:Common Authorisations                    |
		| 0:AD:G:Deny All                                 |
		| 0:CML:M:Client Maintainer                       |
		| 0:CML:M:Client Maintainer - Close               |
		| 0:CML:M:Client Maintainer - Finance             |
		| 0:CML:M:Matter Maintainer                       |
		| 0:CML:M:Matter Maintainer - Close               |
		| 0:CML:M:Matter Maintainer - Finance             |
		| 0:FM:A:Client Account Analyser                  |
		| 0:PP:V:Accounts Payable Viewer                  |
		| 0:WC:A:Billing Analyser                         |
		| 0:WC:A:Client Account Analyser                  |
		| 0:WC:A:Collections Analyser                     |
		| 0:WC:P:Bill Modification Processor              |
		| 0:WC:P:Billing Statement Processor              |
		| 0:WC:P:Cash Receipt Processor                   |
		| 0:WC:P:Charge Entry Processor                   |
		| 0:WC:P:Charge Modification Processor            |
		| 0:WC:P:Client Account Bill Processor            |
		| 0:WC:P:Client Account Disbursement Processor    |
		| 0:WC:P:Client Account Transfer Processor        |
		| 0:WC:P:Cost Entry Processor                     |
		| 0:WC:P:Cost Modification Processor              |
		| 0:WC:P:Credit Note Processor                    |
		| 0:WC:P:Group Billing Processor                  |
		| 0:WC:P:Matter Split Reversal Processor          |
		| 0:WC:P:Matter Splits Processor                  |
		| 0:WC:P:Proforma Generation Processor            |
		| 0:WC:P:Proforma Processor                       |
		| 0:WC:P:Proforma Processor - Finance             |
		| 0:WC:P:Proforma Processor - Apply BOA           |
		| 0:WC:P:Proforma Processor - Apply Trust         |
		| 0:WC:P:Proforma Processor - Apply Unallocated   |
		| 0:WC:P:Quick Billing Proforma Processor         |
		| 0:WC:U:WIP Uploader                             |
		| 0:WC:V:Billing Viewer                           |
		| 0:WC:V:Client Account Viewer                    |
		| 0:WC:V:Collections Viewer                       |
		| 0:WC:W:Client Account Processor[: Unit: Office] |
		| 3EProformaEditorRole                            |
		| 0:WC:W:Group Billing Processor[: Unit: Office]  |
		

Scenario: PROFILE: Rates Services
	When I search the profile "PROFILE: Rates Services"
	Then the below roles are available
		| Roles                                             |
		| 0:AD:A:Exchange Rate Analyser                     |
		| 0:AD:G:Common Authorisations                      |
		| 0:AD:G:Deny All                                   |
		| 0:CML:M:Client Maintainer                         |
		| 0:CML:M:Client Maintainer - Close                 |
		| 0:CML:M:Client Maintainer - Contacts              |
		| 0:CML:M:Client Maintainer - Rates                 |
		| 0:CML:M:Client Maintainer - Volume Discount       |
		| 0:CML:M:Matter Maintainer                         |
		| 0:CML:M:Matter Maintainer - Close                 |
		| 0:CML:M:Matter Maintainer - Rates                 |
		| 0:CML:M:Matter Maintainer - Volume Discount       |
		| 0:FM:A:Client Account Analyser                    |
		| 0:PP:V:Accounts Payable Viewer                    |
		| 0:WC:A:Billing Analyser                           |
		| 0:WC:A:Client Account Analyser                    |
		| 0:WC:A:Collections Analyser                       |
		| 0:WC:M:Alternative Billing Arrangement Maintainer |
		| 0:WC:M:Fee Earner Rates Maintainer                |
		| 0:WC:M:Rates Maintainer                           |
		| 0:WC:M:Volume Discount Maintainer                 |
		| 0:WC:P:Rate Recalculation Processor               |
		| 0:WC:U:Rates Uploader                             |
		| 0:WC:V:Billing Viewer                             |
		| 0:WC:V:Client Account Viewer                      |
		| 0:WC:V:Collections Viewer                         |


Scenario: PROFILE: Rates Supervision
	When I search the profile "PROFILE: Rates Supervision"
	Then the below roles are available
		| Roles                                             |
		| 0:AD:A:Exchange Rate Analyser                     |
		| 0:AD:G:Common Authorisations                      |
		| 0:AD:G:Deny All                                   |
		| 0:CML:M:Client Maintainer                         |
		| 0:CML:M:Client Maintainer - Close                 |
		| 0:CML:M:Client Maintainer - Contacts              |
		| 0:CML:M:Client Maintainer - Rates                 |
		| 0:CML:M:Client Maintainer - Volume Discount       |
		| 0:CML:M:Matter Maintainer                         |
		| 0:CML:M:Matter Maintainer - Close                 |
		| 0:CML:M:Matter Maintainer - Rates                 |
		| 0:CML:M:Matter Maintainer - Volume Discount       |
		| 0:FM:A:Client Account Analyser                    |
		| 0:PP:V:Accounts Payable Viewer                    |
		| 0:WC:A:Billing Analyser                           |
		| 0:WC:A:Client Account Analyser                    |
		| 0:WC:A:Collections Analyser                       |
		| 0:WC:M:Alternative Billing Arrangement Maintainer |
		| 0:WC:M:Fee Earner Rates Maintainer                |
		| 0:WC:M:Rates Maintainer                           |
		| 0:WC:M:Volume Discount Maintainer                 |
		| 0:WC:P:Rate Recalculation Processor               |
		| 0:WC:U:Rates Uploader                             |
		| 0:WC:V:Billing Viewer                             |
		| 0:WC:V:Client Account Viewer                      |
		| 0:WC:V:Collections Viewer                         |



Scenario: PROFILE: Rates Services (EU)
	When I search the profile "PROFILE: Rates Services (EU)"
	Then the below roles are available
		| Roles                                       |
		| 0:AD:A:Exchange Rate Analyser               |
		| 0:AD:G:Common Authorisations                |
		| 0:AD:G:Deny All                             |
		| 0:CML:M:Client Maintainer                   |
		| 0:CML:M:Client Maintainer - Close           |
		| 0:CML:M:Client Maintainer - Rates           |
		| 0:CML:M:Client Maintainer - Volume Discount |
		| 0:CML:M:Matter Maintainer                   |
		| 0:CML:M:Matter Maintainer - Close           |
		| 0:CML:M:Matter Maintainer - Rates           |
		| 0:CML:M:Matter Maintainer - Volume Discount |
		| 0:FM:A:Client Account Analyser              |
		| 0:PP:V:Accounts Payable Viewer              |
		| 0:WC:A:Billing Analyser                     |
		| 0:WC:A:Client Account Analyser              |
		| 0:WC:A:Collections Analyser                 |
		| 0:WC:M:Fee Earner Rates Maintainer          |
		| 0:WC:M:Rates Maintainer                     |
		| 0:WC:M:Volume Discount Maintainer           |
		| 0:WC:P:Rate Recalculation Processor         |
		| 0:WC:U:Rates Uploader                       |
		| 0:WC:V:Billing Viewer                       |
		| 0:WC:V:Client Account Viewer                |
		| 0:WC:V:Collections Viewer                   |


Scenario: PROFILE: Rates Supervision (EU)
	When I search the profile "PROFILE: Rates Supervision (EU)"
	Then the below roles are available
		| Roles                                       |
		| 0:AD:A:Exchange Rate Analyser               |
		| 0:AD:G:Common Authorisations                |
		| 0:AD:G:Deny All                             |
		| 0:CML:M:Client Maintainer                   |
		| 0:CML:M:Client Maintainer - Close           |
		| 0:CML:M:Client Maintainer - Rates           |
		| 0:CML:M:Client Maintainer - Volume Discount |
		| 0:CML:M:Matter Maintainer                   |
		| 0:CML:M:Matter Maintainer - Close           |
		| 0:CML:M:Matter Maintainer - Rates           |
		| 0:CML:M:Matter Maintainer - Volume Discount |
		| 0:FM:A:Client Account Analyser              |
		| 0:PP:V:Accounts Payable Viewer              |
		| 0:WC:A:Billing Analyser                     |
		| 0:WC:A:Client Account Analyser              |
		| 0:WC:A:Collections Analyser                 |
		| 0:WC:M:Fee Earner Rates Maintainer          |
		| 0:WC:M:Rates Maintainer                     |
		| 0:WC:M:Volume Discount Maintainer           |
		| 0:WC:P:Rate Recalculation Processor         |
		| 0:WC:U:Rates Uploader                       |
		| 0:WC:V:Billing Viewer                       |
		| 0:WC:V:Client Account Viewer                |
		| 0:WC:V:Collections Viewer                   |


Scenario: PROFILE: eBilling Services
	When I search the profile "PROFILE: eBilling Services"
	Then the below roles are available
		| Roles                                |
		| 0:AD:A:Exchange Rate Analyser        |
		| 0:AD:G:Common Authorisations         |
		| 0:AD:G:Deny All                      |
		| 0:CML:M:Client Maintainer - Close    |
		| 0:CML:M:Client Maintainer - Contacts |
		| 0:CML:M:Client Maintainer - eBilling |
		| 0:CML:M:Matter Maintainer            |
		| 0:CML:M:Matter Maintainer - Close    |
		| 0:CML:M:Matter Maintainer - eBilling |
		| 0:CML:M:Matter Maintainer - Finance  |
		| 0:FM:A:Client Account Analyser       |
		| 0:PP:V:Accounts Payable Viewer       |
		| 0:WC:A:Billing Analyser              |
		| 0:WC:A:Client Account Analyser       |
		| 0:WC:A:Collections Analyser          |
		| 0:WC:P:eBilling Invoice Processor    |
		| 0:WC:P:eBilling Proforma Processor   |
		| 0:WC:V:Billing Viewer                |
		| 0:WC:V:Client Account Viewer         |
		| 0:WC:V:Collections Viewer            |


Scenario: PROFILE: eBilling Supervision
	When I search the profile "PROFILE: eBilling Supervision"
	Then the below roles are available
		| Roles                                |
		| 0:AD:A:Exchange Rate Analyser        |
		| 0:AD:G:Common Authorisations         |
		| 0:AD:G:Deny All                      |
		| 0:CML:M:Client Maintainer - Close    |
		| 0:CML:M:Client Maintainer - Contacts |
		| 0:CML:M:Client Maintainer - eBilling |
		| 0:CML:M:Client Maintainer - Finance  |
		| 0:CML:M:Matter Maintainer            |
		| 0:CML:M:Matter Maintainer - Close    |
		| 0:CML:M:Matter Maintainer - eBilling |
		| 0:FM:A:Client Account Analyser       |
		| 0:PP:V:Accounts Payable Viewer       |
		| 0:WC:A:Billing Analyser              |
		| 0:WC:A:Client Account Analyser       |
		| 0:WC:A:Collections Analyser          |
		| 0:WC:P:eBilling Invoice Processor    |
		| 0:WC:P:eBilling Proforma Processor   |
		| 0:WC:V:Billing Viewer                |
		| 0:WC:V:Client Account Viewer         |
		| 0:WC:V:Collections Viewer            |


Scenario: PROFILE: eBilling Services (EU)
	When I search the profile "PROFILE: eBilling Services (EU)"
	Then the below roles are available
		| Roles                                |
		| 0:AD:A:Exchange Rate Analyser        |
		| 0:AD:G:Common Authorisations         |
		| 0:AD:G:Deny All                      |
		| 0:CML:M:Client Maintainer - Close    |
		| 0:CML:M:Client Maintainer - eBilling |
		| 0:CML:M:Matter Maintainer            |
		| 0:CML:M:Matter Maintainer - Close    |
		| 0:CML:M:Matter Maintainer - eBilling |
		| 0:CML:M:Matter Maintainer - Finance  |
		| 0:FM:A:Client Account Analyser       |
		| 0:PP:V:Accounts Payable Viewer       |
		| 0:WC:A:Billing Analyser              |
		| 0:WC:A:Client Account Analyser       |
		| 0:WC:A:Collections Analyser          |
		| 0:WC:P:eBilling Invoice Processor    |
		| 0:WC:P:eBilling Proforma Processor   |
		| 0:WC:V:Billing Viewer                |
		| 0:WC:V:Client Account Viewer         |
		| 0:WC:V:Collections Viewer            |


Scenario: PROFILE: eBilling Supervision (EU)
	When I search the profile "PROFILE: eBilling Supervision (EU)"
	Then the below roles are available
		| Roles                                |
		| 0:AD:A:Exchange Rate Analyser        |
		| 0:AD:G:Common Authorisations         |
		| 0:AD:G:Deny All                      |
		| 0:CML:M:Client Maintainer - Close    |
		| 0:CML:M:Client Maintainer - eBilling |
		| 0:CML:M:Client Maintainer - Finance  |
		| 0:CML:M:Matter Maintainer            |
		| 0:CML:M:Matter Maintainer - Close    |
		| 0:CML:M:Matter Maintainer - eBilling |
		| 0:FM:A:Client Account Analyser       |
		| 0:PP:V:Accounts Payable Viewer       |
		| 0:WC:A:Billing Analyser              |
		| 0:WC:A:Client Account Analyser       |
		| 0:WC:A:Collections Analyser          |
		| 0:WC:P:eBilling Invoice Processor    |
		| 0:WC:P:eBilling Proforma Processor   |
		| 0:WC:V:Billing Viewer                |
		| 0:WC:V:Client Account Viewer         |
		| 0:WC:V:Collections Viewer            |




