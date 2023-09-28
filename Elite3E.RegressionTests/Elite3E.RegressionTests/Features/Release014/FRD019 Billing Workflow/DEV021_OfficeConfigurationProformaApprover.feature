Feature: DEV021_OfficeConfigurationProformaApprover

Verify 'Proforma approver cannot bill' is present in Office Configuration process (True)

@CancelProcess @training @staging @canada @europe @uk @singapore @ft @qa
Scenario: 010 Verify Proforma Approver checkbox in Office Configuration process
	Given I open the office configuration process
	Then I verify the proforma approver checkbox exists

@CancelProcess
Scenario: 020 Set Proforma Approver checkbox to true then Save
	Given I search for 'Office Configuration'
	When I advanced find and select
		| Search Column      | Search Operator | Search Value |
		| Office Description | Equals          | <Office>     |  
	When I set the proforma approver checkbox to 'true'
	And I submit it
	Then I search for 'Office Configuration'
	And I advanced find and select
		| Search Column      | Search Operator | Search Value |
		| Office Description | Equals          | <Office>     |
	And I validate the proforma approver checkbox is set to 'true'

@training @canada @europe @uk @ft @qa
Examples:
	| Office   |
	| Brisbane |
@singapore @staging
Examples:
	| Office    |
	| Singapore |

@CancelProcess
Scenario: 030 Set Proforma Approver checkbox to false then Save
	Given I search for 'Office Configuration'
	And I advanced find and select
		| Search Column      | Search Operator | Search Value |
		| Office Description | Equals          | <Office>     |
	When I set the proforma approver checkbox to 'false'
	And I submit it
	Then I search for 'Office Configuration'
	And I advanced find and select
		| Search Column      | Search Operator | Search Value |
		| Office Description | Equals          | <Office>     |
	And I validate the proforma approver checkbox is set to 'false'

@training @canada @europe @uk @ft @qa
Examples:
	| Office   |
	| Brisbane |
@singapore @staging
Examples:
	| Office    |
	| Singapore |