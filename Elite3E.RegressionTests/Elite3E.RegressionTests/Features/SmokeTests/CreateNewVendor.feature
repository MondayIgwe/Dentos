@smoke @CreateNewVendor
Feature: CreateNewVendor

Scenario Outline: 001 Create a new entity
	Given the entity person process is opened
	When I enter the  person entity details
		| Entity Type | First Name | Last Name | Format Code | Relationship |
		| Employee    | {Auto}+10  | {Auto}+10 | Default     | Self         |
	And enter site details
		| Description | Site Type  | Country   | Street   | Language |
		| {Auto}+36   | <SiteType> | <Country> | <Street> | English  |
	Then I can submit the new entity details
	And I validate submit was successful

@ft @qa @training @staging @canada @europe @uk @singapore
Examples:
	| SiteType | Country      | Street                         |
	| Billing  | SOUTH AFRICA | 10 Umhlanga rocks drive,Durban |

Scenario Outline: 020_Create a new vendor
	Given the vendor maintenace process is opened
	When I select the new entity
	And set the global vendor "<GlobalVendor>"
	Then I can submit the new vendor details
	And I validate submit was successful

@ft @qa @training  @uk
Examples:
	| GlobalVendor                 |
	| Dentons Client Global Vendor |
@canada @staging
Examples:
	| GlobalVendor       |
	| Amex Global Vendor |
@singapore @europe
Examples:
	| GlobalVendor    |
	| GlobalVendor_UK |

@CancelProcess @training @staging @canada @europe @uk @singapore @ft @qa
Scenario: 030_Search the vendor
	When I search for the vendor
	Then I can confirm the vendor is created
