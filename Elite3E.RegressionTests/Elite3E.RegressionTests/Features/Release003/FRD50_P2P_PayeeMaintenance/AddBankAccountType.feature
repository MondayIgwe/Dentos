@release3 @frd050 @AddBankAccountType
Feature: AddBankAccountType
	Add new Bank Account Type

@ft @qa @training @staging @canada @europe @uk @singapore
Scenario: 010 Add new Bank Account Type
	When I add the bank account type
		| Code      | Description | Intermediary Bank |
		| {Auto}+13 | {Auto}+36   | Yes               |
	And I validate submit was successful
	Then the bank account type is saved
	And the intermediary bank field is correctly named
