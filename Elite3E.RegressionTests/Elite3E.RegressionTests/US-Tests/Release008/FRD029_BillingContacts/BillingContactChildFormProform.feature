@us
Feature: BillingContactChildFormProform

Verify that on Proforma Generation Using the lead matter> Load in the Billing Contacts from the ‘Billing Contacts’ child form, 
where the ‘Active’ boolean for the contact is set to True

#Matter Defect - temporary issue 

@CancelProcess
Scenario Outline: 001 Prepare required data for the billing rule validation list
	Given I create a fee earner with details
		| EntityName          |
		| <FeeEarnerFullName> |
	And I create the Payer with Api
		| PayerName   | Entity        |
		| <PayorName> | LimeLight Ltd |
	And I create a matter with details:
		| Client   | FeeEarnerFullName   | Status   | OpenDate   | MatterName   | Currency   | MatterCurrencyMethod   | Office   | Department   | Section   | Rate   | ChargeTypeGroupName   | CostTypeGroupName   | PayorName   |
		| <Client> | <FeeEarnerFullName> | <Status> | <OpenDate> | <MatterName> | <Currency> | <MatterCurrencyMethod> | <Office> | <Department> | <Section> | <Rate> | <ChargeTypeGroupName> | <CostTypeGroupName> | <PayorName> |

Examples:
	| User         | Client         | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency            | MatterCurrencyMethod | Office         | Department | Section | Rate     | ChargeTypeGroupName | CostTypeGroupName | PayorName |
	| Johnson Rose | Johnson Client | Johnson Rose      | Fully Open | {Today}-1 | {Auto}+36  | USD - US Dollar     | Bill                 | Chicago        | Default    | Default | Standard | FixedChargeType     | FixedCostType     | Lara Moon |

@CancelProcess
Scenario Outline: 020 Create a time entry/modify
	Given I submit a time modify
		| Time Type  | Hours   | Narrative   | FeeEarnerName   | WorkRate | WorkAmount | WorkCurrency | Tax Code  |
		| <TimeType> | <Hours> | <Narrative> | <FeeEarnerName> | 1        | 1          | <Currency>   | <TaxCode> |

Examples:
	| FeeEarnerName | TimeType          | Hours | Narrative         | TaxCode                 | Currency        |
	| Johnson Rose  | Fixed-Capped Fees | 1     | test automation 1 | US Output Domestic Zero | USD - US Dollar |

@CancelProcess
Scenario Outline: 030 Post Disbursement and Generate Proforma
	Given I post the disbursement
		| Work Date | Disbursement Type  | Work Currency  | Work Amount  | Narrative | Tax Code  |
		| {Today}-1 | <DisbursementType> | <WorkCurrency> | <WorkAmount> | {Auto}+36 | <TaxCode> |
	Then I validate the disbursement is posted with no errors
	When I can generate the proforma
		| Description | Include Other Proformas | Proforma Status |
		| {Auto}+36   | <IncludeOtherProformas> | Draft           |

Examples:
	| DisbursementType                      | WorkCurrency | WorkAmount | TaxCode                 | IncludeOtherProformas |
	| Court & Stamp Fees - Anticipated (NT) | USD          | 5000       | US Output Domestic Zero | No                    |



@CancelProcess
Scenario Outline: 040 Add a new Billing Contact
	Given I create the Payer with Api
		| PayerName   | Entity      |
		| <PayerName> | Test Entity |
	And I navigate to the matter maintenance process
	And I reopen a saved Matter
	When I add a Matter Payer
		| Start Date |
		| {Today}+2  |
	And I add a Billing Contact info
		| ContactType   | Email   | Payer       | EntityPerson   |
		| <ContactType> | <Email> | <PayerName> | <EntityPerson> |
	And I update it
	And I submit the form
	Then I validate submit was successful
	

Examples:
	| ContactType               | PayerName | Email              | FirstName | LastName | EntityPerson | SiteType | Country       | Street           |
	| Billing - Primary Contact | Lara Moon | lauramoon@test.com | Test      | Entity   | Laura Moon   | Billing  | UNITED STATES | 10 UNITED STATES |

