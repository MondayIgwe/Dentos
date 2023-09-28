@release6 @frd053 @GJEntryWithRequiresApprovalCategory 
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
@ft
Examples:
	| Category               | Currency | OperatingUnit | GLType                                   | GLBook                     |
	| Auto Requires Approval | GBP      | 3000          | Local Adj - Dentons UK & Middle East LLP | Local UK & Middle East LLP |

@uk
Examples:
	| Category               | Currency | OperatingUnit | GLType             | GLBook |
	| Auto Requires Approval | GBP      | 0000          | All Books GL Types |        |
@qa
Examples:
	| Category               | Currency | OperatingUnit | GLType             | GLBook |
	| Auto Requires Approval | AUD      | 0000          | All Books GL Types |        |
@canada
Examples:
	| Category               | Currency | OperatingUnit | GLType                           | GLBook |
	| Auto Requires Approval | CAD      | 5801          | Canada Accrual as per Enterprise |        |
@europe
Examples:
	| Category               | Currency | OperatingUnit | GLType             | GLBook |
	| Auto Requires Approval | EUR      | 0000          | All Books GL Types |        |
@staging
Examples:
	| Category               | Currency | OperatingUnit | GLType             | GLBook |
	| Auto Requires Approval | AUD      | 0000          | All Books GL Types |        |
@singapore
Examples:
	| Category               | Currency | OperatingUnit | GLType             | GLBook |
	| Auto Requires Approval | GBP      | 1201          | Local Dentons Rodyk & Davidson LLP |  Local Dentons Rodyk & Davidson LLP      |
@training
Examples:
	| Category               | Currency | OperatingUnit | GLType             | GLBook |
	| Auto Requires Approval | GBP      | 0000          | All Books GL Types |        |

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
@staging
Examples:
	| DebitAccount                           | Amount | CreditAccount                          |
	| 1201-208510-9301-0000-0000-8010-000000 | 10000  | 1201-308510-2302-0000-0000-0000-000000 |
@training
Examples:
	| DebitAccount                           | Amount | CreditAccount                          |
	| 3000-208010-2302-0000-0000-8054-000000 | 10000  | 3000-208010-2035-0000-0000-8054-000000 |
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
	And I open the general journal approval and 'Approve'
	Then verify the gl sequence number created

@ft @training @staging @canada @europe @uk @singapore @qa
Scenario Outline: 040 verify the status of genearal jouranl entry
	Then verify the status is 'Posted'
