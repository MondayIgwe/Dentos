@release6 @frd053 @GJEntryApprovalCategoryAmountValidation 
Feature: GJEntryApprovalCategoryAmountValidation

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
@ft @uk
Examples:
	| Category               | Currency | GLType                                   | GLBook                     |
	| Auto Requires Approval | GBP      | Local Adj - Dentons UK & Middle East LLP | Local UK & Middle East LLP |
@training @staging
Examples:
	| Category               | Currency | GLType                             | GLBook                             |
	| Auto Requires Approval | GBP      | Local Dentons Rodyk & Davidson LLP | Local Dentons Rodyk & Davidson LLP |
@canada            
Examples:
	| Category               | Currency | GLType                           | GLBook |
	| Auto Requires Approval | CAD      | Canada Accrual as per Enterprise |        |
@qa
Examples:
	| Category               | Currency | GLType             | GLBook |
	| Auto Requires Approval | AUD      | All Books GL Types |        |
@singapore
Examples:
	| Category               | Currency | GLType                             | GLBook                             |
	| Auto Requires Approval | SGD      | Local Dentons Rodyk & Davidson LLP | Local Dentons Rodyk & Davidson LLP |
@europe
Examples:
	| Category               | Currency | GLType             | GLBook |
	| Auto Requires Approval | EUR      | All Books GL Types |        |

Scenario Outline: 020 add genearal journal deatils
	When I add general journal detail child form
		| Gl Account     | Original DR |
		| <DebitAccount> | <Amount>    |
	And I add general journal detail child form
		| Gl Account      | Original CR |
		| <CreditAccount> | <Amount>    |
@ft
Examples:
	| DebitAccount                           | Amount | CreditAccount                          |
	| 3000-101010-1000-0000-0000-0000-000000 | 1000   | 3000-101010-1000-0000-0000-0000-000000 |
@staging
Examples:
	| DebitAccount                           | Amount | CreditAccount                          |
	| 1201-208020-9002-0000-0000-0000-000000 | 1000   | 3000-308510-0000-0000-0000-8054-000000 |
@training
Examples:
	| DebitAccount                           | Amount | CreditAccount                          |
	| 3000-208010-2302-0000-0000-8054-000000 | 1000   | 3000-208010-2035-0000-0000-8054-000000 |
@europe
Examples:
	| DebitAccount                           | Amount | CreditAccount                          |
	| 3501-101020-1001-0000-0000-8159-000000 | 1000   | 3501-101020-1001-0000-0000-8159-000000 |
@canada
Examples:
	| DebitAccount                           | Amount | CreditAccount                          |
	| 5801-103010-5804-0000-0000-8019-000000 | 1000   | 5801-103010-5806-0000-0000-8019-000000 |
@qa
Examples:
	| DebitAccount                           | Amount | CreditAccount                          |
	| 3000-101010-1000-0000-0000-0000-000000 | 1000   | 3000-101010-1000-0000-0000-0000-000000 |
@uk
Examples:
	| DebitAccount                           | Amount | CreditAccount                          |
	| 3000-204530-1001-0000-0000-8054-000000 | 1000   | 3000-204530-1001-0000-0000-8054-000000 |
@singapore
Examples:
	| DebitAccount                           | Amount | CreditAccount                          |
	| 1201-101020-1000-0000-0000-8010-000000 | 1000   | 1201-101012-1000-0000-0000-8010-000000 |

@ft @qa @training @staging @canada @europe @uk @singapore
Scenario: 030 Submit General Journal entry
	When I submit it
	And I validate submit was successful
	Then verify the gl sequence number created

@ft @qa @training @staging @canada @europe @uk @singapore
Scenario Outline: 040 verify the status of general journal entry
	Then verify the status is 'Posted'