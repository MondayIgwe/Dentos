@release2 @frd045 @VerifyDisbursementAndChargeFilter
Feature: VerifyDisbursementAndChargeFilter
	
Verify the Cost Types/Disbursements applied to Matter are respected

@CancelProcess
Scenario Outline: 010 Create Cost Type
	Given the below disbursement types are available
		| Code    | Description         | TransactionType    | HardOrSoftDisbursement   |
		| <Code1> | <DisbursementType1> | <TransactionType1> | <HardOrSoftDisbursement> |

@qa @training @staging @europe @uk
Examples:
	| Code1  | DisbursementType1 | TransactionType1       | HardOrSoftDisbursement |
	| H90151 | Accomodation      | True Hard Cost Mark up | IsHardCost             |

@ft
Examples:
	| Code1  | DisbursementType1 | TransactionType1       | HardOrSoftDisbursement |
	| H90151 | Accomodation      | True Hard Cost Mark up | IsHardCost             |
@singapore
Examples:
	| Code1   | DisbursementType1                    | TransactionType1       | HardOrSoftDisbursement |
	| H52005A | Registration Fees - Anticipated (NT) | True Hard Cost Mark up | IsHardCost             |
@canada
Examples:
	| Code1   | DisbursementType1                     | TransactionType1       | HardOrSoftDisbursement |
	| H20005A | Court & Stamp Fees - Anticipated (NT) | True Hard Cost Mark up | IsHardCost             |

@CancelProcess
Scenario Outline: 020 Create Disbursement Type Group & Cost Type
	Given the cost type group exists
		| Code   | Description   | CostTypeGroupExcludeOrIncludeList |
		| <Code> | <Description> | <IncludeOrExcludeOption>          |
	And the below disbursement type added to the group
		| Code    | Description         | TransactionType    | HardOrSoftDisbursement   |
		| <Code1> | <DisbursementType1> | <TransactionType1> | <HardOrSoftDisbursement> |

@ft @qa @training @uk
Examples:
	| IncludeOrExcludeOption | Code1  | DisbursementType1         | TransactionType1 | HardOrSoftDisbursement | Code       | Description    |
	| IsIncludeList          | H90151 | Anticipated Costs General | Hard Cost        | IsHardCost             | at_CE_CTG1 | Auto_CE_Group1 |
@staging
Examples:
	| IncludeOrExcludeOption | Code1  | DisbursementType1         | TransactionType1 | HardOrSoftDisbursement | Code             | Description      |
	| IsIncludeList          | H90151 | Anticipated Costs General | Hard Cost        | IsHardCost             | SG_CostTypeGroup | SG_CostTypeGroup |
@canada
Examples:
	| IncludeOrExcludeOption | Code1   | DisbursementType1                     | TransactionType1 | HardOrSoftDisbursement | Code       | Description    |
	| IsIncludeList          | H20005A | Court & Stamp Fees - Anticipated (NT) | Hard Cost        | IsHardCost             | at_CE_CTG1 | Auto_CE_Group1 |
@singapore
Examples:
	| IncludeOrExcludeOption | Code1   | DisbursementType1                     | TransactionType1 | HardOrSoftDisbursement | Code       | Description    |
	| IsIncludeList          | H20005A | Court & Stamp Fees - Anticipated (NT) | Hard Cost        | IsHardCost             | at_CE_CTG1 | Auto_CE_Group1 |
@europe
Examples:
	| IncludeOrExcludeOption | Code1   | DisbursementType1                     | TransactionType1 | HardOrSoftDisbursement | Code       | Description    |
	| IsIncludeList          | H20005A | Court & Stamp Fees - Anticipated (NT) | Hard Cost        | IsHardCost             | at_CE_CTG1 | Auto_CE_Group1 |

@CancelProcess
Scenario Outline: 030 Create Charge Type Group & Charge Type
	Given the charge type group exists
		| Code        | Description    | ChargeTypeGroupExcludeOrIncludeList |
		| at_CE_CHTG5 | Auto_CE_Group5 | IsIncludeList                       |
	And the below charge type added to the group
		| Code   | Description  | TransactionType   | Category          |
		| <Code> | <ChargeType> | <TransactionType> | Billed on Account |

@ft @qa @training @staging @canada @europe @uk @singapore
Examples:
	| Code | ChargeType    | TransactionType      |
	| MISC | Miscellaneous | Miscellaneous Income |

