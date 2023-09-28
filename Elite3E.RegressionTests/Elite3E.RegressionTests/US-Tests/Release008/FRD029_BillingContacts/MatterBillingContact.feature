@us
Feature: MatterBillingContact
DEV004- Billing Contacts Child form on Matter


Scenario Outline: 010 Create Matter
	Given I create the Payer with Api
		| PayerName   | Entity      |
		| <PayorName> | Paragon Ltd |
	When I create a matter with details:
		| Client   | Status     | OpenDate | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| <Client> | Fully Open | {Today}  | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <ChargeTypeGroup>   | <CostTypeGroup>   | <PayorName> |

Examples:
	| Client                       | Currency | CurrencyMethod | Office  | Department | Section | ChargeTypeGroup | CostTypeGroup | PayorName   |
	| Client_Automation at_HTOvMOn | USD      | Bill           | Chicago | Default    | Default | Desc_at_3e8glw7 | Auto__18Xeq   | Carla Bates |

@CancelProcess
Scenario Outline: 020 Add a new Billing Contact
	Given I create the Payer with Api
		| PayerName   | Entity      |
		| <PayerName> | Paragon Ltd |
	And I navigate to the matter maintenance process
	And I reopen a saved Matter
	When I add a Matter Payer
		| Start Date |
		| {Today}+2  |
	And I add a new Billing Contact info
		| ContactType   | FirstName | LastName | ContactName | Email     | NewEmail | Payer Name  |
		| <ContactType> | {Auto}+4  | {Auto}+5 | {Auto}+5    | {Auto}+15 | {Auto}+6 | <PayerName> |
	And I update it
	And I submit the form
	Then the details should be saved correctly on the matter
	And I submit the form

Examples:
	| ContactType               | PayerName   |
	| Billing - Primary Contact | Carla Bates |
