@release1 @frd024 @PayorUnit
Feature: PayorUnit
Verify the payor Unit

Scenario Outline: 010 Add new Payor
	Given I search for process '<Process>'
	And I enter a new payer
		| Entity   | Payer Name  | Site   | Tax Area  |
		| <Entity> | <PayerName> | <Site> | <TaxArea> |

@ft
Examples:
	| Process | Entity                    | PayerName | Site                      | TaxArea                           |
	| Payer   | Aberdeen - Dentons Client | Auto_Mark | Aberdeen - Dentons Client | United Kingdom - Land Transaction |
@qa
Examples:
	| Process | Entity                    | PayerName | Site                      | TaxArea        |
	| Payer   | Aberdeen - Dentons Client | Auto_Mark | Aberdeen - Dentons Client | United Kingdom |
@training @uk
Examples:
	| Process | Entity                    | PayerName | Site           | TaxArea        |
	| Payer   | Aberdeen - Dentons Client | Auto_Mark | London UK site | United Kingdom |

@staging 
Examples:
	| Process | Entity          | PayerName | Site           | TaxArea        |
	| Payer   | Dentons England | Auto_Mark | London UK site | United Kingdom |
@singapore
Examples:
	| Process | Entity                          | PayerName | Site          | TaxArea   |
	| Payer   | Client_Automation DentonsGlobal | Auto_Mark | Singapore ltd | Singapore |
@canada
Examples:
	| Process | Entity                | PayerName         | Site           | TaxArea |
	| Payer   | Auto_Test Donotdelete | Auto_Mark William | London UK site | Canada  |
@us
Examples:
	| Process | Entity    | PayerName         | Site         | TaxArea       |
	| Payer   | Mark ADAM | Auto_Mark William | Test Site US | United States |
@europe
Examples:
	| Process | Entity         | PayerName | Site           | TaxArea |
	| Payer   | DENTONS BERLIN | Auto_Mark | DENTONS BERLIN | Germany |

Scenario Outline: 020 Add new Payor Unit
	When I submit new payor unit
		| Description   | Status   |
		| <Description> | <Status> |
	And I search for process '<Process>'
	And search the new payer
	Then the payor is saved
	And payor unit title is "<Title>"
	And the payor unit is saved

@ft @uk @qa
Examples:
	| Process | Description               | Status   | Title          |
	| Payer   | Dentons Australia Limited | Approved | Payor Unit (1) |
@singapore
Examples:
	| Process | Description                  | Status   | Title          |
	| Payer   | Dentons Rodyk & Davidson LLP | Approved | Payor Unit (1) |
@canada
Examples:
	| Process | Description        | Status   | Title          |
	| Payer   | Dentons Canada LLP | Approved | Payor Unit (1) |
@training 
Examples:
	| Process | Description                    | Status   | Title          |
	| Payer   | Dentons UK and Middle East LLP | Approved | Payor Unit (1) |

@staging
Examples:
	| Process | Description                    | Status   | Title          |
	| Payer   | Dentons UK and Middle East LLP | Approved | Payor Unit (1) |
	
@us
Examples:
	| Process | Description                | Status   | Title          |
	| Payer   | Dentons United States, LLP | Approved | Payor Unit (1) |
@europe
Examples:
	| Process | Description             | Status   | Title          |
	| Payer   | Dentons Europe LLP (UK) | Approved | Payor Unit (1) |

@CancelProcess
Scenario Outline: 030 Add Duplicate Payor Unit
	When I try to add a duplicate payor unit
		| Description   | Status   |
		| <Description> | <Status> |
	Then an error message "<Error>" is displayed

@ft @uk @qa
Examples:
	| Description               | Status   | Error                       |
	| Dentons Australia Limited | Approved | Enter a unique Payer, Unit. |
@singapore
Examples:
	| Description                  | Status   | Error                       |
	| Dentons Rodyk & Davidson LLP | Approved | Enter a unique Payer, Unit. |
@canada
Examples:
	| Description        | Status   | Error                       |
	| Dentons Canada LLP | Approved | Enter a unique Payer, Unit. |
@training
Examples:
	| Description                    | Status   | Error                       |
	| Dentons UK and Middle East LLP | Approved | Enter a unique Payer, Unit. |

@staging
Examples:
	| Description                    | Status   | Error                       |
	| Dentons UK and Middle East LLP | Approved | Enter a unique Payer, Unit. |
	
@us
Examples:
	| Description                | Status   | Error                       |
	| Dentons United States, LLP | Approved | Enter a unique Payer, Unit. |
@europe
Examples:
	| Description             | Status   | Error                       |
	| Dentons Europe LLP (UK) | Approved | Enter a unique Payer, Unit. |