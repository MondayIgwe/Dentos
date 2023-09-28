@us
Feature: VerifyNarrativeOnChargeType

Scenario Outline: 010 Add Additional Postings to a Charge Type
	Given I search for 'Automation do not delete' charge type
	And add additional postings
		| GL Type  | Value   | PostingStage   | Debit   | Credit   |
		| <GLType> | <Value> | <PostingStage> | <Debit> | <Credit> |
	When I submit it
	Then "Additional Posting Narrative" is mandatory

Examples:
	| GLType                | Value       | PostingStage | Debit                                            | Credit                                           |
	| Local Dentons US, LLP | Bill Amount | Bill posting | BTkWMBill-202010-Dflt-0000-0000-BTkWMBill-000000 | BTkWMBill-202010-Dflt-0000-0000-BTkWMBill-000000 |

Scenario: 020 Verify Narrative is mandatory and save
	Given I add the narrative 'Automation text'
	And submit it
	When I search for the saved charge type
	Then the narrative is saved

Scenario: 030 Delete Additional Postings
	Given I delete additional postings
	And I submit it
