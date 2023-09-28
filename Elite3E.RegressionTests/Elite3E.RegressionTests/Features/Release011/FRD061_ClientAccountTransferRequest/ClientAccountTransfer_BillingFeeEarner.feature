Feature: ClientAccountTransfer_BillingFeeEarner

In Trust transfer request form, when the from and to billing fee earners are same
the approval goes directly to Finance approval team's workflow dashboard

@CancelProcess
Scenario: 005 Create Finance approval user
	Given I create a user with details
		| UserName | DataRoleAlias   | DefaultOperatingAlias   | UserRoleList   |
		| <User1>  | <DataRoleAlias> | <DefaultOperatingAlias> | <UserRoleList> |
	Then I submit it
	
@training @staging @canada @europe @uk @singapore @ft @qa
Examples:
	| User1       | DataRoleAlias | DefaultOperatingAlias     | UserRoleList                                                                                                                                                                                                                |
	| FRD061_User | Admin         | Dentons Australia Limited | 0:WC:R:Client Account Processor: 1001: 8001,0:WC:R:Client Account Processor: 1001: 8006,0:WC:R:Client Account Processor: 1001: 8007,0:WC:R:Client Account Processor: 1001: 8008,0:WC:R:Client Account Processor: 1001: 8009 |

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
	And I remove workflow user from fee earner
		| Search Column      | Search Operator | Search Value | FeeEarnerUser   |
		| Workflow User.Name | Equals          | null         | <FeeEarnerName> |

@training @staging @ft @qa
Examples:
	| FeeEarnerName          | Client                 | Currency                | CurrencyMethod | Office | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | DefaultOperatingAlias          | PayorName     |
	| Transfer EarnerSurname | Transfer ClientSurname | AUD - Australian Dollar | Bill           | Sydney | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | Sydney        | Dentons UK and Middle East LLP | Lisa Marvelle |
@uk
Examples:
	| FeeEarnerName          | Client                 | Currency            | CurrencyMethod | Office      | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | DefaultOperatingAlias          | PayorName     |
	| Transfer EarnerSurname | Transfer ClientSurname | GBP - British Pound | Bill           | London (EU) | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | London (EU)   | Dentons UK and Middle East LLP | Lisa Marvelle |
@singapore
Examples:
	| FeeEarnerName          | Client                 | Currency               | CurrencyMethod | Office    | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | DefaultOperatingAlias        | PayorName     |
	| Transfer EarnerSurname | Transfer ClientSurname | SGD - Singapore Dollar | Bill           | Singapore | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | Singapore     | Dentons Rodyk & Davidson LLP | Lisa Marvelle |
@europe
Examples:
	| FeeEarnerName          | Client                 | Currency   | CurrencyMethod | Office      | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | DefaultOperatingAlias   | PayorName     |
	| Transfer EarnerSurname | Transfer ClientSurname | EUR - Euro | Bill           | London (EU) | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | London (EU)   | Dentons Europe LLP (UK) | Lisa Marvelle |
@canada
Examples:
	| FeeEarnerName          | Client                 | Currency              | CurrencyMethod | Office    | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | DefaultOperatingAlias | PayorName     |
	| Transfer EarnerSurname | Transfer ClientSurname | CAD - Canadian Dollar | Bill           | Vancouver | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | Vancouver     | Dentons Canada LLP    | Lisa Marvelle |

Scenario: 020 Create a receipt request to load balance for matter 1 account
	Given I 'add' a workflow user '<FeeEarnerName>' to fee earner '<FeeEarnerName>'
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


@CancelProcess @training @staging @canada @europe @uk @singapore @ft @qa
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

@training @staging @ft @qa
Examples:
	| Code | Description | UnallocatedType       | OperatingUnit                  |
	| CASH | CASH        | Duplicate/Overpayment | Dentons UK and Middle East LLP |
@uk
Examples:
	| Code | Description | UnallocatedType       | OperatingUnit                  |
	| CASH | CASH        | Duplicate/Overpayment | Dentons UK and Middle East LLP |
@canada
Examples:
	| Code | Description | UnallocatedType       | OperatingUnit                  |
	| CASH | CASH        | Duplicate/Overpayment | Dentons UK and Middle East LLP |
@singapore
Examples:
	| Code | Description | UnallocatedType       | OperatingUnit                  |
	| CASH | CASH        | Duplicate/Overpayment | Dentons UK and Middle East LLP |
@europe
Examples:
	| Code | Description | UnallocatedType       | OperatingUnit                  |
	| CASH | CASH        | Duplicate/Overpayment | Dentons UK and Middle East LLP |

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

@training @staging @ft @qa
Examples:
	| Code | Description | Client                 | OperatingUnit                  | Currency            | Office  | BillingOffice | CurrencyMethod | Department | Section | FeeEarnerName          | UnallocatedType       | PayorName   |
	| CASH | CASH        | Transfer ClientSurname | Dentons UK and Middle East LLP | GBP - British Pound | Chicago | Sydney        | Bill           | Default    | Default | Transfer EarnerSurname | Duplicate/Overpayment | James Smith |
@uk
Examples:
	| Code | Description | Client                 | OperatingUnit                  | Currency            | Office      | BillingOffice  | CurrencyMethod | Department | Section | FeeEarnerName          | UnallocatedType       | PayorName   |
	| CASH | CASH        | Transfer ClientSurname | Dentons UK and Middle East LLP | GBP - British Pound | London (EU) | London (UKIME) | Bill           | Default    | Default | Transfer EarnerSurname | Duplicate/Overpayment | James Smith |
@canada
Examples:
	| Code | Description | Client                 | OperatingUnit      | Currency              | Office    | BillingOffice | CurrencyMethod | Department | Section | FeeEarnerName          | UnallocatedType       | PayorName   |
	| CASH | CASH        | Transfer ClientSurname | Dentons Canada LLP | CAD - Canadian Dollar | Vancouver | Vancouver     | Bill           | Default    | Default | Transfer EarnerSurname | Duplicate/Overpayment | James Smith |
@singapore
Examples:
	| Code | Description | Client                 | OperatingUnit                | Currency               | Office    | BillingOffice | CurrencyMethod | Department | Section | FeeEarnerName          | UnallocatedType       | PayorName   |
	| CASH | CASH        | Transfer ClientSurname | Dentons Rodyk & Davidson LLP | SGD - Singapore Dollar | Singapore | Singapore     | Bill           | Default    | Default | Transfer EarnerSurname | Duplicate/Overpayment | James Smith |
@europe
Examples:
	| Code | Description | Client                 | OperatingUnit           | Currency   | Office      | BillingOffice | CurrencyMethod | Department | Section | FeeEarnerName          | UnallocatedType       | PayorName   |
	| CASH | CASH        | Transfer ClientSurname | Dentons Europe LLP (UK) | EUR - Euro | London (EU) | London (EU)   | Bill           | Default    | Default | Transfer EarnerSurname | Duplicate/Overpayment | James Smith |

Scenario: 050 Verify the transfer request having same billing fee earner goes direct to finance approval
	Given I 'add' a workflow user '<FeeEarnerName>' to fee earner '<FeeEarnerName>'
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

@CancelProcess @training @staging @canada @europe @uk @singapore @ft @qa
Examples:
	| FeeEarnerName          | FromAccount                       | ToAccount                         |
	| Transfer EarnerSurname | Sydney - WBC Bank Trust Acc - AUD | Sydney - ANZ Bank Trust Acc - AUD |
