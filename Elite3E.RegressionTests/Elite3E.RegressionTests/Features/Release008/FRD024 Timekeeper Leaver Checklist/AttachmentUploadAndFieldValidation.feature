@release8 @frd024.1 @AttachmentUploadAndFieldValidation
Feature: Attachment Upload and Field Validation
	CUST_306 - DEV001
	Azure Test Cases: 
		23247 - Login Nav Close
		23249 - Ribbon Button Vlidation
		23251 - Attachment Upload
		23297 - Workflow History Childform
		23298 - Office Configuration validation

@ft @training @staging  @canada @europe @uk @singapore @qa  
Scenario: 010 Attachment Upload
	Given I search for 'Timekeeper Leaver Checklist'
	When I verify the following buttons
		| Buttons   |
		| Submit    |
		| Save      |
		| Close     |
		| Reassign  |
		| Terminate |
	And I upload an attachment to the timekeeper leaver checklist
	Then I verify timekeeper leaver child form exists
		| Child Form       |
		| Workflow History |
	And I close timekeeper leaver checklist

@ft @training @staging  @canada @europe @uk @singapore @qa  
Scenario: 020 Office Configuration Field Validation
	Given I add a new 'Office Configuration'
	When I verify required fields on office configuration
		| Required Fields                         |
		| Office                                  |
		| Timekeeper Leaver Workflow Finance Role |
	Then I cancel office configuration
