@release1 @frd024 @PayorMaintenance
Feature: PayorMaintenance
	Verify the customisations for Payor Maintenance

Scenario Outline: 010 Verify Tax Area is mandatory
	Given I search for process '<Process>'
	When I try to add a new payer
		| Entity   | Payer Name  | Site   |
		| <Entity> | <PayerName> | <Site> |
	Then "Tax Area" is mandatory

@ft @qa
Examples:
	| Process | Entity                    | PayerName         | Site                      |
	| Payer   | Aberdeen - Dentons Client | Auto_Mark William | Aberdeen - Dentons Client |
@europe @uk
Examples:
	| Process | Entity                 | PayerName         | Site           |
	| Payer   | National AustraliaBank | Auto_Mark William | London UK site |
@singapore
Examples:
	| Process | Entity                          | PayerName         | Site          |
	| Payer   | Client_Automation DentonsGlobal | Auto_Mark William | Singapore ltd |
@canada
Examples:
	| Process | Entity                | PayerName         | Site           |
	| Payer   | Auto_Test Donotdelete | Auto_Mark William | London UK site |
@training 
Examples:
	| Process | Entity                         | PayerName         | Site           |
	| Payer   | Dentons UK and Middle East LLP | Auto_Mark William | London UK site |

	@staging
Examples:
	| Process | Entity          | PayerName         | Site           |
	| Payer   | Dentons England | Auto_Mark William | London UK site |	
@us
Examples:
	| Process | Entity    | PayerName         | Site         |
	| Payer   | Mark ADAM | Auto_Mark William | Test Site US |

Scenario Outline: 020 Add new Payor
	When I add additional tax area to the new payer
		| Tax Area  |
		| <TaxArea> |
	And I search for process '<Process>'
	And advanced find the payor
		| Search Column   | Search Operator  | Search Value   |
		| <SearchColumn1> | <SearchOperator> | <SearchValue1> |
		| <SearchColumn2> | <SearchOperator> | <SearchValue2> |
	Then the payor is found

@ft
Examples:
	| Process | TaxArea                           | SearchColumn1 | SearchColumn2 | SearchOperator | SearchValue1      | SearchValue2 |
	| Payer   | United Kingdom - Land Transaction | Payer Name    | Tax Area.Code | Equals         | Auto_Mark William | GB02         |
@qa @europe @uk
Examples:
	| Process | TaxArea        | SearchColumn1 | SearchColumn2 | SearchOperator | SearchValue1      | SearchValue2 |
	| Payer   | United Kingdom | Payer Name    | Tax Area.Code | Equals         | Auto_Mark William | GB01         |
@singapore
Examples:
	| Process | TaxArea   | SearchColumn1 | SearchColumn2 | SearchOperator | SearchValue1      | SearchValue2 |
	| Payer   | Singapore | Payer Name    | Tax Area.Code | Equals         | Auto_Mark William | SG01         |
@canada
Examples:
	| Process | TaxArea        | SearchColumn1 | SearchColumn2 | SearchOperator | SearchValue1      | SearchValue2 |
	| Payer   | United Kingdom | Payer Name    | Tax Area.Code | Equals         | Auto_Mark William | GB01         |
@training @staging
Examples:
	| Process | TaxArea        | SearchColumn1 | SearchColumn2 | SearchOperator | SearchValue1      | SearchValue2 |
	| Payer   | United Kingdom | Payer Name    | Tax Area.Code | Equals         | Auto_Mark William | GB01         |
@us
Examples:
	| Process | TaxArea       | SearchColumn1 | SearchColumn2 | SearchOperator | SearchValue1      | SearchValue2 |
	| Payer   | United States | Payer Name    | Tax Area.Code | Equals         | Auto_Mark William | US01         |