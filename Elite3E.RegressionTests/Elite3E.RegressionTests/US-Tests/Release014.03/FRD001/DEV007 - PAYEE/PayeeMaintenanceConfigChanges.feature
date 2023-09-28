@ignore @us
#Ignored due to defect
#https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/79528
Feature: PayeeMaintenanceConfigChanges

https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/78516
https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/78517
https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/78518
https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/78519
https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/79523

@CancelProcess
Scenario: 010 Verify Payee Maintenance Advanced find query attributes
	Given I search for process 'Payee Maintenance'
	Then I verify below query attributes in advanced find
		| SearchAttribute          |
		| Payee Type               |
		| Currency (Currency Code) |
		| Unit (Description)       |
		| Office                   |

@CancelProcess
Scenario: 020 Verify that Description field has been renamed to Bank Name for Payee Bank Child
	Given I search for process 'Payee Maintenance'
	And I click add
	When I click add button on child form 'Payee Bank'
	Then Verify the given fields are not present
		| FieldName   |
		| Description |
	And Verify the given fields are present
		| FieldName |
		| Bank Name |