Scenario Outline: 040 Create a new Matter
	Given I create a user with details
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
		| PayerName   | Entity   |
		| <PayorName> | <Client> |
	When I create the Payer with Api
		| PayerName   | Entity   |
		| <PayorName> | <Client> |
	And I create a matter with details:
		| Client   | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | ChargeTypeGroupName | CostTypeGroupName | BillingOffice   | PayorName   |
		| <Client> | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <ChargeTypeGroup>   | <CostTypeGroup>   | <BillingOffice> | <PayorName> |

@training @staging @uk @qa
Examples:
	| User      | Client        | FeeEarnerName | DefaultOperatingAlias          | Currency            | CurrencyMethod | Office  | Department | Section | ChargeTypeGroup | CostTypeGroup  | BillingOffice | PayorName | UserRoleList |
	| Gavin Jim | Gavin Mandela | Gavin Jim     | Dentons UK and Middle East LLP | GBP - British Pound | Bill           | Default | Default    | Default | Auto_CE_Group5  | Auto_CE_Group1 | London (EU)   | Elba Beck |              |
@ft
Examples:
	| User      | Client        | FeeEarnerName | DefaultOperatingAlias          | Currency            | CurrencyMethod | Office  | Department | Section | ChargeTypeGroup | CostTypeGroup  | BillingOffice | PayorName | UserRoleList |
	| Gavin Jim | Gavin Mandela | Gavin Jim     | Dentons UK and Middle East LLP | GBP - British Pound | Bill           | Default | Default    | Default | Auto_CE_Group5  | Auto_CE_Group1 | London (EU)   | Elba Beck |              |
@europe
Examples:
	| User      | Client        | FeeEarnerName | DefaultOperatingAlias          | Currency   | CurrencyMethod | Office      | Department | Section | ChargeTypeGroup | CostTypeGroup  | BillingOffice | PayorName | UserRoleList |
	| Gavin Jim | Gavin Mandela | Gavin Jim     | Dentons UK and Middle East LLP | EUR - Euro | Bill           | London (EU) | Default    | Default | Auto_CE_Group5  | Auto_CE_Group1 | London (EU)   | Elba Beck |              |
@canada
Examples:
	| User      | Client        | FeeEarnerName | DefaultOperatingAlias          | Currency              | CurrencyMethod | Office   | Department | Section | ChargeTypeGroup | CostTypeGroup  | BillingOffice | PayorName | UserRoleList |
	| Gavin Jim | Gavin Mandela | Gavin Jim     | Dentons UK and Middle East LLP | CAD - Canadian Dollar | Bill           | Montreal | Default    | Default | Auto_CE_Group5  | Auto_CE_Group1 | Montreal      | Elba Beck |              |
@singapore
Examples:
	| User      | Client        | FeeEarnerName | DefaultOperatingAlias          | Currency               | CurrencyMethod | Office    | Department            | Section | ChargeTypeGroup | CostTypeGroup  | BillingOffice | PayorName | UserRoleList |
	| Gavin Jim | Gavin Mandela | Gavin Jim     | Dentons UK and Middle East LLP | SGD - Singapore Dollar | Bill           | Singapore | Corporate - Singapore | Default | Auto_CE_Group5  | Auto_CE_Group1 | Singapore     | Elba Beck |              |

@CancelProcess
Scenario Outline: 050 Verify Disbursement Type on Disbursement Entry
	When I try to post the unavailable disbursement type
		| Work Date | Disbursement Type  | Work Currency  | Work Amount  | Narrative | Tax Code  |
		| {Today}-1 | <DisbursementType> | <WorkCurrency> | <WorkAmount> | {Auto}+36 | <TaxCode> |
	Then an error message "<ErrorMessage>" is displayed

@ft
Examples:
	| DisbursementType    | WorkCurrency | WorkAmount | TaxCode                         | ErrorMessage                                                                                                       |
	| Advertising Charges | GBP          | 5000       | UK Output Domestic Standard 20% | Disbursement Type must be active as of current date, allowed for Direct Entry, and valid for matter and work date. |
@staging
Examples:
	| DisbursementType        | WorkCurrency | WorkAmount | TaxCode                     | ErrorMessage                                                                                                       |
	| Automation Accomodation | GBP          | 5000       | UK Output Domestic Standard | Disbursement Type must be active as of current date, allowed for Direct Entry, and valid for matter and work date. |
