Feature: CollectionItemNewFields

Verify that new eBH invoice status fields are available for MatterCollectionInvoiceForm and Grid

@ft @CancelProcess
Scenario Outline: 010 Verify custom fields
	Given I search for process 'Collection Items' without add button
	And I select an existing client in collection item
		| Client                         |
		| Dentons UK and Middle East LLP |
	Then I verify ebh fields are present in form view
	And I verify ebh fields are present in grid view
	
