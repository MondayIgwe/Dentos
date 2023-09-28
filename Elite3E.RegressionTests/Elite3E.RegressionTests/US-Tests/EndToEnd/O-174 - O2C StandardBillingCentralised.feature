@us @sanity
Feature: O-174 - O2C: Standard Billing (Centralised)

The proforma is then sent for approval (please check the proforma follows the write down threshold policy).
Azure link: https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/82235


Scenario Outline: 010 Prepare Required Data
	Given I create a user with details
		| UserName | DataRoleAlias |
		| <User>   | Admin         |
	And I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	Then I add a workflow user to a FeeEarner
		| User   | Name            |
		| <User> | <FeeEarnerName> |
	And I search or create a client
		| Entity Name | FeeEarnerFullName |
		| <Client>    | <FeeEarnerName>   |
	And I create the Payer with Api
		| PayerName   | Entity   |
		| <PayorName> | <Client> |
	Then I create a matter with details:
		| Client   | Status     | OpenDate   | MatterName | Currency   | MatterCurrencyMethod | Office   | Department | Section | Rate     | ChargeTypeGroupName | CostTypeGroupName | PayorName   | FeeEarnerFullName |
		| <Client> | Fully Open | {Today}-20 | {Auto}+36  | <Currency> | Bill                 | <Office> | Default    | Default | Standard | <ChargeTypeGroup>   | <CostTypeGroup>   | <PayorName> | <FeeEarnerName>   |
	Then I create a submatter 1 with details:
		| Client   | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department | Section | ChargeTypeGroupName | CostTypeGroupName | BillingOffice | PayorName   |
		| <Client> | <FeeEarnerName>   | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | Bill                 | <Office> | Default    | Default | <ChargeTypeGroup>   | <CostTypeGroup>   | Office>       | <PayorName> |

Examples:
	| User        | FeeEarnerName | Currency        | Client            | Office  | OperationUnit              | ChargeTypeGroup | CostTypeGroup | PayorName    |
	| Brown Jones | Brown Jones   | USD - US Dollar | BrownJones Client | Chicago | Dentons United States, LLP | Desc_at_3e8glw7 | Auto__18Xeq   | Lucas Wright |
	

Scenario Outline: 020 Add new Client using Client Maintenance process
	Given the client maintenace process is opened
	When I enter client details
		| Opening Fee Earner | Date Opened | Status     | Status Date | Country       | Currency | Narrative | Language | Invoice Site   |
		| <OpeningFeeEarner> | {Today}     | Fully Open | {Today}     | UNITED STATES | USD      | {Auto}+8  | English  | London UK site |
	Then I can enter the effective dates information an save
		| Billing Fee Earner | Responsible Fee Earner | Supervisor Fee Earner | Office   |
		| <BillingFeeEarner> | <ResponsibleFeeEarner> | <SupervisorFeeEarner> | <Office> |

Examples:
	| OpeningFeeEarner | BillingFeeEarner | ResponsibleFeeEarner | SupervisorFeeEarner | Office  |
	| 203135           | 224182           | 224183               | 224181              | Chicago |

	
Scenario Outline: 003 Matter Maintenance
	Given I create the Payer with Api
		| PayerName   | Entity       |
		| <PayorName> | CentralCoast |
	And I create a new matter
	When I update the matter
		| OpeningFeeEarner   | Status     | Open Date | Matter Name | Matter Currency Method | Statement Site                  | MatterType                          | MatterAttribute | Language |
		| <OpeningFeeEarner> | Fully Open | {Today}   | {Auto}+10   | Bill                   | 10 Umhlanga rocks drive, Durban | Non Taxable Real Estate Transaction | Client Billable | English  |
	And I add a Matter Payer
		| Start Date |
		| {Today}+1  |
	And I update the effective dated information
		| Child Form                  | Office  |
		| Effective Dated Information | Chicago |
	And I update the matter rates
		| Child Form   | Rate     |
		| Matter Rates | Standard |
	And I add a new cost type group
		| Cost Type Group |
		| Desc_at_VowqCrR |
	And I add new charge type group
		| Charge Type Group |
		| Desc_at_3e8glw7   |
	And I submit it
	Then verify the matter number is generated
	And I submit a time modify
		| Time Type  | Hours | Narrative | FeeEarnerName      | WorkRate | WorkAmount | WorkCurrency | TaxCode   |
		| <TimeType> | 0.1   | {Auto}+10 | <OpeningFeeEarner> | 1        | 0.1        | <WorkCurrency>   | <TaxCode> |
Examples:
	| TimeType | OpeningFeeEarner | PayorName   | WorkCurrency | WorkAmount | TaxCode                          |
	| FEES     | 203135           | James Mayor | USD          | 5000       | US Output Domestic Standard Rate |
