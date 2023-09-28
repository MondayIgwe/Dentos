Feature: AmendSupplierRecord_VMS

https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/57434
Please refer to comments section in the ADO when revisiting the test

@CancelProcess @e2eft
Scenario: 001 Create Payee
	Given I create a new Payee with the Api 
		| PayeeName      |
		| Payee_Supplier |
		Given I navigate to the Payee maintenance process
		And I reopen an existing Payee
		When add the payee bank
		| DefaultBank   | Description           | Account Number  | Beneficiary Name  | Bank Code  | Address 1  | Address 2  | Address 3  | Clearing Code Type | Clearing Code  | Payment Reference  |
		| <DefaultBank> | Automation payee bank | <AccountNumber> | <BeneficiaryName> | <BankCode> | <Address1> | <Address2> | <Address3> | <ClearingCodeType> | <ClearingCode> | <PaymentReference> |
		Then I submit it
@e2eft
Examples:
	| DefaultBank | AccountNumber | BeneficiaryName        | BankCode | Address1   | Address2 | Address3 | ClearingCodeType | ClearingCode | PaymentReference |
	| Yes         | 8759686       | Automation Beneficiary | 366569   | 21 Bank St | Adelaide | SA 5000  | CTC Cash         | Cash         | PR-987654        |

		
		Scenario Outline: 002 Amend payee bank in payee maintenance
		Given I navigate to the Payee maintenance process
		And I reopen an existing Payee
		When update the payee bank
		| Description     | Account Number  |
		| Test payee bank | <AccountNumber> | 
		Then I submit it
@e2eft
Examples:
	| AccountNumber | 
	| 9359610       |

	@e2eft @CancelProcess
	Scenario: 003 Verify the change supplier report
	 Given I search for process 'AU Supplier Data Audit Report' without add button
	When I run the report searching by payee number
	| SearchColumn |
	| Payee Number |
	Then I verify the AU supplier data audit report values
