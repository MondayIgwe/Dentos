@smoke @EntityPerson
Feature: EntityPerson

@PopulateEntityDataSet1 @LoginAsAdminUser1
Scenario Outline: 010 Create a new entity
	Given  the entity person process is opened
	When I enter the entity details
		| Entity Type | First Name | Last Name | Format Code | Relationship | Description | Site Type  | Country   | Street   | Language |
		| Contact     | {Auto}+10  | {Auto}+10 | Full Name   | Self         | {Auto}+10   | <SiteType> | <Country> | <Street> | English  |
	Then I can submit the new entity details
	And I validate submit was successful

@ft @qa @training @staging  @canada @europe @uk @singapore @PipelineSuccessfulTest
Examples:
	| SiteType | Country      | Street                         |
	| Billing  | SOUTH AFRICA | 10 Umhlanga rocks drive,Durban |

@CancelProxy @ft @training @staging  @canada @europe @uk @singapore @qa
Scenario: 020 validate the entity created
	Then the entity details are saved correctly	