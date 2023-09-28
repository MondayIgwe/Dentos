@release2 @frd045 @CreateChargeTypeGroup
Feature: CreateChargeTypeGroup
	Test 1: The Charge Type Group process allow groups to be created and edited.
	Test 2: The Charge Type Group process allows charge type to be added to group.
	Test 3: The Charge Type Group process allows more than one charge type to be added to the group.
	Test 4: The Charge Type Group process does not allow for the duplicate charge type to be added to the group.
	Test 5: The Charge Type Group process allows charge type to be deleted from group.

Scenario Outline: 010 Create Charge Type
	Given the below Charge types are available
		| Code     | Description   | TransactionType    | Category          |
		| AU_CT_B2 | <ChargeType1> | <TransactionType1> | Billed on Account |
		| AMISC    | <ChargeType2> | <TransactionType1> | Billed on Account |

@singapore @qa @training @staging @canada @europe @uk   @ft
Examples:
	| ChargeType1   | ChargeType2   | TransactionType1     |
	| Sundry Income | Miscellaneous | Miscellaneous Income |

Scenario Outline: 020 Add New Charge Type Group
	Given I add a new charge type group
		| Code   | Description   |
		| <Code> | <Description> |
	Then the new group is added to the child object in charge type

@training @staging @canada @europe @uk @singapore   @ft @qa
Examples:
	| Code | Description |
	| ng_  | newgroup_   |

Scenario Outline: 030 Add Charge Type To Group
	Given I select a charge type group
	Then I can add a charge type to the group
		| Charge Type Details |
		| <ChargeTypeDetails> |


@singapore @qa @training @staging @canada @europe @uk   @ft
Examples:
	| ChargeTypeDetails |
	| Sundry Income     |

Scenario Outline: 040 Add More Than One Charge Type to a Group
	Given I select a charge type group
	Then I can add more than one charge type to the group
		| Charge Type Details  |
		| <ChargeTypeDetails1> |

@singapore @training @staging @canada @europe @uk   @qa @ft
Examples:
	| ChargeTypeDetails1 |
	| Miscellaneous      |

Scenario Outline: 050 No Duplicate Charge Type Allowed in a Group
	Given I select a charge type group
	When I add a duplicate charge type to a group
		| Charge Type Details |
		| <ChargeTypeDetails> |
	Then an error occurs
		| Mandatory Field  |
		| <MandatoryField> |
	And I cancel the process


@singapore @qa @training @staging @canada @europe @uk   @ft
Examples:
	| ChargeTypeDetails | MandatoryField                         |
	| Miscellaneous     | Duplicate charge type MISC in the list |

Scenario Outline: 060 Delete Charge Type From a Group
	Given I select a charge type group
	When I remove a charge type from a group
		| Charge Type  |
		| <ChargeType> |
	Then I verify the charge type is removed
		| Charge Type  |
		| <ChargeType> |

@singapore @qa @training @staging @canada @europe @uk   @ft
Examples:
	| ChargeType    |
	| Miscellaneous |

Scenario Outline: 070 Verify the Charge Type Group added
	Given I select a charge type group
	Then the charge type is available
		| Charge Type  |
		| <ChargeType> |
	And I cancel the process

@singapore @ft @qa @training @staging @canada @europe @uk  
Examples:
	| ChargeType    |
	| Sundry Income |
	
