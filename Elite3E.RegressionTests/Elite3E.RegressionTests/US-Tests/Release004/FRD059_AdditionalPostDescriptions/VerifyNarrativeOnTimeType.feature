@us
Feature: VerifyNarrativeOnTimeType

Scenario: 010 Add Additional Postings to a Time Type
	Given I search for '<TimeType>' time type
	And add additional postings
		| GL Type  | Value   | PostingStage   | Debit   | Credit   |
		| <GLType> | <Value> | <PostingStage> | <Debit> | <Credit> |
	When I submit it
	Then "Additional Posting Narrative" is mandatory

Examples:
	| TimeType                          | GLType                | Value       | PostingStage | Debit                                            | Credit                                           |
	| Appellate Motions and Submissions | Local Dentons US, LLP | Bill Amount | Bill posting | BTkWMBill-202010-Dflt-0000-0000-BTkWMBill-000000 | BTkWMBill-202010-Dflt-0000-0000-BTkWMBill-000000 |

Scenario: 020 Verify Narrative is mandatory and save
	Given I add the narrative 'Automation text'
	And submit it
	When I search for the saved time type
	Then the narrative is saved

Scenario: 030 Delete Additional Postings
	Given I delete additional postings
	And I submit it
