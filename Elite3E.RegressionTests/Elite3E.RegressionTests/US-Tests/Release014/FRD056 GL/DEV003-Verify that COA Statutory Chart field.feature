@us
Feature: DEV003-Verify that COA Statutory Chart field

Verify CoA Statutory chart field and columns in units local child process.
Verify advanced find with Coa Statutory Chart

https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/60934
https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/60935
https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/60936


@CancelProcess
Scenario Outline: 010 Create COA Statutory Chart record and verify it in GL Unit
	Given I search for process 'COA Statutory Chart'
	And I add new coa statutory chart
		| Code      | Description | AccountNumber | FirmDescription | LocalDescription |
		| {Auto}+10 | {Auto}+20   | {Auto}+6      | {Auto}+10       | {Auto}+10        |
	When I search for process 'GL Unit'
	And I select an existing record
	Then Verify the given fields are present
		| FieldName           |
		| COA Statutory Chart |
	Then I Input and verify CoAStatutory chart and unit Local columns in GL Unit
		| GL Natural  | LocalAccount   |
		| <GLNatural> | <LocalAccount> |
	And I update it
	And I submit it
	And I validate submit was successful
	And I search for process 'GL Unit'
	And I advanced find and select unlocked statutory record
		| Search Column                   | Search Operator | Search Value |
		| COA Statutory Chart.Description | Equals          |              |
	And I verify CoA Statutory Chart field value
	When I delete recently added local account
		| Local Account  |
		| <LocalAccount> |
	And I submit it
	And I validate submit was successful


Examples:
	| GLNatural | LocalAccount |
	| 101010    | 8574         |