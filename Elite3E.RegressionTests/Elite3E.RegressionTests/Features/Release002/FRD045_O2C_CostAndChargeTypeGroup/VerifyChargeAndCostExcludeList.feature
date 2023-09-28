@release2 @frd045 @VerifyChargeAndCostExcludeList
Feature: VerifyChargeAndCostExcludeList
	Test 1: Create a Cost Type Group of Exclude and add couple of Disbursement Types via API
	Test 2: Create a Charge Type Group of Exclude and add couple of Charge Types via API
	Test 3: Create a Disbursement Types via API
	Test 4: Create a Charge Types via API
	Test 5: Create a matter with the step 10 & 20 Charge Type Group & Cost Type Group
	Test 6: Add disbursement via disbursement entry from step 3, add Charge type via charge entry from step 4, generate proforma
	Test 7: Verify the disbursement type is not availalbe created in Test 1
	Test 8: Verify the Charge type is not availalbe created in Test2
	
@CancelProcess
Scenario Outline: 010 Create Cost Type Group & Cost Type
	Given the cost type group exists
		| Code       | Description    | CostTypeGroupExcludeOrIncludeList |
		| at_CE_CTG2 | Auto_CE_Group2 | <IncludeOrExcludeOption>          |
	And the below disbursement type added to the group
		| Code    | Description         | TransactionType    | HardOrSoftDisbursement   |
		| <Code1> | <DisbursementType1> | <TransactionType1> | <HardOrSoftDisbursement> |
		| <Code2> | <DisbursementType2> | <TransactionType2> | <HardOrSoftDisbursement> |

@qa
Examples:
	| IncludeOrExcludeOption | Code1   | DisbursementType1              | Code2   | DisbursementType2           | TransactionType1       | TransactionType2      | HardOrSoftDisbursement |
	| IsExcludeList          | AS45025 | Automation Agency Registration | AH90150 | Automation Anticipated Cost | True Hard Cost Mark up | Anticipated Hard Cost | IsHardCost             |
@ft
Examples:
	| IncludeOrExcludeOption | Code1           | DisbursementType1        | Code2   | DisbursementType2        | TransactionType1       | TransactionType2      | HardOrSoftDisbursement |
	| IsExcludeList          | Code_at_rBUfUXC | Agency Registration (NT) | H20030A | Stamp Duty - Anticipated | True Hard Cost Mark up | Anticipated Hard Cost | IsHardCost             |

@training @staging @europe @uk
Examples:
	| IncludeOrExcludeOption | Code1   | DisbursementType1              | Code2   | DisbursementType2           | TransactionType1 | TransactionType2      | HardOrSoftDisbursement |
	| IsExcludeList          | AS45025 | Automation Agency Registration | AH90150 | Automation Anticipated Cost | Hard Cost        | Anticipated Hard Cost | IsHardCost             |
@singapore
Examples:
	| IncludeOrExcludeOption | Code1   | DisbursementType1                     | Code2   | DisbursementType2                    | TransactionType1 | TransactionType2      | HardOrSoftDisbursement |
	| IsExcludeList          | H20005A | Court & Stamp Fees - Anticipated (NT) | H52005A | Registration Fees - Anticipated (NT) | Hard Cost        | Anticipated Hard Cost | IsHardCost             |
@canada
Examples:
	| IncludeOrExcludeOption | Code1   | DisbursementType1                     | Code2  | DisbursementType2           | TransactionType1 | TransactionType2      | HardOrSoftDisbursement |
	| IsExcludeList          | H20005A | Court & Stamp Fees - Anticipated (NT) | H30065 | Bankruptcy Certificate (NT) | Hard Cost        | Anticipated Hard Cost | IsHardCost             |


Scenario Outline: 020 Create Charge Type Group & Charge Type
	Given the charge type group exists
		| Code        | Description    | ChargeTypeGroupExcludeOrIncludeList |
		| at_CE_CHTG2 | Auto_CE_Group2 | <IncludeOrExcludeOption>            |
	And the below charge type added to the group
		| Code    | Description   | TransactionType   | Category          |
		| ASUNDRY | <ChargeType1> | <TransactionType> | Billed on Account |
		| AMISC   | <ChargeType2> | <TransactionType> | Billed on Account |

