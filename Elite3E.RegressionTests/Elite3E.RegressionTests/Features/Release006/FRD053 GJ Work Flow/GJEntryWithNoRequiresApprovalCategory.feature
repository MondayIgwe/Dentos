@release6 @frd053 @GJEntryWithNoRequiresApprovalCategory 
Feature: GJEntryWithNoRequiresApprovalCategory

Notes:
For the Category, data needs to be created: 
Go to process: GJ Category Setup
Add 'Auto approval not required' and keep requires approval checkbox unticked1.

Scenario Outline: 010 Create a General Journal Request
	Given I search and create a gj category with api
		| GJCategoryDescription | IsRequireApprovalCheckboxAlias |
		| <Category>            | No                             |
	And I add a new 'General Journal Request'
	When I add the general journal details:
		| Category   | Currency   | Journal   | GLType   | GLBook   |
		| <Category> | <Currency> | {Auto}+10 | <GLType> | <GLBook> |
@ft @uk
Examples:
	| Category               | Currency | GLType                                   | GLBook                     |
	| Auto Requires Approval | GBP      | Local Adj - Dentons UK & Middle East LLP | Local UK & Middle East LLP |
@qa
Examples:
	| Category                   | Currency | GLType             | GLBook |
	| Auto approval not required | AUD      | All Books GL Types |        |
@training @staging
Examples:
	| Category                   | Currency | GLType                        | GLBook                        |
	| Auto approval not required | GBP      | Local Dentons Europe LLP (UK) | Local Dentons Europe LLP (UK) |
@singapore
Examples:
	| Category                   | Currency | GLType                             | GLBook                             |
	| Auto approval not required | SGD      | Local Dentons Rodyk & Davidson LLP | Local Dentons Rodyk & Davidson LLP |
@europe
Examples:
	| Category                   | Currency | GLType             | GLBook |
	| Auto approval not required | EUR      | All Books GL Types |        |
@canada
Examples:
	| Category                   | Currency | GLType                           | GLBook |
	| Auto approval not required | CAD      | Canada Accrual as per Enterprise |        |

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
@training
Examples:
	| DebitAccount                           | Amount | CreditAccount                          |
	| 3000-208010-2302-0000-0000-8054-000000 | 1000   | 3000-208010-2035-0000-0000-8054-000000 |
@staging
Examples:
	| DebitAccount                           | Amount | CreditAccount                          |
	| 1201-208510-9301-0000-0000-8010-000000 | 1000   | 1201-308510-2302-0000-0000-0000-000000 |
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

@ft @training @staging @canada @europe @uk @singapore @qa
Scenario: 030 Submit General Journal entry
	When I submit it
	And I validate submit was successful
	Then verify the gl sequence number created

@ft @training @staging @canada @europe @uk @singapore @qa
Scenario Outline: 040 verify the status of genearal jouranl entry
	Then verify the status is 'Posted'


