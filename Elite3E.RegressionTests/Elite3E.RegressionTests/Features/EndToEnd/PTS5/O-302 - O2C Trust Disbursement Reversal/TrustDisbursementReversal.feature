Feature: TrustDisbursementReversal

Fee Earner/Secretary request for the disbursement and payment to be reversed.
Azure: https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/56817


@CancelProcess @e2eft
Scenario Outline: 010 Create Client Account Disbursement
	Given I navigate to the client account disbursement process
	And I add new disbursement
	And I load balance for the client account
		| TransactionDate | DisbursementType | ClientAccountAcct                    | Amount | PaymentName | DocumentNumber | UseDetails | IntendedUse |
		| {Today}-1       | Completion CHQ   | Singapore - SCB Bank Trust Acc - GBP | 1      | {Auto}+10   | {Auto}+11      | {Auto}+10  | General     |
	Then I update it
	When I submit it
	And I validate submit was successful

@CancelProcess @e2eft
Scenario Outline: 020 Create a Cheque Account
	Given I navigate to the client account cheque process
	And I add a new client account cheque
		| ClientAccountAcc                     | ChequeNumber | NameOnCheque | Printer    | Template           |
		| Singapore - SCB Bank Trust Acc - GBP | {Auto}+10    | {Auto}+10    | NO_PRINTER | TE_EDG_Trust_Check |
	And I add new disbursement for the client account cheque
	And I load balance for the client account
		| TransactionDate | DisbursementType | UseDetails | IntendedUse | Amount |
		| {Today}-1       | Completion CHQ   | {Auto}+10  | General     | 1      |
	And I submit it
	When I update it
	And I submit it
	Then I validate submit was successful

@CancelProcess @e2eft
Scenario Outline: 030 Void a Cheque Account
	Given I navigate to the client account cheque process
	And I select an existing cheque
	And I void a cheque
		| VoidReason |
		| Correction |
	When I update it
	And I submit it
	Then I validate submit was successful

@CancelProcess @e2eft
Scenario Outline: 040 Verify Disbursement does not exist
	Given I navigate to the client account disbursement process
	Then I verify that the cheque disbursement does not exist
	
