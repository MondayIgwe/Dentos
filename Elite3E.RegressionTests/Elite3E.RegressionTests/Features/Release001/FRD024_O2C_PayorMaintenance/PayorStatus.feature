@release1 @frd024 @PayorStatus
Feature: PayorStatus
	Verify the payor Status values

@CancelProcess
Scenario Outline: 010 Verify Payor Status
	Given I search for process '<Process>'
	When I view the payor status
	Then it should have the status values
		| Code   | Description   |
		| <Code1> | <Description1> |
		| <Code2> | <Description2> |
		| <Code3> | <Description3> |

@ft @qa @training @staging  @canada @europe @uk @singapore @us
Examples:
	| Process      | Code1 | Code2 | Code3   | Description1 | Description2 | Description3    |
	| Payor Status | UNAPP | APP   | UPD_PEN | Unapproved   | Approved     | Updates Pending |


