@us
Feature: Client Account Intended Use

Scenario Outline: 001_Create Client Account Intended Use
	Given I create a new ClientAccount Intended Use APi
		| Code   | Description   |
		| <Code> | <Description> |

Examples:
	| Code            | Description     |
	| Code_at_TowqCrR | Desc_at_YowqCrR |

Scenario Outline: 002_Client Account Intended Use Allow for Billing
	When I open the client account intended use process
	And search client account intended use
	Then I can set the allow for billing
	And I can set the the allow disbursements


Scenario: 003_Client Account Intended Use Allow Disbursements
	Then I can submit the record


Scenario Outline: 004_Confirm Updates
	When I open the client account intended use process
	And search client account intended use
	Then I can verify that all the checkbox selected


