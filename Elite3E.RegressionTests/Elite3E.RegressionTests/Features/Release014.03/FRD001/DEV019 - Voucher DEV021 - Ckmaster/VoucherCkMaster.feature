Feature: VoucherCkMaster

Azure: https://dev.azure.com/dentonsglobal/GFT%203E/_testPlans/execute?planId=78404&suiteId=79046
Azure: https://dev.azure.com/dentonsglobal/GFT%203E/_testPlans/execute?planId=78404&suiteId=79047

@CancelProcess @ft
Scenario Outline: 010 Verify that for Voucher Quick find 'Payee Name' operator has been changed to 'Contains'
	Given I navigate to the voucher maintenance process
	Then I verify that the quick find include 'Contains Payee Name'


@CancelProcess @ft
Scenario Outline: 020 Verify that  for CKMASTER Query Attributes 'Bank Account Name' has been added
	Given I navigate to the cheque maintenance process
	Then I verify the 'Bank Account Name (BankAcctName)' field exists on the advanced find
