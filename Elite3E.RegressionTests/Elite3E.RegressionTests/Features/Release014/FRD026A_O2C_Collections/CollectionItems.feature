
Feature: CollectionItems


Scenario Outline: 010 Create a new Matter
	Given there exists a fiscal invoice setup
		| Unit   | Unit Description  |
		| <Unit> | <UnitDescription> |
	And I create a user with details
		| UserName | DataRoleAlias | UserRoleList   |
		| <User>   | Admin         | <UserRoleList> |
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
		| PayerName   | Entity        |
		| <PayorName> | LimeLight Ltd |
	And I create a matter with details:
		| Client   | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department | Section | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| <Client> | <FeeEarnerName>   | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | Bill                 | <Office> | Default    | Default | Desc_at_3e8glw7     | Desc_at_3e8glw7   | <PayorName> |
@ft
Examples:
	| DefaultOperatingAlias        | FeeEarnerName | User      | Client       | Unit | UnitDescription                         | Currency            | CurrencyMethod | Office         | Department | Section | Rate     | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName | UserRoleList |
	| Dentons Rodyk & Davidson LLP | Leo Peter     | Leo Peter | LeoPeter Ltd | 3511 | Dentons Europe Studio Legale Tributario | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Standard | Auto_IncludeALL | Auto_IncludeALL | Milan         | James May |              |
@singapore
Examples:
	| DefaultOperatingAlias        | FeeEarnerName | User      | Client       | Unit | UnitDescription              | Currency               | CurrencyMethod | Office    | Department | Section | Rate     | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName | UserRoleList |
	| Dentons Rodyk & Davidson LLP | Leo Peter     | Leo Peter | LeoPeter Ltd | 1201 | Dentons Rodyk & Davidson LLP | SGD - Singapore Dollar | Bill           | Singapore | Default    | Default | Standard | Auto_IncludeALL | Auto_IncludeALL | Milan         | James May |              |
@us
Examples:
	| DefaultOperatingAlias      | FeeEarnerName | User      | Client       | Unit | UnitDescription            | Currency        | CurrencyMethod | Office  | Department | Section | Rate     | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName   | UserRoleList |
	| Dentons United States, LLP | Leo Peter     | Leo Peter | LeoPeter Ltd | 1201 | Dentons United States, LLP | USD - US Dollar | Bill           | Chicaco | Default    | Default | Standard | Auto_IncludeALL | Auto_IncludeALL | Chicago       | James Mayor |              |

Scenario Outline: 020 Create a proforma Edit
	When I post the disbursement
		| Work Date | Disbursement Type  | Work Currency  | Work Amount  | Narrative | Tax Code  |
		| {Today}-1 | <DisbursementType> | <WorkCurrency> | <WorkAmount> | {Auto}+36 | <TaxCode> |
	And I validate the disbursement is posted with no errors
	Then I can generate the proforma
		| Description | Include Other Proformas | Proforma Status |
		| {Auto}+36   | <IncludeOtherProformas> | Draft           |
	And I cancel the process
@us
Examples:
	| DisbursementType                   | WorkCurrency | WorkAmount | TaxCode                          | IncludeOtherProformas |
	| Data Storage & Costs - Anticipated | USD          | 5000       | US Output Domestic Standard Rate | No                    |

@ft
Examples:
	| DisbursementType    | WorkCurrency | WorkAmount | TaxCode                         | IncludeOtherProformas |
	| Advertising Charges | GBP          | 5000       | UK Output Domestic Standard 20% | No                    |

@singapore
Examples:
	| DisbursementType | WorkCurrency | WorkAmount | TaxCode                     | IncludeOtherProformas |
	| Court Fees (NT)  | SGD          | 5000       | SG Output Domestic Standard | No                    |

Scenario Outline: 030 Create a time entry/modify
	Given I submit a time modify
		| Time Type  | Hours   | Narrative   | FeeEarnerName   | WorkRate | WorkAmount | WorkCurrency | Tax Code  |
		| <TimeType> | <Hours> | <Narrative> | <FeeEarnerName> | 1        | 1          | <Currency>   | <TaxCode> |

@ft
Examples:
	| FeeEarnerName | TimeType          | Hours | Narrative         | TaxCode                    | Currency            |
	| Leo Peter     | Fixed-Capped Fees | 1     | test automation 1 | UK Output Domestic Zero 0% | GBP - British Pound |

@us
Examples:
	| FeeEarnerName | TimeType          | Hours | Narrative         | TaxCode                    | Currency            |
	| Leo Peter     | Fixed-Capped Fees | 1     | test automation 1 | US Output Domestic Standard Rate | USD |


Scenario: 031 Proforma Edit
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
@ft @singapore @us
Examples:
	| User      |
	| Leo Peter |

@CancelProcess @training @canada @europe @uk @singapore @qa @ft
Scenario: 035 Validate Invoice GL has been posted
	Given I search for 'Workflow Dashboard'
	And I open the Invoice Dispatch workflow task
	And I send the invoice routed to finance team when dispatch method not set
	When I view the invoices

@CancelProcess
Scenario: 040 Start Notification task
	Given I search for 'Notification Start Task'
	And I start a notification task '<NotificationTask>'

@ft @training @staging @canada @europe @uk @singapore
Examples:
	| NotificationTask |
	| 2_Collections    |

@CancelProcess @training @canada @europe @uk @singapore @qa @ft
Scenario: 045 Validate if Include Invoice Attachment check exists in Collection items
	Given I search for process 'Collection Items' without add button
	When search for a payer
	And I select payer
	Then I validate 'Include Invoice Attachment' checkbox is available
	And validate if Include Invoice Attachment checkbox check box is editable
	And I cancel it

@CancelProcess @training @canada @europe @uk @singapore @qa @ft
Scenario: 050 Validate if Include Invoice Attachment check exists in Collection items using matter
	Given I search for process 'Collection Items' without add button
	When I search for the created matter
	Then I validate 'Include Invoice Attachment' checkbox is available
	And validate if Include Invoice Attachment checkbox check box is editable
	And I cancel it

@CancelProcess @training @canada @europe @uk @singapore @qa @ft
Scenario: 060 Validate if Include Invoice Attachment check exists in Collection items using invoice
	Given I search for process 'Collection Items' without add button
	When I quick search by invoice number
	Then I validate 'Include Invoice Attachment' checkbox is available
	And validate if Include Invoice Attachment checkbox check box is editable
	And I cancel it