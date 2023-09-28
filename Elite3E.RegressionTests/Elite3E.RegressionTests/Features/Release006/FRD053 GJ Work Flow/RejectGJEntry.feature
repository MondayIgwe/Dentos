@release6 @frd053 @RejectGJEntry
Feature: RejectGJEntry

On QA, Go to Workflow Config, check the GJApproval section, check 'Routing for next workflow step'
This will have a role, which your user must have in order for the GJ Requests to show up in your Workflow Dashboard.
In QA, this is: GJApprover


Scenario Outline: 010 Create a General Journal Request
	Given I search and create a gj category with api
		| GJCategoryDescription | IsRequireApprovalCheckboxAlias |
		| <Category>            | Yes                            |
	And I add a new 'General Journal Request'
	When I add the general journal details:
		| Category   | Currency   | Journal   | GLType   | GLBook   |
		| <Category> | <Currency> | {Auto}+10 | <GLType> | <GLBook> |
@ft
Examples:
	| Category               | Currency | GLType                                   | GLBook                     |
	| Auto Requires Approval | GBP      | Local Adj - Dentons UK & Middle East LLP | Local UK & Middle East LLP |
@uk
Examples:
	| Category               | Currency | GLType             | GLBook |
	| Auto Requires Approval | GBP      | All Books GL Types |        |
@qa
Examples:
	| Category               | Currency | GLType             | GLBook |
	| Auto Requires Approval | AUD      | All Books GL Types |        |
@training @staging
Examples:
	| Category               | Currency | GLType                        | GLBook                        |
	| Auto Requires Approval | GBP      | Local Dentons Europe LLP (UK) | Local Dentons Europe LLP (UK) |
@singapore
Examples:
	| Category               | Currency | GLType                             | GLBook                             |
	| Auto Requires Approval | GBP      | Local Dentons Rodyk & Davidson LLP | Local Dentons Rodyk & Davidson LLP |
@europe
Examples:
	| Category               | Currency | GLType             | GLBook |
	| Auto Requires Approval | EUR      | All Books GL Types |        |
@canada
Examples:
	| Category               | Currency | GLType                           | GLBook |
	| Auto Requires Approval | CAD      | Canada Accrual as per Enterprise |        |

Scenario Outline: 020 add genearal journal deatils and submit
	When I add general journal detail child form
		| Gl Account     | Original DR |
		| <DebitAccount> | <Amount>    |
	And I add general journal detail child form
		| Gl Account      | Original CR |
		| <CreditAccount> | <Amount>    |
	And I submit it
	And I validate submit was successful
@ft
Examples:
	| DebitAccount                           | Amount | CreditAccount                          |
	| 3000-101010-1000-0000-0000-0000-000000 | 10000  | 3000-101010-1000-0000-0000-0000-000000 |
@training
Examples:
	| DebitAccount                           | Amount | CreditAccount                          |
	| 3000-208010-2302-0000-0000-8054-000000 | 10000  | 3000-208010-2035-0000-0000-8054-000000 |
@staging
Examples:
	| DebitAccount                           | Amount | CreditAccount                          |
	| 1201-208510-9301-0000-0000-8010-000000 | 10000  | 1201-308510-2302-0000-0000-0000-000000 |
@europe
Examples:
	| DebitAccount                           | Amount | CreditAccount                          |
	| 3501-101020-1001-0000-0000-8159-000000 | 10000  | 3501-101020-1001-0000-0000-8159-000000 |
@canada
Examples:
	| DebitAccount                           | Amount | CreditAccount                          |
	| 5801-103010-5804-0000-0000-8019-000000 | 10000  | 5801-103010-5806-0000-0000-8019-000000 |
@qa
Examples:
	| DebitAccount                           | Amount | CreditAccount                          |
	| 3000-101010-1000-0000-0000-0000-000000 | 10000  | 3000-101010-1000-0000-0000-0000-000000 |
@uk
Examples:
	| DebitAccount                           | Amount | CreditAccount                          |
	| 3000-204530-1001-0000-0000-8054-000000 | 10000  | 3000-204530-1001-0000-0000-8054-000000 |
@singapore
Examples:
	| DebitAccount                           | Amount | CreditAccount                          |
	| 1201-101020-1000-0000-0000-8010-000000 | 1000   | 1201-101012-1000-0000-0000-8010-000000 |

@ft @training @staging @canada @europe @uk @singapore @qa
Scenario: 030 Approve the genearl journal entry
	When I search for 'Workflow Dashboard'
	And I open the general journal approval and 'Reject'

@ft @training @staging @canada @europe @uk @singapore @qa
Scenario Outline: 040 verify the workflow dashboard has receject log
	Then verify the gj request is shown at reject grid
