@release2 @frd045 @ChargeCostEntryAndModify
Feature: ChargeCostEntryAndModify
		Test 1: The Custom Charge and Cost Type Group filters are respected in the following process: Charge Entry
		Test 2: The Custom Charge and Cost Type Group filters are respected in the following process: Charge Modify
		Test 3: The Custom Charge and Cost Type Group filters are respected in the following process: Cost Entry
		Test 4: The Custom Charge and Cost Type Group filters are respected in the following process: Cost Modify
		Test 5: The Custom Charge and Cost Type Group filters are respected in the following process: Voucher Maintenance
		Test 6: In Disbursement Modify, users can only purge anticipated cards if they are members of the role defined in the PurgeProcessor_ccc option. 

Scenario Outline: 010 Create Disbursement Type Group & Cost Type
	Given the cost type group exists
		| Code       | Description    | CostTypeGroupExcludeOrIncludeList |
		| at_CE_CTG1 | Auto_CE_Group1 | <IncludeOrExcludeOption>          |
	And the below disbursement type added to the group
		| Code    | Description         | TransactionType    | HardOrSoftDisbursement   |
		| <Code1> | <DisbursementType1> | <TransactionType1> | <HardOrSoftDisbursement> |
		| <Code2> | <DisbursementType2> | <TransactionType2> | <HardOrSoftDisbursement> |
@ft
Examples:
	| IncludeOrExcludeOption | Code1   | DisbursementType1                     | Code2   | DisbursementType2                      | TransactionType1 | TransactionType2      | HardOrSoftDisbursement |
	| IsIncludeList          | H20005A | Court & Stamp Fees - Anticipated (NT) | H75010A | Bank Charges & Admin Fee - Anticipated | Hard Cost        | Anticipated Hard Cost | IsHardCost             |
@training
Examples:
	| IncludeOrExcludeOption | Code1  | DisbursementType1 | Code2  | DisbursementType2  | TransactionType1 | TransactionType2      | HardOrSoftDisbursement |
	| IsIncludeList          | H40280 | Accident Report   | H40010 | Administration Fee | Hard Cost        | Anticipated Hard Cost | IsHardCost             |
@qa
Examples:
	| IncludeOrExcludeOption | Code1  | DisbursementType1   | Code2  | DisbursementType2 | TransactionType1 | TransactionType2      | HardOrSoftDisbursement |
	| IsIncludeList          | S45025 | Agency Registration | H10200 | Accommodation     | Hard Cost        | Anticipated Hard Cost | IsHardCost             |
@staging @europe @uk
Examples:
	| IncludeOrExcludeOption | Code1   | DisbursementType1                                      | Code2   | DisbursementType2                     | TransactionType1 | TransactionType2      | HardOrSoftDisbursement |
	| IsIncludeList          | H52085A | Registraton Fees - Issuance of SSCT - Anticipated (NT) | H20005A | Court & Stamp Fees - Anticipated (NT) | Hard Cost        | Anticipated Hard Cost | IsHardCost             |
@singapore
Examples:
	| IncludeOrExcludeOption | Code1   | DisbursementType1                    | Code2   | DisbursementType2                     | TransactionType1 | TransactionType2      | HardOrSoftDisbursement |
	| IsIncludeList          | H52005A | Registration Fees - Anticipated (NT) | H20005A | Court & Stamp Fees - Anticipated (NT) | Hard Cost        | Anticipated Hard Cost | IsHardCost             |
@canada
Examples:
	| IncludeOrExcludeOption | Code1   | DisbursementType1                                      | Code2   | DisbursementType2                     | TransactionType1 | TransactionType2      | HardOrSoftDisbursement |
	| IsIncludeList          | H52085A | Registraton Fees - Issuance of SSCT - Anticipated (NT) | H20005A | Court & Stamp Fees - Anticipated (NT) | Hard Cost        | Anticipated Hard Cost | IsHardCost             |

