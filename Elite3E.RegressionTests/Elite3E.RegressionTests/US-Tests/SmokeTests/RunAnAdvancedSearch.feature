@us
Feature: RunAnAdvancedSearch
	Run an Advanced search

Scenario Outline: 010 Run an Advanced search
	Given I navigate to the matter maintenance process
	When I update the advanced find search terms
		| Matter Display Name Index | Matter Display Text | Client Display Name Index | Client Display Text | Search Criteria |
		| 1                         | Test                | 6                         | <ClientDisplayText> | Contains        |
	Then a reduced number of matters is returned

Examples: 
		| ClientDisplayText |
		| London            |