@training
Examples:
	| DisbursementType        | WorkCurrency | WorkAmount | TaxCode                     | ErrorMessage                                                                                                       |
	| Automation Accomodation | GBP          | 5000       | UK Output Domestic Standard | Disbursement Type must be active as of current date, allowed for Direct Entry, and valid for matter and work date. |
@uk
Examples:
	| DisbursementType        | WorkCurrency | WorkAmount | TaxCode                     | ErrorMessage                                                                                                       |
	| Automation Accomodation | AED          | 5000       | AU Output Domestic Standard | Disbursement Type must be active as of current date, allowed for Direct Entry, and valid for matter and work date. |
@singapore
Examples:
	| DisbursementType                          | WorkCurrency | WorkAmount | TaxCode                     | ErrorMessage                                                                                                       |
	| Commercial Property & Liability Insurance | SGD          | 5000       | SG Output Domestic Standard | Disbursement Type must be active as of current date, allowed for Direct Entry, and valid for matter and work date. |
@qa
Examples:
	| DisbursementType                | WorkCurrency | WorkAmount | TaxCode                         | ErrorMessage                                                                                                       |
	| External Catering - Anticipated | GBP          | 5000       | UK Output Domestic Standard 20% | Disbursement Type must be active as of current date, allowed for Direct Entry, and valid for matter and work date. |
@europe
Examples:
	| DisbursementType        | WorkCurrency | WorkAmount | TaxCode               | ErrorMessage                                                                                                       |
	| Automation Accomodation | EUR          | 5000       | ES Output Europe Zero | Disbursement Type must be active as of current date, allowed for Direct Entry, and valid for matter and work date. |
@canada
Examples:
	| DisbursementType                          | WorkCurrency | WorkAmount | TaxCode                         | ErrorMessage                                                                                                       |
	| Commercial Property & Liability Insurance | CAD          | 5000       | CA Output Domestic Standard GST | Disbursement Type must be active as of current date, allowed for Direct Entry, and valid for matter and work date. |

@CancelProcess
Scenario Outline: 060 Verify Disbursement Type on Disbursement Modify
	When I try to post the unavailable disbursement type
		| Work Date | Disbursement Type  | Work Currency  | Work Amount  | Narrative | Tax Code  | PurgeType   |
		| {Today}-1 | <DisbursementType> | <WorkCurrency> | <WorkAmount> | {Auto}+36 | <TaxCode> | <PurgeType> |
	Then an error message "<ErrorMessage>" is displayed

@ft
Examples:
	| DisbursementType    | WorkCurrency | WorkAmount | TaxCode                         | PurgeType      | ErrorMessage                                                                                                       |
	| Advertising Charges | GBP          | 5000       | UK Output Domestic Standard 20% | Billable Purge | Disbursement Type must be active as of current date, allowed for Direct Entry, and valid for matter and work date. |
@staging
Examples:
	| DisbursementType        | WorkCurrency | WorkAmount | TaxCode                     | PurgeType      | ErrorMessage                                                                                                       |
	| Automation Accomodation | GBP          | 5000       | UK Output Domestic Standard | Billable Purge | Disbursement Type must be active as of current date, allowed for Direct Entry, and valid for matter and work date. |
@training
Examples:
	| DisbursementType        | WorkCurrency | WorkAmount | TaxCode                     | PurgeType      | ErrorMessage                                                                                                       |
	| Automation Accomodation | GBP          | 5000       | UK Output Domestic Standard | Billable Purge | Disbursement Type must be active as of current date, allowed for Direct Entry, and valid for matter and work date. |
@uk
Examples:
	| DisbursementType        | WorkCurrency | WorkAmount | TaxCode                     | PurgeType      | ErrorMessage                                                                                                       |
	| Automation Accomodation | AED          | 5000       | AU Output Domestic Standard | Billable Purge | Disbursement Type must be active as of current date, allowed for Direct Entry, and valid for matter and work date. |
@qa
Examples:
	| DisbursementType                | WorkCurrency | WorkAmount | TaxCode                         | PurgeType      | ErrorMessage                                                                                                       |
	| External Catering - Anticipated | GBP          | 5000       | UK Output Domestic Standard 20% | Billable Purge | Disbursement Type must be active as of current date, allowed for Direct Entry, and valid for matter and work date. |
@europe
Examples:
	| DisbursementType        | WorkCurrency | WorkAmount | TaxCode               | PurgeType      | ErrorMessage                                                                                                       |
	| Automation Accomodation | EUR          | 5000       | ES Output Europe Zero | Billable Purge | Disbursement Type must be active as of current date, allowed for Direct Entry, and valid for matter and work date. |
