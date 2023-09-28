@profile
Feature: FrontOffice
	Verify the roles for Profiles

#Scenario: PROFILE: Partner (Centralised Billing)
#	When I search the profile "PROFILE: Partner (Centralised Billing)"
#	Then the below roles are available
#		| Roles                                                 |
#		| 0:AD:G:Common Authorisations                          |
#		| 0:AD:G:Deny All                                       |
#		| 0:AD:G:Organisation Chart[: Unit: Office: Department] |
#		| 0:CML:Q:Records Requester                             |
#		| 0:WC:O:Proforma Approver[: Unit]                      |
#		| 0:WC:O:Proforma Client Account Approver               |
#		| 0:WC:P:Proforma Processor                             |
#		| 0:WC:P:Quick Billing Proforma Processor               |
#		| 0:WC:P:Time Modification Processor                    |
#		| 0:WC:P:Time Recorder                                  |
#		| 3EProformaEditorRole                                  |

#Scenario: PROFILE: Fee Earner (Centralised Billing)
#	When I search the profile "PROFILE: Fee Earner (Centralised Billing)"
#	Then the below roles are available
#		| Roles                                   |
#		| 0:AD:G:Common Authorisations            |
#		| 0:AD:G:Deny All                         |
#		| 0:CML:Q:Records Requester               |
#		| 0:WC:P:Proforma Processor               |
#		| 0:WC:P:Quick Billing Proforma Processor |
#		| 0:WC:P:Time Modification Processor      |
#		| 0:WC:P:Time Recorder                    |
#		| 3EProformaEditorRole                    |


Scenario: PROFILE: Legal Assistant (Centralised Billing)
	When I search the profile "PROFILE: Legal Assistant (Centralised Billing)"
	Then the below roles are available
		| Roles                                                 |
		| 0:AD:G:Common Authorisations                          |
		| 0:AD:G:Deny All                                       |		
		| 0:CML:M:Client Maintainer                             |
		| 0:CML:M:Matter Maintainer                             |
		| 0:WC:P:Proforma Processor                             |
		| 0:WC:P:Quick Billing Proforma Processor               |
		| 0:WC:P:Time Modification Processor                    |
		| 0:WC:P:Time Recorder                                  |
		| 3EProformaEditorRole                                  |



#Scenario: PROFILE: Partner (Decentralised Billing)
#	When I search the profile "PROFILE: Partner (Decentralised Billing)"
#	Then the below roles are available
#		| Roles                                   |
#		| 0:AD:G:Common Authorisations            |
#		| 0:AD:G:Deny All                         |
#		| 0:WC:O:Proforma Approver[: Unit]        |
#		| 0:WC:O:Proforma Client Account Approver |
#		| 0:WC:P:Proforma Processor               |
#		| 0:WC:P:Quick Billing Proforma Processor |
#		| 0:WC:P:Time Modification Processor      |
#		| 0:WC:P:Time Recorder                    |
#		| 3EProformaEditorRole                    |
#
#Scenario: PROFILE: Fee Earner (Decentralised Billing)
#	When I search the profile "PROFILE: Fee Earner (Decentralised Billing)"
#	Then the below roles are available
#		| Roles                                   |
#		| 0:AD:G:Common Authorisations            |
#		| 0:AD:G:Deny All                         |
#		| 0:WC:P:Proforma Processor               |
#		| 0:WC:P:Quick Billing Proforma Processor |
#		| 0:WC:P:Time Modification Processor      |
#		| 0:WC:P:Time Recorder                    |
#		| 3EProformaEditorRole                    |
Scenario: PROFILE: Trusted Billing Services
	When I search the profile "PROFILE: Trusted Billing Services"
	Then the below roles are available
		| Roles                                   |
		| 0:AD:G:Common Authorisations            |
		| 0:AD:G:Deny All                         |	
		| 0:WC:P:Proforma Processor               |
		| 0:WC:P:Quick Billing Proforma Processor |
		| 0:WC:P:Time Modification Processor      |
		| 0:WC:P:Time Recorder                    |
		| 3EProformaEditorRole                    |


Scenario: PROFILE: Partner
	When I search the profile "PROFILE: Partner"
	Then the below roles are available
		| Roles                                                 |
		| 0:AD:G:Common Authorisations                          |
		| 0:AD:G:Deny All                                       |
		| 0:AD:G:Organisation Chart[: Unit: Office: Department] |
		| 0:WC:P:Proforma Processor                             |
		| 0:WC:P:Quick Billing Proforma Processor               |
		| 0:WC:P:Time Modification Processor                    |
		| 0:WC:P:Time Recorder                                  |
		| 3EProformaEditorRole                                  |

Scenario: PROFILE: Approving Partner
	When I search the profile "PROFILE: Approving Partner"
	Then the below roles are available
		| Roles                                                    |
		| 0:AD:G:Common Authorisations                             |
		| 0:AD:G:Deny All                                          |
		| 0:AD:G:Organisation Chart[: Unit: Office: Department]    |
		| 0:WC:P:Quick Billing Proforma Processor                  |
		| 0:WC:P:Time Modification Processor                       |
		| 0:WC:P:Time Recorder                                     |
		| 3EProformaEditorRole                                     |
		| 3EProformaApproverRole                                   |
		| 0:WC:W:Write Off Approver L1[: Unit: Office: Department] |
		| 0:WC:W:Write Off Approver L2[: Unit: Office: Department] |
		| 0:WC:W:Write Off Approver L3[: Unit: Office: Department] |

Scenario: PROFILE: Fee Earner
	When I search the profile "PROFILE: Fee Earner"
	Then the below roles are available
		| Roles                                   |
		| 0:AD:G:Common Authorisations            |
		| 0:AD:G:Deny All                         |
		| 0:WC:P:Proforma Processor               |
		| 0:WC:P:Quick Billing Proforma Processor |
		| 0:WC:P:Time Modification Processor      |
		| 0:WC:P:Time Recorder                    |
		| 3EProformaEditorRole                    |

Scenario: PROFILE: Billing Collaborator
	When I search the profile "PROFILE: Billing Collaborator"
	Then the below roles are available
		| Roles                                   |
		| 0:AD:G:Common Authorisations            |
		| 0:AD:G:Deny All                         |
		| 0:WC:P:Proforma Processor               |
		| 0:WC:P:Quick Billing Proforma Processor |
		| 0:WC:P:Time Modification Processor      |
		| 0:WC:P:Time Recorder                    |
		| 3EProformaEditorRole                    |

Scenario: PROFILE: Legal Assistant (Decentralised Billing)
	When I search the profile "PROFILE: Legal Assistant (Decentralised Billing)"
	Then the below roles are available
		| Roles                                   |
		| 0:AD:G:Common Authorisations            |
		| 0:AD:G:Deny All                         |
		| 0:CML:M:Matter Maintainer               |
		| 0:WC:P:Matter Split Reversal Processor  |
		| 0:WC:P:Matter Splits Processor          |
		| 0:WC:P:Proforma Processor               |
		| 0:WC:P:Quick Billing Proforma Processor |
		| 0:WC:P:Time Modification Processor      |
		| 0:WC:P:Time Recorder                    |
		| 3EProformaEditorRole                    |
