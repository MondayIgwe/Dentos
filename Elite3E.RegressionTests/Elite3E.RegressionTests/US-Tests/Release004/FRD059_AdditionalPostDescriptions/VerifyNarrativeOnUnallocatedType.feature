@us
Feature: VerifyNarrativeOnUnallocatedType

Scenario Outline: 010 Add Additional Postings to a Unallocated Type
	Given I search for '<UnallocatedType>' unallocated type
	And add additional postings
		| GL Type  | Debit   | Credit   |
		| <GLType> | <Debit> | <Credit> |
	When I submit it
	Then "Additional Posting Narrative" is mandatory

Examples:
	| UnallocatedType | GLType                | Debit                                            | Credit                                           |
	| BAL_HLD         | Local Dentons US, LLP | BTkWMBill-202010-Dflt-0000-0000-BTkWMBill-000000 | BTkWMBill-202010-Dflt-0000-0000-BTkWMBill-000000 |


Scenario: 020 Verify Narrative is mandatory and save
	Given I add the narrative 'Automation text'
	And submit it
	When I search for the saved unallocated type
	Then the narrative is saved

Scenario: 030 Delete Additional Postings
	Given I delete additional postings
	And I submit it
