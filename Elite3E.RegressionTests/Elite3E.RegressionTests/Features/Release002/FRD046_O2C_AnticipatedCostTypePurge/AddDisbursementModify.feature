@release2 @frd046 @AddDisbursementModify
Feature: AddDisbursementModify


Scenario Outline: 010 Create a new Matter
		Given I create the Payer with Api
		| PayerName   | Entity          |
		| <PayorName> | SafeHarbour Ltd |
	And I create a matter with details:
		| Client   | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | ChargeTypeGroupName | CostTypeGroupName | BillingOffice   | PayorName   |
		| <Client> | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <ChargeTypeGroup>   | <CostTypeGroup>   | <BillingOffice> | <PayorName> |

@training @staging @uk @qa @PipelineApiSuccessfulTest
Examples:
	| Client                       | Currency            | CurrencyMethod | Office  | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName   |
	| Client_Automation at_HTOvMOn | GBP - British Pound | Bill           | Default | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | London (EU)   | Melisa Tate |
@ft
Examples:
	| Client                       | Currency            | CurrencyMethod | Office         | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName   |
	| Client_Automation at_HTOvMOn | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | London (EU)   | Melisa Tate |
@europe
Examples:
	| Client                       | Currency            | CurrencyMethod | Office      | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName   |
	| Client_Automation at_HTOvMOn | GBP - British Pound | Bill           | London (EU) | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | London (EU)   | Melisa Tate |
@canada
Examples:
	| Client                       | Currency              | CurrencyMethod | Office   | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName   |
	| Client_Automation at_HTOvMOn | CAD - Canadian Dollar | Bill           | Montreal | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | Montreal      | Melisa Tate |
@singapore
Examples:
	| Client                       | Currency               | CurrencyMethod | Office    | Department            | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName   |
	| Client_Automation at_HTOvMOn | SGD - Singapore Dollar | Bill           | Singapore | Corporate - Singapore | Default | Auto_IncludeALL | Auto_IncludeALL | Singapore     | Melisa Tate |

Scenario Outline: 020 Create a Disbursement Modify and Submit
	When I submit the disbursement modify
		| Work Date | Disbursement Type  | Work Currency  | Work Amount  | Narrative | Tax Code  | Purge Type  |
		| {Today}-1 | <DisbursementType> | <WorkCurrency> | <WorkAmount> | {Auto}+36 | <TaxCode> | <PurgeType> |
	Then the disbursement is not available

@ft
Examples:
	| DisbursementType          | WorkCurrency | WorkAmount | TaxCode                         | PurgeType      |
	| Anticipated Costs General | GBP          | 5000       | UK Output Domestic Standard 20% | Billable Purge |
@qa
Examples:
	| DisbursementType               | WorkCurrency | WorkAmount | TaxCode                         | PurgeType      |
	| Automation Agency Registration | GBP          | 5000       | UK Output Domestic Standard 20% | Billable Purge |
@staging @uk
Examples:
	| DisbursementType   | WorkCurrency | WorkAmount | TaxCode                     | PurgeType      |
	| Administration Fee | AED          | 5000       | AE Output Domestic Standard | Billable Purge |
@singapore
Examples:
	| DisbursementType                      | WorkCurrency | WorkAmount | TaxCode                     | PurgeType      |
	| Court & Stamp Fees - Anticipated (NT) | SGD          | 5000       | SG Output Domestic Standard | Billable Purge |
@training
Examples:
	| DisbursementType                      | WorkCurrency | WorkAmount | TaxCode                     | PurgeType      |
	| Court & Stamp Fees - Anticipated (NT) | AED          | 5000       | AE Output Domestic Standard | Billable Purge |
@europe
Examples:
	| DisbursementType        | WorkCurrency | WorkAmount | TaxCode               | PurgeType      |
	| Automation Accomodation | EUR          | 5000       | ES Output Europe Zero | Billable Purge |

@canada
Examples:
	| DisbursementType                                       | WorkCurrency | WorkAmount | TaxCode                         | PurgeType      |
	| Registraton Fees - Issuance of SSCT - Anticipated (NT) | CAD          | 5000       | CA Output Domestic Standard GST | Billable Purge |