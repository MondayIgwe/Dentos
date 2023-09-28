 @ignore @us
#Defect-Trust receipt request process not found https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/64878
#Defect - https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/81730 - Routing issues
Feature: ClientAccountTransfer_BillingFeeEarner

In Trust transfer request form, when the from and to billing fee earners are same
the approval goes directly to Finance approval team's workflow dashboard

There is an open defect https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/52908
where the receipt request is not approved straightaway when proxying as the fee earner and goes to workflow inbox of
user with Default_Worflow_Role

@CancelProcess
Scenario: 005 Create Finance approval user
	Given I create a user with details
		| UserName | DataRoleAlias   | DefaultOperatingAlias   | UserRoleList   |
		| <User1>  | <DataRoleAlias> | <DefaultOperatingAlias> | <UserRoleList> |
	Then I submit it
	

Examples:
	| User1       | DataRoleAlias | DefaultOperatingAlias      | UserRoleList                                                                                                                                                                                                                |
	| FRD061_User | Admin         | Dentons United States, LLP | 0:WC:R:Client Account Processor: 1001: 8001,0:WC:R:Client Account Processor: 1001: 8006,0:WC:R:Client Account Processor: 1001: 8007,0:WC:R:Client Account Processor: 1001: 8008,0:WC:R:Client Account Processor: 1001: 8009 |

Scenario Outline: 010 Prepare Data for matter 1
	Given I create a user with details
		| UserName        | DataRoleAlias | DefaultOperatingAlias   |
		| <FeeEarnerName> | Admin         | <DefaultOperatingAlias> |
	And I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	When I search or create a client
		| Entity Name | FeeEarnerFullName |
		| <Client>    | <FeeEarnerName>   |
	And I create the Payer with Api
		| PayerName   | Entity        |
		| <PayorName> | RedBlocks Ltd |
	And I create a submatter 1 with details:
		| Client   | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department | Section | ChargeTypeGroupName | CostTypeGroupName | BillingOffice   | PayorName   |
		| <Client> | <FeeEarnerName>   | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | Bill                 | <Office> | Default    | Default | Auto_IncludeALL     | Auto_IncludeALL   | <BillingOffice> | <PayorName> |

Examples:
	| FeeEarnerName          | Client                 | Currency        | CurrencyMethod | Office  | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | DefaultOperatingAlias      | PayorName     |
	| Transfer EarnerSurname | Transfer ClientSurname | USD - US Dollar | Bill           | Chicago | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | Chicago       | Dentons United States, LLP | Lisa Marvelle |

Scenario: 020 Create a receipt request to load balance for matter 1 account
	Given I add a workflow user to a FeeEarner
	| User            | Name            |
	| <FeeEarnerName> | <FeeEarnerName> |
	Then I proxy as user '<FeeEarnerName>'
	Then I open the client account receipt request process
	And I set the aml checks complete checkbox to true
	And I complete all the client account receipt required fields
		| Narrative | TransactionDate | Amount | Reason                         |
		| {Auto}+10 | {Today}-1       | 100    | Client Account Receipt Request |
	When the approval checkbox is checked
	And I submit it
	And I validate submit was successful
	And I cancel proxy
	And I remove workflow user from fee earner
		| Search Column      | Search Operator | Search Value | FeeEarnerUser   |
		| Workflow User.Name | Equals          | null         | <FeeEarnerName> |

@CancelProcess
Examples:
	| FeeEarnerName          |
	| Transfer EarnerSurname |

Scenario Outline: 030 Create unallocated receipt for matter 1
	Given I create a receipt type with details:
		| Code   | Description   | Tolerance Amount | Tolerance Percentage |
		| <Code> | <Description> | 50.00            | 50.00                |
	And I add a new receipt
		| Receipt Date |
		| {Today}      |
	And change the operating unit "<OperatingUnit>"
	And I update the receipt
		| Narrative |
		| {Auto}+12 |
	When I add an unallocated child form for submatter 1
		| Unallocated Type  | Receipt Amount | Child Form  |
		| <UnallocatedType> | 5000           | Unallocated |
	And I receipt the total amount
	And I update the receipt
		| Document Number |
		| {Auto}+36       |
	Then the payer is auto populated
	And I can submit the receipt
	And I validate submit was successful


Examples:
	| Code | Description | UnallocatedType       |
	| CASH | CASH        | Duplicate/Overpayment |

Scenario Outline: 040 Create matter 2 and unallocated receipt for matter 2
	Given I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	And I search or create a client
		| Entity Name | FeeEarnerFullName | ResponsibleFeeEarnerName | SupervisingFeeEarnerName |
		| <Client>    | <FeeEarnerName>   | Jade EarnerSurname       | Mark EarnerSurname       |
	And I create the Payer with Api
		| PayerName   | Entity       |
		| <PayorName> | BlackBox Ltd |
	And I create a submatter 2 with details:
		| Client   | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department | Section | ChargeTypeGroupName | CostTypeGroupName | BillingOffice   | PayorName   |
		| <Client> | <FeeEarnerName>   | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | Bill                 | <Office> | Default    | Default | Auto_IncludeALL     | Auto_IncludeALL   | <BillingOffice> | <PayorName> |
	When I create a receipt type with details:
		| Code   | Description   | Tolerance Amount | Tolerance Percentage |
		| <Code> | <Description> | 50.00            | 50.00                |
	And I add a new receipt
		| Receipt Date |
		| {Today}      |
	And change the operating unit "<OperatingUnit>"
	And I update the receipt
		| Narrative |
		| {Auto}+12 |
	And I add an unallocated child form for submatter 2
		| Unallocated Type  | Receipt Amount | Child Form  |
		| <UnallocatedType> | 5000           | Unallocated |
	And I receipt the total amount
	And I update the receipt
		| Document Number |
		| {Auto}+36       |
	Then the payer is auto populated
	And I can submit the receipt
	And I validate submit was successful


Examples:
	| Code | Description | Client                 | OperatingUnit              | Currency        | Office  | BillingOffice | CurrencyMethod | Department | Section | FeeEarnerName          | UnallocatedType       | PayorName   |
	| CASH | CASH        | Transfer ClientSurname | Dentons United States, LLP | USD - US Dollar | Chicago | Chicago       | Bill           | Default    | Default | Transfer EarnerSurname | Duplicate/Overpayment | James Smith |

Scenario: 050 Verify the transfer request having same billing fee earner goes direct to finance approval
	Given I add a workflow user to a FeeEarner
	| User            | Name            |
	| <FeeEarnerName> | <FeeEarnerName> |
	Then I proxy as user '<FeeEarnerName>'
	Given I make a client account transfer request
		| TransferType              | FromAccount   | ToAccount   | IntendedUse |
		| Matter to Matter Transfer | <FromAccount> | <ToAccount> | General     |
	And I submit it
	And I validate submit was successful
	And I cancel proxy
	When I proxy as user 'FRD061_User'
	And I search for 'Workflow Dashboard'
	When I navigate to the client account transfer section
	And I navigate to the client account transfer section and search by '<FeeEarnerName>'
	And I submit it
	And I cancel proxy
	And I remove workflow user from fee earner
		| Search Column      | Search Operator | Search Value | FeeEarnerUser   |
		| Workflow User.Name | Equals          | null         | <FeeEarnerName> |

@CancelProcess
Examples:
	| FeeEarnerName          | FromAccount                                               | ToAccount                                |
	| Transfer EarnerSurname | Dentons US LLP (IOLTA) Interest on Lawyer Trust-204968895 | American Savings Bank - IOLTA-8103649398 |
