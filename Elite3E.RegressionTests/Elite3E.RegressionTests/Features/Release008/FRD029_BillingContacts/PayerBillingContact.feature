@release8 @frd029 @PayerBillingContact
Feature: PayerBillingContact
DEV003 - Billing Contacts child form on Payer


@ft @training @staging  @canada @europe @uk @singapore @qa
Scenario Outline: 010 Create Payer
	Given I create the Payer with Api
		| PayerName    | Entity        |
		| PN_TestPayee | PN_TestEntity |

#Europe did not have a contact type that met the criteria. One is being created for it.
Scenario Outline: 015 Create Contact Type with IsCentralBilling_ccc
	Given I navigate to the Contact Type process
	Then I add a new Contact Type record with all the details
		| Code     | Description   | Checkboxes           |
		| {Auto}+6 | <ContactType> | IsCentralBilling_ccc |
	And I submit the form
	And I validate submit was successful

@europe @singapore
Examples:
	| ContactType                 |
	| FRD029 Billing Contact Type |

Scenario Outline: 020 Add a new Billing Contact
	Given I reopen an existing Payer
	And I add a new Central Billing Contact info
		| ContactType   | FirstName | LastName | Email    |
		| <ContactType> | {Auto}+4  | {Auto}+5 | {Auto}+5 |
	And I submit the form
	And I validate submit was successful
	Then the details should be saved correctly
	And I submit the form

@ft @training @staging @canada @qa
Examples:
	| ContactType                                  |
	| Client Central AP function - Primary Contact |

@singapore @europe
Examples:
	| ContactType                 |
	| FRD029 Billing Contact Type |
@uk
Examples:
	| ContactType     |
	| Ap Contact Type |

