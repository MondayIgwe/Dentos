@release2 @frd018 @DirectRecordSwapAndMultipleRecords
Feature: Direct record swap and multiple records
GL Type and Access process,one record can be set as the default for receipts 

Scenario Outline: 010_Confirm reciept box exist on ALL_BOOK type(Direct swap)
	When I navigate to the GL Type and Access process
	And I select the  "<GLType>" gl type'
	And I confirm and set receipts checkbox
	
@ft @qa @training @staging @canada @europe @uk @singapore
Examples:
	| GLType   |
	| ALL_BOOK |
	
Scenario Outline: 020_Confirm reciept box exist on CHS_Base type(Direct swap)
	When I click on the search button
	And I search for the gl type
		| SearchType   |
		| <SearchType> |
	And I confirm  receipts checkbox exist

@ft @qa
Examples:
	| SearchType |
	| CSH_BASE   |
@training @staging @canada
Examples:
	| SearchType                       |
	| Canada Accrual as per Enterprise |
@uk
Examples:
	| SearchType |
	| CSH_BASE   |
@singapore
Examples:
	| SearchType                       |
	| Singapore Accrual as per Aderant |

@europe
Examples:
	| SearchType |
	| CSH_EU_R   |

Scenario Outline: 030_Multiple records enable checkbox
	When I click on the search button
	And I search for the gl type
		| SearchType   |
		| <SearchType> |
	And I confirm  receipts checkbox exist
	Then click submit

@ft @qa @training @europe @uk
Examples:
	| SearchType |
	| FISCAL     |
@staging
Examples:
	| SearchType |
	| TR_FSUS    |
@canada @singapore
Examples:
	| SearchType |
	| MAN_ADJ    |
	 
Scenario Outline: 040_Confirm both reciept box are updated_tc_1
	When I navigate to the GL Type and Access process
	And I select the  "<GLType>" gl type'
	Then Confirm checkbox enabled
	And click submit

@ft @qa @training @staging @canada @europe @uk @singapore
Examples:
	| GLType   |
	| ALL_BOOK |

Scenario Outline: 050_Confirm both reciept box are updated_tc_2
	When I navigate to the GL Type and Access process
	And I select the  "<GLType>" gl type'
	Then Confirm checkbox disabled
	And click submit

@ft @qa
Examples:
	| GLType   |
	| CSH_BASE |
@training @staging @canada
Examples:
	| GLType                           |
	| Canada Accrual as per Enterprise |
@uk
Examples:
	| GLType   |
	| CSH_BASE |
@europe
Examples:
	| GLType   |
	| CSH_R_EU |
@singapore
Examples:
	| GLType                           |
	| Singapore Accrual as per Aderant |

Scenario Outline: 060_Confirm both reciept box are updated_tc_3
	When I navigate to the GL Type and Access process
	And I select the  "<GLType>" gl type'
	Then Confirm checkbox disabled
	And click submit

@ft @qa @training @europe @uk
Examples:
	| GLType |
	| FISCAL |
@canada @singapore
Examples:
	| GLType  |
	| MAN_ADJ |
@staging
Examples:
	| GLType  |
	| IT_FSUS |

