@ignore
#Ignoring this test as there are some steps that require to be incorporated based on the discussion in the below ADO ticket
Feature: BankReconciliation_MatchingMethod

https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/55660
 The reconciliation accountant receives a new monthly bank statement to be uploaded onto 3E. 
 The Reconciliation Accountant sends the unreconciled report to Controller and FD to approve.

 @e2eft
Scenario: 010 Create bank statement
	Given I create a new bank statement
	| BankGroup                             | StatementNumber | StatementDate | Description | ClearDate | BeginningBalance | Deposit | Withdrawal | EndingBalance | DepositReference | WithdrawalReference |
	| UKME - Application Off-Set Bank - GBP | {Auto}+10       | {Today}       | {Auto}+16   | {Today}-1 | 1000.00          | 100.00  | (100.00)   | 1000.00       | {Auto}+8         | {Auto}+8            |
	And I validate submit was successful
	And I locate the bank statement
	Then  I validate the transactions in the bank statement

# Steps to upload bank statement via Biztalk needs to be added here 

@e2eft
Scenario Outline: 020 Reconcile and validate the items
Given I locate the bank statement in Full Bank Reconciliation
When I enter details in full bank reconciliation process with '<WorksheetID>'
And I search for a process 'Bank Reconciled Items Report' and select a pie chart 'Bank Reconciled Items Report'
And I advanced find and verify bank reconciled items report
		| Search Column   | Search Operator | Search Value |
		| Bank Group Name | Equals          | <BankGroup>  |
		| Worksheet ID    | Equals          |              |
		| Statement Date  | Equals          | {Today}      |
	Then  I verify there are no items in unreconciled items report for GL book 'Cash'
Then I close the full bank reconciliation for the bank statement
Examples: 
	| BankGroup                             | WorksheetID |
	| UKME - Application Off-Set Bank - GBP | {Auto}+10   |

