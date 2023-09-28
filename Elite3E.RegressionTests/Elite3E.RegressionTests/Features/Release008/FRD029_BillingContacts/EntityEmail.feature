@release8 @frd029 @EntityEmail
Feature: EntityEmail
DEV002 - Entity Email 

@ft @training @staging  @canada @europe @uk @singapore @qa
Scenario Outline: 010 Create Person Entity
	Given I create a person entity
		| FirstName | LastName  |
		| {Auto}+10 | {Auto}+10 |

@ft @training @staging  @canada @europe @uk @singapore @qa
Scenario Outline: 020 Validate that the Contact Email Child form exists
	Given I navigate to the Entity Person process
	Then the Contact Email child form should exist along with the fields

Scenario Outline: 030 Validate that the Contact Email record can be added
	Given I navigate to the Entity Person process
	When I open an existing entity person record
	When I add a new Contact Email child form record
		| Email   | EmailDescription | StartDate   | EndDate   | IsEmailDefault |
		| <Email> | <Description>    | <StartDate> | <EndDate> | true           |
	And I submit the form
	And I validate submit was successful
	Then the Contact Email record should be saved
	And I submit the form

@ft @training @staging  @canada @europe @uk @singapore @qa
Examples:
	| Email    | Description | StartDate | EndDate   |
	| {Auto}+8 | {Auto}+10   | {Today}-2 | {Today}+1 |






