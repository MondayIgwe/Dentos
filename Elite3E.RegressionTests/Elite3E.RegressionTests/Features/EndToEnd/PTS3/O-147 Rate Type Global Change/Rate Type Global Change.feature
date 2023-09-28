
Feature: Rate Type Global Change

Check that converted Matters can be Globally Changed (Rates) On a converted group of clients, 
it's been agreed that the Rate Type for the Fee Earner needs to be changed globally. 
A requestor has made a request for the change, and it has been approved. 
Verify that set-up of rate type change is complete.
Azure link: https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/49122


Scenario: 010 Create new Rate Type Maintenance
	Given I navigate to the rate type maintenance process
	When I add a new rate type maintenance record
		| Code     | Description | Default Currency |
		| {Auto}+7 | {Auto}+36   | <Currency>       |
	And I add start date
		| Start Date |
		| {Today}    |
	And I tick the standard rate checkbox
	Then add the effective dates
		| Start Date | StartDate | ReasonType | Description | DefaultRate |
		| {Today}    | {Today}   | BILL_PAID  | {Auto}+10   | 100         |
	And add rate type details
		| Amount   | Currency   | Title | Office |
		| <Amount> | <Currency> |       |        |
	And I update it
	Then I submit it
	And I validate submit was successful
@e2eft
Examples:
	| Amount | Currency |
	| 500    | GBP      |

Scenario: 020 Prepare data for Rate Maintenance
	Given I create the Payer with Api
		| PayerName   | Entity       |
		| <PayorName> | CentralCoast |
	And I create a fee earner with details
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
	| FeeEarnerName  | Client                 | Currency            | CurrencyMethod | Office         | Department | Section | Rate     | ChargeTypeGroup | CostTypeGroup   | PayorName |
	| Rate FeeEarner | Client_Automation Rate | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Standard | TestGroup3      | Desc_at_3e8glw7 | James May |

@e2eft
Scenario: 030 Create new Rate Maintenance
	Given I navigate to the rate maintenance process
	And I add a new rate maintenance record
		| Code      | Description                       | RateType      | Formula  | RateTypeValue |
		| {Auto}+12 | 12% Prevailing Firm Standard Rate | Standard Rate | STD*1.15 | 100           |
	And I test the formula
		| TestResult |
		| 115.00000  |
	When I update it
	Then I submit it
	And I validate submit was successful


@e2eft
Scenario Outline: 040 Rate Exception Group
	Given I search for the client
	And I add new rate exception group
		| Description                       | RateExceptionList | StartDate |
		| 12% Prevailing Firm Standard Rate | Office            |           |
	When I add rate exception detail
		| Office         |
		| London (UKIME) |
	And I add clients using rate exception
	Then I update it
	And I submit the rate exception group
	Then I verify that the exception group is correct
	And I update it
	And I submit it
	And I validate submit was successful


Scenario Outline: 050 I want to make a RateType Global Change
	Given I search for process 'RateType Global Change' without add button
	When I enter ratetype global change details
		| RateType      | EffectiveDate | BasedOnDate | ReasonType   | DefaultRate   | DefaultCurrency   | RoundingMethod   |
		| Standard Rate | {Today}       | {Today}     | <ReasonType> | <DefaultRate> | <DefaultCurrency> | <RoundingMethod> |
	Then I add rate detail currencies
		| ChangeAmount   |
		| <ChangeAmount> |
	And I update it
	Then I submit it
	And I validate submit was successful
@e2eft
Examples:
	| EffectiveDate | BasedOnDate | ReasonType | DefaultRate | DefaultCurrency | RoundingMethod | ChangeAmount |
	| {Today}       | {Today}     | Rates      | 100         | GBP             | Round Up       | 100          |



@e2eft
Scenario Outline: 060 Get Rate Test
	When I navigate to the GetRate Test process
	And I fill in the GetRate Test required fields
		| TransactionType | Work Date | FeeEarnerNumber |
		| Fees            | {Today}   | 100229          |
	And I click Get Rate button
	#And I verify that the exception rate is correct
	Then I cancel the process

@e2eft
Scenario Outline: 070 I Recalculate Time Rate
	Given I search for process 'Time Rate Recalculate' without add button
	And I fill in the time rate calculation required fields
		| StartDate | EndDate | Matter Number Index | Search Value | Search Criteria |
		| {Today}   | {Today} | 0                   | 0            | Equals          |
	And I preview by matter number
		| New Rate |
		| 115.00   |
	Then I submit it
	And I validate submit was successful