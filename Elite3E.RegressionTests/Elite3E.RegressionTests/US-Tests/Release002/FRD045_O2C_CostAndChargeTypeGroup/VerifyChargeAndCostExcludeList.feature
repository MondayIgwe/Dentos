@us
Feature: VerifyChargeAndCostExcludeList
	Test 1: Create a Cost Type Group of Exclude and add couple of Disbursement Types via API
	Test 2: Create a Charge Type Group of Exclude and add couple of Charge Types via API
	Test 3: Create a Disbursement Types via API
	Test 4: Create a Charge Types via API
	Test 5: Create a matter with the step 10 & 20 Charge Type Group & Cost Type Group
	Test 6: Add disbursement via disbursement entry from step 3, add Charge type via charge entry from step 4, generate proforma
	Test 7: Verify the disbursement type is not availalbe created in Test 1
	Test 8: Verify the Charge type is not availalbe created in Test2
	

Scenario Outline: 010 Create Cost Type Group & Cost Type
	Given the cost type group exists
		| Code       | Description    | CostTypeGroupExcludeOrIncludeList |
		| at_CE_CTG2 | Auto_CE_Group2 | <IncludeOrExcludeOption>          |
	And the below disbursement type added to the group
		| Code    | Description         | TransactionType    | HardOrSoftDisbursement   |
		| <Code1> | <DisbursementType1> | <TransactionType1> | <HardOrSoftDisbursement> |
		| <Code2> | <DisbursementType2> | <TransactionType2> | <HardOrSoftDisbursement> |

Examples:
	| IncludeOrExcludeOption | Code1   | DisbursementType1                  | Code2   | DisbursementType2                     | TransactionType1 | TransactionType2      | HardOrSoftDisbursement |
	| IsExcludeList          | H65000A | Data Storage & Costs - Anticipated | H20005A | Court & Stamp Fees - Anticipated (NT) | Hard Cost        | Anticipated Hard Cost | IsHardCost             |

Scenario Outline: 020 Create Charge Type Group & Charge Type
	Given the charge type group exists
		| Code        | Description    | ChargeTypeGroupExcludeOrIncludeList |
		| at_CE_CHTG2 | Auto_CE_Group2 | <IncludeOrExcludeOption>            |
	And the below charge type added to the group
		| Code    | Description   | TransactionType   | Category          |
		| ASUNDRY | <ChargeType1> | <TransactionType> | Billed on Account |
		| AMISC   | <ChargeType2> | <TransactionType> | Billed on Account |

Examples:
	| IncludeOrExcludeOption | ChargeType1   | ChargeType2   | TransactionType      |
	| IsExcludeList          | Sundry Income | Miscellaneous | Miscellaneous Income |


Scenario Outline: 030 Create Cost Type
	Given the below disbursement types are available
		| Code    | Description         | TransactionType    | HardOrSoftDisbursement   |
		| <Code1> | <DisbursementType1> | <TransactionType1> | <HardOrSoftDisbursement> |

Examples:
	| Code1   | DisbursementType1                                             | TransactionType1       | HardOrSoftDisbursement |
	| H52015A | Registration Fees - Application for New Certificates of Title | True Hard Cost Mark up | IsHardCost             |

Scenario Outline: 040 Create Charge Type
	Given the below Charge types are available
		| Code     | Description   | TransactionType    | Category          |
		| AU_CT_B1 | <ChargeType1> | <TransactionType1> | Billed on Account |

Examples:
	| ChargeType1   | TransactionType1     |
	| Sundry Income | Miscellaneous Income |

#Previous Matter is 100000078

Scenario Outline: 050 Create a new Matter and Proforma Data
	Given I create a user with details
		| UserName | DataRoleAlias |
		| <User>   | Admin         |
	Then I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	And I add a workflow user to a FeeEarner
		| User   | Name            |
		| <User> | <FeeEarnerName> |
	And I search or create a client
		| Entity Name | FeeEarnerFullName |
		| <Client>    | <FeeEarnerName>   |
	And I create the Payer with Api
		| PayerName   | Entity   |
		| <PayorName> | <Client> |
	And I create a matter with details:
		| Client   | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | ChargeTypeGroupName | CostTypeGroupName | BillingOffice   | PayorName   |
		| <Client> | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | Auto_CE_Group2      | Auto_CE_Group2    | <BillingOffice> | <PayorName> |

Examples:
	| User      | Client        | FeeEarnerName | Currency        | CurrencyMethod | Office  | Department | Section | BillingOffice | PayorName |
	| Gavin Jim | Gavin Mandela | Gavin Jim     | USD - US Dollar | Bill           | Chicago | Default    | Default | Chicago       | Elba Beck |

Scenario Outline: 060 Create a disbursement, charge entry and generate proforma
	When I add a disbursement entry
		| Disbursement Type | Work Currency | Work Amount | Narrative | Tax Code  |
		| <Disbursement1>   | <Currency>    | <Amount1>   | {Auto}+10 | <TaxCode> |
	And I add a charge entry
		| Charge Type   | Amount          | Tax Code  |
		| <ChargeType1> | <ChargeAmount1> | <TaxCode> |
	And I can generate the proforma
		| Description | Include Other Proformas | Invoice Date | Proforma Status |
		| {Auto}+36   | No                      | {Today}-1    | Draft           |
		
Examples:
	| Disbursement1                                                 | Currency | Amount1 | TaxCode                          | ChargeType1   | ChargeAmount1 |
	| Registration Fees - Application for New Certificates of Title | USD      | 200     | US Output Domestic Standard Rate | Sundry Income | 300.00        |

Scenario Outline: 070 Verify Cost Types in Group in Proforma Edit
	Given I proxy as user '<User>'
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	And I open the proforma for submission
	When I try to add the excluded disbursements on proforma edit
		| Disbursement Types  |
		| <DisbursementType1> |
		| <DisbursementType2> |
	Then the excluded disbursements are not available
	

Examples:
	| DisbursementType1                  | DisbursementType2                     | User      |
	| Data Storage & Costs - Anticipated | Court & Stamp Fees - Anticipated (NT) | Gavin Jim |

Scenario Outline: 080 Verify Charge Types in Group in Proforma Edit
	When I try to add the excluded charge types on proforma edit
		| Charge Types  |
		| <ChargeType1> |
		| <ChargeType2> |
	Then the excluded charge types are not available

Examples:
	| ChargeType1          | ChargeType2     |
	| Automation Sundry In | Automation Misc |

Scenario Outline: 090 Cancel Proxy
	Given I cancel proxy


		
