
@release9 @frd016 @ClientMaintenanceAudit
Feature: ClientMaintenanceAudit

FRD016 - Test1 Part 1 - Client Maintenance Audit

Scenario Outline: 010 Client Maintenance Audit Validation
	Given I search or create a client
		| Entity Name  |
		| <ClientName> |
	When I search for the client
	Then I fill client credit details
		| Form Above   | Risk Score   | Credit Score Rating   | Credit Limit   | Currency   | AML Risk   | Finance Risk Score   |
		| <Form Above> | <Risk Score> | <Credit Score Rating> | <Credit Limit> | <Currency> | <AML Risk> | <Finance Risk Score> |
	And I submit it
	And I validate submit was successful
	And I search for the client
	And I validate client credit details audit
		| Form Above   | Risk Score   | Credit Score Rating   | Credit Limit   | Currency   | AML Risk   | Finance Risk Score   |
		| <Form Above> | <Risk Score> | <Credit Score Rating> | <Credit Limit> | <Currency> | <AML Risk> | <Finance Risk Score> |
	And I cancel the process

@ft @qa @training @staging  @canada @europe @uk @singapore @us
	Examples: 
		| ClientName | Form Above                                 | Risk Score | Credit Score Rating | Credit Limit | Currency | AML Risk | Finance Risk Score |
		| {Auto}+8   | Client and eBilling Additional Information | 20.00      | 151.57              | 900.50       | EUR      | 77.58    | 12.21              |