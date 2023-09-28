@ignore
#ignored due to defect 
#https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/79707
#https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/79713

Feature: BankAccountOfficeConfigChanges

https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/78525
https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/78526
https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/78527


@ft @CancelProcess
Scenario: 010 Verify Bank Account Office Quick find changes
	Given I search for process 'Bank Account Process'
	Then I verify that the quick find include following
		| FieldName                          |
		| Contains Fractional Routing Number |
		| Contains Sort Name                 |

@ft @CancelProcess
Scenario: 020 Verify Bank Account Office Advanced find query attributes
	Given I search for process 'Bank Account Process'
	Then I verify below query attributes in advanced find
		| SearchAttribute          |
		| Sort Name                |
	