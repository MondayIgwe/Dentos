@us
Feature: AddPurgeDetailDisbursement
	Add a new Purge Detail Disbursement

#Scenario Outline: 010 Create Disbursement Type Group & Cost Type
#	Given the cost type group exists
#		| Code    | Description   | CostTypeGroupExcludeOrIncludeList | 
#		| at_PD_1 | <Description> | <IncludeOrExcludeOption>                | 
#	And the below disbursement type added to the group
#		| Code      | Description         | TransactionType    | HardOrSoftDisbursement   |
#		| at_PD_DT1 | <DisbursementType1> | <TransactionType1> | <HardOrSoftDisbursement> |
#		| at_PD_DT2 | <DisbursementType2> | <TransactionType2> | <HardOrSoftDisbursement> | 
#@ft @qa
#	Examples: 
#		| Description   | IncludeOrExcludeOption | DisbursementType1   | DisbursementType2         | TransactionType1 | TransactionType2      | HardOrSoftDisbursement | 
#		| Auto_PD_Group | IsIncludeList          | Agency Registration | Anticipated Costs General | Hard Cost        | Anticipated Hard Cost | IsHardCost                    |
#@training @staging  @canada @europe @uk @singapore @us
#	Examples: 
#		| Description   | IncludeOrExcludeOption | DisbursementType1   | DisbursementType2                     | TransactionType1 | TransactionType2      | HardOrSoftDisbursement | 
#		| Auto_PD_Group | IsIncludeList          | Agency Registration | Court & Stamp Fees - Anticipated (NT) | Hard Cost        | Anticipated Hard Cost | Hard                         | 


Scenario Outline: 020 Create a new Matter
	Given I create the Payer with Api
		| PayerName   | Entity          |
		| <PayorName> | SafeHarbour Ltd |
	And I create a matter with details:
		| Client   | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | ChargeTypeGroupName | CostTypeGroupName | BillingOffice   | PayorName   |
		| <Client> | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <ChargeTypeGroup>   | <CostTypeGroup>   | <BillingOffice> | <PayorName> |


Examples:
	| Client                       | Currency        | CurrencyMethod | Office  | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName   |
	| Client_Automation at_HTOvMOn | USD - US Dollar | Bill           | Chicago | Default    | Default | Auto_PD_CHGroup | Auto_PD_CTGroup | Chicago       | Melisa Tate |

Scenario Outline: 030 Post Disbursements from Disbursement Entry
	Given I post the disbursement
		| Work Date | Disbursement Type   | Work Currency  | Work Amount   | Narrative | Tax Code  |
		| {Today}-1 | <DisbursementType1> | <WorkCurrency> | <WorkAmount1> | {Auto}+36 | <TaxCode> |
		| {Today}-1 | <DisbursementType2> | <WorkCurrency> | <WorkAmount2> | {Auto}+36 | <TaxCode> |
	When I validate the disbursement is posted with no errors

Examples:
	| DisbursementType1                  | DisbursementType2             | WorkCurrency | WorkAmount1 | WorkAmount2 | TaxCode                          |
	| Data Storage & Costs - Anticipated | Dentons United States Funding | USD          | 5000        | 5001        | US Output Domestic Standard Rate |

Scenario Outline: 040 Add a new Purge Disbursement
	Given I select the matters for purging
	And add the purge detail
		| Purge Type  | Start Date | End Date | Purge Disbursement  | Exclude Anticipated  |
		| <PurgeType> | {Today} -1 | {Today}  | <PurgeDisbursement> | <ExcludeAnticipated> |

Examples:
	| PurgeType      | PurgeDisbursement | ExcludeAnticipated |
	| Billable Purge | Yes               | Yes                |

Scenario Outline: 050 Calculate the Purge Detail
	When I calculate the purge detail
	Then the purged item count is correct
		| Matters   | Disbursement   |
		| <Matters> | <Disbursement> |

Examples:
	| Matters | Disbursement |
	| 1       | 1            |

Scenario Outline: 060 Re-Calculate the Purge Detail
	Given I include anticipated
	When I calculate the purge detail
	Then the purged item count is correct
		| Matters   | Disbursement   |
		| <Matters> | <Disbursement> |

Examples:
	| Matters | Disbursement |
	| 1       | 2            |
