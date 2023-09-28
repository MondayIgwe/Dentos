Feature: PayeeWithTaxCertificateDate

@ft @training @staging  @canada @europe @uk @singapore @us @qa
Scenario Outline: 010 Create Payee
	Given I create a new Payee with the Api
		| PayeeName    |
		| PN_TestPayee |
@ft @training @staging  @canada @europe @uk @singapore @us @qa
Scenario Outline: 020 Add  tax certification date to the payee
	Given I navigate to the Payee maintenance process
	And I reopen an existing Payee
	And I add tax certification date to the payee
	| TaxCertificateDate |
	| {Today}            |
	And I submit the form