@canada
Examples:
	| DisbursementType                          | WorkCurrency | WorkAmount | TaxCode                         | PurgeType      | ErrorMessage                                                                                                       |
	| Commercial Property & Liability Insurance | CAD          | 5000       | CA Output Domestic Standard GST | Billable Purge | Disbursement Type must be active as of current date, allowed for Direct Entry, and valid for matter and work date. |
@singapore
Examples:
	| DisbursementType                          | WorkCurrency | WorkAmount | TaxCode                     | PurgeType      | ErrorMessage                                                                                                       |
	| Commercial Property & Liability Insurance | SGD          | 5000       | SG Output Domestic Standard | Billable Purge | Disbursement Type must be active as of current date, allowed for Direct Entry, and valid for matter and work date. |

@CancelProcess
Scenario Outline: 070 Verify Charge Type on Charge Entry
	When I try to post the charge entry
		| Work Date | Charge Type  | Work Amount  |
		| {Today}-1 | <ChargeType> | <WorkAmount> |
	Then an error message "<ErrorMessage>" is displayed

@qa @training @staging @canada @europe @uk @singapore @ft
Examples:
	| ChargeType    | WorkAmount | ErrorMessage                  |
	| Sundry Income | 500.00     | Charge Type Value is required |

@CancelProcess
Scenario Outline: 080 Verify Charge Type on Charge Modify
	When I try to update the charge modify
		| Work Date | Charge Type  | Work Amount  |
		| {Today}-1 | <ChargeType> | <WorkAmount> |
	Then an error message "<ErrorMessage>" is displayed

@singapore @qa @training @staging @canada @europe @uk @ft
Examples:
	| ChargeType    | WorkAmount | ErrorMessage                                              |
	| Sundry Income | 500.00     | Charge Type: Assign required attribute before continuing. |
	
Scenario Outline: 090 Create a proforma Edit
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
	| Agency Registration | GBP          | 5000       | UK Output Domestic Standard 20% | No                    |
@qa
Examples:
	| DisbursementType               | WorkCurrency | WorkAmount | TaxCode                         | IncludeOtherProformas |
	| Automation Agency Registration | GBP          | 5000       | UK Output Domestic Standard 20% | No                    |
@training
Examples:
	| DisbursementType                                       | WorkCurrency | WorkAmount | TaxCode                     | IncludeOtherProformas |
	| Registraton Fees - Issuance of SSCT - Anticipated (NT) | GBP          | 5000       | UK Output Domestic Standard | No                    |
@staging
Examples:
	| DisbursementType                      | WorkCurrency | WorkAmount | TaxCode                     | IncludeOtherProformas |
	| Court & Stamp Fees - Anticipated (NT) | GBP          | 5000       | UK Output Domestic Standard | No                    |
@uk
Examples:
	| DisbursementType                                       | WorkCurrency | WorkAmount | TaxCode                     | IncludeOtherProformas |
	| Registraton Fees - Issuance of SSCT - Anticipated (NT) | GBP          | 5000       | UK Output Domestic Standard | No                    |

@europe
Examples:
	| DisbursementType                      | WorkCurrency | WorkAmount | TaxCode               | IncludeOtherProformas |
	| Court & Stamp Fees - Anticipated (NT) | EUR          | 5000       | ES Output Europe Zero | No                    |
@canada
Examples:
	| DisbursementType                      | WorkCurrency | WorkAmount | TaxCode                         | IncludeOtherProformas |
	| Court & Stamp Fees - Anticipated (NT) | CAD          | 5000       | CA Output Domestic Standard GST | No                    |
@singapore
Examples:
	| DisbursementType                      | WorkCurrency | WorkAmount | TaxCode                     | IncludeOtherProformas |
	| Court & Stamp Fees - Anticipated (NT) | SGD          | 5000       | SG Output Domestic Standard | No                    |

@CancelProcess
Scenario Outline: 100 Validate Disbursement Type on proforma Edit
	Given I proxy as user '<User>'
	And I search for 'Workflow Dashboard'
	When I open the proforma workflow task
	And I open the proforma for submission
	And add the disbursement on the proforma edit
		| Work Date | Disbursement Type  | Anticipated   | Reference Currency  | Work Amount  | Tax Code  | Narrative |
		| {Today}-1 | <DisbursementType> | <Anticipated> | <ReferenceCurrency> | <WorkAmount> | <TaxCode> | {Auto}+36 |
	Then error messages displayed should contain
		| Error Message  |
		| <ErrorMessage> |
	And I cancel it
