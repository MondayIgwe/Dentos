@us
Feature: VerifyChildForms
Verify that CRC - Client Relationship Credit child forms within 'Partner Credit' form has been added
Verify that PMC – Project Management Credit child forms within 'Partner Credit' form has been added
Verify that REC - Relationship Enhancement Credit child forms within 'Partner Credit' form has been added


Scenario Outline: 001 Create Matter
	Given I create the Payer with Api
		| PayerName   | Entity            |
		| <PayorName> | Linear Associates |
	When I create a matter with details:
		| Client   | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | Rate   | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| <Client> | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <Rate> | <ChargeTypeGroup>   | <CostTypeGroup>   | <PayorName> |


Examples:
	| Client                       | Currency        | CurrencyMethod | Office  | Department | Section | Rate     | ChargeTypeGroup | CostTypeGroup | PayorName |
	| Client_Automation at_HTOvMOn | USD - US Dollar | Bill           | Chicago | Default    | Default | Standard | Desc_at_3e8glw7 | Auto__18Xeq   | Dana Tayler |


Scenario Outline: 002 Validate Child Forms
	Given I navigate to the matter maintenance process
	Then the Additional Child Forms should exist


Scenario Outline: 003 Validate the Client Relationship Credit child form values
	Given I navigate to the matter maintenance process
	And I reopen a saved Matter
	When I update the 'Client Relationship Credit' child forms with the details:
		| StartDate |
		| {Today}   |
	And I submit the form
	Then the 'Client Relationship Credit' form values should be saved
	

Scenario Outline: 004 Validate the Project Management Credit child form values
	Given I navigate to the matter maintenance process
	And I reopen a saved Matter
	When I update the 'Project Management Credit' child forms with the details:
		| StartDate |
		| {Today}   |
	And I submit the form
	Then the 'Project Management Credit' form values should be saved


Scenario Outline: 005 Validate the Relationship Enhancement Credit child form values
	Given I navigate to the matter maintenance process
	And I reopen a saved Matter
	When I update the 'Relationship Enhancement Credit' child forms with the details:
		| StartDate |
		| {Today}   |
	And I submit the form
	Then the 'Relationship Enhancement Credit' form values should be saved
