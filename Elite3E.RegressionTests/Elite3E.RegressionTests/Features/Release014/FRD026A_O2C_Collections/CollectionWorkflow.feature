
Feature: CollectionsCollectionWorkflow

A short summary of the feature

@ft @singapore
Scenario:010 Validate if Include Invoice Attachment check exists in Collection Workflow
	Given I search for process 'Collection Workflows'
	When I search for 'Standard' workflow 
	Then I validate 'Include Invoice Attachment' checkbox is available
	And validate if Include Invoice Attachment checkbox check box is editable
	And I cancel the process
