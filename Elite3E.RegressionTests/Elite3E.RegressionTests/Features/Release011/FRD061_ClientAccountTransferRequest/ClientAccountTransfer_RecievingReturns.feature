
@ignore
#Staging defect receipt request process not found https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/90934/
Feature: ClientAccountTransfer_RecievingReturns

Scenario: 005 Create Finance approval user
	Given I create a user with details
		| UserName | DataRoleAlias   | DefaultOperatingAlias   | UserRoleList   |
		| <User1>  | <DataRoleAlias> | <DefaultOperatingAlias> | <UserRoleList> |
	Then I submit it

@training @staging @canada @europe @uk @singapore @ft @qa
Examples:
	| User1       | DataRoleAlias | DefaultOperatingAlias     | UserRoleList                                                                                                                                                                                                                |
	| FRD061_User | Admin         | Dentons Australia Limited | 0:WC:R:Client Account Processor: 1001: 8001,0:WC:R:Client Account Processor: 1001: 8006,0:WC:R:Client Account Processor: 1001: 8007,0:WC:R:Client Account Processor: 1001: 8008,0:WC:R:Client Account Processor: 1001: 8009 |

@CancelProcess
Scenario Outline: 010 Prepare Data for Workflow User for Fee Earner 1
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
	And I remove workflow user from fee earner
		| Search Column      | Search Operator | Search Value | FeeEarnerUser   |
		| Workflow User.Name | Equals          | null         | <FeeEarnerName> |

@ft
Examples:
	| FeeEarnerName          | Client                 | Currency                | CurrencyMethod | Office | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | DefaultOperatingAlias          | PayorName     |
	| Transfer EarnerSurname | Transfer ClientSurname | AUD - Australian Dollar | Bill           | Sydney | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | Sydney        | Dentons UK and Middle East LLP | Lisa Marvelle |
@singapore
Examples:
	| FeeEarnerName          | Client                 | Currency               | CurrencyMethod | Office    | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | DefaultOperatingAlias        | PayorName     |
	| Transfer EarnerSurname | Transfer ClientSurname | SGD - Singapore Dollar | Bill           | Singapore | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | Sydney        | Dentons Rodyk & Davidson LLP | Lisa Marvelle |
@staging
Examples:
	| FeeEarnerName          | Client                 | Currency            | CurrencyMethod | Office         | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice       | DefaultOperatingAlias          | PayorName     |
	| Transfer EarnerSurname | Transfer ClientSurname | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | London Billing (EU) | Dentons UK and Middle East LLP | Lisa Marvelle |


Scenario: 020 Verify that user is able to Submit Client Account Receipt Request
	Given I 'add' a workflow user '<FeeEarnerName>' to fee earner '<FeeEarnerName>'
	Then I proxy as user '<FeeEarnerName>'
	And I open the client account receipt request process
	And I set the aml checks complete checkbox to true
	And I complete all the client account receipt required fields
		| Narrative | TransactionDate | Amount | Reason                         |
		| {Auto}+10 | {Today}-1       | 100    | Client Account Receipt Request |
	When the approval checkbox is checked
	And I add an attachment
		| File           |
		| Attachment.txt |
	And I submit it
	And I validate submit was successful
	And I cancel proxy
	And I remove workflow user from fee earner
		| Search Column      | Search Operator | Search Value | FeeEarnerUser   |
		| Workflow User.Name | Equals          | null         | <FeeEarnerName> |

@CancelProcess @training @staging @canada @europe @uk @singapore @ft @qa
Examples:
	| FeeEarnerName          |
	| Transfer EarnerSurname |
	 
@CancelProcess
Scenario Outline: 040 Create Matter 1 and submit receipt
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
@ft
Examples:
	| Client                 | OperatingUnit                  | FeeEarnerName          | UnallocatedType       | Currency            | CurrencyMethod | Office         | Department | Section | Code | Description | BillingOffice |
	| Transfer ClientSurname | Dentons UK and Middle East LLP | Transfer EarnerSurname | Duplicate/Overpayment | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | CASH | CASH        | Sydney        |
@singapore
Examples:
	| Client                 | OperatingUnit                | FeeEarnerName          | UnallocatedType       | Currency               | CurrencyMethod | Office    | Department | Section | Code | Description | BillingOffice |
	| Transfer ClientSurname | Dentons Rodyk & Davidson LLP | Transfer EarnerSurname | Duplicate/Overpayment | SGD - Singapore Dollar | Bill           | Singapore | Default    | Default | CASH | CASH        | Sydney        |


Scenario Outline: 050 Create Matter 2 and submit receipt
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

@ft @singapore
Examples:
	| Client                                | OperatingUnit                  | FeeEarnerName                       | UnallocatedType       | Currency                | CurrencyMethod | Office | Department | Section | Code | Description | BillingOffice | PayorName   |
	| Sydney - Domestic Client - Subsidiary | Dentons UK and Middle East LLP | Sydney - Partner Full Interest Prem | Duplicate/Overpayment | AUD - Australian Dollar | Bill           | Sydney | Default    | Default | CASH | CASH        | Sydney        | James Smith |

@singapore
Examples:
	| Client             | OperatingUnit                | FeeEarnerName          | UnallocatedType       | Currency               | CurrencyMethod | Office    | Department | Section | Code | Description | BillingOffice | PayorName   |
	| Rose ClientSurname | Dentons Rodyk & Davidson LLP | Transfer EarnerSurname | Duplicate/Overpayment | SGD - Singapore Dollar | Bill           | Singapore | Default    | Default | CASH | CASH        | Sydney        | James Smith |

Scenario Outline: 060 Verify Client Account Transfer
	Given I make a client account transfer request
		| TransferType              | FromAccount   | ToAccount   | IntendedUse |
		| Matter to Matter Transfer | <FromAccount> | <ToAccount> | General     |
	Then I submit it
	And I validate submit was successful

@ft @singapore
Examples:
	| FromAccount                       | ToAccount                         |
	| Sydney - WBC Bank Trust Acc - AUD | Sydney - ANZ Bank Trust Acc - AUD |

@ft @singapore
Scenario Outline: 070 Request Approval from the Fee Earner recieving funds
	And I proxy as user 'FRD061_User'
	When I search for 'Workflow Dashboard'
	Then Then i open the client account transfer
	And I request approval from the 'To' account fee earner
	Then I cancel proxy

@ft @singapore
Scenario Outline: 080 Request Approval,The Fee Earner recieving funds returns the request
	Given I proxy as receiving matter's billing fee earner
	When I search for 'Workflow Dashboard'
	Then Then i open the client account transfer link for trust transfer finance approval
	When I add an attachment
		| File           |
		| Attachment.txt |
	Then I close the process
	Then I reopen the client account transfer
	And I return it
	
@ft @singapore
Scenario Outline: 090 Verify that the request record is now in the transferring matter's Billing Fee Earner
	Given I proxy as user transferring matter's billing fee earner
	When I search for 'Workflow Dashboard'
	Then Then i open the client account transfer
	And I close the process