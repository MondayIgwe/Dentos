@us
Feature: ClientMaintenanceGroups

Scenario Outline: 010 Create client group
	Given I open the Client Group process
	When Add a client record
		| Group Name | Description | Group Type  |
		| {Auto}+10  | {Auto}+10   | <GroupType> |
	And add a new fee earner
		| Name        |
		| <FeeEarner> |
	And add the first client
		| Client                |
		| Automation_Client Two |
	And add a second client
		| Client                  |
		| Automation_Client Three |
	Then submit the client group

Examples:
	| GroupType        | FeeEarner      |
	| DentonsGlobalLtd | Dentons Brazil |

Scenario: 020 Client maintenance process
	Given I open the Client maintenance process
	When search for the first client
	And open the client child form
	And delete the client group for the client
	Then I can submit the Client Group type
	
Scenario: 030 Confirming client details updated in client group process
	Given The client group process is open
	When I search the client group
	And I can verify the client  updates