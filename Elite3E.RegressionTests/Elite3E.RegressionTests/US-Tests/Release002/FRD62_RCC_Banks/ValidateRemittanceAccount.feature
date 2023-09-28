@us
Feature: ValidateRemittanceAccount
	Verify Remittance & Ultimate Remittance Account

Scenario Outline: 010 Try to add a duplicate Ultimate Remittance Account
	Given a bank account office exists "<BankAccountOffice>"


Examples:
	| BankAccountOffice                                |
	| Dentons US, LLP - Application Off-Set Bank - USD |

Scenario Outline: 020 Try to add a duplicate Ultimate Remittance Account
	Given I view the bank account office "<BankAccountOffice>"
	And clone the bank account office
		| Account Name  | Description   | Remittance Account  |
		| <AccountName> | <Description> | <RemittanceAccount> |
	When I try to add a duplicate ultimate remittance account
	Then the message "<ErrorMessage>" is displayed


Examples:
	| AccountName                                       | Description                                       | RemittanceAccount | BankAccountOffice                                | ErrorMessage                                                                  |
	| Dentons US, LLP - Application Off-Set Bank - USD1 | Dentons US, LLP - Application Off-Set Bank - USD1 | No                | Dentons US, LLP - Application Off-Set Bank - USD | There are two or more records with Ultimate Remittance set to True per Office |

Scenario Outline: 030 Try to add duplicate Remittance Account
	When I try to add a duplicate remittance account
	Then the message "<ErrorMessage>" is displayed

Examples:
	| ErrorMessage                                                                                               |
	| There are two or more records with Remittance Account set to True for a combination of Office and Currency |

