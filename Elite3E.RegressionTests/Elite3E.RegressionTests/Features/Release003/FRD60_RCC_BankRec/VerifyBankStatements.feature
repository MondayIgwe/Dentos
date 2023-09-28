@release3 @frd060 @VerifyBankStatements
Feature: VerifyBankStatements
	Verify the comments column in the various Bank Statements

Scenario Outline: 010 Full Bank Reconcilliation
	Given I view the full bank reconcilliation for "<BankGroup>"
	When I view bank/cash unreconciled transactions
	Then recon comments column exists

@ft @qa
Examples:
	| BankGroup                    |
	| Sydney - WBC Off 1 Acc - AUD |
#@training @staging  @canada @europe @uk @singapore  
#Examples:
#	| BankGroup                    |
#	| Sydney - WBC Off 1 Acc - AUD |


Scenario Outline: 020 FBR Unreconciled Items Report
	Given I view the fbr unreconciled items report
	When I set the fbr unreconciled items report
		| Bank Group  | Bank Statement  | GL Book  |
		| <BankGroup> | <BankStatement> | <GLBook> |
	And run the report
	Then reconciliation comments column exists on the report
@ft @qa
Examples:
	| BankGroup                    | BankStatement | GLBook                |
	| Sydney - WBC Off 1 Acc - AUD | 4             | Consolidated Accounts |
#@training @staging  @canada @europe @uk @singapore  
#Examples:
#	| BankGroup                    | BankStatement | GLBook                |
#	| Sydney - WBC Off 1 Acc - AUD | 2             | Consolidated Accounts |


Scenario Outline: 030 FBR Reconciled Items Report
	Given I view the fbr Reconciled items report
	When I set the fbr reconciled items report
		| Bank Group  | Bank Statement  | Work Sheet Id | Reconciled As Of Date |
		| <BankGroup> | <BankStatement> | <WorkSheetId> | {Today}               |
	And run the report for reconciled items
	Then reconciliation comments column exists on the report
@ft @qa
Examples:
	| BankGroup                    | BankStatement | WorkSheetId |
	| Sydney - WBC Off 1 Acc - AUD | 4             | 12092020SPM |
#@training @staging  @canada @europe @uk @singapore  
#Examples:
#	| BankGroup                    | BankStatement | WorkSheetId |
#	| Sydney - WBC Off 1 Acc - AUD | 1             | 4-9/29/2021 |


@ft @qa
Scenario: 040 FBR Reconciled Items Report
	When I try to add a bank group maintenance
	Then the bank reconciliation frequency available are
		| Frequency |
		| Daily     |
		| Monthly   |
		| Weekly    |
