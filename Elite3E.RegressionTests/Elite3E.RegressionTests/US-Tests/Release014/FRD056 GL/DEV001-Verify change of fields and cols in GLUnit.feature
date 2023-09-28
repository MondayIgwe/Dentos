@us
Feature: DEV001-Verify change of fields and cols in GLUnit

Verify that the ‘Use Local Account’ and ‘GL Local Chart’ have been disabled with in GL unit
Verify that The combination of GL Natural, Local within the GL Unit will be unique based on the stock archetype having a unique index on Unit, Natural and Local.
Verify that the Local Account for the GL Natural and Unit combination can be changed from 1000 

https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/27232
https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/27233
https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/27235
https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/27236
https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/27237
https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/27270
https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/27240

@CancelProcess 
Scenario: 010 Verify Use Local Account and GL Local Chart fields are not present
	Given I search for process 'GL Unit'
	And I click add
	Then Verify the given fields are not present
		| FieldName         |
		| Use Local Account |
		| GL Local Chart    |
	And I cancel the process

@CancelProcess
Scenario Outline: 020 Verify column changes and adding unit local account
	Given I search for process 'GL Unit'
	And I perform quick find for '<GLUnit>'
	Then Verify the given fields are not present
		| FieldName         |
		| Use Local Account |
		| GL Local Chart    |
	And I verify given columns are not present under Units Local section
		| FieldName           |
		| Local Chart Account |
		| GL Local            |
		| Local Description   |
	And I verify columns are present under Units Local section
		| FieldName                       |
		| Local Account                   |
		| Local Account Description       |
		| Local Account Local Description |
	When I input unit local account details and verify default values
		| GL Natural  | Default Local Account | Local Account  |
		| <GLNatural> | 1000                  | <LocalAccount> |
	And I update it
	And I submit it
	And I validate submit was successful
	And I search for process 'GL Unit'
	And I perform quick find for '<GLUnit>'
	And I delete recently added local account
		| Local Account  |
		| <LocalAccount> |
	And I submit it

Examples:
	| GLUnit | GLNatural | LocalAccount |
	| 3000   | 101021    | 9835         |

	
@CancelProcess
Scenario Outline: 030 Verify that Adding duplicate Gl Unit Local Accounts Shows an error
	Given I search for process 'GL Unit'
	And I perform quick find for '<GLUnit>'
	When I add duplicate gl unit local accounts
		| GLNatural   |
		| <GLNatural> |
		| <GLNatural> |
	And I update it
	Then an error appears


Examples:
	| GLUnit | GLNatural |
	| 3000   | 101010    |


@CancelProcess
Scenario Outline: 040 Verify that the error is generated on local account when entering more than 4 digits
	Given I search for process 'GL Unit'
	And I perform quick find for '<GLUnit>'
	When I add local account
	Then I enter more than four digits on the local account field
		| Amount | GLNatural   |
		| 10009  | <GLNatural> |
	And I get an error regarding the maximum number of digits allowed
		| Error Message                      |
		| Maximum account number length is 4 |

Examples:
	| GLUnit | GLNatural |
	| 3000   | 101010    |



@CancelProcess
Scenario Outline: 050 Verify that the Local Account for the GL Natural and Unit combination can be changed from 1000
	Given I search for process 'GL Unit'
	And I perform quick find for '<GLUnit>'
	Then I input unit local account details and verify default values
		| GL Natural  | Default Local Account | Local Account  |
		| <GLNatural> | 1000                  | <LocalAccount> |
	And I update it
	And I submit it
	And I validate submit was successful
	And I search for process 'GL Unit'
	And I perform quick find for '<GLUnit>'
	And I verify that the local account was saved correctly
		| GLNatural   | LocalAccount   |
		| <GLNatural> | <LocalAccount> |
	When I delete recently added local account
		| Local Account  |
		| <LocalAccount> |
	And I submit it


Examples:
	| GLUnit | GLNatural | LocalAccount |
	| 3000   | 101010    | 9867         |
