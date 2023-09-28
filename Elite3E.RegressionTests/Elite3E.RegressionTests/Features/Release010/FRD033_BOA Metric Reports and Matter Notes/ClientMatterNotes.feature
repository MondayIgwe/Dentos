@release10 @frd033 @ClientMatterNotes
Feature: ClientMatterNotes

At time of writing, Client notes have not been setup. Only Matter notes can be tested.


Scenario: 10 API Prep Data
	Given I create the Payer with Api
		| PayerName   | Entity        |
		| <PayorName> | WaterFlow Ltd |
	And I create a matter with details:
		| Client   | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department | Section | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| <Client> | <FeeEarnerName>   | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | Bill                 | <Office> | Default    | Default | Desc_at_3e8glw7     | Desc_at_3e8glw7   | <PayorName> |

@qa @ft
Examples:
	| Client       | FeeEarnerName   | Office         | TaxCodeDescription      | Currency | TimeType       | PayorName |
	| Notes Client | Notes FeeEarner | London (UKIME) | UK Output Domestic Zero | GBP      | FEES (Default) | Agnes Old |

@singapore
Examples:
	| Client       | FeeEarnerName   | Office    | TaxCodeDescription          | Currency               | TimeType       | PayorName |
	| Notes Client | Notes FeeEarner | Singapore | SG Output Domestic Standard | SGD - Singapore Dollar | FEES (Default) |Agnes Old |
@uk
Examples:
	| Client       | FeeEarnerName   | Office         | TaxCodeDescription      | Currency | TimeType       | PayorName |
	| Notes Client | Notes FeeEarner | London (UKIME) | UK Output Domestic Zero | GBP      | FEES (Default) |Agnes Old |

@training @staging 
Examples:
	| Client       | FeeEarnerName   | Office         | TaxCodeDescription          | Currency | TimeType | PayorName |
	| Notes Client | Notes FeeEarner | London (UKIME) | UK Output Domestic Standard | GBP      | FEES     |Agnes Old |

@europe
Examples:
	| Client       | FeeEarnerName   | Office      | TaxCodeDescription          | Currency | TimeType | PayorName |
	| Notes Client | Notes FeeEarner | London (EU) | UK Output Domestic Standard | EUR      | FEES     |Agnes Old |
@canada
Examples:
	| Client       | FeeEarnerName   | Office    | TaxCodeDescription          | Currency | TimeType | PayorName |
	| Notes Client | Notes FeeEarner | Vancouver | UK Output Domestic Standard | CAD      | FEES     |Agnes Old |


Scenario: 20 Add Note to Matter
	Given I view an existing matter
	When I retrieve the current username
	Then I add a note to a matter
		| DateEntered | NoteType   | Note          |
		| {Today}     | <NoteType> | <MatterNotes> |
	And I submit it
	And I validate submit was successful

@qa @ft @canada @europe @singapore @uk @training @staging 
Examples:
	| NoteType            | MatterNotes |
	| Billing Information | {Auto}+36   |

@qa @ft @canada @europe @singapore @uk @training @staging 
Scenario: 30 Validate Matter Notes on  Matter Group Enquiry
	Given I search for process 'Matter Group Enquiry' without add button
	When I search in matter group enquiry
		| SearchPhrase | SearchValue |
		| Matter       |             |
	And I submit it
	Then I validate matter notes in matter group enquiry