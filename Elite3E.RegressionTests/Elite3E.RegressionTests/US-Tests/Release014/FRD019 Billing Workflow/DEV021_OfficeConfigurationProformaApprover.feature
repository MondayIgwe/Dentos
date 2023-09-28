@us
Feature: DEV021_OfficeConfigurationProformaApprover

Verify 'Proforma approver cannot bill' is present in Office Configuration process (True)

@CancelProcess
Scenario: 010 Verify Proforma Approver checkbox in Office Configuration process
	Given I open the office configuration process
	Then I verify the proforma approver checkbox exists

@CancelProcess
Scenario: 020 Set Proforma Approver checkbox to true then Save
	Given  I search for process 'Office Configuration'
	And I open existing office configuration record
		| Office   |
		| <Office> |
	When I set the proforma approver checkbox to 'true'
	And I submit it
	Then I search for 'Office Configuration'
	And I open existing office configuration record
		| Office   |
		| <Office> |
	And I validate the proforma approver checkbox is set to 'true'


Examples:
	| Office  |
	| Phoenix |


@CancelProcess
Scenario: 030 Set Proforma Approver checkbox to false then Save
	Given I search for 'Office Configuration'
	And I open existing office configuration record
		| Office   |
		| <Office> |
	When I set the proforma approver checkbox to 'false'
	And I submit it
	Then I search for 'Office Configuration'
	And I open existing office configuration record
		| Office   |
		| <Office> |
	And I validate the proforma approver checkbox is set to 'false'


Examples:
	| Office  |
	| Phoenix |
