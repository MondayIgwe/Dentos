@release2 @frd046 @PEPurgeAniticipatedCost
Feature: PEPurgeAniticipatedCost
	Purge Cost Cards from the Proforma Edit process


Scenario Outline: 020 Create a new Matter
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
		| PayerName   | Entity          |
		| <PayorName> | SafeHarbour Ltd |
	And I create a matter with details:
		| Client   | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | ChargeTypeGroupName | CostTypeGroupName | BillingOffice   | PayorName   |
		| <Client> | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <ChargeTypeGroup>   | <CostTypeGroup>   | <BillingOffice> | <PayorName> |

@ft @training @staging @uk @qa
Examples:
	| User       | Client         | FeeEarnerName | Currency            | CurrencyMethod | Office  | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName   |
	| Sticks Sam | Sticks Mandela | Sticks Sam    | GBP - British Pound | Bill           | Default | Default    | Default | Auto_PD_CHGroup | Auto_PD_CTGroup | London (EU)   | Melisa Tate |
@europe
Examples:
	| User       | Client         | FeeEarnerName | Currency   | CurrencyMethod | Office      | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName   |
	| Sticks Sam | Sticks Mandela | Sticks Sam    | EUR - Euro | Bill           | London (EU) | Default    | Default | Auto_PD_CHGroup | Auto_PD_CTGroup | London (EU)   | Melisa Tate |
@canada
Examples:
	| User       | Client         | FeeEarnerName | Currency              | CurrencyMethod | Office   | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName   |
	| Sticks Sam | Sticks Mandela | Sticks Sam    | CAD - Canadian Dollar | Bill           | Montreal | Default    | Default | Auto_PD_CHGroup | Auto_PD_CTGroup | Montreal      | Melisa Tate |
@singapore
Examples:
	| User       | Client         | FeeEarnerName | Currency               | CurrencyMethod | Office    | Department            | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName   |
	| Sticks Sam | Sticks Mandela | Sticks Sam    | SGD - Singapore Dollar | Bill           | Singapore | Corporate - Singapore | Default | Auto_PD_CHGroup | Auto_PD_CTGroup | Singapore     | Melisa Tate |

Scenario Outline: 030 Create a Disbursement and Generate Proforma
	When I post the disbursement
		| Work Date | Disbursement Type  | Work Currency  | Work Amount  | Narrative | Tax Code  |
		| {Today}-1 | <DisbursementType> | <WorkCurrency> | <WorkAmount> | {Auto}+36 | <TaxCode> |
	And I validate the disbursement is posted with no errors
	Then I can generate the proforma
		| Description | Include Other Proformas | Proforma Status |
		| {Auto}+36   | <IncludeOtherProformas> | Draft           |

@ft
Examples:
	| DisbursementType                      | WorkCurrency | WorkAmount | TaxCode                         | IncludeOtherProformas |
	| Court & Stamp Fees - Anticipated (NT) | GBP          | 5000       | UK Output Domestic Standard 20% | No                    |
@qa
Examples:
	| DisbursementType | WorkCurrency | WorkAmount | TaxCode                         | IncludeOtherProformas |
	| Accommodation    | GBP          | 5000       | UK Output Domestic Standard 20% | No                    |
@training @uk
Examples:
	| DisbursementType    | WorkCurrency | WorkAmount | TaxCode                 | IncludeOtherProformas |
	| Agency Registration | GBP          | 5000       | UK Output Domestic Zero | No                    |
@staging
Examples:
	| DisbursementType                      | WorkCurrency | WorkAmount | TaxCode                 | IncludeOtherProformas |
	| Registration Fees - Caveat -Anticipated (NT) | GBP          | 5000       | UK Output Domestic Zero | No                    |
@europe
Examples:
	| DisbursementType                                       | WorkCurrency | WorkAmount | TaxCode               | IncludeOtherProformas |
	| Registraton Fees - Issuance of SSCT - Anticipated (NT) | EUR          | 5000       | ES Output Europe Zero | No                    |
@canada
Examples:
	| DisbursementType                      | WorkCurrency | WorkAmount | TaxCode                         | IncludeOtherProformas |
	| Court & Stamp Fees - Anticipated (NT) | CAD          | 5000       | CA Output Domestic Standard GST | No                    |
@singapore
Examples:
	| DisbursementType                      | WorkCurrency | WorkAmount | TaxCode                     | IncludeOtherProformas |
	| Court & Stamp Fees - Anticipated (NT) | SGD          | 5000       | SG Output Domestic Standard | No                    |

