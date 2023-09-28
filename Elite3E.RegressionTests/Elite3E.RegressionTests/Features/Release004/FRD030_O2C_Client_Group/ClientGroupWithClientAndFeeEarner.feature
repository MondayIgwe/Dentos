@release4 @frd030 @ClientGroupWithClientAndFeeEarner
Feature: ClientGroupWithClientAndFeeEarner

Scenario Outline:010 Create client group with client and feeearner
	When I open the Client Group process
	And Add a another client record					
		| Group Name | Description | Group Type  |
		| {Auto}+10  | {Auto}+10   | <GroupType> |
	And add a new fee earner 
		| Name        |
		| <FeeEarner> |
	And add the first client 
		| Client   |
		| <ClientEntityName> |
	Then submit the client group
@ft @training @staging  @canada @europe @uk @singapore @qa
Examples: 
		| GroupType        | FeeEarner      | ClientEntityName      |
		| DentonsGlobalLtd | Dentons Brazil | Automation_Client Two |

@ft @training @staging  @canada @europe @uk @singapore @qa
Scenario:020 Verify new cleint group exist
	Given I search the client group and delete it
