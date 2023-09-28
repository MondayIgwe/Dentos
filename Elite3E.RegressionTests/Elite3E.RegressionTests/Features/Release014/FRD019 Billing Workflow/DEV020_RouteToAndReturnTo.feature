﻿Feature: DEV020_RouteToAndReturnTo

Verify Return to Billing Timekeeper boolean is set to False
in Proforma Edit for Biller Edit task proforma should go to supervising timekeeper

@CancelProcess
Scenario Outline: 010 Create a new Matter and data preparation
	Given I create the Payer with Api
		| PayerName   | Entity       |
		| <PayorName> | CentralCoast |
	When I create a user with details
		| UserName | DataRoleAlias | UserRoleList   |
		| <User>   | Admin         | <UserRoleList> |
	And I create a user with details
		| UserName      | DataRoleAlias | DefaultOperatingAlias   | UserRoleList   |
		| <BillingUser> | Admin         | <DefaultOperatingAlias> | <UserRoleList> |
	Then I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	And I add a workflow user to a FeeEarner
	| User   | Name            |
	| <User> | <FeeEarnerName> |
	And I search or create a client
		| Entity Name | FeeEarnerFullName |
		| <Client>    | <FeeEarnerName>   |
	And I create a matter with details:
		| Client   | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | ChargeTypeGroupName | CostTypeGroupName | BillingOffice   | PayorName   |
		| <Client> | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <ChargeTypeGroup>   | <CostTypeGroup>   | <BillingOffice> | <PayorName> |

@ft @training @qa @uk
Examples:
	| BillingUser       | FeeEarnerName | User       | Client       | Currency            | CurrencyMethod | Office  | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName | DefaultOperatingAlias          | UserRoleList |
	| Billing FeeEarner | FRD19 John    | FRD19 John | FRD19 Client | GBP - British Pound | Bill           | Default | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | London (EU)   | James May | Dentons UK and Middle East LLP |              |
@staging
Examples:
	| BillingUser       | FeeEarnerName | User       | Client       | Currency            | CurrencyMethod | Office  | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName | DefaultOperatingAlias          | UserRoleList                                                                |
	| Billing FeeEarner | FRD19 John    | FRD19 John | FRD19 Client | GBP - British Pound | Bill           | Default | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | London (EU)   | James May | Dentons UK and Middle East LLP | 0:AD:G:Common Authorisations,0:AD:G:System Administrator (read-only setups) |

@europe
Examples:
	| BillingUser       | FeeEarnerName | User       | Client       | Currency   | CurrencyMethod | Office              | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName | DefaultOperatingAlias          | UserRoleList |
	| Billing FeeEarner | FRD19 John    | FRD19 John | FRD19 Client | EUR - Euro | Bill           | London Billing (EU) | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | London (EU)   | James May | Dentons UK and Middle East LLP |              |
@canada
Examples:
	| BillingUser       | FeeEarnerName | User       | Client       | Currency              | CurrencyMethod | Office   | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName | DefaultOperatingAlias          | UserRoleList |
	| Billing FeeEarner | FRD19 John    | FRD19 John | FRD19 Client | CAD - Canadian Dollar | Bill           | Montreal | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | Montreal      | James May | Dentons UK and Middle East LLP |              |
@singapore
Examples:
	| BillingUser       | FeeEarnerName | User       | Client       | Currency               | CurrencyMethod | Office    | Department            | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName | DefaultOperatingAlias          | UserRoleList |
	| Billing FeeEarner | FRD19 John    | FRD19 John | FRD19 Client | SGD - Singapore Dollar | Bill           | Singapore | Corporate - Singapore | Default | Auto_IncludeALL | Auto_IncludeALL | Singapore     | James May | Dentons UK and Middle East LLP |              |

@CancelProcess
Scenario Outline: 020 Post Disbursement and Generate Proforma
	When I post the disbursement
		| Work Date | Disbursement Type  | Work Currency  | Work Amount  | Narrative | Tax Code  |
		| {Today}-1 | <DisbursementType> | <WorkCurrency> | <WorkAmount> | {Auto}+36 | <TaxCode> |
	And I validate the disbursement is posted with no errors
	Then I can generate the proforma
		| Description | Include Other Proformas | Proforma Status |
		| {Auto}+36   | <IncludeOtherProformas> | Draft           |
@ft
Examples:
	| DisbursementType    | WorkCurrency | WorkAmount | TaxCode                         | IncludeOtherProformas |
	| Advertising Charges | GBP          | 5000       | UK Output Domestic Standard 20% | No                    |
@uk
Examples:
	| DisbursementType        | WorkCurrency | WorkAmount | TaxCode                     | IncludeOtherProformas |
	| Automation Accomodation | GBP          | 5000       | UK Output Domestic Standard | No                    |
@qa
Examples:
	| DisbursementType | WorkCurrency | WorkAmount | TaxCode                         | IncludeOtherProformas |
	| Accommodation    | GBP          | 5000       | UK Output Domestic Standard 20% | No                    |
@training
Examples:
	| DisbursementType                                       | WorkCurrency | WorkAmount | TaxCode                     | IncludeOtherProformas |
	| Registraton Fees - Issuance of SSCT - Anticipated (NT) | GBP          | 5000       | UK Output Domestic Standard | No                    |
@staging
Examples:
	| DisbursementType            | WorkCurrency | WorkAmount | TaxCode                     | IncludeOtherProformas |
	| Bank & Finance Charges (NT) | GBP          | 5000       | UK Output Domestic Standard | No                    |
@singapore
Examples:
	| DisbursementType         | WorkCurrency | WorkAmount | TaxCode                     | IncludeOtherProformas |
	| Agency Registration (NT) | SGD          | 5000       | SG Output Domestic Standard | No                    |
@canada
Examples:
	| DisbursementType           | WorkCurrency | WorkAmount | TaxCode                   | IncludeOtherProformas |
	| Bank of Canada Certificate | CAD          | 5000       | CA Output BC Standard PST | No                    |
@europe
Examples:
	| DisbursementType        | WorkCurrency | WorkAmount | TaxCode               | IncludeOtherProformas |
	| Automation Accomodation | EUR          | 5000       | ES Output Europe Zero | No                    |

@CancelProcess
Scenario Outline: 030 Verify Return to Responsible Timekeeper
	Given I proxy as user '<User>'
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	And I open the proforma for submission
	Then I set the return to billing fee earner to false
	And I update it
	And I submit it
	And I cancel proxy
	And I proxy as user '<BillingUser>'
	And I search for 'Workflow Dashboard'
	When I verify that there are no workflow tasks visible
	And I cancel proxy
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	And I open the proforma for billing
	And I bill it without printing

@ft @training @staging @canada @europe @uk @singapore @qa
Examples:
	| User       | BillingUser       |
	| FRD19 John | Billing FeeEarner |

