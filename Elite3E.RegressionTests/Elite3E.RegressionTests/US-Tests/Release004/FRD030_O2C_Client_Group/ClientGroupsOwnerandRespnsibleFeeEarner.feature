@us
Feature: ClientGroupsSingleOwnerandRespnsibleFeeEarner
	
Scenario Outline:010 Create client group 
	When I open the Client Group process
	And Add a client record					
		| Group Name | Description | Group Type  |
		| {Auto}+10  | {Auto}+10   | <GroupType> |
	And add a new fee earner 
		| Name           |
		| <FeeEarnerOne> |
		| <FeeEarnerTwo> |
	Then submit the client group

Examples: 
		| GroupType        | FeeEarnerOne   | FeeEarnerTwo    |
		| DentonsGlobalLtd | Dentons Brazil | Dentons England |

Scenario:020 Verify owner checkbox error message
		When more than one fee owner error is displayed 
		And I unseelect the owner for 'Dentons England' fee earner
		Then submit the client group

Scenario:030 Verify responsible checkbox error message
	When more than one responsible fee owner earner is displayed 
	And I unseelect the responsible for 'Dentons England' fee earner
	Then submit the client group

Scenario:040 Verify new cleint group exist
	Given The client group process is open
	When I search the client group  
	Then I can view the client group and delete
