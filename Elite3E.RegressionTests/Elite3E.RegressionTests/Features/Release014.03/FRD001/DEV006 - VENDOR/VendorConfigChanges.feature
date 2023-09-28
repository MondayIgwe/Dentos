Feature: VendorConfigChanges

https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/78452
https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/78453
https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/78454
https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/78455
https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/78456
https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/78457
https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/78458

@ft @CancelProcess
Scenario: 010 Verify Vendor Maintenance Quick find changes
	Given I search for process 'Vendor Maintenance'
	Then I verify that the quick find include following
		| FieldName            |
		| Contains Vendor Name |
		| Contains Payee Name  |

@ft @CancelProcess
Scenario: 020 Verify Vendor Maintenance Advanced find query attributes
	Given I search for process 'Vendor Maintenance'
	Then I verify below query attributes in advanced find
		| SearchAttribute           |
		| Vendor Type (Description) |
		| Payee Name                |
		| Payee Type                |
		| Currency (Currency Code)  |
		| Unit (Description)        |
