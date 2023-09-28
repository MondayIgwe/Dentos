@us
Feature: DEV010 - General Journal Entry - GJ Category field

Verify General journal category field in General Journal Entry
https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/61083
https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/61097
https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/61392
https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/61393


@CancelProcess
Scenario Outline: 010 Verify General Journal Category Field
	Given I add a new 'General Journal Entry'
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
