@us @ignore 
# Test ignored because of workflow configuration not set for the operating unit 
#Defect - posting error https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/64017
Feature: GJEntryApprovalCategoryAmountValidation

Ignored because of this bug: https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/64017


Amount value 1000 should not require approval
Notes:
For the Category, data needs to be created: 
Go to process: GJ Category Setup
Add 'Auto Requires Approval' and tick requires approval checkbox.

When adding the gl account, if you get the message: 
'You are posting into a Future Period, this posting will not post until period is Opened. 1,12,2022 is within a period of Unit 1001 that is not yet opened.'
Go to process: Unit Period Review and Update
Search for your year and month, Update the unit you're using (1001) to open status
This likely needs to be automated and added to the test flow

Note:
This test has a category that require approval.
However, we input an amount of 1000 that does not require approval
Since the value is less than 10 000, the request gets automatically posted.


Scenario Outline: 010 Create a General Journal Request
	Given I search and create a gj category with api
		| GJCategoryDescription | IsRequireApprovalCheckboxAlias |
		| <Category>            | Yes                            |
	And I add a new 'General Journal Request'
	When I add the general journal details:
		| Category   | Currency   | Journal   | GLType   | GLBook   |
		| <Category> | <Currency> | {Auto}+10 | <GLType> | <GLBook> |

Examples:
	| Category               | Currency | GLType                | GLBook                |
	| Auto Requires Approval | USD      | Local Dentons US, LLP | Local Dentons US, LLP |

Scenario Outline: 020 add genearal journal deatils
	When I add general journal detail child form
		| Gl Account     | Original DR |
		| <DebitAccount> | <Amount>    |
	And I add general journal detail child form
		| Gl Account      | Original CR |
		| <CreditAccount> | <Amount>    |

Examples:
	| DebitAccount                           | Amount | CreditAccount                          |
	| 5000-301050-4580-0000-0000-8025-000000 | 1000   | 5000-305140-6101-0000-0000-8025-000000 |


Scenario: 030 Submit General Journal entry
	When I submit it
	And I validate submit was successful
	Then verify the gl sequence number created

Scenario Outline: 040 verify the status of general journal entry
	Then verify the status is 'Posted'