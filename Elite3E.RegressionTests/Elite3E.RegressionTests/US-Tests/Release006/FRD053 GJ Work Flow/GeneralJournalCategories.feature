@us
Feature: GeneralJournalCategories

Scenario Outline: 010 Requires Approval Boolean is available for General Journal Categories
	Given I search for process '<processName>'
	When I perform quick find for '<searchFor>'
	Then I verify the category has 'RequireApproval' checkbox


Examples:
	| searchFor         | processName       |
	| Manual Allocation | GJ Category Setup |

Scenario Outline: 020 Requires Approval checkbox is editable
	Then I 'check' the 'RequireApproval' checkbox
	And I 'uncheck' the 'RequireApproval' checkbox
	And I submit it