@us @ignore 
# Test ignored because of workflow configuration not set for the operating unit 
#Defect - posting error https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/64017
Feature: GJEntryWithNoRequiresApprovalCategory

Ignored because of this bug: https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/64017

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

Examples:
	| Category                   | Currency | GLType                | GLBook                |
	| Auto approval not required | USD      | Local Dentons US, LLP | Local Dentons US, LLP |

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


Scenario Outline: 040 verify the status of genearal jouranl entry
	Then verify the status is 'Posted'


