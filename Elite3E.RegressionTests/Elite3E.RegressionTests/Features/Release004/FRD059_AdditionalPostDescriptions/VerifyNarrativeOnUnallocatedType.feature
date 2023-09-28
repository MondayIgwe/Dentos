@release4 @frd059 @VerifyNarrativeOnUnallocatedType
Feature: VerifyNarrativeOnUnallocatedType

Scenario Outline: 010 Add Additional Postings to a Unallocated Type
	Given I search for '<UnallocatedType>' unallocated type
	And add additional postings
		| GL Type  | Debit   | Credit   |
		| <GLType> | <Debit> | <Credit> |
	When I submit it
	Then "Additional Posting Narrative" is mandatory
@ft 
Examples:
	| UnallocatedType | GLType                                   | Debit                                            | Credit                                           |
	| BAL_HLD         | Local Adj - Dentons UK & Middle East LLP | BTkWMBill-203360-Dflt-0000-0000-BTkWMBill-000000 | BTkWMBill-203360-Dflt-0000-0000-BTkWMBill-000000 |
@training
Examples:
	| UnallocatedType | GLType                               | Debit                                                      | Credit                                                     |
	| BAL_HLD         | Local Dentons UK and Middle East LLP | BTkWMWork-757090-Dflt-BTkWMWork-BTkWMWork-BTkWMWork-000000 | BTkWMWork-757090-Dflt-BTkWMWork-BTkWMWork-BTkWMWork-000000 |
@staging 
Examples:
	| UnallocatedType | GLType                 | Debit                                            | Credit                                           |
	| BAL_HLD         | Inter-Verein Recharges | BTkWMBill-201010-Dflt-0000-0000-BTkWMBill-000000 | BTkWMBill-201010-Dflt-0000-0000-BTkWMBill-000000 |
@qa
Examples:
	| UnallocatedType          | GLType                                   | Debit                                                      | Credit                                                     |
	| Automation do not delete | Local Adj - Dentons UK & Middle East LLP | BTkWMBill-601070-Dflt-BTkWMBill-BTkWMBill-BTkWMBill-000000 | BTkWMBill-601070-Dflt-BTkWMBill-BTkWMBill-BTkWMBill-000000 |
@canada
Examples:
	| UnallocatedType | GLType                   | Debit                                            | Credit                                           |
	| Unallocated     | Local Dentons Canada LLP | BTkWMBill-202010-Dflt-0000-0000-BTkWMBill-000000 | BTkWMBill-202010-Dflt-0000-0000-BTkWMBill-000000 |
@europe
Examples:
	| UnallocatedType | GLType                        | Debit                                            | Credit                                           |
	| BAL_HLD         | Local Dentons Europe LLP (UK) | BTkWMBill-202010-Dflt-0000-0000-BTkWMBill-000000 | BTkWMBill-202010-Dflt-0000-0000-BTkWMBill-000000 |
@singapore
Examples:
	| UnallocatedType          | GLType                                    | Debit                                            | Credit                                           |
	| Automation do not delete | Singapore Accrual (Region) - NEW | BTkWMBill-202010-Dflt-0000-0000-BTkWMBill-000000 | BTkWMBill-202010-Dflt-0000-0000-BTkWMBill-000000 |
@uk
Examples:
	| UnallocatedType          | GLType                                   | Debit                                            | Credit                                           |
	| Automation do not delete | Local Adj - Dentons UK & Middle East LLP | BTkWMBill-202010-Dflt-0000-0000-BTkWMBill-000000 | BTkWMBill-202010-Dflt-0000-0000-BTkWMBill-000000 |


@training @staging  @canada @europe @uk @singapore  @ft  @qa
Scenario: 020 Verify Narrative is mandatory and save
	Given I add the narrative 'Automation text'
	And submit it
	When I search for the saved unallocated type
	Then the narrative is saved

@training @staging  @canada @europe @uk @singapore @ft  @qa
Scenario: 030 Delete Additional Postings
	Given I delete additional postings
	And I submit it
