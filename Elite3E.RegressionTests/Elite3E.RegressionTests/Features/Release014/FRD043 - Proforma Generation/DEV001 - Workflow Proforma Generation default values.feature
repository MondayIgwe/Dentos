Feature: DEV001 - Workflow Proforma Generation default values

Verify that an override system option for the default proforma status has been created

@CancelProcess
Scenario: 010 Verify the override system option for the default proforma status
	Given I navigate to create/delete/modify system options process
	And I search for 'Billing' override
	Then I verify that '<SystemOverrides>' override is displayed
	And I verify the description '<Description>'

@ft @training @staging @canada @europe @uk @singapore @qa
Examples:
	| SystemOverrides | Description                                 |
	| ProfStatus_ccc  | Default workflow proforma generation status |

@CancelProcess
Scenario Outline: 020 Verify Override System options for the default proforma status
	Given I navigate to the Override/Set System Options process
	Then I verify that '<SystemOverrides>' option is displayed

@ft @training @staging @canada @europe @uk @singapore @qa
Examples:
	| SystemOverrides |
	| ProfStatus_ccc  |
