@us
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

		Examples:
		| Code    | Description                           | TransactionType        | HardOrSoftDisbursement |
		| H65000A | Data Storage & Costs - Anticipated    | True Hard Cost Mark up | IsHardCost             |

Scenario Outline: 020 Add New Cost Type Group
	Given I add a new cost type group
		| Code   | Description   |
		| <Code> | <Description> |
	Then the new group is added to the child object in cost type

Examples:
	| Code | Description |
	| ng_  | newgroup_   |

Scenario Outline: 030 Add Cost Type To Group
	Given I select a cost type group
	Then I can add a cost type to the group
		| Cost Type Details |
		| <CostTypeDetails> |

Examples:
	| CostTypeDetails                       |
	| Court & Stamp Fees - Anticipated (NT) |

Scenario Outline: 040 Add More Than One Cost Type to a Group
	Given I select a cost type group
	Then I can add more than one cost type to the group
		| Cost Type Details  |
		| <CostTypeDetails1> |

Examples:
	| CostTypeDetails1                   |
	| Data Storage & Costs - Anticipated |

	Scenario Outline: 050 No Duplicate Cost Type Allowed in a Group
	Given I select a cost type group
	When I add a duplicate cost type to a group
	| Cost Type Details           |
	| <CostTypeDetails>           |
	Then an error occurs
	| Mandatory Field             |
	| <MandatoryField>            |
	And I cancel the process

Examples:
	| CostTypeDetails                    | MandatoryField                          |
	| Data Storage & Costs - Anticipated | Duplicate cost type H65000A in the list |

Scenario Outline: 060 Delete Cost Type From a Group
	Given I select a cost type group
	When I remove a cost type from a group
		| Cost Type  |
		| <CostType> |
	Then I verify the cost type is removed
		| Cost Type  |
		| <CostType> |

Examples:
	| CostType                           |
	| Data Storage & Costs - Anticipated |

Scenario Outline: 070 Add Cost Type from Disbursement Type Process
	Given I select a cost type group
	Then the cost type is available
		| Cost Type  |
		| <CostType> |

Examples:
	| CostType                              |
	| Court & Stamp Fees - Anticipated (NT) |