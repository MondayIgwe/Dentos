Feature: UpdateRates

A short summary of the feature
The tests fails due to input data for  Fee Earner Rate Load and Profitability Cost Rate Load
030 I want to Update Rate is not displaying correct results 
Azure link: https://dev.azure.com/dentonsglobal/GFT%203E/_testPlans/execute?view=_TestManagement&planId=12845&suiteId=38475

@e2eft
Scenario: 005 Data Prep Create new Rate
	Given I navigate to the rate maintenance process
	And I add a new rate maintenance record
		| Code      | Description                  | RateType      | Formula    | RateTypeValue |
		| {Auto}+12 | 12% Automation Standard Rate | Standard Rate | STD/100*12 | 100           |
	And I test the formula
		| TestResult |
		| 12.00000   |
	When I update it
	Then I submit it
	And I validate submit was successful


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

@e2eft
Scenario: 020 Fee Earner Rate Load and Profitability Cost Rate Load
Given I navigate fee earner rate load and profitability cost rate load
	Given I upload 'search.csv'
	Then I click the 'Validate' button.
	And I submit it
	Then I validate submit was successful


@e2eft
Scenario Outline: 030 I want to Update Rate
	Given I navigate to the rate maintenance process
	And I search for the rate
	Then I update rate type value '100' 
	And I test the formula
		| TestResult |
		| 12.00000   |
	When I update it
	Then I submit it
	And I validate submit was successful


@e2eft
Scenario Outline: 040 I Recalculate Time Rate
	Given I search for process 'Time Rate Recalculate' without add button
	And I fill in the time rate calculation required fields
		| StartDate | EndDate | Matter Number Index | Search Value | Search Criteria |
		| {Today}   | {Today} | 0                   | 0            | Equals          |
	And I preview by matter number
		| New Rate |
		| 115.00   |
	Then I submit it
	And I validate submit was successful