Scenario Outline: 020 Create Charge Type Group & Charge Type
	Given the charge type group exists
		| Code       | Description    | ChargeTypeGroupExcludeOrIncludeList |
		| at_CE_CHTG | Auto_CE_Group1 | <IncludeOrExcludeOption>            |
	And the below charge type added to the group
		| Code   | Description   | TransactionType    | Category          |
		| SUNDRY | <ChargeType1> | <TransactionType1> | Billed on Account |
		| MISC   | <ChargeType2> | <TransactionType2> | Billed on Account |
@ft @qa
Examples:
	| IncludeOrExcludeOption | ChargeType1   | ChargeType2   | TransactionType1     |
	| IsIncludeList          | Sundry Income | Miscellaneous | Miscellaneous Income |
@staging @canada @europe @uk @singapore
Examples:
	| IncludeOrExcludeOption | ChargeType1   | ChargeType2   | TransactionType1     |
	| IsIncludeList          | Sundry Income | Miscellaneous | Miscellaneous Income |
@training
Examples:
	| IncludeOrExcludeOption | ChargeType1   | ChargeType2    | TransactionType1     |
	| IsIncludeList          | Admin Charges | Address change | Miscellaneous Income |

#
# Previous Matter is 100000078
Scenario Outline: 030 Create a new Matter
	Given I create the Payer with Api
		| PayerName   | Entity        |
		| <PayorName> | EspiritEntity |
	Given I create a matter with details:
		| Client   | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | ChargeTypeGroupName | CostTypeGroupName | BillingOffice   | PayorName   |
		| <Client> | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <ChargeTypeGroup>   | <CostTypeGroup>   | <BillingOffice> | <PayorName> |
@ft @training @staging @uk @qa
Examples:
	| Client                       | Currency            | CurrencyMethod | Office  | Department | Section | ChargeTypeGroup | CostTypeGroup  | BillingOffice | PayorName |
	| Client_Automation at_HTOvMOn | GBP - British Pound | Bill           | Default | Default    | Default | Auto_CE_Group1  | Auto_CE_Group1 | London (EU)   | Elba Beck |
@europe
Examples:
	| Client                       | Currency   | CurrencyMethod | Office      | Department | Section | ChargeTypeGroup | CostTypeGroup  | BillingOffice | PayorName |
	| Client_Automation at_HTOvMOn | EUR - Euro | Bill           | London (EU) | Default    | Default | Auto_CE_Group1  | Auto_CE_Group1 | London (EU)   | Elba Beck |
@canada
Examples:
	| Client                       | Currency              | CurrencyMethod | Office   | Department | Section | ChargeTypeGroup | CostTypeGroup  | BillingOffice | PayorName |
	| Client_Automation at_HTOvMOn | CAD - Canadian Dollar | Bill           | Montreal | Default    | Default | Auto_CE_Group1  | Auto_CE_Group1 | Montreal      | Elba Beck |
@singapore
Examples:
	| Client                       | Currency               | CurrencyMethod | Office    | Department            | Section | ChargeTypeGroup | CostTypeGroup  | BillingOffice | PayorName |
	| Client_Automation at_HTOvMOn | SGD - Singapore Dollar | Bill           | Singapore | Corporate - Singapore | Default | Auto_CE_Group1  | Auto_CE_Group1 | Singapore     | Elba Beck |

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

@ft
Examples:
	| Charge1       | Amount1 | ChargeCode1 | Charge2       | Amount2 | ChargeCode2 | TaxCode                        |
	| Sundry Income | 300.00  | SUNDRY      | Miscellaneous | 1000.00 | MISC        | UK ICB Output Domestic Zero 0% |
@qa
Examples:
	| Charge1       | Amount1 | ChargeCode1 | Charge2       | Amount2 | ChargeCode2 | TaxCode                             |
	| Sundry Income | 300.00  | SUNDRY      | Miscellaneous | 1000.00 | MISC        | AE Output Domestic Standard Rate 5% |
