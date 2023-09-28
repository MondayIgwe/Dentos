@release6 @frd022 @NoInternalComments
Feature: NoInternalComments
	Test 5: If Internal Comments is not populated, cards are saved, and the workflow ends.

Scenario: 010 Create entity and fee earner
	Given I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	And I create the Payer with Api
		| PayerName   | Entity        |
		| <PayorName> | NorthSide Ltd |
	When I create a matter with details:
		| Client       | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department | Section | Rate     | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| <ClientName> | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | Bill                 | <Office> | Default    | Default | Standard | Desc_at_3e8glw7     | Desc_at_3e8glw7   | <PayorName> |
	And I submit a time modify
		| Time Type  | Hours | Narrative | FeeEarnerName   | WorkRate | WorkCurrency |
		| <TimeType> | 0.1   | {Auto}+10 | <FeeEarnerName> | 100      | <Currency>   |
	And add a user fee earner map
	Then I select the unbilled time card

@ft @qa @training @staging  @uk
Examples:
	| ClientName      | FeeEarnerName     | TimeType | Currency            | Office         | PayorName |
	| Randy NoComment | Stanley NoComment | FEES     | GBP - British Pound | London (UKIME) | Max Hart  |
@singapore
Examples:
	| ClientName      | FeeEarnerName     | TimeType | Currency            | Office    |PayorName |
	| Randy NoComment | Stanley NoComment | FEES     | GBP - British Pound | Singapore |Max Hart  |
@europe
Examples:
	| ClientName      | FeeEarnerName     | TimeType | Currency | Office      |PayorName |
	| Randy NoComment | Stanley NoComment | FEES     | AED      | London (EU) |Max Hart  |
@canada
Examples:
	| ClientName      | FeeEarnerName     | TimeType | Currency              | Office    | PayorName |
	| Randy NoComment | Stanley NoComment | FEES     | CAD - Canadian Dollar | Vancouver | Max Hart  |

@CancelProcess 
Scenario: 020 Enter narrative edit
	Given I edit a narrative
		| Narrative   |
		| <Narrative> |
	Then the change is reflected
		| Narrative   |
		| <Narrative> |
	And remove a user fee earner map

@ft @qa @training @staging  @canada @europe @uk @singapore
Examples:
	| Narrative                                        |
	| This is a test that is being carried out by SKG. |