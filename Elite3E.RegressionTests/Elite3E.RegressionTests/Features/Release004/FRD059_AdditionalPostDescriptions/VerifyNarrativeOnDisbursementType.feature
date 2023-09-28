@release4 @frd059 @VerifyNarrativeOnDisbursementType
Feature: VerifyNarrativeOnDisbursementType

Scenario Outline: 010 Add Additional Postings to a Disbursement Type
	Given I search for 'Automation do not delete' disbursement type
	When add additional postings
		| GL Type  | Value   | PostingStage   | Debit   | Credit   |
		| <GLType> | <Value> | <PostingStage> | <Debit> | <Credit> |
	When I submit it
	Then "Additional Posting Narrative" is mandatory
@ft
Examples:
	| GLType                                   | Value       | PostingStage | Debit                                            | Credit                                           |
	| Local Adj - Dentons UK & Middle East LLP | Bill Amount | Bill posting | BTkWMBill-203360-Dflt-0000-0000-BTkWMBill-000000 | BTkWMBill-203360-Dflt-0000-0000-BTkWMBill-000000 |
@training
Examples:
	| GLType                               | Value       | PostingStage | Debit                                                      | Credit                                                     |
	| Local Dentons UK and Middle East LLP | Bill Amount | Bill posting | BTkWMWork-757090-Dflt-BTkWMWork-BTkWMWork-BTkWMWork-000000 | BTkWMWork-757090-Dflt-BTkWMWork-BTkWMWork-BTkWMWork-000000 |
@staging 
Examples:
	| GLType                 | Value       | PostingStage | Debit                                            | Credit                                           |
	| Inter-Verein Recharges | Bill Amount | Bill posting | BTkWMBill-201010-Dflt-0000-0000-BTkWMBill-000000 | BTkWMBill-201010-Dflt-0000-0000-BTkWMBill-000000 |
@qa
Examples:
	| GLType                                   | Value       | PostingStage | Debit                                            | Credit                                           |
	| Local Adj - Dentons UK & Middle East LLP | Bill Amount | Bill posting | BTkWMBill-203360-Dflt-0000-0000-BTkWMBill-000000 | BTkWMBill-203360-Dflt-0000-0000-BTkWMBill-000000 |
@canada
Examples:
	| GLType                   | Value       | PostingStage | Debit                                            | Credit                                           |
	| Local Dentons Canada LLP | Bill Amount | Bill posting | BTkWMBill-202010-Dflt-0000-0000-BTkWMBill-000000 | BTkWMBill-202010-Dflt-0000-0000-BTkWMBill-000000 |
@europe
Examples:
	| GLType                        | Value       | PostingStage | Debit                                            | Credit                                           |
	| Local Dentons Europe LLP (UK) | Bill Amount | Bill posting | BTkWMBill-202010-Dflt-0000-0000-BTkWMBill-000000 | BTkWMBill-202010-Dflt-0000-0000-BTkWMBill-000000 |
@uk
Examples:
	| GLType                                   | Value       | PostingStage | Debit                                            | Credit                                           |
	| Local Adj - Dentons UK & Middle East LLP | Bill Amount | Bill posting | BTkWMBill-202010-Dflt-0000-0000-BTkWMBill-000000 | BTkWMBill-202010-Dflt-0000-0000-BTkWMBill-000000 |
@singapore
Examples:
	| GLType                           | Value       | PostingStage | Debit                                            | Credit                                           |
	| Singapore Accrual (Region) - NEW | Bill Amount | Bill posting | BTkWMBill-202010-Dflt-0000-0000-BTkWMBill-000000 | BTkWMBill-202010-Dflt-0000-0000-BTkWMBill-000000 |

@training @staging  @canada @europe @uk @singapore @ft @qa
Scenario: 020 Verify Narrative is mandatory and save
	Given I add the narrative 'Automation text'
	And submit it
	When I search for the saved disbursement type
	Then the narrative is saved

@training @staging  @canada @europe @uk @singapore @ft @qa
Scenario: 030 Delete Additional Postings
	Given I delete additional postings
	And I submit it
