@us
Feature: VerifyDisbursementAndChargeFilter
	
Verify the Cost Types/Disbursements applied to Matter are respected

Scenario Outline: 010 Create Cost Type
	Given the below disbursement types are available
		| Code    | Description         | TransactionType    | HardOrSoftDisbursement   |
		| <Code1> | <DisbursementType1> | <TransactionType1> | <HardOrSoftDisbursement> |

Examples:
	| Code1   | DisbursementType1                     | TransactionType1       | HardOrSoftDisbursement |
	| H20005A | Court & Stamp Fees - Anticipated (NT) | True Hard Cost Mark up | IsHardCost             |

Scenario Outline: 020 Create Disbursement Type Group & Cost Type
	Given the cost type group exists
		| Code       | Description    | CostTypeGroupExcludeOrIncludeList |
		| at_CE_CTG1 | Auto_CE_Group1 | <IncludeOrExcludeOption>          |
	And the below disbursement type added to the group
		| Code    | Description         | TransactionType    | HardOrSoftDisbursement   |
		| <Code1> | <DisbursementType1> | <TransactionType1> | <HardOrSoftDisbursement> |

Examples:
	| IncludeOrExcludeOption | Code1   | DisbursementType1                     | TransactionType1 | HardOrSoftDisbursement |
	| IsIncludeList          | H20005A | Court & Stamp Fees - Anticipated (NT) | Hard Cost        | IsHardCost             |

Scenario Outline: 030 Create Charge Type Group & Charge Type
	Given the charge type group exists
		| Code        | Description    | ChargeTypeGroupExcludeOrIncludeList |
		| at_CE_CHTG5 | Auto_CE_Group5 | IsIncludeList                       |
	And the below charge type added to the group
		| Code   | Description  | TransactionType   | Category          |
		| <Code> | <ChargeType> | <TransactionType> | Billed on Account |

Examples:
	| Code | ChargeType    | TransactionType      |
	| MISC | Miscellaneous | Miscellaneous Income |

Scenario Outline: 040 Create a new Matter
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
		| PayerName   | Entity   |
		| <PayorName> | <Client> |
	When I create the Payer with Api
		| PayerName   | Entity   |
		| <PayorName> | <Client> |
	And I create a matter with details:
		| Client   | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | ChargeTypeGroupName | CostTypeGroupName | BillingOffice   | PayorName   |
		| <Client> | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <ChargeTypeGroup>   | <CostTypeGroup>   | <BillingOffice> | <PayorName> |


Examples:
	| User      | Client        | FeeEarnerName | DefaultOperatingAlias      | Currency        | CurrencyMethod | Office  | Department | Section | ChargeTypeGroup | CostTypeGroup  | BillingOffice | PayorName |
	| Gavin Jim | Gavin Mandela | Gavin Jim     | Dentons United States, LLP | USD - US Dollar | Bill           | Chicago | Default    | Default | Auto_CE_Group5  | Auto_CE_Group1 | Chicago       | Elba Beck |

Scenario Outline: 050 Verify Disbursement Type on Disbursement Entry
	When I try to post the unavailable disbursement type
		| Work Date | Disbursement Type  | Work Currency  | Work Amount  | Narrative | Tax Code  |
		| {Today}-1 | <DisbursementType> | <WorkCurrency> | <WorkAmount> | {Auto}+36 | <TaxCode> |
	Then an error message "<ErrorMessage>" is displayed


Examples:
	| DisbursementType                          | WorkCurrency | WorkAmount | TaxCode                          | ErrorMessage                                                                                                       |
	| Commercial Property & Liability Insurance | USD          | 5000       | US Output Domestic Standard Rate | Disbursement Type must be active as of current date, allowed for Direct Entry, and valid for matter and work date. |

Scenario Outline: 060 Verify Disbursement Type on Disbursement Modify
	When I try to post the unavailable disbursement type
		| Work Date | Disbursement Type  | Work Currency  | Work Amount  | Narrative | Tax Code  | PurgeType   |
		| {Today}-1 | <DisbursementType> | <WorkCurrency> | <WorkAmount> | {Auto}+36 | <TaxCode> | <PurgeType> |
	Then an error message "<ErrorMessage>" is displayed


Examples:
	| DisbursementType                          | WorkCurrency | WorkAmount | TaxCode                          | PurgeType      | ErrorMessage                                                                                                       |
	| Commercial Property & Liability Insurance | USD          | 5000       | US Output Domestic Standard Rate | Billable Purge | Disbursement Type must be active as of current date, allowed for Direct Entry, and valid for matter and work date. |

Scenario Outline: 070 Verify Charge Type on Charge Entry
	When I try to post the charge entry
		| Work Date | Charge Type  | Work Amount  |
		| {Today}-1 | <ChargeType> | <WorkAmount> |
	Then an error message "<ErrorMessage>" is displayed

Examples:
	| ChargeType    | WorkAmount | ErrorMessage                  |
	| Sundry Incomes | 500.00     | Charge Type Value is required |


Scenario Outline: 080 Verify Charge Type on Charge Modify
	When I try to update the charge modify
		| Work Date | Charge Type  | Work Amount  |
		| {Today}-1 | <ChargeType> | <WorkAmount> |
	Then an error message "<ErrorMessage>" is displayed

Examples:
	| ChargeType    | WorkAmount | ErrorMessage                                              |
	| Sundry Incomes | 500.00     | Charge Type: Assign required attribute before continuing. |
	
Scenario Outline: 090 Create a proforma Edit
	When I post the disbursement
		| Work Date | Disbursement Type  | Work Currency  | Work Amount  | Narrative | Tax Code  |
		| {Today}-1 | <DisbursementType> | <WorkCurrency> | <WorkAmount> | {Auto}+36 | <TaxCode> |
	And I validate the disbursement is posted with no errors
	Then I can generate the proforma
		| Description | Include Other Proformas | Proforma Status |
		| {Auto}+36   | <IncludeOtherProformas> | Draft           |

Examples:
	| DisbursementType                      | WorkCurrency | WorkAmount | TaxCode                          | IncludeOtherProformas |
	| Court & Stamp Fees - Anticipated (NT) | USD          | 5000       | US Output Domestic Standard Rate | No                    |


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
Examples:
	| User      | DisbursementType                         | Anticipated | ReferenceCurrency | WorkAmount | TaxCode                          | ErrorMessage                                                                                                       |
	| Gavin Jim | Commercial Property & Liability Insuranc | Yes         | USD               | 5001       | US Output Domestic Standard Rate | Disbursement Type must be active as of current date, allowed for Direct Entry, and valid for matter and work date. |

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

Examples:
	| ChargeType    | WorkAmount | ErrorMessage1                                                  | ErrorMessage2                                                  | ErrorMessage3                                             |
	| Sundry Incomes | 500.00     | Transaction Type: Assign required attribute before continuing. | Transaction Type: Assign required attribute before continuing. | Charge Type: Assign required attribute before continuing. |
		

Scenario Outline: 120 Cancel Proxy
	Given I cancel proxy
	