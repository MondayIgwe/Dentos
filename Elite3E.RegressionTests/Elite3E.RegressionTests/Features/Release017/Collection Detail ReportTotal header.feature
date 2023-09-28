Feature: Collection Detail Report Total header

Bug: https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/67283

@ft @qa @training @staging @canada @europe @uk @singapore
Scenario Outline: 010 Collection Detail Report
	Given I search for process 'Collection Detail Report' without add button
	When I run the report
	Then I verify the collection total header in the report
	
Examples:
	| PayerName                                 |
	| Singapore - Domestic Client - Head office |
	