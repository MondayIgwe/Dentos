@ignore @us
#Defect-Trust receipt request process not found https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/64878
Feature: ClientAccountTransfer_DifferentFeeEarner

In Trust transfer request form, when the from and to billing fee earners are different
the approval first goes to Finance Approver ->Receiver's billing fee earner approval->Finance Approver approval

There is an open defect https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/52908
where the receipt request is not approved straightaway when proxying as the fee earner and goes to workflow inbox of
user with Default_Worflow_Role


Scenario: 005 Create Finance approval user
	Given I create a user with details
		| UserName | DataRoleAlias   | DefaultOperatingAlias   | UserRoleList   |
		| <User1>  | <DataRoleAlias> | <DefaultOperatingAlias> | <UserRoleList> |

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

	
Scenario Outline: 020 Create a receipt request to load balance for matter 1 account
	Given I 'add' a workflow user '<FeeEarnerName>' to fee earner '<FeeEarnerName>'
	Then I proxy as user '<FeeEarnerName>'
	And I open the client account receipt request process
	And I set the aml checks complete checkbox to true
	And I complete all the client account receipt required fields
		| Narrative | TransactionDate | Amount | Reason                         |
		| {Auto}+10 | {Today}-1       | 100    | Client Account Receipt Request |
	When the approval checkbox is checked
	And I submit it
	And I validate submit was successful
	And I cancel proxy

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
	| Client                 | OperatingUnit              | FeeEarnerName          | UnallocatedType       | Currency        | CurrencyMethod | Office  | Department | Section | Code | Description | BillingOffice |
	| Transfer ClientSurname | Dentons United States, LLP | Transfer EarnerSurname | Duplicate/Overpayment | USD - US Dollar | Bill           | Chicago | Default    | Default | CASH | CASH        | Chicago       |



Scenario Outline: 040  Create matter 2 and unallocated receipt for matter 2
	Given I create a user with details
		| UserName        | DataRoleAlias | DefaultOperatingAlias |
		| <FeeEarnerName> | Admin         | <OperatingUnit>       |
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
	When I add an unallocated child form for submatter 2
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
	| Client         | OperatingUnit              | FeeEarnerName         | UnallocatedType       | Currency        | CurrencyMethod | Office  | Department | Section | Code | Description | BillingOffice | PayorName   |
	| KYLE J. MATULA | Dentons United States, LLP | Dentons Firm Attorney | Duplicate/Overpayment | USD - US Dollar | Bill           | Chicago | Default    | Default | CASH | CASH        | Chicago       | James Smith |


Scenario Outline: 050 Verify the transfer request having different billing fee earner goes  to finance approval first
	Given I proxy as user '<FeeEarnerName>'
	And I make a client account transfer request
		| TransferType              | FromAccount   | ToAccount   | IntendedUse |
		| Matter to Matter Transfer | <FromAccount> | <ToAccount> | General     |
	When I add an attachment
		| File           |
		| Attachment.txt |
	Then I submit it
	And I validate submit was successful
	And I cancel proxy
	And I proxy as user 'FRD061_User'
	Given I search for 'Workflow Dashboard'
	And I navigate to the client account transfer section
	And I navigate to the client account transfer section and search by '<FeeEarnerName>'
	When the transfer from approval checkbox is checked
	And the approval required field is entered with '<ReceivingFeeEarner>'
	And I send the request for ApprovalRequired
	And I cancel proxy
	And I remove workflow user from fee earner
		| Search Column      | Search Operator | Search Value | FeeEarnerUser   |
		| Workflow User.Name | Equals          | null         | <FeeEarnerName> |

@CancelProcess
Examples:
	| FeeEarnerName          | FromAccount                                               | ToAccount                                | ReceivingFeeEarner    |
	| Transfer EarnerSurname | Dentons US LLP (IOLTA) Interest on Lawyer Trust-204968895 | American Savings Bank - IOLTA-8103649398 | Dentons Firm Attorney |

Scenario Outline: 060 Approval as receiving fee earner
	Then I proxy as user '<FeeEarnerName>'
	And I search for 'Workflow Dashboard'
	Given I navigate to the client account transfer section
	And I open the trust transfer finance approval task in receiving fee earner inbox and search by 'FRD061_User'
	When I add an attachment
		| File           |
		| Attachment.txt |
	And I submit it
	And I validate submit was successful
	And I cancel proxy

@CancelProcess
Examples:
	| FeeEarnerName         | FromAccount                                               | ToAccount                                |
	| Dentons Firm Attorney | Dentons US LLP (IOLTA) Interest on Lawyer Trust-204968895 | American Savings Bank - IOLTA-8103649398 |

Scenario Outline: 070 Final Approval as finance approver
	Then I proxy as user 'FRD061_User'
	And I search for 'Workflow Dashboard'
	And I navigate to the client account transfer section
	And I open the trust transfer finance approval task and search by '<FeeEarnerName>'
	And I submit it
	And I validate submit was successful
	And I cancel proxy

@CancelProcess
Examples:
	| FeeEarnerName         | FromAccount                                               | ToAccount                                |
	| Dentons Firm Attorney | Dentons US LLP (IOLTA) Interest on Lawyer Trust-204968895 | American Savings Bank - IOLTA-8103649398 |

@CancelProcess
Scenario: 080 Verify balance in receiving matter
	Given I search for process 'Matter Balances In Matter Currency' without add button
	When I verify the balance for the receiving fee earner matter
