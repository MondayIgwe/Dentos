@us
Feature: MatterMaintenance


@CancelProcess
Scenario Outline: 010 Verify that for INVMASTER Quick find has been changed to Contains Invoice Number rather than Equals
Given I search for process 'Matter Maintenance'
Then I verify that the quick find include 'Contains Matter Name, Begins With Description,  or Equals Open Date'


@CancelProcess
Scenario Outline: 020 Verify the attributes added to advanced find in matter maintenance 
	Given I search for process 'Matter Maintenance'
	Then I verify following attributes in advanced find query
	| FieldName                     |
	| Open Date                     |
	| Matter Billing Fee-earner     |
	| Matter Supervising Fee Earner |
	| Matter Office                 |
	| Currency Code                 |
	And I verify the query result attributes
	| FieldName                     |
	| MATTER SUPERVISING FEE EARNER |
	| OFFICE                        |
	| DEPARTMENT                    |
	| ARRANGEMENT                   |
	| NON BILLABLE                  |