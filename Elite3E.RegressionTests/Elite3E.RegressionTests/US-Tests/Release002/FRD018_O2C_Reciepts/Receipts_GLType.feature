@us
Feature: Direct record swap and multiple records
GL Type and Access process,one record can be set as the default for receipts 

Scenario Outline: 010_Confirm reciept box exist on ALL_BOOK type(Direct swap)
	When I navigate to the GL Type and Access process
	And I select the  "<GLType>" gl type'
	And I confirm and set receipts checkbox

Examples:
	| GLType   |
	| ALL_BOOK |
	
Scenario Outline: 020_Confirm reciept box exist on CHS_Base type(Direct swap)
	When I click on the search button
	And I search for the gl type
		| SearchType   |
		| <SearchType> |
	And I confirm  receipts checkbox exist

Examples:
	| SearchType |
	| CSH_US_E   |


Scenario Outline: 030_Multiple records enable checkbox
	When I click on the search button
	And I search for the gl type
		| SearchType   |
		| <SearchType> |
	And I confirm  receipts checkbox exist
	Then click submit

Examples:
	| SearchType |
	| MAN_ADJ    |
	 
Scenario Outline: 040_Confirm both reciept box are updated_tc_1
	When I navigate to the GL Type and Access process
	And I select the  "<GLType>" gl type'
	Then Confirm checkbox enabled
	And click submit

Examples:
	| GLType   |
	| ALL_BOOK |

Scenario Outline: 050_Confirm both reciept box are updated_tc_2
	When I navigate to the GL Type and Access process
	And I select the  "<GLType>" gl type'
	Then Confirm checkbox disabled
	And click submit

Examples:
	| GLType   |
	| CSH_US_E |

Scenario Outline: 060_Confirm both reciept box are updated_tc_3
	When I navigate to the GL Type and Access process
	And I select the  "<GLType>" gl type'
	Then Confirm checkbox disabled
	And click submit

Examples:
	| GLType  |
	| MAN_ADJ |

