@release9 @frd043.1 @CreateAttachmentsForMyMatters
Feature: CreateAttachmentsForMyMatters

Creating attachments from My Matters

@ft @training @staging @canada @europe @uk @singapore @us @qa @CancelProcess
Scenario Outline: 001 Verify that MyMatters overrides are setup correctly
	Given I navigate to the Override/Set System Options process
	Then the MyMarttrsIsGenerateIndividualProformas_ccc should be set to true
		| OptionName                                 | SystemDefault |
		| MyMattersIsGenerateIndividualProformas_ccc | True          |
	When I navigate to my billable matters process
	And the Individual documents checkbox is ticked
	And I close my billable matter

Scenario Outline: 002 Verify that generating a proforma causes an error when an override is set to true
	Given I create the Payer with Api
		| PayerName   | Entity       |
		| <PayorName> | CentralCoast |
	And I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	And I search or create a client
		| Entity Name | FeeEarnerFullName |
		| <Client>    | <FeeEarnerName>   |
	And I create a matter with details:
		| Client   | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | ChargeTypeGroupName | CostTypeGroupName | BillingOffice   | PayorName   |
		| <Client> | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <ChargeTypeGroup>   | <CostTypeGroup>   | <BillingOffice> | <PayorName> |
	When I post the disbursement
		| Work Date | Disbursement Type  | Work Currency  | Work Amount  | Narrative | Tax Code  |
		| {Today}-1 | <DisbursementType> | <WorkCurrency> | <WorkAmount> | {Auto}+36 | <TaxCode> |
	And I validate the disbursement is posted with no errors
	And I navigate to the Override/Set System Options process
	Then the ProfGen_Proforma_Template_ccc should be set to true
		| OptionName                    | FirmOverride |
		| ProfGen_Proforma_Template_ccc | True         |
	And I navigate to my billable matters process
	And I search or filter by matter
	And I get the Proforma Generation Attachment Error
	And I close my billable matter

@ft @training @qa @uk
Examples:
	| FeeEarnerName | User          | Client        | Currency            | CurrencyMethod | Office  | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName | DisbursementType    | WorkCurrency | WorkAmount | TaxCode                         |
	| Mike Thanks   | Proforma User | Mikel Mandela | GBP - British Pound | Bill           | Default | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | London (EU)   | James May | Advertising Charges | GBP          | 5000       | UK Output Domestic Standard 20% |

@staging
Examples:
	| FeeEarnerName | User          | Client        | Currency            | CurrencyMethod | Office  | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName | DisbursementType            | WorkCurrency | WorkAmount | TaxCode                 |
	| Mike Thanks   | Proforma User | Mikel Mandela | GBP - British Pound | Bill           | Default | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | London (EU)   | James May | Bank & Finance Charges (NT) | GBP          | 5000       | UK Output Domestic Zero |


	
Scenario Outline: 003 Create Matter
	Given I create the Payer with Api
		| PayerName   | Entity     |
		| <PayorName> | EntityByte |
	When I create a matter with details:
		| Client   | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | Rate   | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| <Client> | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <Rate> | <ChargeTypeGroup>   | <CostTypeGroup>   | <PayorName> |

@ft
Examples:
	| Client                       | Currency            | CurrencyMethod | Office         | Department | Section | Rate     | ChargeTypeGroup | CostTypeGroup | PayorName    |
	| Client_Automation at_HTOvMOn | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Standard | Desc_at_3e8glw7 | Auto__18Xeq   | Percy Tanner |

@training @staging @qa
Examples:
	| Client                       | Currency            | CurrencyMethod | Office         | Department | Section | Rate     | ChargeTypeGroup | CostTypeGroup | PayorName    |
	| Client_Automation at_HTOvMOn | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Standard | Desc_at_3e8glw7 | Auto__18Xeq   | Percy Tanner |
@europe
Examples:
	| Client                       | Currency | CurrencyMethod | Office      | Department | Section | Rate     | ChargeTypeGroup | CostTypeGroup | PayorName    |
	| Client_Automation at_HTOvMOn | EUR      | Bill           | London (EU) | Default    | Default | Standard | Desc_at_3e8glw7 | Auto__18Xeq   | Percy Tanner |
@canada
Examples:
	| Client                       | Currency               | CurrencyMethod | Office  | Department | Section | Rate     | ChargeTypeGroup | CostTypeGroup | PayorName    |
	| Client_Automation at_HTOvMOn | CAD - Canadian Dollars | Bill           | Calgary | Default    | Default | Standard | Desc_at_3e8glw7 | Auto__18Xeq   | Percy Tanner |
