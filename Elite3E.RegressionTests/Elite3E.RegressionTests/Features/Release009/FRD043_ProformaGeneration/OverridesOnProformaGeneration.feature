@release9 @frd043 @OverridesOnProformaGeneration
Feature: OverridesOnProformaGeneration

Renaming and setting overrides on Proforma Generation function

@CancelProcess @ft @training @staging @canada @europe @uk @singapore @us @qa
Scenario Outline: 001 Verify that the processes are renamed correctly
	Given I navigate to the Billing dashboard
	Then the X Proforma Generation Process should exist
	Then the Proforma Generation process should be named X Proforma Generation
		| ProcessName    |
		| ProfGeneration |
	And the WF Proforma Generation process should exist
		| ProcessName       |
		| WF_ProfGeneration |

@CancelProcess @ft @training @staging @canada @europe @uk @singapore @us @qa
Scenario Outline: 002 Verify that the overrides are setup correctly
	Given I navigate to the Override/Set System Options process
	Then the ProStatus_ccc should be set to OVERRIDE THIS
		| OptionName     | SystemDefault |
		| ProfStatus_ccc | OVERRIDE THIS |
	And the ProStatusOther_ccc should be set to OVERRIDE THIS
		| OptionName          | SystemDefault |
		| ProfStatusOther_ccc | OVERRIDE THIS |
	When I navigate to the Proforma Generation Process
	And I click Add
	And the status values are initialsed as per the System Override
		| ProfStatus | ChangeStatusTo |
		| Draft      | CLOSED_DISCARD |

@CancelProcess @ft @training @staging @canada @europe @uk @singapore @us @qa
Scenario Outline: 003 Verify that the worflow overrides are setup correctly
	Given I navigate to the Override/Set System Options process
	Then the MyMattersIsStartWF_ccc should be set to true
		| OptionName             | SystemDefault |
		| MyMattersIsStartWF_ccc | True          |
	
		
