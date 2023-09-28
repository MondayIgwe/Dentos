@us @ignore
# Test ignored because of workflow configuration not set for the operating unit 
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

Examples:
	| Category               | Currency | GLType                | GLBook                |
	| Auto Requires Approval | USD      | Local Dentons US, LLP | Local Dentons US, LLP |

Scenario Outline: 020 add genearal journal deatils and submit
	When I add general journal detail child form
		| Gl Account     | Original DR |
		| <DebitAccount> | <Amount>    |
	And I add general journal detail child form
		| Gl Account      | Original CR |
		| <CreditAccount> | <Amount>    |
	And I submit it
	And I validate submit was successful

Examples:
| DebitAccount                           | Amount | CreditAccount                          |
| 5000-301050-4580-0000-0000-8025-000000 | 10000  | 5000-305140-6101-0000-0000-8025-000000 |



Scenario: 030 Approve the genearl journal entry
	When I search for 'Workflow Dashboard'
	And I open the general journal approval and 'Reject'


Scenario Outline: 040 verify the workflow dashboard has receject log
	Then verify the gj request is shown at reject grid
