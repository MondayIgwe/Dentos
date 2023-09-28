@ignore
Feature: DEV008 - Statutory Account in advanced search

Defect: https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/67948

Verify Statutory Account field in GL Account

https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/61208
https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/61209

@CancelProcess @ft @training @staging @qa @uk @europe @canada @singapore
Scenario: 010 Verify statutory account in GL Account
	Given I search for process 'GL Account'
	Then I verify statutory account in advanced find
		| Search Column                      | Search Operator | Search Value |
		| Statutory Account (Account Number) | Equals          | 1234567      |