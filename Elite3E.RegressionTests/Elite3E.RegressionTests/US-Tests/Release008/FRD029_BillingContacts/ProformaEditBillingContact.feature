@us
Feature: ProformaEditBillingContact
DEV005 - Billing Contacts child form on Proforma Edit

@CancelProcess
Scenario Outline: 010 Create Matter
	Given I create a user with details
		| UserName | DataRoleAlias | UserRoleList                                                                               |
		| <User>   | Admin         | 0:AD:G:Common Authorisations,0:WC:P:Proforma Processor,0:WC:P:Proforma Processor - Finance |
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

Examples:
	| FeeEarnerName    | User             | Client           | Currency        | CurrencyMethod | Office  | Department | Section | ChargeTypeGroup | CostTypeGroup | PayorName   |
	| Belgravia Thanks | Belgravia Thanks | Belgraiva client | USD - US Dollar | Bill           | Chicago | Default    | Default | Desc_at_3e8glw7 | Auto__18Xeq   | Carla Bates |

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

Examples:
	| DisbursementType                      | WorkCurrency | WorkAmount | Narrative                 | TaxCode                          | IncludeOtherProformas |
	| Court & Stamp Fees - Anticipated (NT) | USD          | 5000       | Automation Disbursement 1 | US Output Domestic Standard Rate | No                    |

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

Examples:
	| User             | ContactType               | Payer       |
	| Belgravia Thanks | Billing - Primary Contact | Carla Bates |

