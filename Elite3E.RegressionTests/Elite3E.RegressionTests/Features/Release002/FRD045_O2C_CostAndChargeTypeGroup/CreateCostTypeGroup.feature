@release2 @frd045 @CreateCostTypeGroup
Feature: CreateCostTypeGroup
	Test 6: The Cost Type Group process allow groups to be created and edited. 
	Test 7: The Cost Type Group process allows cost type to be added to group.
	Test 8: The Cost Type Group process allows more than one cost type to be added to the group. 
	Test 9: The Cost Type Group process dores not allow for the duplicate cost type to be added to the group. 
	Test 10: The Cost Type Group process allows cost type to be deleted from group.
	Test 12: Cost Types can also be added to groups from the Disbursment Type process.

Scenario Outline: 010 Create Cost Type
	Given the below disbursement types are available
		| Code   | Description   | TransactionType   | HardOrSoftDisbursement   |
		| <Code> | <Description> | <TransactionType> | <HardOrSoftDisbursement> |

@qa @training @staging @europe @uk
		Examples:
		| Code     | Description                      | TransactionType        | HardOrSoftDisbursement |
		| A9520X   | Automation Accomodation          | True Hard Cost Mark up | IsHardCost             |
		| AH20005X | Automation Anticipated Costs Gen | True Hard Cost Mark up | IsHardCost             |

@ft
		Examples:
		| Code    | Description                            | TransactionType        | HardOrSoftDisbursement |
		| H20005A | Court & Stamp Fees - Anticipated (NT)  | True Hard Cost Mark up | IsHardCost             |
		| H75010A | Bank Charges & Admin Fee - Anticipated | True Hard Cost Mark up | IsHardCost             |
@canada
		Examples:
		| Code    | Description                           | TransactionType        | HardOrSoftDisbursement |
		| H20005A | Court & Stamp Fees - Anticipated (NT) | True Hard Cost Mark up | IsHardCost             |
		| H23010A | Electronic Filing Fees - Anticipated  | True Hard Cost Mark up | IsHardCost             |
@singapore
		Examples:
		| Code    | Description           | TransactionType        | HardOrSoftDisbursement |
		| H50015  | Application Fees (NT) | True Hard Cost Mark up | IsHardCost             |
		| I960012 | Bank Fees             | True Hard Cost Mark up | IsHardCost             |

Scenario Outline: 020 Add New Cost Type Group
	Given I add a new cost type group
		| Code   | Description   |
		| <Code> | <Description> |
	Then the new group is added to the child object in cost type

@ft @qa
Examples:
	| Code | Description |
	| ng_  | newgroup_   |
@training @staging  @canada @europe @uk @singapore  
Examples:
	| Code | Description |
	| ng_  | newgroup_   |

Scenario Outline: 030 Add Cost Type To Group
	Given I select a cost type group
	Then I can add a cost type to the group
		| Cost Type Details |
		| <CostTypeDetails> |

@qa
Examples:
	| CostTypeDetails         |
	| Automation Accomodation |
@training @staging @europe @uk
Examples:
	| CostTypeDetails         |
	| Automation Accomodation |
@singapore
Examples:
	| CostTypeDetails       |
	| Application Fees (NT) |
@canada @ft  
Examples:
	| CostTypeDetails                       |
	| Court & Stamp Fees - Anticipated (NT) |

Scenario Outline: 040 Add More Than One Cost Type to a Group
	Given I select a cost type group
	Then I can add more than one cost type to the group
		| Cost Type Details  |
		| <CostTypeDetails1> |

@qa
Examples:
	| CostTypeDetails1                 |
	| Automation Anticipated Costs Gen |
@ft
Examples:
	| CostTypeDetails1                       |
	| Bank Charges & Admin Fee - Anticipated |
@training @staging @europe @uk
Examples:
	| CostTypeDetails1                 |
	| Automation Anticipated Costs Gen |
@singapore
Examples:
	| CostTypeDetails1 |
	| Bank Fees        |
@canada
Examples:
	| CostTypeDetails1                     |
	| Electronic Filing Fees - Anticipated |
 


	Scenario Outline: 050 No Duplicate Cost Type Allowed in a Group
	Given I select a cost type group
	When I add a duplicate cost type to a group
	| Cost Type Details           |
	| <CostTypeDetails>           |
	Then an error occurs
	| Mandatory Field             |
	| <MandatoryField>            |
	And I cancel the process

@ft
Examples:
	| CostTypeDetails                        | MandatoryField                          |
	| Bank Charges & Admin Fee - Anticipated | Duplicate cost type H75010A in the list |
@qa @uk @training @staging  @europe
Examples:
	| CostTypeDetails                  | MandatoryField                           |
	| Automation Anticipated Costs Gen | Duplicate cost type AH20005X in the list |

@singapore
Examples:
	| CostTypeDetails | MandatoryField                          |
	| Bank Fees       | Duplicate cost type I960012 in the list |
@canada 
Examples:
	| CostTypeDetails                       | MandatoryField                          |
	| Court & Stamp Fees - Anticipated (NT) | Duplicate cost type H20005A in the list |

Scenario Outline: 060 Delete Cost Type From a Group
	Given I select a cost type group
	When I remove a cost type from a group
		| Cost Type  |
		| <CostType> |
	Then I verify the cost type is removed
		| Cost Type  |
		| <CostType> |

@qa
Examples:
	| CostType                         |
	| Automation Anticipated Costs Gen |
@ft
Examples:
	| CostType                               |
	| Bank Charges & Admin Fee - Anticipated |
@training @staging @europe @uk
Examples:
	| CostType                         |
	| Automation Anticipated Costs Gen |
@singapore
Examples:
	| CostType  |
	| Bank Fees |
@canada
Examples:
	| CostType                             |
	| Electronic Filing Fees - Anticipated |


Scenario Outline: 070 Add Cost Type from Disbursement Type Process
	Given I select a cost type group
	Then the cost type is available
		| Cost Type  |
		| <CostType> |
@ft
Examples:
	| CostType                              |
	| Court & Stamp Fees - Anticipated (NT) |
@qa @training @staging @europe @uk
Examples:
	| CostType                |
	| Automation Accomodation |
@singapore
Examples:
	| CostType              |
	| Application Fees (NT) |
@canada 
Examples:
	| CostType                              |
	| Court & Stamp Fees - Anticipated (NT) |