@us
Feature: CollectionInvoiceMetric

Verify the new custom metric report based on the Collection Invoice Metric (MxCollectionInvoice)

@CancelProcess
Scenario: 010_Verify new custom metric report
	Given I search for 'Collection Invoices Metric(Custom)'
	And I perform quick find for '<Metric>'
	When I run the metric
	And I submit it
	And I search for collection productivity report
	And I run the report
	Then I verify the correct fields are displayed
		| Payer                                  | Field1      | Field2          | Field3 |
		| London - Domestic Client - Head office | Open Amount | Original Amount | Manual |

Examples:
	| Metric                  |
	| MxCollectionInvoice_ccc |

@CancelProcess 
Scenario: 020_Verify Cash Receipts Target
	Given I navigate to cash receipt target
	And I add new cash receipt target
		| Description | Currency            |
		| {Auto+10}   | GBP - British Pound |
	When I update it
	And I submit it
	And I validate submit was successful

@CancelProcess
Scenario: 030_Verify new separate report based on Collection Item
	Given I search for collection item report
	And I run the report
	Then I verify the column headings
		| Field1                  | Field2                           | Field3 | Field4              |
		| Days To Complete Action | Days To Complete Followup Action | Status | Status Changed Date |


