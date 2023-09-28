@ignore @us
#Ignoed due to bug https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/79471
Feature: GlAccount

https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/78437
https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/78438

Scenario: 010 Verify that for GLACCT Quick find changes
	Given I search for process 'GL Account'
	Then I verify that the quick find include following
		| FieldName               |
		| Contains GL Description |
		| Contains GL Account     |
