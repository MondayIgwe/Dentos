Feature: Close inactive client

A number of clients have been inactive for 3 years and need to be closed. 
There are no open matters on these clients.
Azure link: https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/41526

@CancelProcess
Scenario Outline: 001 Create a new entity
	Given the entity person process is opened
	When I enter the  person entity details
		| Entity Type | First Name | Last Name | Format Code | Relationship |
		| Employee    | {Auto}+6   | {Auto}+6  | Default     | Self         |
	And enter site details
		| Description | Site Type  | Country   | Street   | Language |
		| {Auto}+10   | <SiteType> | <Country> | <Street> | Xhosa    |
	Then I can submit the new entity details

@e2eft
Examples:
	| SiteType | Country      | Street                         |
	| Billing  | SOUTH AFRICA | 10 Umhlanga rocks drive,Durban |

@CancelProcess
Scenario Outline: 020 Create a new deactivated client
	Given the client maintenace process is opened
	When I select the new entity
	And enter the client details
		| Opening Fee Earner | Date Opened | Status             | Status Date |
		| <OpeningFeeEarner> | {Today}     | Deactivated Client | {Today}     |
	Then I can enter the effective dates information an save
		| Billing Fee Earner | Responsible Fee Earner | Supervisor Fee Earner | Office   |
		| <BillingFeeEarner> | <ResponsibleFeeEarner> | <SupervisorFeeEarner> | <Office> |
@e2eft
Examples:
	| OpeningFeeEarner | BillingFeeEarner | ResponsibleFeeEarner | SupervisorFeeEarner | Office          |
	| 100229           | 100259           | 100259               | 100259              | Brussels (UKME) |

@CancelProcess @e2eft
Scenario Outline: 030 Modify a deactivated client
	Given I search for the client
	When I change the status of the client
		| Status       |
		| Fully Closed |
	And I submit it
	And I validate submit was successful

@CancelProcess @e2eft
Scenario Outline: 040 Verify the Client status
	Given I search for the client
	Then I validate the status was updated