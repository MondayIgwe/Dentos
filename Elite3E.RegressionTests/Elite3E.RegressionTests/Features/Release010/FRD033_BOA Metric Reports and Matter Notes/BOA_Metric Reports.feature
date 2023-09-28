
#@release10 @frd033 @BOA_MetricReports
#Ignored because of defect https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/37954
@ignore
Feature: BOA_Metric Reports

PREP:
API - Create Fee Earner, Charge Type Group, ChargeType, Matter, charge modify entry
UI - Proforma Generation > Bill on Proforma Edit (create invoice) > Receipt Invoice

Test: 
Add to process 'Client Management Metric (with BOA)'
Input Table Name ClientMan_at[random]
Save and Run Metric, Submit

Validation:
Table created in DB

Note: 
SSMS Queries can be found in DIR: Global_UI_Automation\Elite3E.RegressionTests\Elite3E.RegressionTests\Resources\DentonsQueries
You'll need a connection string in the appsettings.local as well as DB access for your account in order to perform this tests.



Scenario Outline: 010 API Prep Data
	Given I create a user with details
		| UserName | DataRoleAlias |
		| <User>   | Admin         |
	And I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	Then I add a workflow user to a FeeEarner
		| User   | Name            |
		| <User> | <FeeEarnerName> |
	Given the charge type group exists
		| Code         | Description       | ChargeTypeGroupExcludeOrIncludeList |
		| at_BOA_Group | <ChargeTypeGroup> | IsIncludeList                       |
	And the below charge type added to the group
		| Code   | Description             | TransactionType | Category          |
		| at_BOA | <ChargeTypeDescription> | BOA Fees        | Billed on Account |
	And I create the Payer with Api
		| PayerName   | Entity        |
		| <PayorName> | WaterFlow Ltd |
	And I create a matter with details:
		| Client   | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department | Section | Rate     | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| <Client> | <FeeEarnerName>   | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | Bill                 | <Office> | Default    | Default | Standard | <ChargeTypeGroup>   | Desc_at_3e8glw7   | <PayorName> |
	And I add a charge modify with api
		| Narrative | TaxCodeDescription   | ChargeType              | Amount         | Currency   |
		| {Auto}+16 | <TaxCodeDescription> | <ChargeTypeDescription> | <ChargeAmount> | <Currency> |

@ft
Examples:
	| User       | Client       | FeeEarnerName | Office         | ChargeTypeDescription  | TaxCodeDescription         | ChargeAmount | ChargeTypeGroup | Currency | PayorName |
	| Rocky Cena | Rocky Client | Rocky Cena    | London (UKIME) | Automation BOA Charges | UK Output Domestic Zero 0% | 500.00       | at_BOA_Group    | GBP      | Agnes Old |

@singapore
Examples:
	| User       | Client       | FeeEarnerName | Office    | ChargeTypeDescription | TaxCodeDescription          | ChargeAmount | ChargeTypeGroup | Currency | PayorName |
	| Rocky Cena | Rocky Client | Rocky Cena    | Singapore | Interest              | SG Output Domestic Standard | 500.00       | at_BOA_Group    | SGD      | Agnes Old |

@uk
Examples:
	| User       | Client       | FeeEarnerName | Office         | ChargeTypeDescription  | TaxCodeDescription      | ChargeAmount | ChargeTypeGroup | Currency | PayorName |
	| Rocky Cena | Rocky Client | Rocky Cena    | London (UKIME) | Automation BOA Charges | UK Output Domestic Zero | 500.00       | at_BOA_Group    | GBP      | Agnes Old |
@qa
Examples:
	| User       | Client       | FeeEarnerName | Office         | ChargeTypeDescription  | TaxCodeDescription      | ChargeAmount | ChargeTypeGroup | Currency | PayorName |
	| Rocky Cena | Rocky Client | Rocky Cena    | London (UKIME) | Automation BOA Charges | UK Output Domestic Zero | 500.00       | at_BOA_Group    | GBP      | Agnes Old |
@training @staging
Examples:
	| User       | Client       | FeeEarnerName | Office         | ChargeTypeDescription  | TaxCodeDescription          | ChargeAmount | ChargeTypeGroup | Currency | PayorName |
	| Rocky Cena | Rocky Client | Rocky Cena    | London (UKIME) | Automation BOA Charges | UK Output Domestic Standard | 500.00       | at_BOA_Group    | GBP      | Agnes Old |
