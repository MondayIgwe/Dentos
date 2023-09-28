@us
Feature: CreateDeleteModifySystemOptions

Verify that the following system overrides for the Proforma Options of the My Billable Matters process exist in the 'Create/Delete/Modify System Options' process in 3E

Scenario: System overrides verification
	When I navigate to create/delete/modify system options process
	And I search for 'Billing' override
	Then I verify that '<SystemOverrides>' system override exist


Examples:
	| SystemOverrides                                                          |
	| MyMattersProfStatus_ccc,MyMattersProfStatusOther_ccc,MyMattersSource_ccc |


