@us
Feature: DEV001_Timekeeper Workflow Routing

Verify Only Supervising Timekeeper to receive Proforma
Scenario Outline: 010 Create a new Matter and data preparation
    Given I create the Payer with Api
        | PayerName   | Entity       |
        | <PayorName> | CentralCoast |
    And I create a user with details
        | UserName           | DataRoleAlias | DefaultOperatingAlias   | UserRoleList          |
        | <CollaboratorUser> | Admin         | <DefaultOperatingAlias> | DEFAULT_WORKFLOW_ROLE |
    And I create a user with details
        | UserName | DataRoleAlias | DefaultOperatingAlias   |
        | <User>   | Admin         | <DefaultOperatingAlias> |
    Then I create a fee earner with details
        | EntityName      |
        | <FeeEarnerName> |
    And I 'add' the '<CollaboratorUser>' to the fee earner collaborator
    And I add a workflow user to a FeeEarner
        | User   | Name            |
        | <User> | <FeeEarnerName> |
    And I search or create a client
        | Entity Name | FeeEarnerFullName |
        | <Client>    | <FeeEarnerName>   |
    And I create a matter with details:
        | Client   | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | ChargeTypeGroupName | CostTypeGroupName | BillingOffice   | PayorName   |
        | <Client> | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <ChargeTypeGroup>   | <CostTypeGroup>   | <BillingOffice> | <PayorName> |

Examples:
	| CollaboratorUser   | FeeEarnerName | User       | Client       | Currency        | CurrencyMethod | Office  | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName   | DefaultOperatingAlias      |
	| Gavin Collaborator | FRD19 John    | FRD19 John | FRD19 Client | USD - US Dollar | Bill           | Chicago | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | Chicago       | James Mayer | Dentons United States, LLP |


@CancelProcess
Scenario Outline: 020 Post Disbursement and Generate Proforma
	When I post the disbursement
		| Work Date | Disbursement Type  | Work Currency  | Work Amount  | Narrative | Tax Code  |
		| {Today}-1 | <DisbursementType> | <WorkCurrency> | <WorkAmount> | {Auto}+36 | <TaxCode> |
	And I validate the disbursement is posted with no errors

Examples:
	| DisbursementType              | WorkCurrency | WorkAmount | TaxCode                          | IncludeOtherProformas |
	| Dentons United States Funding | USD          | 5000       | US Output Domestic Standard Rate | No                    |


@CancelProcess
Scenario Outline: 030 Verification on Proforma Workflow
	Given I create a user with details
        | UserName      | DataRoleAlias | DefaultOperatingAlias   |
        | <BillingUser> | Admin         | <DefaultOperatingAlias> |
    And I proxy as user '<User>'
    And I search for 'Workflow Dashboard'
    And I verify that the proforma workflow task exists
    Then I cancel proxy
    And I navigate to the home page
    When I proxy as user '<BillingUser>'
    And I search for 'Workflow Dashboard'
    And I verify that there are no workflow tasks visible
    Then I cancel proxy

Examples:
	| DefaultOperatingAlias      | User       | BillingUser       |
	| Dentons United States, LLP | FRD19 John | Billing FeeEarner |


@CancelProcess
Scenario Outline: 040 Verify a Supervisor Timekeeper is able to submit the proforma
     Given I proxy as user '<User>'
    And I search for 'Workflow Dashboard'
    And I open the proforma workflow task
    And I open the proforma for submission
    Then I submit it
    And I cancel proxy
    And I search for 'Workflow Dashboard'
    And I open the proforma workflow task
    When I open the proforma for billing
    And I can verify that the proforma task has been proceeded to the next stage

Examples:
	| User       |
	| FRD19 John |

@CancelProcess
Scenario Outline: 050 Verify that a user belonging to the Superviser Timekeeper can bill the proforma
  Given I proxy as user '<CollaboratorUser>'
    And I search for 'Workflow Dashboard'
    And I open the proforma workflow task
    When I open the proforma for billing
    And I bill it without printing
    Then I cancel proxy
    And I 'remove' the '' to the fee earner collaborator

Examples:
	| CollaboratorUser   | User       |
	| Gavin Collaborator | FRD19 John |
	
