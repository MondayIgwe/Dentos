@release3 @frd050 @AddPayee
Feature: AddPayee
	Add Payee and verify the Bank customisation

@ft @training @staging @canada @europe @uk @singapore @qa
Scenario Outline: 001_Vendor Maintenance
	Given I create a new Vendor with the Api

@DeleteEntity
Scenario Outline: 002 Add new Bank Account Type
	Given I add the payee from payee maintenance
		| Payment Terms  | Office   | Site   | PayeeType   |
		| <PaymentTerms> | <Office> | <Site> | <PayeeType> |

@ft @qa @training @canada @uk
Examples:
	| PaymentTerms      | Office  | Site                                           | PayeeType |
	| 28th of the Month | Default | London - Domestic - Non Vatable Local Currency | Vendors   |
@staging
Examples:
	| PaymentTerms      | Office  | Site                                           | PayeeType    |
	| 28th of the Month | Default | London - Domestic - Non Vatable Local Currency | Client Payee |
@europe @singapore
Examples:
	| PaymentTerms      | Office  | Site                                           | PayeeType    |
	| 28th of the Month | Default | London - Domestic - Non Vatable Local Currency | Client Payee |

Scenario Outline: 003 Add payee bank
	When add the payee bank
		| DefaultBank   | Description           | Account Number  | Beneficiary Name  | Bank Code  | Address 1  | Address 2  | Address 3  | Clearing Code Type | Clearing Code  | Payment Reference  |
		| <DefaultBank> | Automation payee bank | <AccountNumber> | <BeneficiaryName> | <BankCode> | <Address1> | <Address2> | <Address3> | <ClearingCodeType> | <ClearingCode> | <PaymentReference> |

@ft @qa @training @staging @canada @europe @uk @singapore
Examples:
	| DefaultBank | AccountNumber | BeneficiaryName        | BankCode | Address1   | Address2 | Address3 | ClearingCodeType | ClearingCode | PaymentReference |
	| Yes         | 8759686       | Automation Beneficiary | 366569   | 21 Bank St | Adelaide | SA 5000  | CTC Cash         | Cash         | PR-987654        |

Scenario Outline: 004 Add intermediary bank
	When add the intermediary bank
		| IB Address 1 | IB Address 2 | IB Address 3 | IB Clearing Code Type | IB Clearing Code | Swift Code  | Pay Now Registration Number |
		| <IBAddress1> | <IBAddress2> | <IBAddress3> | <IBClearingCodeType>  | <IBClearingCode> | <SwiftCode> | <PayNowRegistrationNumber>  |
	And submit the vendor/payee
	And I validate submit was successful

@ft @qa @training @staging @canada @europe @uk @singapore
Examples:
	| IBAddress1 | IBAddress2 | IBAddress3 | IBClearingCodeType | IBClearingCode | SwiftCode | PayNowRegistrationNumber |
	| 22 Bank St | Adelaide   | SA 5000    | CTC Cash1          | Cash1          | 020504    | 0123875                  |

@ft @qa @training @staging @canada @europe @uk @singapore
Scenario Outline: 005 Search the payee and verify bank details
	When I search the payee maintenance
	Then payee bank is saved
	And intermediary bank is saved
