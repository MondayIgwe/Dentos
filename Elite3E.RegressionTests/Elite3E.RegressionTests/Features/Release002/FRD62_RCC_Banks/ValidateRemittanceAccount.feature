@release2 @frd062 @ValidateRemittanceAccount
Feature: ValidateRemittanceAccount
	Verify Remittance & Ultimate Remittance Account

Scenario Outline: 010 Try to add a duplicate Ultimate Remittance Account
	Given a bank account office exists "<BankAccountOffice>"

@ft @qa
Examples:
	| BankAccountOffice                       |
	| ICB - 1201 - Singapore - ICB Bank - SGD |
@training @staging 
Examples:
	| BankAccountOffice                                |
	| Dentons Europe UK - HSBC - Operating Acc 1 - GBP |
@singapore
Examples:
	| BankAccountOffice                       |
	| ICB - 1201 - Singapore - ICB Bank - SGD |

@uk
Examples:
	| BankAccountOffice           |
	| ALMATY GBP CLEARING ACCOUNT |
@europe
Examples:
	| BankAccountOffice                                                |
	| Dentons Europe (AZ) - Bank of Azerbaijan - Operating Acc 1 - EUR |
@canada
Examples:
	| BankAccountOffice                                                |
	| Dentons Canada Asset Holdings L - Application Off-Set Bank - CAD |

Scenario Outline: 020 Try to add a duplicate Ultimate Remittance Account
	Given I view the bank account office "<BankAccountOffice>"
	And clone the bank account office
		| Account Name  | Description   | Remittance Account  |
		| <AccountName> | <Description> | <RemittanceAccount> |
	When I try to add a duplicate ultimate remittance account
	Then the message "<ErrorMessage>" is displayed

@ft @qa
Examples:
	| AccountName                              | Description                              | RemittanceAccount | BankAccountOffice                       | ErrorMessage                                                                  |
	| ICB - 1201 - Singapore - ICB Bank - SGD1 | ICB - 1201 - Singapore - ICB Bank - SGD1 | No                | ICB - 1201 - Singapore - ICB Bank - SGD | There are two or more records with Ultimate Remittance set to True per Office |
@training @staging 
Examples:
	| AccountName                                       | Description                           | RemittanceAccount | BankAccountOffice                                | ErrorMessage                                                                  |
	| Dentons Europe UK - HSBC - Operating Acc 1 - GBP1 | GBP - HSBC Operating Account 1 - 5851 | No                | Dentons Europe UK - HSBC - Operating Acc 1 - GBP | There are two or more records with Ultimate Remittance set to True per Office |
@singapore
Examples:
	| AccountName                              | Description                              | RemittanceAccount | BankAccountOffice                       | ErrorMessage                                                                  |
	| ICB - 1201 - Singapore - ICB Bank - SGD1 | ICB - 1201 - Singapore - ICB Bank - SGD1 | No                | ICB - 1201 - Singapore - ICB Bank - SGD | There are two or more records with Ultimate Remittance set to True per Office |
@uk
Examples:
	| AccountName                  | Description                  | RemittanceAccount | BankAccountOffice           | ErrorMessage                                                                  |
	| ALMATY GBP CLEARING ACCOUNT1 | ALMATY GBP CLEARING ACCOUNT1 | No                | ALMATY GBP CLEARING ACCOUNT | There are two or more records with Ultimate Remittance set to True per Office |
@europe
Examples:
	| AccountName                                                       | Description                                                       | RemittanceAccount | BankAccountOffice                                                | ErrorMessage                       |
	| Dentons Europe (AZ) - Bank of Azerbaijan - Operating Acc 1 - EUR1 | Dentons Europe (AZ) - Bank of Azerbaijan - Operating Acc 1 - EUR1 | No                | Dentons Europe (AZ) - Bank of Azerbaijan - Operating Acc 1 - EUR | There are still errors in the data |
@canada
Examples:
	| AccountName                            | Description                          | RemittanceAccount | BankAccountOffice                                                | ErrorMessage                                                                  |
	| Calgary - BMO - Operating Acc 1 - CAD1 | CAD - BMO Operating Account 1 - 7131 | No                | Dentons Canada Asset Holdings L - Application Off-Set Bank - CAD | There are two or more records with Ultimate Remittance set to True per Office |

Scenario Outline: 030 Try to add duplicate Remittance Account
	When I try to add a duplicate remittance account
	Then the message "<ErrorMessage>" is displayed

@ft @qa @training @staging  @canada @uk @singapore  
Examples:
	| ErrorMessage                                                                                               |
	| There are two or more records with Remittance Account set to True for a combination of Office and Currency |
@europe
Examples:
	| ErrorMessage                       |
	| There are still errors in the data |
