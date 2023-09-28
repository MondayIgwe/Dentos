@us
Feature: BillingContanctType
DEV001 - Billing Contact Type 


Scenario Outline: 010 Validate that the Billing Contact Type process is now named Contact Type process
	Given I navigate to the Contact Type process
	Then the Contact Type process should exist along with the fields

Scenario Outline: 020 Validate that the Contact Type fields exists
	Given I navigate to the Contact Type process
	Then I add a new Contact Type record with all the details
		| Code   | Description   | Checkboxes               |
		| <Code> | <Description> | IsPrimary, IsBilling_ccc |
	And I submit the form
	When I reopen the Contact Type record the information is stored correctly

Examples:
	| Code     | Description |
	| {Auto}+6 | {Auto}+10   |