@CancelProcess @training @staging @canada @europe @uk @ft @qa @singapore
Examples:
	| IncludeOrExcludeOption | ChargeType1   | ChargeType2   | TransactionType      |
	| IsExcludeList          | Sundry Income | Miscellaneous | Miscellaneous Income |

@CancelProcess
Scenario Outline: 030 Create Cost Type
	Given the below disbursement types are available
		| Code    | Description         | TransactionType    | HardOrSoftDisbursement   |
		| <Code1> | <DisbursementType1> | <TransactionType1> | <HardOrSoftDisbursement> |

@qa @training @staging @europe @uk
Examples:
	| Code1  | DisbursementType1       | TransactionType1       | HardOrSoftDisbursement |
	| A9520X | Automation Accomodation | True Hard Cost Mark up | IsHardCost             |
@ft
Examples:
	| Code1   | DisbursementType1                     | TransactionType1       | HardOrSoftDisbursement |
	| H20005A | Court & Stamp Fees - Anticipated (NT) | True Hard Cost Mark up | IsHardCost             |
@canada
Examples:
	| Code1   | DisbursementType1                                             | TransactionType1       | HardOrSoftDisbursement |
	| H52015A | Registration Fees - Application for New Certificates of Title | True Hard Cost Mark up | IsHardCost             |
@singapore
Examples:
	| Code1  | DisbursementType1        | TransactionType1       | HardOrSoftDisbursement |
	| S40035 | Agency Registration (NT) | True Hard Cost Mark up | IsHardCost             |

Scenario Outline: 040 Create Charge Type
	Given the below Charge types are available
		| Code     | Description   | TransactionType    | Category          |
		| AU_CT_B1 | <ChargeType1> | <TransactionType1> | Billed on Account |


@CancelProcess @singapore @qa @training @staging @canada @europe @uk @ft
Examples:
	| ChargeType1   | TransactionType1     |
	| Sundry Income | Miscellaneous Income |

#Previous Matter is 100000078
@CancelProcess
Scenario Outline: 050 Create a new Matter and Proforma Data
	Given I create a user with details
		| UserName | DataRoleAlias |
		| <User>   | Admin         |
	Then I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	And I add a workflow user to a FeeEarner
		| User   | Name            | UserRoleList   |
		| <User> | <FeeEarnerName> | <UserRoleList> |
	And I search or create a client
		| Entity Name | FeeEarnerFullName |
		| <Client>    | <FeeEarnerName>   |
	And I create the Payer with Api
		| PayerName   | Entity   |
		| <PayorName> | <Client> |
	And I create a matter with details:
		| Client   | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | ChargeTypeGroupName | CostTypeGroupName | BillingOffice   | PayorName   |
		| <Client> | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | Auto_CE_Group2      | Auto_CE_Group2    | <BillingOffice> | <PayorName> |

@training @staging @uk @qa
Examples:
	| User      | Client        | FeeEarnerName | Currency            | CurrencyMethod | Office  | Department | Section | BillingOffice | PayorName | UserRoleList |
	| Gavin Jim | Gavin Mandela | Gavin Jim     | GBP - British Pound | Bill           | Default | Default    | Default | London (EU)   | Elba Beck |              |
@ft
Examples:
	| User      | Client        | FeeEarnerName | Currency            | CurrencyMethod | Office  | Department | Section | BillingOffice | PayorName | UserRoleList |
	| Gavin Jim | Gavin Mandela | Gavin Jim     | GBP - British Pound | Bill           | Default | Default    | Default | London (EU)   | Elba Beck |              |
@canada
Examples:
	| User      | Client        | FeeEarnerName | Currency              | CurrencyMethod | Office   | Department | Section | BillingOffice | PayorName | UserRoleList |
	| Gavin Jim | Gavin Mandela | Gavin Jim     | CAD - Canadian Dollar | Bill           | Montreal | Default    | Default | Montreal      | Elba Beck |              |
@europe
Examples:
	| User      | Client        | FeeEarnerName | Currency   | CurrencyMethod | Office      | Department | Section | BillingOffice | PayorName | UserRoleList |
	| Gavin Jim | Gavin Mandela | Gavin Jim     | EUR - Euro | Bill           | London (EU) | Default    | Default | London (EU)   | Elba Beck |              |
