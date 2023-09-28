@release8 @frd029 @ProformaEditBillingContact
Feature: ProformaEditBillingContact
DEV005 - Billing Contacts child form on Proforma Edit

@CancelProcess
Scenario Outline: 010 Create Matter
	Given I create a user with details
		| UserName | DataRoleAlias |
		| <User>   | Admin         |
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
		| PayerName   | Entity      |
		| <PayorName> | Paragon Ltd |
	When I create a matter with details:
		| Client   | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| <Client> | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <ChargeTypeGroup>   | <CostTypeGroup>   | <PayorName> |

@europe
Examples:
	| FeeEarnerName    | User             | Client           | Currency | CurrencyMethod | Office      | Department | Section | ChargeTypeGroup | CostTypeGroup | PayorName   |
	| Belgravia Thanks | Belgravia Thanks | Belgraiva client | EUR      | Bill           | London (EU) | Default    | Default | Desc_at_3e8glw7 | Auto__18Xeq   | Carla Bates |
@singapore
Examples:
	| FeeEarnerName    | User             | Client           | Currency               | CurrencyMethod | Office    | Department            | Section | ChargeTypeGroup | CostTypeGroup | PayorName   |
	| Belgravia Thanks | Belgravia Thanks | Belgraiva client | SGD - Singapore Dollar | Bill           | Singapore | Corporate - Singapore | Default | Desc_at_3e8glw7 | Auto__18Xeq   | Carla Bates |
@canada
Examples:
	| FeeEarnerName    | User             | Client           | Currency              | CurrencyMethod | Office  | Department | Section | ChargeTypeGroup | CostTypeGroup | PayorName   |
	| Belgravia Thanks | Belgravia Thanks | Belgraiva client | CAD - Canadian Dollar | Bill           | Calgary | Default    | Default | Desc_at_3e8glw7 | Auto__18Xeq   | Carla Bates |
@uk
Examples:
	| FeeEarnerName    | User             | Client           | Currency            | CurrencyMethod | Office         | Department | Section | ChargeTypeGroup | CostTypeGroup | PayorName   |
	| Belgravia Thanks | Belgravia Thanks | Belgraiva client | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Desc_at_3e8glw7 | Auto__18Xeq   | Carla Bates |
@training @staging
Examples:
	| FeeEarnerName    | User             | Client           | Currency            | CurrencyMethod | Office         | Department | Section | ChargeTypeGroup | CostTypeGroup | PayorName   |
	| Belgravia Thanks | Belgravia Thanks | Belgraiva client | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Desc_at_3e8glw7 | Auto__18Xeq   | Carla Bates |
@qa
Examples:
	| FeeEarnerName    | User             | Client           | Currency            | CurrencyMethod | Office         | Department | Section | ChargeTypeGroup | CostTypeGroup | PayorName   |
	| Belgravia Thanks | Belgravia Thanks | Belgraiva client | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Desc_at_3e8glw7 | Auto__18Xeq   | Carla Bates |
@ft
Examples:
	| FeeEarnerName    | User             | Client           | Currency            | CurrencyMethod | Office         | Department | Section | ChargeTypeGroup | CostTypeGroup | PayorName   |
	| Belgravia Thanks | Belgravia Thanks | Belgraiva client | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Desc_at_3e8glw7 | Auto__18Xeq   | Carla Bates |

Scenario Outline: 020 Create a proforma Edit
	Given I create a hard cost disbursement type with details
		| Code               | Description        |
		| <DisbursementType> | <DisbursementType> |
	When I post the disbursement
		| Work Date | Disbursement Type  | Work Currency  | Work Amount  | Narrative   | Tax Code  |
		| {Today}-1 | <DisbursementType> | <WorkCurrency> | <WorkAmount> | <Narrative> | <TaxCode> |
	And I validate the disbursement is posted with no errors
	Then I can generate the proforma
		| Description | Include Other Proformas | Proforma Status |
		| {Auto}+36   | No                      | Draft           |

@europe
Examples:
	| DisbursementType                    | WorkCurrency | WorkAmount | Narrative                 | TaxCode                   | IncludeOtherProformas |
	| DENEU-Dentons Cross Region Invoices | EUR          | 5000       | Automation Disbursement 1 | EU Output Conversion Code | No                    |
@ft
Examples:
	| DisbursementType    | WorkCurrency | WorkAmount | Narrative                 | TaxCode                    | IncludeOtherProformas |
	| Advertising Charges | GBP          | 5000       | Automation Disbursement 1 | UK Output Domestic Zero 0% | No                    |
@qa
Examples:
	| DisbursementType               | WorkCurrency | WorkAmount | Narrative                 | TaxCode                    | IncludeOtherProformas |
	| Automation Agency Registration | GBP          | 5000       | Automation Disbursement 1 | UK Output Domestic Zero 0% | No                    |
@staging
Examples:
	| DisbursementType        | WorkCurrency | WorkAmount | Narrative                 | TaxCode                 | IncludeOtherProformas |
	| Automation Accomodation | GBP          | 5000       | Automation Disbursement 1 | UK Output Domestic Zero | No                    |
@training
Examples:
	| DisbursementType          | WorkCurrency | WorkAmount | Narrative                 | TaxCode                 | IncludeOtherProformas |
	| Court Fees  - Anticipated | GBP          | 5000       | Automation Disbursement 1 | UK Output Domestic Zero | No                    |
@canada
Examples:
	| DisbursementType           | WorkCurrency | WorkAmount | Narrative                 | TaxCode                         | IncludeOtherProformas |
	| Bank of Canada Certificate | CAD          | 5000       | Automation Disbursement 1 | CA Output Domestic Standard GST | No                    |
@singapore
Examples:
	| DisbursementType      | WorkCurrency | WorkAmount | Narrative                 | TaxCode                 | IncludeOtherProformas |
	| Application Fees (NT) | SGD          | 5000       | Automation Disbursement 1 | SG Output Domestic Zero | No                    |
@uk
Examples:
	| DisbursementType                    | WorkCurrency | WorkAmount | Narrative               | TaxCode                 | IncludeOtherProformas |
	| DENUK-Dentons Cross Region Invoices | GBP          | 5000       | Automation Accomodation | AE Output Domestic Zero | No                    |

Scenario: 030 Add Billing Contact info in Proforma Edit
	Given I proxy as user '<User>'
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	And I open the proforma for submission
	And I add a new Billing Contact info in Proforma
		| ContactType   | FirstName | LastName | ContactName | Email    | Payer   |
		| <ContactType> | {Auto}+4  | {Auto}+5 | {Auto}+10   | {Auto}+5 | <Payer> |
	And I submit it
	When I cancel proxy
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	And I open the proforma for billing
	Then the details should be saved correctly in the Proforma
	And I submit the form

@ft @training @staging @canada @europe @uk @singapore @qa
Examples:
	| User             | ContactType               | Payer       |
	| Belgravia Thanks | Billing - Primary Contact | Carla Bates |