@europe
Examples:
	| User       | Client       | FeeEarnerName | Office      | ChargeTypeDescription  | TaxCodeDescription          | ChargeAmount | ChargeTypeGroup | Currency | PayorName |
	| Rocky Cena | Rocky Client | Rocky Cena    | London (EU) | Automation BOA Charges | UK Output Domestic Standard | 500.00       | at_BOA_Group    | EUR      | Agnes Old |
@canada
Examples:
	| User       | Client       | FeeEarnerName | Office    | ChargeTypeDescription | TaxCodeDescription          | ChargeAmount | ChargeTypeGroup | Currency | PayorName |
	| Rocky Cena | Rocky Client | Rocky Cena    | Vancouver | Automation Charge     | UK Output Domestic Standard | 500.00       | at_BOA_Group    | CAD      | Agnes Old |

Scenario Outline: 020 QA Perform Proforma Generation
#Using exsiting Proforma Generation due to QA Proforma Generation Bug: https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/34009
	Given I navigate to the Proforma Generation Process
	When I perform quick find for '<ProformaGenerationDesc>'
	Then I can generate an open proforma
		| Description              | Include Other Proformas | Proforma Status |
		| <ProformaGenerationDesc> | <IncludeOtherProformas> | Draft           |
@qa
Examples:
	| ProformaGenerationDesc     | IncludeOtherProformas |
	| MW Proforma Generation Run | No                    |
		
#This is for all environments other than QA
Scenario Outline: 025 Perform Proforma Generation
	Given I can generate the proforma
		| Description | Include Other Proformas | Proforma Status |
		| {Auto}+36   | <IncludeOtherProformas> | Draft           |
@ft @canada @europe @singapore @uk @training @staging
Examples:
	| ProformaGenerationDesc     | IncludeOtherProformas |
	| MW Proforma Generation Run | No                    |

@CancelProcess @ft @training @staging @canada @europe @uk @singapore @qa
Scenario Outline: 030 Bill No Print on Proforma Edit
	Given I proxy as user '<User>'
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	And I open the proforma for submission
	Then I submit it
	And I cancel proxy
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	When I open the proforma for billing
	And I bill it without printing
	And the invoice number is generated
	Given I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	And I open the billing workflow task and send the invoice

@ft @training @staging @canada @europe @uk @singapore @qa
Examples:
	| User       |
	| Rocky Cena |
	
	
Scenario Outline: 040 Receipt Invoice
	Given I add a new receipt
		| Receipt Date | Receipt Type  | Document Number | Narrative | Operating Unit  |
		| {Today}      | <ReceiptType> | {Auto}+36       | {Auto}+36 | <OperatingUnit> |
	When add the invoice on the receipt
	And the payer is auto populated
	And I receipt the total amount
	Then update the receipt
	And I can submit the receipt
	And I validate submit was successful

@ft @qa @training @staging @canada @europe @singapore
Examples:
	| ReceiptType  | OperatingUnit                  |
	| ICB_3000_GBP | Dentons UK and Middle East LLP |
@uk @training @staging
Examples:
	| ReceiptType  | OperatingUnit                  |
	| IGB_3000_GBP | Dentons UK and Middle East LLP |
@europe
Examples:
	| ReceiptType     | OperatingUnit                              |
	| AZBANKAZOP01EUR | Dentons Europe (Central Asia) Limited (AZ) |
@canada
Examples:
	| ReceiptType    | OperatingUnit      |
	| CACABMOOP01CAD | Dentons Canada LLP |


Scenario Outline: 050 BOA Metric Run and Database Validation
	Given I run the boa metric
		| Process                                    | TableName        | Currency   | EndDate |
		| Client Management Metric (with BOA)        | ClientManagement | <Currency> |         |
		| Matter Aged WIP Metric (By Aging with BOA) | MatterAgedWIP    | <Currency> |         |
		| Investment Metric (with BOA)               | InvestmentMetric | <Currency> | {Today} |
	When I navigate to my billable matters process
	And I enter details and search for my billable matters
		| WIPExcludeZeroCheckbox | ARExcludeZeroCheckbox | ClientAccountExcludeZeroCheckbox |
		| false                  | false                 | false                            |
	And I validate my billable matter boa balance is more than zero
	And I cancel the process
	And I perform database validation for boa metric '<ChargeAmount>'
	
@ft @singapore @uk @qa @training @staging
Examples:
	| ChargeAmount | Currency |
	| 500.00       | GBP      |
@europe
Examples:
	| ChargeAmount | Currency |
	| 500.00       | EUR      |
@canada
Examples:
	| ChargeAmount | Currency |
	| 500.00       | CAD      |
