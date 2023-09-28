@us
Feature: ClientGroupsTimekeeper

Scenario Outline:010 Create client group 
	When I open the Client Group process
	And Add a client record					
		| Group Name | Description | Group Type  |
		| {Auto}+10  | {Auto}+10   | <GroupType> |
	And add a new fee earner 
		| Name        | IsResponsible | IsOwner |
		| <FeeEarner> | false         | false   | 
	Then submit the client group

Examples: 
		| GroupType        | FeeEarner      |
		| DentonsGlobalLtd | Dentons Brazil |

Scenario:020 Verify owner checkbox error message
	When the owner checkboox is unchecked an error is displayed
	And I can select the owner checkbox
	Then submit the client group

Scenario:030 Verify responsible checkbox error message
	When the responsible checkbox is unchecked an error is displayed
	And I can select the responsible checkbox
	Then submit the client group

Scenario:040 Verify new cleint group exist
	Given The client group process is open
	When I search the client group  
	Then I can view the client group and delete 	
