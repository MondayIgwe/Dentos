Feature: BankReconciliationClearingMethod

https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/56310

@e2eft
Scenario: 010 Create Quick Bank Reconsiliation
	Given I search for process 'Quick Bank Reconciliation'
	And I click add
	When I want to add quick reconciliation details
		| Bank Group | Statement Date | Statement Number | Description | Deposits | DepositsOverWrite |
		| 117        | {Today}        | {Auto}+6         | {Auto}+10   | 100.00   | 0                 |
	Then I update it
	And I submit it
	And I validate submit was successful

