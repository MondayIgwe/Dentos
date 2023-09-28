Feature: TimeKeeper

@CancelProcess @training @staging @canada @europe @uk @singapore @ft @qa
Scenario Outline: 020 Verify the attributes added to advanced find in matter maintenance 
	Given I search for process 'Fee Earner Maintenance'
	Then I verify following attributes in advanced find query
	| FieldName  |
	| Office     |
	| Department |
