Feature: Rate Exception Groups

A Client runs a 12% from the prevailing firm standard rate. 
Verify that Set up of the rate exception is complete.
Azure link: https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/41384

@CancelProcess
Scenario: 010 Prepare data for Rate Maintenance
	Given I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	And I search or create a client
		| Entity Name | FeeEarnerFullName |
		| <Client>    | <FeeEarnerName>   |
	And I create a matter with details:
		| Client   | Status     | OpenDate   | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | Rate   | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| <Client> | Fully Open | {Today}-31 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <Rate> | <ChargeTypeGroup>   | <CostTypeGroup>   | <PayorName> |

@e2eft
Examples:
	| FeeEarnerName | Client        | Currency            | CurrencyMethod | Office         | Department | Section | Rate     | ChargeTypeGroup | CostTypeGroup   | DisbursementType        | TaxCode                         | PayorName |
	| Mike Thanks   | Mikel Mandela | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Standard | TestGroup3      | Desc_at_3e8glw7 | Automation Accomodation | UK Output Domestic Standard 20% | James May |

@CancelProcess @e2eft
Scenario: 020 Create new Rate
	Given I navigate to the rate maintenance process
	And I add a new rate maintenance record
		| Code      | Description                       | RateType      | Formula    | RateTypeValue |
		| {Auto}+12 | 12% Prevailing Firm Standard Rate | Standard Rate | STD/100*12 | 100           |
	And I test the formula
		| TestResult |
		| 12.00000   |
	When I update it
	Then I submit it
	And I validate submit was successful

@CancelProcess @e2eft
Scenario Outline: 030 Rate Exception Group
	Given I search for the client
	And I add new rate exception group
		| Description                       | RateExceptionList |StartDate  |
		| 12% Prevailing Firm Standard Rate | Firm              |{Today}    |
	When I add rate exception detail
		| Office         |
		| London (UKIME) |
	And I update it
	And I submit the rate exception group
	Then I verify that the exception group is correct
	And I update it
	And I submit it
	And I validate submit was successful


@CancelProcess @e2eft
Scenario Outline: 040 Get Rate Test
	When I navigate to the GetRate Test process
	And I fill in the GetRate Test required fields
		| TransactionType | Work Date |
		| Fees            | {Today}   |
	And I click Get Rate button
	And I verify that the exception rate is correct

@CancelProcess @e2eft
Scenario Outline: 050 Clean Up
	Given I search for the client
	And I remove the exception rate group
	When I update it
	And I submit it
	And I validate submit was successful


	

	