@staging @singapore
Examples:
	| Charge1       | Amount1 | ChargeCode1 | Charge2       | Amount2 | ChargeCode2 | TaxCode                     |
	| Sundry Income | 300.00  | SUNDRY      | Miscellaneous | 1000.00 | MISC        | SG Output Domestic Standard |
@training
Examples:
	| Charge1       | Amount1 | ChargeCode1 | Charge2        | Amount2 | ChargeCode2 | TaxCode                     |
	| Admin Charges | 300.00  | DOAC        | Address change | 1000.00 | ADM_CHARGE  | AE Output Domestic Standard |

@uk
Examples:
	| Charge1       | Amount1 | ChargeCode1 | Charge2       | Amount2 | ChargeCode2 | TaxCode                 |
	| Sundry Income | 300.00  | SUNDRY      | Miscellaneous | 1000.00 | MISC        | UK Output Domestic Zero |
@europe
Examples:
	| Charge1       | Amount1 | ChargeCode1 | Charge2       | Amount2 | ChargeCode2 | TaxCode               |
	| Sundry Income | 300.00  | SUNDRY      | Miscellaneous | 1000.00 | MISC        | ES Output Europe Zero |
@canada
Examples:
	| Charge1       | Amount1 | ChargeCode1 | Charge2       | Amount2 | ChargeCode2 | TaxCode                         |
	| Sundry Income | 300.00  | SUNDRY      | Miscellaneous | 1000.00 | MISC        | CA Output Domestic Standard GST |

Scenario Outline: 050 Charge Modify
	Given I try to modify the charge types
	Then only the charge types set on the charge group is available
		| Charge Type   |
		| <ChargeType1> |
		| <ChargeType2> |
@ft @qa
Examples:
	| ChargeType1   | ChargeType2   |
	| Sundry Income | Miscellaneous |
@staging @canada @europe @uk @singapore
Examples:
	| ChargeType1   | ChargeType2   |
	| Sundry Income | Miscellaneous |
@training
Examples:
	| ChargeType1   | ChargeType2    |
	| Admin Charges | Address change |
		
Scenario Outline: 060 Cost Entry
	Given I add a disbursement entry
		| Disbursement Type | Work Currency | Work Amount | Narrative | Tax Code  |
		| <Disbursement1>   | <Currency>    | <Amount1>   | {Auto}+36 | <TaxCode> |
		| <Disbursement2>   | <Currency>    | <Amount2>   | {Auto}+36 | <TaxCode> |
	Then I verify all cost types are added to the matter
		| Disbursement Type Code  |
		| <DisbursementTypeCode1> |
		| <DisbursementTypeCode2> |
@ft
Examples:
	| Disbursement1                         | Disbursement2                          | Currency | Amount1 | Amount2 | Amount3 | Amount4 | TaxCode                        | DisbursementTypeCode1 | DisbursementTypeCode2 |
	| Court & Stamp Fees - Anticipated (NT) | Bank Charges & Admin Fee - Anticipated | GBP      | 200     | 50      | 100     | 300     | UK ICB Output Domestic Zero 0% | H20005A               | H75010A               |
@training
Examples:
	| Disbursement1                         | Disbursement2                                          | Currency | Amount1 | Amount2 | Amount3 | Amount4 | TaxCode                     | DisbursementTypeCode1 | DisbursementTypeCode2 |
	| Court & Stamp Fees - Anticipated (NT) | Registraton Fees - Issuance of SSCT - Anticipated (NT) | GBP      | 200     | 50      | 100     | 300     | UK Output Domestic Standard | H20005A               | H52085A               |
@singapore
Examples:
	| Disbursement1                        | Disbursement2                         | Currency | Amount1 | Amount2 | Amount3 | Amount4 | TaxCode                     | DisbursementTypeCode1 | DisbursementTypeCode2 |
	| Registration Fees - Anticipated (NT) | Court & Stamp Fees - Anticipated (NT) | SGD      | 200     | 50      | 100     | 300     | SG Output Domestic Standard | H52005A               | H20005A               |
