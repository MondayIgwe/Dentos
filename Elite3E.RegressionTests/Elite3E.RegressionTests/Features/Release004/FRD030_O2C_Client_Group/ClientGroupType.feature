@release4 @frd030 @ClientGroupType
Feature: ClientGroupType
	Client Group Types can be created and modified using the Client Group Type process.

Scenario:  010 Create client group type				
     When I open the Client Group Type	
	 And Add a new record
	 And add the required fields
		 | Code      | Description | Group Type  |
		 | {Auto}+10 | {Auto}+10   | <GroupType> |		
	 And  validate the active box is checked				
	 Then I can submit the Client Group type 	
@training @staging  @canada @europe @uk @singapore @ft @qa
	Examples: 
	| GroupType        |
	| DentonsGlobalLtd |

@training @staging  @canada @europe @uk @singapore @ft @qa		
Scenario:  020 Verify new cleint group exist				
	Given The client group type process is open				
	When I search the client group type 				
	Then I can view the client group and delete
