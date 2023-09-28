@ignore @us
Feature: CollectionDetailsReport

#Defect https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/64226

@CancelProcess
Scenario:Verify Collection Details Report
	Given I search for Collection Details report
	When I run the report using invoice in collection
	| SearchColumn | SearchOperator | SearchValue |
	| Payer Name   | Equals         | <PayerName> |
	Then I verify the payer name '<PayerName>' in the report
	
	Examples: 
	| PayerName                                 |
	| Singapore - Domestic Client - Head office |
