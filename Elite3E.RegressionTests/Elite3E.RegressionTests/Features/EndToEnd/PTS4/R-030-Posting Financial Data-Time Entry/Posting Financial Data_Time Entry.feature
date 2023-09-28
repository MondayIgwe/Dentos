Feature: Posting Financial Data_Time Entry
R-030 - R2R: Posting Financial Data - Time Entry 
https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/53160


	@CancelProcess
	Scenario Outline: 010 Create a new Matter
	Given I create a matter with details:
		| Client   | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   |  ChargeTypeGroupName | CostTypeGroupName | BillingOffice   | PayorName   |
		| <Client> | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <ChargeTypeGroup>   | <CostTypeGroup>   | <BillingOffice> |<PayorName> |

	@e2eft
	Examples:
	| Client                       | Currency            | CurrencyMethod | Office         | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName  |
	| Client_Automation at_HTOvMOn | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | London (EU)   | James Matt |


	@CancelProcess
	Scenario Outline: 020 Create a time entry and submit
	Given I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	When I create a matter with details:
		| Client       | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department | Section | Rate     | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| <ClientName> | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | Bill                 | <Office> | Default    | Default | Standard | Desc_at_3e8glw7     | Desc_at_3e8glw7   | <PayorName> |
	And I submit a time modify
		| Time Type  | Hours | Narrative | FeeEarnerName   | WorkRate | WorkCurrency |
		| <TimeType> | 0.1   | {Auto}+10 | <FeeEarnerName> | 100      | <Currency>   |
	And I locate the submitted entry in 'Time Modify' process
	And I get the time index of the time card
	And I validate the gl postings for operating unit '<OperatingUnit>'

	@e2eft
	Examples:
	| ClientName                   | FeeEarnerName      | TimeType | Currency            | Office         | OperatingUnit | PayorName  |
	| Client_Automation at_HTOvMOn | Lena EarnerSurname | FEES     | GBP - British Pound | London (UKIME) | 3000          | James Matt |

	@e2eft @CancelProcess
    Scenario Outline: 030 Verify the Post Queue
	Given I navigate to the post queue process
	When I verify the cost/time/charge/receipt card is not present in the post queue

	@e2eft @CancelProcess
	Scenario: 040 Verify in Posting Results
	Given I search for the entry in posting results process 
	Then verify the posting status is posted

	@e2eft
	Scenario: 050 Verify in Journal Register
	Given I search for the entry in journal register
	Then verify the posting details in journal register
            | Search Column   | Search Operator |
            | Journal Manager | Equals          |