@ft
Examples:
	| User      | DisbursementType    | Anticipated | ReferenceCurrency | WorkAmount | TaxCode                         | ErrorMessage                                                                                                       |
	| Gavin Jim | Advertising Charges | Yes         | GBP               | 5001       | UK Output Domestic Standard 20% | Disbursement Type must be active as of current date, allowed for Direct Entry, and valid for matter and work date. |
@qa
Examples:
	| User      | DisbursementType                | Anticipated | ReferenceCurrency | WorkAmount | TaxCode                         | ErrorMessage                                                                                                       |
	| Gavin Jim | External Catering - Anticipated | Yes         | GBP               | 5001       | UK Output Domestic Standard 20% | Disbursement Type must be active as of current date, allowed for Direct Entry, and valid for matter and work date. |
@staging
Examples:
	| User      | DisbursementType        | Anticipated | ReferenceCurrency | WorkAmount | TaxCode                     | ErrorMessage                                                                                                       |
	| Gavin Jim | Automation Accomodation | Yes         | GBP               | 5001       | UK Output Domestic Standard | Disbursement Type must be active as of current date, allowed for Direct Entry, and valid for matter and work date. |
@training
Examples:
	| User      | DisbursementType        | Anticipated | ReferenceCurrency | WorkAmount | TaxCode                     | ErrorMessage                                                                                                       |
	| Gavin Jim | Automation Accomodation | Yes         | GBP               | 5001       | UK Output Domestic Standard | Disbursement Type must be active as of current date, allowed for Direct Entry, and valid for matter and work date. |
@uk
Examples:
	| User      | DisbursementType        | Anticipated | ReferenceCurrency | WorkAmount | TaxCode                     | ErrorMessage                                                                                                       |
	| Gavin Jim | Automation Accomodation | Yes         | GBP               | 5001       | AU Output Domestic Standard | Disbursement Type must be active as of current date, allowed for Direct Entry, and valid for matter and work date. |
@singapore
Examples:
	| User      | DisbursementType                          | Anticipated | ReferenceCurrency | WorkAmount | TaxCode                     | ErrorMessage                                                                                                       |
	| Gavin Jim | Commercial Property & Liability Insurance | Yes         | SGD               | 5001       | SG Output Domestic Standard | Disbursement Type must be active as of current date, allowed for Direct Entry, and valid for matter and work date. |
@europe
Examples:
	| User      | DisbursementType        | Anticipated | ReferenceCurrency | WorkAmount | TaxCode               | ErrorMessage                                                                                                       |
	| Gavin Jim | Automation Accomodation | Yes         | EUR               | 5001       | ES Output Europe Zero | Disbursement Type must be active as of current date, allowed for Direct Entry, and valid for matter and work date. |
@canada
Examples:
	| User      | DisbursementType                          | Anticipated | ReferenceCurrency | WorkAmount | TaxCode                         | ErrorMessage                                                                                                       |
	| Gavin Jim | Commercial Property & Liability Insurance | Yes         | CAD               | 5001       | CA Output Domestic Standard GST | Disbursement Type must be active as of current date, allowed for Direct Entry, and valid for matter and work date. |

@CancelProcess
Scenario Outline: 110 Validate charge type on proforma Edit
	Given I open the proforma for submission
	And add the charge type on the proforma edit
		| Charge Type  | Work Amount  |
		| <ChargeType> | <WorkAmount> |
	When I close charge details
	And update the proforma edit
	Then error messages displayed should contain
		| Error Message   |
		| <ErrorMessage1> |
		| <ErrorMessage2> |
		| <ErrorMessage3> |

@singapore @ft @qa @staging @canada @europe @uk @training
Examples:
	| ChargeType    | WorkAmount | ErrorMessage1                                                  | ErrorMessage2                                                  | ErrorMessage3                                             |
	| Sundry Income | 500.00     | Transaction Type: Assign required attribute before continuing. | Transaction Type: Assign required attribute before continuing. | Charge Type: Assign required attribute before continuing. |
		
@CancelProcess @training @staging @canada @europe @uk @ft @qa @singapore
Scenario Outline: 120 Cancel Proxy
	Given I cancel proxy
	