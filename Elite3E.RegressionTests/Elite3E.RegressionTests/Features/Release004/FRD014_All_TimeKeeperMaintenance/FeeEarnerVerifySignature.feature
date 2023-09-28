@release4 @frd014 @FeeEarnerVerifySignature
Feature: FeeEarnerVerifySignature

NOTE: Scenario 3 Has some requirements.
A Rate Type that has the fee earner checkbox marked and is a Firm Default.
This won't work with Standard Rate Type as that is not a Fee Earner Rate type.

For FT, I used FEE_STD with Fee Earner Checkbox ticked and Firm Default Ticked.
We also had STD that was a regular rate type with standard checkbox ticked.

Scenario Outline: 010 verify signature is available on Fee Earner Maintaince
	Given I create a person entity '<Entity>'
	When I view the view the fee earner maintenance 
		| Entity   | Local Language Name |
		| <Entity> | English             |
	Then signature is available
	And I can upload a signature 'TestSignature.png'
	
@ft @qa @training @staging  @canada @europe @uk @singapore 
Examples: 
		| Entity                     |
		| Auto_Entity John_FeeEarner |

Scenario: 020 Effective Dated Information
	When I add effective dated information
		| Office   | Title   |
		| <Office> | <Title> |

@ft @qa @training @staging  @europe @uk @singapore 
Examples: 
		| Office  | Title   |
		| Default | Partner |
@canada
Examples: 
		| Office    | Title               |
		| Vancouver | CA Partner (Income) |


@ft @qa @training @staging  @canada @europe @uk @singapore
Scenario: 030 Add Fee Earner Rate Entry
	When I add fee earner entry
		| Default Rates |
		| 2.00          |

Scenario: 040 Add partner points
	Given I add partner points
		| Partner Points  | Budget Partner Points | Partner Points Value | Budget Partner Points Value | Fixed Profit Share | Management Value  | Economic Value  | Net Operating Efficiency |
		| <PartnerPoints> | <BudgetPartnerPoints> | <PartnerPointsValue> | <BudgetPartnerPointsValue>  | <FixedProfitShare> | <ManagementValue> | <EconomicValue> | <NetOperatingEfficiency> |
	And submit it
	And I validate submit was successful

@ft @qa @training @staging  @canada @europe @uk @singapore
Examples: 
		| PartnerPoints | BudgetPartnerPoints | PartnerPointsValue | BudgetPartnerPointsValue | FixedProfitShare | ManagementValue | EconomicValue | NetOperatingEfficiency |
		| 50%           | 20.00               | 30.00              | 40.00                    | 30.00            | 40.00           | 70.00         | 30.00                  |

@ft @training @staging  @canada @europe @uk @singapore @qa
Scenario: 050 validate fee earner maintaince
	Then local language is saved
	And signature is saved
	And the partner points is saved	

@ft @training @staging  @canada @europe @uk @singapore @qa
Scenario: 060 Delete Fee Earner 
	When I delete the fee earner maintenance

