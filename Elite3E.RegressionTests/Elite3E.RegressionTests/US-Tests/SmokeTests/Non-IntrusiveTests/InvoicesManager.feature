Feature: InvoicesManager

R14 - FRD26b - DEV001 - Search by single or multiple office brings back no/incorrect results

@CancelProcess
Scenario: Verify Invoice Manager process
	Given I search for process 'Invoice Manager' without add button
	When I search by a billing office
		| Office   |
		| Aberdeen |
	Then I verify that the results appear