@singapore
Examples:
	| User      | Client        | FeeEarnerName | Currency               | CurrencyMethod | Office    | Department            | Section | BillingOffice | PayorName | UserRoleList |
	| Gavin Jim | Gavin Mandela | Gavin Jim     | SGD - Singapore Dollar | Bill           | Singapore | Corporate - Singapore | Default | Singapore     | Elba Beck |              |

@CancelProcess
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
		
@ft
Examples:
	| Disbursement1       | Currency | Amount1 | TaxCode                    | ChargeType1   | ChargeAmount1 |
	| Advertising Charges | GBP      | 200     | UK Output Domestic Zero 0% | Sundry Income | 300.00        |
@staging
Examples:
	| Disbursement1           | Currency | Amount1 | TaxCode          | ChargeType1   | ChargeAmount1 |
	| Automation Accomodation | GBP      | 200     | UK Output Europe | Sundry Income | 300.00        |
@training
Examples:
	| Disbursement1                                          | Currency | Amount1 | TaxCode          | ChargeType1   | ChargeAmount1 |
	| Registraton Fees - Issuance of SSCT - Anticipated (NT) | GBP      | 200     | UK Output Europe | Sundry Income | 300.00        |
@uk
Examples:
	| Disbursement1       | Currency | Amount1 | TaxCode                     | ChargeType1   | ChargeAmount1 |
	| Agency Registration | GBP      | 200     | UK Output Domestic Standard | Sundry Income | 300.00        |
@singapore
Examples:
	| Disbursement1            | Currency | Amount1 | TaxCode                     | ChargeType1   | ChargeAmount1 |
	| Agency Registration (NT) | SGD      | 200     | SG Output Domestic Standard | Sundry Income | 300.00        |
@canada
Examples:
	| Disbursement1                                                 | Currency | Amount1 | TaxCode                         | ChargeType1   | ChargeAmount1 |
	| Registration Fees - Application for New Certificates of Title | CAD      | 200     | CA Output Domestic Standard GST | Sundry Income | 300.00        |
@qa
Examples:
	| Disbursement1       | Currency | Amount1 | TaxCode                         | ChargeType1   | ChargeAmount1 |
	| Advertising Charges | ZAR      | 200     | AE ICB Output  Domestic Zero 0% | Sundry Income | 300.00        |
@europe
Examples:
	| Disbursement1             | Currency | Amount1 | TaxCode               | ChargeType1   | ChargeAmount1 |
	| Court Fees  - Anticipated | EUR      | 200     | ES Output Europe Zero | Sundry Income | 300.00        |


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
	
@ft
Examples:
	| DisbursementType1        | DisbursementType2        | User      |
	| Agency Registration (NT) | Stamp Duty - Anticipated | Gavin Jim |
@qa
Examples:
	| DisbursementType1              | DisbursementType2           | User      |
	| Automation Agency Registration | Automation Anticipated Cost | Gavin Jim |
@training @europe @uk
Examples:
	| DisbursementType1              | DisbursementType2           | User      |
	| Automation Agency Registration | Automation Anticipated Cost | Gavin Jim |
@staging
Examples:
	| DisbursementType1 | DisbursementType2             | User      |
	| Agency Automation | Automation Court Registration | Gavin Jim |
@canada
Examples:
	| DisbursementType1                     | DisbursementType2           | User      |
	| Court & Stamp Fees - Anticipated (NT) | Bankruptcy Certificate (NT) | Gavin Jim |
@singapore
Examples:
	| DisbursementType1                     | DisbursementType2                    | User      |
	| Court & Stamp Fees - Anticipated (NT) | Registration Fees - Anticipated (NT) | Gavin Jim |

@CancelProcess
Scenario Outline: 080 Verify Charge Types in Group in Proforma Edit
	When I try to add the excluded charge types on proforma edit
		| Charge Types  |
		| <ChargeType1> |
		| <ChargeType2> |
	Then the excluded charge types are not available

@training @staging @canada @europe @uk @ft @qa @singapore
Examples:
	| ChargeType1          | ChargeType2     |
	| Automation Sundry In | Automation Misc |

@CancelProcess @training @staging @canada @europe @uk @ft @qa @singapore
Scenario Outline: 090 Cancel Proxy
	Given I cancel proxy


		