@staging
Examples:
	| Disbursement1                         | Disbursement2                         | Currency | Amount1 | Amount2 | Amount3 | Amount4 | TaxCode                     | DisbursementTypeCode1 | DisbursementTypeCode2 |
	| Court & Stamp Fees - Anticipated (NT) | Court & Stamp Fees - Anticipated (NT) | GBP      | 200     | 50      | 100     | 300     | UK Output Domestic Standard | H20005A               | H20005A               |
@uk
Examples:
	| Disbursement1 | Disbursement2                         | Currency | Amount1 | Amount2 | Amount3 | Amount4 | TaxCode                 | DisbursementTypeCode1 | DisbursementTypeCode2 |
	| Accommodation | Court & Stamp Fees - Anticipated (NT) | AED      | 200     | 50      | 100     | 300     | UK Output Domestic Zero | H10200                | H20005A               |
@qa
Examples:
	| Disbursement1 | Disbursement2           | Currency | Amount1 | Amount2 | Amount3 | Amount4 | TaxCode                 | DisbursementTypeCode1 | DisbursementTypeCode2 |
	| Catering      | Copying - Black & White | ZAR      | 200     | 50      | 100     | 300     | UK Output Domestic Zero | H70010                | S10010                |
@europe
Examples:
	| Disbursement1                                          | Disbursement2                         | Currency | Amount1 | Amount2 | Amount3 | Amount4 | TaxCode               | DisbursementTypeCode1 | DisbursementTypeCode2 |
	| Registraton Fees - Issuance of SSCT - Anticipated (NT) | Court & Stamp Fees - Anticipated (NT) | EUR      | 200     | 50      | 100     | 300     | ES Output Europe Zero | H52085A               | H20005A               |
@canada
Examples:
	| Disbursement1                                          | Disbursement2                         | Currency | Amount1 | Amount2 | Amount3 | Amount4 | TaxCode                         | DisbursementTypeCode1 | DisbursementTypeCode2 |
	| Registraton Fees - Issuance of SSCT - Anticipated (NT) | Court & Stamp Fees - Anticipated (NT) | CAD      | 200     | 50      | 100     | 300     | CA Output Domestic Standard GST | H52085A               | H20005A               |

Scenario Outline: 070 Cost Modify
	Given I try to modify the disbursement types
	Then I verify the new disbursement types
		| Disbursement    |
		| <Disbursement1> |
		| <Disbursement2> |
@ft
Examples:
	| Disbursement1                          | Disbursement2       |
	| Bank Charges & Admin Fee - Anticipated | Agency Registration |
@training
Examples:
	| Disbursement1                                          | Disbursement2                         |
	| Registraton Fees - Issuance of SSCT - Anticipated (NT) | Court & Stamp Fees - Anticipated (NT) |
@staging
Examples:
	| Disbursement1                         | Disbursement2                         |
	| Court & Stamp Fees - Anticipated (NT) | Court & Stamp Fees - Anticipated (NT) |
@uk
Examples:
	| Disbursement1 | Disbursement2                         |
	| Accommodation | Court & Stamp Fees - Anticipated (NT) |
@singapore
Examples:
	| Disbursement1                        | Disbursement2                        |
	| Bank & Finance Charges - Anticipated | Registration Fees - Anticipated (NT) |

@qa
Examples:
	| Disbursement1 | Disbursement2           |
	| Catering      | Copying - Black & White |
@europe
Examples:
	| Disbursement1                                          | Disbursement2                         |
	| Registraton Fees - Issuance of SSCT - Anticipated (NT) | Court & Stamp Fees - Anticipated (NT) |
@canada
Examples:
	| Disbursement1                         | Disbursement2                                   |
	| Court & Stamp Fees - Anticipated (NT) | Searches and Official Copies Fees - Anticipated |
