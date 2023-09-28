
Feature: ManageRateExceptionGroups

A short summary of the feature
Azure link: https://dev.azure.com/dentonsglobal/GFT%203E/_testPlans/execute?view=_TestManagement&planId=12845&suiteId=38475

Unable to validate 050 Validate Matter Notes on  Matter Group Enquiry New Rate does not show on final step
Not showing right results : 030 Get Rate Test


Scenario: 005 Prepare data for Rate Maintenance
	Given I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	And I search or create a client
		| Entity Name | FeeEarnerFullName |
		| <Client>    | <FeeEarnerName>   |
	And I create a matter with details:
		| PayorName   | Client   | Status     | OpenDate   | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | Rate                              | ChargeTypeGroupName | CostTypeGroupName |
		| <PayorName> | <Client> | Fully Open | {Today}-31 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | 12% Prevailing Firm Standard Rate | <ChargeTypeGroup>   | <CostTypeGroup>   |
	And I submit a time modify
		| Time Type  | Hours | Narrative         | FeeEarnerName   | WorkRate | WorkAmount | WorkCurrency | Tax Code  |
		| <TimeType> | 1     | test automation 1 | <FeeEarnerName> | 1        | 1          | <Currency>   | <TaxCode> |
	
@e2eft
Examples:
	|PayorName | FeeEarnerName  | Client                 | Currency            | CurrencyMethod | Office         | Department | Section | ChargeTypeGroup | CostTypeGroup   | TimeType          | TaxCode                    |
	|James May | Rate FeeEarner | Client_Automation Rate | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | TestGroup3      | Desc_at_3e8glw7 | Fixed-Capped Fees | UK Output Domestic Zero 0% |


@e2eft
Scenario: 010 Create new Rate Maintenance
	Given I navigate to the rate maintenance process
	And I add a new rate maintenance record
		| Code      | Description                       | RateType      | Formula  | RateTypeValue |
		| {Auto}+12 | 12% Prevailing Firm Standard Rate | Standard Rate | STD*1.10 | 100           |
	And I test the formula
		| TestResult |
		| 110.00000  |
	When I update it
	Then I submit it
	And I validate submit was successful


@e2eft
Scenario Outline: 020 Rate Exception Group
	Given I search for the client
	And I add new rate exception group
		| Description                       | RateExceptionList | StartDate |
		| 12% Prevailing Firm Standard Rate | Office            | {Today}   |
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

@e2eft
Scenario Outline: 030 Get Rate Test
	When I navigate to the GetRate Test process
	And I fill in the GetRate Test required fields
		| TransactionType | Work Date | FeeEarnerNumber |
		| Fees            | {Today}   |                 |
	And I click Get Rate button
	And I verify that the exception rate is correct
	Then I cancel the process


@e2eft
Scenario Outline: 040 I Recalculate Time Rate
	Given I search for process 'Time Rate Recalculate' without add button
	And I fill in the time rate calculation required fields
		| StartDate | EndDate | Matter Number Index | Search Value | Search Criteria |
		| {Today}   | {Today} | 0                   | 0            | Equals          |
	And I preview by matter number
		| New Rate |
		| 110.00   |
	Then I submit it
	And I validate submit was successful


@e2eft
Scenario: 050 Validate Matter Notes on  Matter Group Enquiry
	Given I search for process 'Matter Group Enquiry' without add button
	When I search in matter group enquiry
		| SearchPhrase | SearchValue |
		| Matter       |             |
	Then I submit it
	And I view 'Time Detail - Date'
	Then I validate rate is correct
		| New Rate |
		| 110.00   |