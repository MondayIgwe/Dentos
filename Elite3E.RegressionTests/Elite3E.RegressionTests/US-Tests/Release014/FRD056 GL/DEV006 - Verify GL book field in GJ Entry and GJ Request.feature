@us
Feature: DEV006 - Verify GL book field in GJ Entry and GJ Request

https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/61078
https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/61079
https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/61078
https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/61084
https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/61696
https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/61399
@CancelProcess
Scenario Outline: 010 Verify GL Book field and GL type based on GL Book and Submit
	Given I add a new 'General Journal Request'
	When I add the general journal details:
		| Category   | Currency   | Journal   | GLType   | GLBook   |
		| <Category> | <Currency> | {Auto}+10 | <GLType> | <GLBook> |
	And I add general journal detail child form
		| Gl Account     | Original DR |
		| <DebitAccount> | <Amount>    |
	And I add general journal detail child form
		| Gl Account      | Original CR | GJCategory |
		| <CreditAccount> | <Amount>    | <Category> |
	Then verify the gj category field is present in simple entry form
		| FieldName                |
		| General Journal Category |
	And I submit it
	And I validate submit was successful

Examples:
	| Category               | Currency | GLType                                   | GLBook                     | DebitAccount                           | Amount | CreditAccount                          |
	| Auto Requires Approval | GBP      | Local Adj - Dentons UK & Middle East LLP | Local UK & Middle East LLP | 3000-101010-1000-0000-0000-0000-000000 | 1000   | 3000-101010-1000-0000-0000-0000-000000 |


Scenario: 020 Verify GL Book field is present in General Journal Entry
	Given I add a new 'General Journal Entry'
	Then Verify the given fields are present
		| FieldName |
		| GL Book   |	
	And I cancel the process

@CancelProcess
Scenario: 030 Verify advanced find in General Journal Entry
	Given I search for process 'General Journal Entry'
	When I advanced find and select
		| Search Column         | Search Operator | Search Value |
		| GL Book (Description) | Equals          | <GLBook>     |
	Then I verify GL Book value is '<GLBook>'


Examples:
	| GLBook                     |
	| Local UK & Middle East LLP |



@CancelProcess
Scenario: 040 Verify advanced find in General Journal Request
	Given I search for process 'General Journal Request'
	When I advanced find and select
		| Search Column         | Search Operator | Search Value |
		| GL Book (Description) | Equals          | <GLBook>     |
	Then I verify GL Book value is '<GLBook>'


Examples:
	| GLBook                     |
	| Local UK & Middle East LLP |
