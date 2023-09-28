@us
Feature: BankAccountsVerifyIdentification
	Verify Identification field

Scenario: 010 Add Bank Account AP
	When I try to add an account for bank account client account
	Then the account identifier is available


Scenario: 020 Add Bank Account AP
	When I try to add an account for bank account office
	Then the account identifier is available

Scenario: 030 Add Bank Account AP
	When I try to add an account for bank group maintenance
	Then the account identifier is available

Scenario Outline: 040 Add Bank Account AP
	When I view bank account client account "<ClientAccount>"
	Then the account identifier is not mandatory on bank account client account

Examples:
	| ClientAccount        |
	| LAMMERS BARREL GROUP |



Scenario Outline: 050 Add Bank Account AP
	When I view bank account office "<BankAccountOffice>"
	Then the account identifier is not mandatory on  bank account office


@us
Examples:
	| BankAccountOffice                                |
	| Dentons US, LLP - Application Off-Set Bank - USD |


Scenario Outline: 060 Add Bank Account AP
	When I view bank group maintenance "<BankGroupMaintenance>"
	Then the account identifier is not mandatory on bank group maintenance

Examples:
	| BankGroupMaintenance                             |
	| Dentons US, LLP - Application Off-Set Bank - USD |
