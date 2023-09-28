@release3 @frd050 @AddVendorPayee
Feature: AddVendorPayee
	Add Vendor/Payee and verify the Bank customisation

@DeleteEntity
Scenario Outline: 001 Add new Bank Account Type
	Given I add the vendor in vendor/payee maintenance
		| Entity   | Global Vendor  |
		| <Entity> | <GlobalVendor> |
	And add the payee for the vendor
		| Payment Terms  | Office   |
		| <PaymentTerms> | <Office> |

@ft @qa
Examples:
	| Entity                  | GlobalVendor                 | PaymentTerms      | Office  |
	| National Australia Bank | Dentons Client Global Vendor | 28th of the Month | Default |
@canada
Examples:
	| Entity            | GlobalVendor       | PaymentTerms      | Office  |
	| Bank of  Montreal | Amex Global Vendor | 28th of the Month | Default |
@training
Examples:
	| Entity                  | GlobalVendor    | PaymentTerms      | Office  |
	| National Australia Bank | GlobalVendor_UK | 28th of the Month | Default |
@staging
Examples:
	| Entity                  | GlobalVendor       | PaymentTerms      | Office  |
	| National Australia Bank | Amex Global Vendor | 28th of the Month | Default |
@uk
Examples:
	| Entity                  | GlobalVendor  | PaymentTerms      | Office  |
	| National Australia Bank | Global Vendor | 28th of the Month | Default |
@europe
Examples:
	| Entity          | GlobalVendor    | PaymentTerms      | Office  |
	| AIG EUROPE S.A. | GlobalVendor_UK | 28th of the Month | Default |
@singapore
Examples:
	| Entity           | GlobalVendor       | PaymentTerms      | Office  |
	| Jet Quay Pte Ltd | Amex Global Vendor | 28th of the Month | Default |

Scenario Outline: 002 Add payee bank
	When add the payee bank
		| DefaultBank   | Description           | Account Number  | Beneficiary Name  | Bank Code  | Address 1  | Address 2  | Address 3  | Clearing Code Type | Clearing Code  | Payment Reference  |
		| <DefaultBank> | Automation payee bank | <AccountNumber> | <BeneficiaryName> | <BankCode> | <Address1> | <Address2> | <Address3> | <ClearingCodeType> | <ClearingCode> | <PaymentReference> |

@ft @training @staging @canada @europe @uk @singapore @qa
Examples:
	| DefaultBank | AccountNumber | BeneficiaryName        | BankCode | Address1   | Address2 | Address3 | ClearingCodeType | ClearingCode | PaymentReference |
	| Yes         | 8759686       | Automation Beneficiary | 366569   | 21 Bank St | Adelaide | SA 5000  | CTC Cash         | Cash         | PR-987654        |

Scenario Outline: 003 Add intermediary bank
	When add the intermediary bank
		| IB Address 1 | IB Address 2 | IB Address 3 | IB Clearing Code Type | IB Clearing Code | Swift Code  | Pay Now Registration Number |
		| <IBAddress1> | <IBAddress2> | <IBAddress3> | <IBClearingCodeType>  | <IBClearingCode> | <SwiftCode> | <PayNowRegistrationNumber>  |
	And I submit payee

@ft @training @staging @canada @europe @uk @singapore @qa
Examples:
	| IBAddress1 | IBAddress2 | IBAddress3 | IBClearingCodeType | IBClearingCode | SwiftCode | PayNowRegistrationNumber |
	| 22 Bank St | Adelaide   | SA 5000    | CTC Cash1          | Cash1          | 020504    | 0123875                  |

@ft @qa @training @staging @canada @europe @uk @singapore
Scenario: 004 Search the payee and verify bank details
	When I search the payee maintenance
	Then payee bank is saved
	And intermediary bank is saved