@uk
Examples:
	| Client                       | Currency            | CurrencyMethod | Office         | Department | Section | Rate     | ChargeTypeGroup | CostTypeGroup | PayorName    |
	| Client_Automation at_HTOvMOn | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Standard | Desc_at_3e8glw7 | Auto__18Xeq   | Percy Tanner |
@singapore
Examples:
	| Client                       | Currency               | CurrencyMethod | Office    | Department            | Section | Rate     | ChargeTypeGroup | CostTypeGroup | PayorName    |
	| Client_Automation at_HTOvMOn | SGD - Singapore Dollar | Bill           | Singapore | Corporate - Singapore | Default | Standard | Desc_at_3e8glw7 | Auto__18Xeq   | Percy Tanner |
@us
Examples:
	| Client                       | Currency        | CurrencyMethod | Office  | Department | Section | Rate     | ChargeTypeGroup | CostTypeGroup | PayorName    |
	| Client_Automation at_HTOvMOn | USD - US Dollar | Bill           | Chicago | Default    | Default | Standard | Desc_at_3e8glw7 | Auto__18Xeq   | Percy Tanner |

Scenario Outline: 004 Generate a Proforma
	Given I create a hard cost disbursement type with details
		| Code               | Description        |
		| <DisbursementType> | <DisbursementType> |
	When I post the disbursement
		| Work Date | Disbursement Type  | Work Currency  | Work Amount  | Narrative   | Tax Code  |
		| {Today}-1 | <DisbursementType> | <WorkCurrency> | <WorkAmount> | <Narrative> | <TaxCode> |
	And I validate the disbursement is posted with no errors
	Then I can generate the proforma
		| Description | Include Other Proformas | Proforma Status |
		| {Auto}+10   | No                      | Draft           |

@qa
Examples:
	| DisbursementType               | WorkCurrency | WorkAmount | Narrative                 | TaxCode                         | IncludeOtherProformas |
	| Automation Agency Registration | GBP          | 5000       | Automation Disbursement 1 | UK Output Domestic Standard 20% | No                    |
@staging
Examples:
	| DisbursementType            | WorkCurrency | WorkAmount | Narrative                 | TaxCode                 | IncludeOtherProformas |
	| Bank & Finance Charges (NT) | GBP          | 5000       | Automation Disbursement 1 | UK Output Domestic Zero | No                    |
@ft
Examples:
	| DisbursementType    | WorkCurrency | WorkAmount | Narrative                 | TaxCode                         | IncludeOtherProformas |
	| Advertising Charges | GBP          | 5000       | Automation Disbursement 1 | UK Output Domestic Standard 20% | No                    |
@training
Examples:
	| DisbursementType          | WorkCurrency | WorkAmount | Narrative                 | TaxCode                         | IncludeOtherProformas |
	| Court Fees  - Anticipated | GBP          | 5000       | Automation Disbursement 1 | UK Output Domestic Standard 20% | No                    |
@canada
Examples:
	| DisbursementType           | WorkCurrency | WorkAmount | Narrative                 | TaxCode                         | IncludeOtherProformas |
	| Bank of Canada Certificate | CAD          | 5000       | Automation Disbursement 1 | CA Output Domestic Standard GST | No                    |
@europe
Examples:
	| DisbursementType                    | WorkCurrency | WorkAmount | Narrative                 | TaxCode                 | IncludeOtherProformas |
	| DENEU-Dentons Cross Region Invoices | EUR          | 5000       | Automation Disbursement 1 | AE Output Domestic Zero | No                    |
@uk
Examples:
	| DisbursementType                    | WorkCurrency | WorkAmount | Narrative                 | TaxCode                 | IncludeOtherProformas |
	| DENUK-Dentons Cross Region Invoices | GBP          | 5000       | Automation Disbursement 1 | AE Output Domestic Zero | No                    |
@singapore
Examples:
	| DisbursementType                    | WorkCurrency | WorkAmount | Narrative                 | TaxCode                     | IncludeOtherProformas |
	| DENSG-Dentons Cross Region Invoices | SGD          | 5000       | Automation Disbursement 1 | SG Output Domestic Standard | No                    |
@us
Examples:
	| DisbursementType                    | WorkCurrency | WorkAmount | Narrative                 | TaxCode                          | IncludeOtherProformas |
	| DENUS-Dentons Cross Region Invoices | USD          | 5000       | Automation Disbursement 1 | US Output Domestic Standard Rate | No                    |



