@us
Feature: CreateClientGroups

Scenario Outline:010 Create client group 					
	When I open the Client Group process					
	And Add a client record					
		| Group Name | Description | Group Type  |
		| {Auto}+10  | {Auto}+10   | <GroupType> |  	
	And add a new fee earner 
		| Name           | IsResponsible | IsOwner |
		| <FeeEarnerOne> | true          | true    |
		| <FeeEarnerTwo> | false         | false   |
	Then submit the client group

Examples: 
		| GroupType        | FeeEarnerOne   | FeeEarnerTwo    |
		| DentonsGlobalLtd | Dentons Brazil | Dentons England |

Scenario:020 Verify new cleint group exist					
	Given The client group process is open					
	When I search the client group  					
	Then I can view the client group and delete
