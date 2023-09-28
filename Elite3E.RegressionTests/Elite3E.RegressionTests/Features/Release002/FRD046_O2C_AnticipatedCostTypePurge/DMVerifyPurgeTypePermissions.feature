@release2 @frd046 @DMVerifyPurgeTypePermissions
Feature: DMVerifyPurgeTypePermissions
Verify Purge Type field is only displayed when you have the purge permissions


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
#@training @staging  @canada @europe @uk @singapore  
#	Examples: 
#		| Description   | IncludeOrExcludeOption | DisbursementType1   | DisbursementType2                     | TransactionType1 | TransactionType2      | HardOrSoftDisbursement | 
#		| Auto_PD_Group | IsIncludeList          | Agency Registration | Court & Stamp Fees - Anticipated (NT) | Hard Cost        | Anticipated Hard Cost | Hard                         | 

@LoginAsAdminUser1
Scenario Outline: 010 Create a new Matter
	Given I create the Payer with Api
		| PayerName   | Entity          |
		| <PayorName> | SafeHarbour Ltd |
	And I create a matter with details:
		| Client   | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | ChargeTypeGroupName | CostTypeGroupName | BillingOffice   | PayorName   |
		| <Client> | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <ChargeTypeGroup>   | <CostTypeGroup>   | <BillingOffice> | <PayorName> |
@ft @training @staging  @uk @qa
Examples:
	| Client                       | Currency            | CurrencyMethod | Office  | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName   |
	| Client_Automation at_HTOvMOn | GBP - British Pound | Bill           | Default | Default    | Default | Auto_PD_CHGroup | Auto_PD_CTGroup | London (EU)   | Melisa Tate |
@europe
Examples:
	| Client                       | Currency   | CurrencyMethod | Office      | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName   |
	| Client_Automation at_HTOvMOn | EUR - Euro | Bill           | London (EU) | Default    | Default | Auto_PD_CHGroup | Auto_PD_CTGroup | London (EU)   | Melisa Tate |
@canada
Examples:
	| Client                       | Currency              | CurrencyMethod | Office   | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName   |
	| Client_Automation at_HTOvMOn | CAD - Canadian Dollar | Bill           | Montreal | Default    | Default | Auto_PD_CHGroup | Auto_PD_CTGroup | Montreal      | Melisa Tate |
@singapore
Examples:
	| Client                       | Currency               | CurrencyMethod | Office    | Department            | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName   |
	| Client_Automation at_HTOvMOn | SGD - Singapore Dollar | Bill           | Singapore | Corporate - Singapore | Default | Auto_PD_CHGroup | Auto_PD_CTGroup | Singapore     | Melisa Tate |

#@LoginAsAdminUser1
#Scenario Outline: 010 Clone Matter and Add Disbursements
#	Given I view the matter "<MatterNumber>"
#	And clone it
#		| Matter Name | Open Date | Status Change Date |
#		| {Auto}+36   | {Today}-1 | {Today}-1          |
#
#	@ft @qa
#	Examples:
#		| MatterNumber |
#		| 100000012    |
#
#	@training @staging  @canada @europe @uk @singapore  
#	Examples:
#		| MatterNumber |
#		| 100000002    |

@CancelProcess
Scenario Outline: 020 User without Purge Permissions
	When I add the disbursement modify
		| Work Date | Disbursement Type  | Work Currency  | Work Amount  | Narrative | Tax Code  |
		| {Today}-1 | <DisbursementType> | <WorkCurrency> | <WorkAmount> | {Auto}+36 | <TaxCode> |
	Then purge type is disabled on disbursement modify

@ft @qa
Examples:
	| DisbursementType                      | WorkCurrency | WorkAmount | TaxCode                         |
	| Court & Stamp Fees - Anticipated (NT) | GBP          | 5000       | UK Output Domestic Standard 20% |
@training @staging
Examples:
	| DisbursementType                      | WorkCurrency | WorkAmount | TaxCode                     |
	| Court & Stamp Fees - Anticipated (NT) | AED          | 5000       | AE Output Domestic Standard |

@uk
Examples:
	| DisbursementType        | WorkCurrency | WorkAmount | TaxCode                     |
	| Automation Accomodation | AED          | 5000       | AE Output Domestic Standard |
@europe
Examples:
	| DisbursementType                                       | WorkCurrency | WorkAmount | TaxCode               |
	| Registraton Fees - Issuance of SSCT - Anticipated (NT) | EUR          | 5000       | ES Output Europe Zero |
@canada
Examples:
	| DisbursementType                      | WorkCurrency | WorkAmount | TaxCode                         |
	| Court & Stamp Fees - Anticipated (NT) | CAD          | 5000       | CA Output Domestic Standard GST |
@singapore
Examples:
	| DisbursementType                      | WorkCurrency | WorkAmount | TaxCode                     |
	| Court & Stamp Fees - Anticipated (NT) | SGD          | 5000       | SG Output Domestic Standard |