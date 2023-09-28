@us @ignore 
# Test ignored because of workflow configuration not set for the operating unit 
#Defect - posting error https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/64017
Feature: GJEntryWithRequiresApprovalCategory

Notes:
Your user profile must have rights for the workflow approval.
Go to process: User / Role Management
	Search your user, go to Assigned Roles, ensure you have: '0:FM:W:GJ Approver: Firm'
	Make sure your default operating unit is either 00 or 0000.

Operating unit must match the unit in Workflow Configuration > GJ Approval > Submit Approval Required > Conditions Set
	For UK and STAGING, I had to change this from 00 to 0000 as that was the only default / firm unit available.

Amount should be 10 000


Scenario Outline: 010 Create a General Journal Request
	Given I search and create a gj category with api
		| GJCategoryDescription | IsRequireApprovalCheckboxAlias |
		| <Category>            | Yes                            |
	And I add a new 'General Journal Request'
	When change the operating unit "<OperatingUnit>"
	And I add the general journal details:
		| Category   | Currency   | Journal   | GLType   | GLBook   |
		| <Category> | <Currency> | {Auto}+10 | <GLType> | <GLBook> |

Examples:
	| Category               | Currency | OperatingUnit | GLType                | GLBook                |
	| Auto Requires Approval | USD      | 5001          | Local Dentons US, LLP | Local Dentons US, LLP |


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
	And I open the general journal approval and 'Approve'
	Then verify the gl sequence number created


Scenario Outline: 040 verify the status of genearal jouranl entry
	Then verify the status is 'Posted'
