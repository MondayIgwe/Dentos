@release9 @frd043 @ProformaAttachmentsProcess
Feature: ProformaAttachmentsProcess

Proforma attachments process and overrides set ups

@ft @training @staging @canada @europe @uk @singapore @us @qa
Scenario Outline: 001 Verify attachment overrides are setup correctly
	Given I navigate to the Override/Set System Options process
	Then the ProfGen_Attach_Proforma_ccc should be set to true
		| OptionName                  | SystemDefault |
		| ProfGen_Attach_Proforma_ccc | True          |
	And the ProfGen_Proforma_Template_ccc should be set to OVERRIDE THIS
		| OptionName                    | SystemDefault |
		| ProfGen_Proforma_Template_ccc | OVERRIDE THIS |
	When I navigate to the Proforma Generation Process
	And I click Add
	And the format including the template are auto populated
	
	
