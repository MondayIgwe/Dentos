@us
Feature: AddBankAccountType
	Add new Bank Account Type

Scenario: 010 Add new Bank Account Type
	When I add the bank account type
	| Code    | Description | Intermediary Bank |
	| {Auto6} | {Auto36}    | Yes               |
	Then the bank account type is saved
	And the intermediary bank field is correctly named
