@release4 @frd030 @ClientGroupDuplicateClient
Feature: ClientGroupDuplicateClient

Scenario Outline:010 Create client group
	When I open the Client Group process
	And Add a client record					
		| Group Name | Description | Group Type  |
		| {Auto}+10  | {Auto}+10   | <GroupType> |
	And add the first client 
		| Client   |
		| <ClientEntityName> |
	And add a second relevant client with same client name
		| Client   |
		| <ClientEntityName> |
	Then a error message "<ErrorMessage>" is displayed
@ft @training @staging  @canada @europe @uk @singapore @qa
Examples: 
	| FeeEarnerFullName | GroupType        | ClientEntityName      | ErrorMessage                                       |
	| Dentons Brazil    | DentonsGlobalLtd | Automation_Client Two | Duplicate client Automation_Client Two in the list |
