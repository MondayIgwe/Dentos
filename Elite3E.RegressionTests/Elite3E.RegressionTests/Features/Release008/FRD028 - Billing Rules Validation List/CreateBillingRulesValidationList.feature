@release8 @frd028 @CreateBillingRulesValidationList
Feature: CreateBillingRulesValidationList

Process Name : Billing Rules Validation List
Create a new billing Rules validation list and delete the rule from the list

Scenario Outline: 010 Create a new billing rule validation list
	Given I search for process 'Billing Rules Validation List'
	When I add a new billing rules validation with details:
		| Code      | Description |
		| {Auto}+10 | {Auto}+10   |
	And I add billing rules validation list rules with details:
		| Billing Rule  | Warning Check Box | Error Check Box |
		| <BillingRule> | <Warning>         | <Error>         |
	And I submit it
	Then I should be able to validate the new billing rule validation list

@ft @training @staging @canada @europe @uk @singapore @qa
Examples:
	| BillingRule                | Warning        | Error       |
	| Allow Specific Payees Only | IsProformaEdit | IsBillError |

@ft @training @staging @canada @europe @uk @singapore @qa
Scenario: 020 Delete billing rule validation list
	Then I want to delete the it
	And I submit it
