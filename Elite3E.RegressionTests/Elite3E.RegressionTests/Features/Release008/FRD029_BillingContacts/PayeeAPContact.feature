@release8 @frd029 @PayeeAPContact
Feature: PayeeAPContact
DEV007 - AP Contacts child form on the Payee Maintenance

@ft @training @staging @canada @europe @uk @singapore @qa
Scenario Outline: 010 Create Payee
	Given I create a new Payee with the Api
		| PayeeName    |
		| PN_TestPayee |

Scenario Outline: 020 Add a new AP Contact
	Given I navigate to the Payee maintenance process
	And I reopen an existing Payee
	And I add a new AP Contact info
		| ContactType   | FirstName | LastName | ContactName | Email    | NewEmail |
		| <ContactType> | {Auto}+4  | {Auto}+5 | {Auto}+5    | {Auto}+5 | {Auto}+6 |
	And I submit the form
	Then the AP details should be saved correctly
	And I submit the form

@ft @uk
Examples:
	| ContactType     |
	| Ap Contact Type |
@canada @singapore
Examples:
	| ContactType           |
	| Supplier Relationship |
@qa
Examples:
	| ContactType                                  |
	| Client Central AP function - Primary Contact |
@training @staging
Examples:
	| ContactType           |
	| Supplier Relationship |
@europe
Examples:
	| ContactType           |
	| Supplier Relationship |