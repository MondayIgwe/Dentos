@us
Feature: CreateNewClient

Scenario Outline: 001 Create a new entity
	Given the entity person process is opened
	When I enter the  person entity details
		| Entity Type | First Name | Last Name | Format Code | Relationship |
		| Employee    | {Auto}+6   | {Auto}+6  | Default     | Self         |
	And enter site details
		| Description | Site Type  | Country   | Street   | Language |
		| {Auto}+10   | <SiteType> | <Country> | <Street> | Xhosa    |
	Then I can submit the new entity details
	And I validate submit was successful


Examples:
	| SiteType | Country      | Street                         |
	| Billing  | SOUTH AFRICA | 10 Umhlanga rocks drive,Durban |

Scenario Outline: 020_Create a new client
	Given I create a fee earner with details
		| EntityName         |
		| <OpeningFeeEarner> |
	Given the client maintenace process is opened
	When I select the new entity
	And enter the client details
		| Opening Fee Earner | Date Opened | Status  | Status Date |
		| <OpeningFeeEarner> | {Today}     | Pending | {Today}     |
	Then I can enter the effective dates information an save
		| Billing Fee Earner | Responsible Fee Earner | Supervisor Fee Earner | Office   |
		| <BillingFeeEarner> | <ResponsibleFeeEarner> | <SupervisorFeeEarner> | <Office> |
	And I validate submit was successful


Examples:
	| OpeningFeeEarner      | BillingFeeEarner      | ResponsibleFeeEarner  | SupervisorFeeEarner   | Office  |
	| DentonsAPI FeeEarner3 | DentonsAPI FeeEarner4 | DentonsAPI FeeEarner4 | DentonsAPI FeeEarner4 | Chicago |

Scenario: 030_Edit the client
	When I search for the client
	And edit the client details
		| Date Opened  | Status  | Status Date |
		| {Today}  - 1 | Pending | {Today} -1  |
	Then I can submit the new client details
	And I validate submit was successful

