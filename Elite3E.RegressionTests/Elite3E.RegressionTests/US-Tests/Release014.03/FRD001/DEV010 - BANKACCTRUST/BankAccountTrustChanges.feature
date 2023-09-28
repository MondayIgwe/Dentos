@us
Feature: BankAccountTrustChanges


Verify that within BANKACCTRUST quick find Include 'Contains' Fractional Routing Number
Verify that Currency has been added for Query Attributes within BankACCTRUST
Azure: https://dev.azure.com/dentonsglobal/GFT%203E/_testPlans/execute?planId=78404&suiteId=78528

@CancelProcess
Scenario Outline: 010 Verify that within BANKACCTRUST quick find Include 'Contains' Fractional Routing Number
	Given I navigate to bank account client account
	Then I verify that the quick find include 'Contains Fractional Routing Number'
	
@CancelProcess
Scenario Outline: 020 Verify that Currency has been added for Query Attributes within BankACCTRUST
	Given I navigate to bank account client account
	Then I verify the 'Currency (Currency Code)' field exists on the advanced find


