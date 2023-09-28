@us
Feature: ChargeCostEntryAndModify
		Test 1: The Custom Charge and Cost Type Group filters are respected in the following process: Charge Entry
		Test 2: The Custom Charge and Cost Type Group filters are respected in the following process: Charge Modify
		Test 3: The Custom Charge and Cost Type Group filters are respected in the following process: Cost Entry
		Test 4: The Custom Charge and Cost Type Group filters are respected in the following process: Cost Modify
		Test 5: The Custom Charge and Cost Type Group filters are respected in the following process: Voucher Maintenance
		Test 6: In Disbursement Modify, users can only purge anticipated cards if they are members of the role defined in the PurgeProcessor_ccc option. 

Scenario Outline: 010 Create Disbursement Type Group & Cost Type
	Given the cost type group exists
		| Code        | Description    | CostTypeGroupExcludeOrIncludeList |
		| at_CE_CHTG5 | Auto_CE_Group5 | <IncludeOrExcludeOption>          |
	And the below disbursement type added to the group
		| Code    | Description         | TransactionType    | HardOrSoftDisbursement   |
		| <Code1> | <DisbursementType1> | <TransactionType1> | <HardOrSoftDisbursement> |
		| <Code2> | <DisbursementType2> | <TransactionType2> | <HardOrSoftDisbursement> |

Examples:
	| IncludeOrExcludeOption | Code1   | DisbursementType1                  | Code2  | DisbursementType2      | TransactionType1 | TransactionType2      | HardOrSoftDisbursement |
	| IsIncludeList          | H65000A | Data Storage & Costs - Anticipated | H20005A | Court & Stamp Fees - Anticipated (NT) | Hard Cost        | Anticipated Hard Cost | IsHardCost             |

Scenario Outline: 020 Create Charge Type Group & Charge Type
	Given the charge type group exists
		| Code        | Description    | ChargeTypeGroupExcludeOrIncludeList |
		| at_CE_CHTG5 | Auto_CE_Group5 | <IncludeOrExcludeOption>            |
	And the below charge type added to the group
		| Code   | Description   | TransactionType    | Category          |
		| SUNDRY | <ChargeType1> | <TransactionType1> | Billed on Account |
		| MISC   | <ChargeType2> | <TransactionType2> | Billed on Account |

Examples:
	| IncludeOrExcludeOption | ChargeType1   | ChargeType2   | TransactionType1     |
	| IsIncludeList          | Sundry Income | Miscellaneous | Miscellaneous Income |

# Previous Matter is 100000078
Scenario Outline: 030 Create a new Matter
	Given I create the Payer with Api
		| PayerName   | Entity        |
		| <PayorName> | EspiritEntity |
	Given I create a matter with details:
		| Client   | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | ChargeTypeGroupName | CostTypeGroupName | BillingOffice   | PayorName   |
		| <Client> | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <ChargeTypeGroup>   | <CostTypeGroup>   | <BillingOffice> | <PayorName> |

Examples:
	| Client                       | Currency        | CurrencyMethod | Office  | Department | Section | ChargeTypeGroup | CostTypeGroup  | BillingOffice | PayorName |
	| Client_Automation at_HTOvMOn | USD - US Dollar | Bill           | Chicago | Default    | Default | Auto_CE_Group5  | Auto_CE_Group5 | Chicago       | Elba Beck |

# Previous Matter is 100000078
Scenario Outline: 040 Charge Entry
	When I add a charge entry
		| Charge Type | Amount    | Tax Code  |
		| <Charge1>   | <Amount1> | <TaxCode> |
		| <Charge2>   | <Amount2> | <TaxCode> |
	Then I verify the selected charge types are added to the matter
		| Charge Type Code |
		| <ChargeCode1>    |
		| <ChargeCode2>    |

Examples:
	| Charge1       | Amount1 | ChargeCode1 | Charge2       | Amount2 | ChargeCode2 | TaxCode                          |
	| Sundry Income | 300.00  | SUNDRY      | Miscellaneous | 1000.00 | MISC        | US Output Domestic Standard Rate |

Scenario Outline: 050 Charge Modify
	Given I try to modify the charge types
	Then only the charge types set on the charge group is available
		| Charge Type   |
		| <ChargeType1> |
		| <ChargeType2> |

Examples:
	| ChargeType1   | ChargeType2   |
	| Sundry Income | Miscellaneous |

		
Scenario Outline: 060 Cost Entry
	Given I add a disbursement entry
		| Disbursement Type | Work Currency | Work Amount | Narrative | Tax Code  |
		| <Disbursement1>   | <Currency>    | <Amount1>   | {Auto}+36 | <TaxCode> |
		| <Disbursement2>   | <Currency>    | <Amount2>   | {Auto}+36 | <TaxCode> |
	Then I verify all cost types are added to the matter
		| Disbursement Type Code  |
		| <DisbursementTypeCode1> |
		| <DisbursementTypeCode2> |

Examples:
	| Disbursement1                      | Disbursement2                         | Currency | Amount1 | Amount2 | Amount3 | Amount4 | TaxCode                          | DisbursementTypeCode1 | DisbursementTypeCode2 |
	| Data Storage & Costs - Anticipated | Court & Stamp Fees - Anticipated (NT) | USD      | 200     | 50      | 100     | 300     | US Output Domestic Standard Rate | H65000A               | H20005A               |

Scenario Outline: 070 Cost Modify
	Given I try to modify the disbursement types
	Then I verify the new disbursement types
		| Disbursement    |
		| <Disbursement1> |
		| <Disbursement2> |

Examples:
	| Disbursement1                      | Disbursement2                         |
	| Data Storage & Costs - Anticipated | Court & Stamp Fees - Anticipated (NT) |
