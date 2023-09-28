@us
Feature: Payer

@CancelProcess
Scenario Outline: 010 Verify that Payer Quick find has the correct query attributes
	Given I search for process 'Payer'
	Then I verify following attributes in advanced find query
	| FieldName                |
	| Payor Unit (Description) |
