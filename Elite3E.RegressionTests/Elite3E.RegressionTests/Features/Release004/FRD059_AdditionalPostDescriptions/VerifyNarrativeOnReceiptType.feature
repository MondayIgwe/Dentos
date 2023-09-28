@release4 @frd059 @VerifyNarrativeOnReceiptType
Feature: VerifyNarrativeOnReceiptType

Scenario Outline: 010 Add Additional Postings to a Receipt Type
	Given I search for 'Automation Receipt Type' receipt type
	And add additional postings
		| GL Type  | Debit   | Credit   |
		| <GLType> | <Debit> | <Credit> |
	When I submit it
	Then "Additional Posting Narrative" is mandatory
@ft
Examples:
	| GLType                                   | Debit                                            | Credit                                           |
	| Local Adj - Dentons UK & Middle East LLP | BTkWMBill-203360-Dflt-0000-0000-BTkWMBill-000000 | BTkWMBill-203360-Dflt-0000-0000-BTkWMBill-000000 |
@training 
Examples:
	| GLType                               | Debit                                                      | Credit                                                     |
	| Local Dentons UK and Middle East LLP | BTkWMWork-757090-Dflt-BTkWMWork-BTkWMWork-BTkWMWork-000000 | BTkWMWork-757090-Dflt-BTkWMWork-BTkWMWork-BTkWMWork-000000 |
@staging 
Examples:
	| GLType                 | Debit                                            | Credit                                           |
	| Inter-Verein Recharges | BTkWMBill-201010-Dflt-0000-0000-BTkWMBill-000000 | BTkWMBill-201010-Dflt-0000-0000-BTkWMBill-000000 |

@qa
Examples:
	| GLType                                   | Debit                                            | Credit                                           |
	| Local Adj - Dentons UK & Middle East LLP | BTkWMBill-203360-Dflt-0000-0000-BTkWMBill-000000 | BTkWMBill-203360-Dflt-0000-0000-BTkWMBill-000000 |
@canada
Examples:
	| GLType                                   | Debit                                            | Credit                                           |
	| Local Dentons Canada LLP | BTkWMBill-202010-Dflt-0000-0000-BTkWMBill-000000 | BTkWMBill-202010-Dflt-0000-0000-BTkWMBill-000000 |
@europe
Examples:
	| GLType                        | Debit                                            | Credit                                           |
	| Local Dentons Europe LLP (UK) | BTkWMBill-202010-Dflt-0000-0000-BTkWMBill-000000 | BTkWMBill-202010-Dflt-0000-0000-BTkWMBill-000000 |
@uk
Examples:
	| GLType                                   | Debit                                            | Credit                                           |
	| Local Adj - Dentons UK & Middle East LLP | BTkWMBill-202010-Dflt-0000-0000-BTkWMBill-000000 | BTkWMBill-202010-Dflt-0000-0000-BTkWMBill-000000 |
@singapore
Examples:
	| GLType                                   | Debit                                            | Credit                                           |
	| Singapore Accrual (Region) - NEW | BTkWMBill-202010-Dflt-0000-0000-BTkWMBill-000000 | BTkWMBill-202010-Dflt-0000-0000-BTkWMBill-000000 |

@training @staging  @canada @europe @uk @singapore @ft @qa
Scenario: 020 Verify Narrative is mandatory and save
	Given I add the narrative 'Automation text'
	And submit it
	When I search for the saved receipt type
	Then the narrative is saved

@training @staging  @canada @europe @uk @singapore @ft @qa
Scenario: 030 Delete Additional Postings
	Given I delete additional postings
	And I submit it