#Defect #https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/69247
#Below scenarios are ignored as well 
@CancelProcess @ignore
Scenario Outline: 040 Create a Proforma Edit, add a disbursement and generate Bill
	Given I proxy as user '<User>'
	And I search for 'Workflow Dashboard'
	When I open the proforma workflow task
	And I open the proforma for submission
	Then add the disbursement on the proforma edit
		| Work Date | Disbursement Type  | Anticipated   | Reference Currency  | Work Amount  | Tax Code  | Narrative | Reason     |
		| {Today}-1 | <DisbursementType> | <Anticipated> | <ReferenceCurrency> | <WorkAmount> | <TaxCode> | {Auto}+36 | Correction |
	And I submit it
	And I cancel proxy
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	When I open the proforma for billing
	And I bill it without printing
	Then the invoice number is generated
	Given I search for 'Workflow Dashboard'
	And I open the Invoice Dispatch workflow task
	When I send the invoice routed to finance team when dispatch method not set
@ft
Examples:
	| User       | DisbursementType                      | Anticipated | ReferenceCurrency | WorkAmount | TaxCode                         |
	| Sticks Sam | Court & Stamp Fees - Anticipated (NT) | Yes         | GBP               | 5001       | UK Output Domestic Standard 20% |
@qa
Examples:
	| User       | DisbursementType | Anticipated | ReferenceCurrency | WorkAmount | TaxCode                         |
	| Sticks Sam | Accommodation    | Yes         | GBP               | 5001       | UK Output Domestic Standard 20% |
@training @staging
Examples:
	| User       | DisbursementType                                       | Anticipated | ReferenceCurrency | WorkAmount | TaxCode                     |
	| Sticks Sam | Registraton Fees - Issuance of SSCT - Anticipated (NT) | Yes         | AED               | 5001       | AE Output Domestic Standard |
@uk
Examples:
	| User       | DisbursementType                     | Anticipated | ReferenceCurrency | WorkAmount | TaxCode                     |
	| Sticks Sam | Bank & Finance Charges - Anticipated | Yes         | AED               | 5001       | AE Output Domestic Standard |

@europe
Examples:
	| User       | DisbursementType          | Anticipated | ReferenceCurrency | WorkAmount | TaxCode               |
	| Sticks Sam | Court Fees  - Anticipated | Yes         | EUR               | 5001       | ES Output Europe Zero |
@canada
Examples:
	| User       | DisbursementType                      | Anticipated | ReferenceCurrency | WorkAmount | TaxCode                         |
	| Sticks Sam | Court & Stamp Fees - Anticipated (NT) | Yes         | CAD               | 5001       | CA Output Domestic Standard GST |
@singapore
Examples:
	| User       | DisbursementType                                       | Anticipated | ReferenceCurrency | WorkAmount | TaxCode                     |
	| Sticks Sam | Registraton Fees - Issuance of SSCT - Anticipated (NT) | Yes         | SGD               | 5001       | SG Output Domestic Standard |

@CancelProcess @ignore
Scenario Outline: 050 New proforma Generation
	Given I can generate the proforma
		| Description | Include Other Proformas | Proforma Status |
		| {Auto}+36   | <IncludeOtherProformas> | Draft           |
	And I proxy as user '<User>'
	And I search for 'Workflow Dashboard'
	When I open the proforma workflow task
	And I open the proforma for submission
	Then a negative entry is generated
	And I cancel it
	And I cancel proxy

@ft @qa
Examples:
	| User       | IncludeOtherProformas |
	| Sticks Sam | No                    |
@training @staging @canada @europe @uk @singapore
Examples:
	| User       | IncludeOtherProformas |
	| Sticks Sam | No                    |

@CancelProcess @LoginAsAdminUser1 @ignore
Scenario Outline: 060 Purge disbursement and update without permission
	Given I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	And I open the proforma for submission
	And select and purge the disbursement on proforma edit
	When I update the proforma
	Then an error message "<ErrorMessage>" is displayed

@ft @qa
Examples:
	| User          | ErrorMessage                                            |
	| Proforma User | Current user is not allowed to purge anticipated costs. |
@training @staging @canada @europe @uk @singapore
Examples:
	| User          | ErrorMessage                                            |
	| Proforma User | Current user is not allowed to purge anticipated costs. |

@CancelProxy @ft @training @staging @canada @europe @uk @singapore @qa @ignore
Scenario: 070 cancel the process
	Given I cancel the process
	 
@CancelProcess @ignore
Scenario: 080 Update disbursement with permission
	Given I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	And I open the proforma for submission
	And select and purge the disbursement on proforma edit
	When I update the proforma
	Then no errors are displayed

@ft @training @staging @canada @europe @uk @singapore @qa
Examples:
	| User       |
	| Sticks Sam |


