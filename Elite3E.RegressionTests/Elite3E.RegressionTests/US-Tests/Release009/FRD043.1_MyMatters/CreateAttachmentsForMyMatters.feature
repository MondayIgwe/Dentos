
@us
Feature: CreateAttachmentsForMyMatters

Creating attachments from My Matters

@CancelProcess
Scenario Outline: 001 Verify that MyMatters overrides are setup correctly
	Given I navigate to the Override/Set System Options process
	Then the MyMarttrsIsGenerateIndividualProformas_ccc should be set to true
		| OptionName                                 | SystemDefault |
		| MyMattersIsGenerateIndividualProformas_ccc | True          |
	When I navigate to my billable matters process
	And the Individual documents checkbox is ticked

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


Examples:
	| FeeEarnerName | User          | Client        | Currency        | CurrencyMethod | Office  | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName   | DisbursementType | WorkCurrency | WorkAmount | TaxCode                          |
	| Mike Thanks   | Proforma User | Mikel Mandela | USD - US Dollar | Bill           | Default | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | Chicago       | James Mayor | Accomodation     | USD          | 5000       | US Output Domestic Standard Rate |


Scenario Outline: 003 Create Matter
	Given I create the Payer with Api
		| PayerName   | Entity     |
		| <PayorName> | EntityByte |
	When I create a matter with details:
		| Client   | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | Rate   | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| <Client> | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <Rate> | <ChargeTypeGroup>   | <CostTypeGroup>   | <PayorName> |

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

Examples:
	| DisbursementType              | WorkCurrency | WorkAmount | Narrative                 | TaxCode                          | IncludeOtherProformas |
	| Dentons United States Funding | USD          | 5000       | Automation Disbursement 1 | US Output Domestic Standard Rate | No                    |



