@release2 @frd062 @BankAccountsVerifyIdentification
Feature: BankAccountsVerifyIdentification
	Verify Identification field

@CancelProcess @ft @training @staging @canada @europe @uk @singapore   @qa
Scenario: 010 Add Bank Account AP
	When I try to add an account for bank account client account
	Then the account identifier is available

@CancelProcess @ft @training @staging @canada @europe @uk @singapore   @qa
Scenario: 020 Add Bank Account AP
	When I try to add an account for bank account office
	Then the account identifier is available

@CancelProcess @ft @training @staging @canada @europe @uk @singapore   @qa
Scenario: 030 Add Bank Account AP
	When I try to add an account for bank group maintenance
	Then the account identifier is available

@CancelProcess
Scenario Outline: 040 Add Bank Account AP
	When I view bank account client account "<ClientAccount>"
	Then the account identifier is not mandatory on bank account client account

@ft @qa
Examples:
	| ClientAccount                                 |
	| Singapore - OCBC (LnL & DrnD) Trust Acc - SGD |
@staging
Examples:
	| ClientAccount   |
	| Test Automation |
@training
Examples:
	| ClientAccount              |
	| Canada Test Bank Account 1 |
@singapore
Examples:
	| ClientAccount          |
	| SGH-Client-RMB C/A-DBS |

@canada
Examples:
	| ClientAccount                       |
	| Bank of Montreal IBA Trust-1248-814 |
@uk
Examples:
	| ClientAccount                                |
	| C27 London (UKIME)_UK Client Account TBC_AUD |
@europe
Examples:
	| ClientAccount                   |
	| Citibank Europe plc (BUDEURESC) |

@CancelProcess
Scenario Outline: 050 Add Bank Account AP
	When I view bank account office "<BankAccountOffice>"
	Then the account identifier is not mandatory on  bank account office

@ft @qa
Examples:
	| BankAccountOffice                       |
	| ICB - 1201 - Singapore - ICB Bank - SGD |
@training @staging
Examples:
	| BankAccountOffice                                                |
	| Dentons Europe (AZ) - Bank of Azerbaijan - Operating Acc 1 - AZN |
@canada
Examples:
	| BankAccountOffice                                   |
	| Dentons Canada LLP - Application Off-Set Bank - CAD |

@uk
Examples:
	| BankAccountOffice           |
	| ALMATY GBP CLEARING ACCOUNT |
@europe
Examples:
	| BankAccountOffice                                                |
	| Dentons Europe (AZ) - Bank of Azerbaijan - Operating Acc 1 - AZN |
@singapore
Examples:
	| BankAccountOffice          |
	| Singapore -SHG BOC A/C-SGD |
	

@CancelProcess
Scenario Outline: 060 Add Bank Account AP
	When I view bank group maintenance "<BankGroupMaintenance>"
	Then the account identifier is not mandatory on bank group maintenance

@ft @qa
Examples:
	| BankGroupMaintenance         |
	| Paris - Petty Cash Acc - GBP |
@training @staging
Examples:
	| BankGroupMaintenance                                             |
	| Dentons UK and Middle East 3001 - Application Off-Set Bank - CAD |
@canada
Examples:
	| BankGroupMaintenance                |
	| Bank of Montreal IBA Trust-1248-814 |

@uk
Examples:
	| BankGroupMaintenance        |
	| ALMATY GBP CLEARING ACCOUNT |
@europe
Examples:
	| BankGroupMaintenance         |
	| Barclays Euro Chq A/C London |

@singapore
Examples:
	| BankGroupMaintenance                    |
	| ICB - 1201 - Singapore - ICB Bank - SGD |