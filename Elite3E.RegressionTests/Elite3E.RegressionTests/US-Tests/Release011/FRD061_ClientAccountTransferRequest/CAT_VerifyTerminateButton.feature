@us
Feature: VerifyTerminateButton

36777 - Verify that the Terminate button works in the Client Account Transfer Request process

@CancelProcess
Scenario Outline: 010 Prepare Data for matter 1
	Given I create a user with details
		| UserName        | DataRoleAlias | DefaultOperatingAlias   | UserRoleList                                   |
		| <FeeEarnerName> | Admin         | <DefaultOperatingAlias> | 0:AD:G:System Administrator (read-only setups) |
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
	| FeeEarnerName             | Client                    | Currency        | CurrencyMethod | Office  | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | DefaultOperatingAlias      | PayorName      |
	| NewTransfer EarnerSurname | NewTransfer ClientSurname | USD - US Dollar | Bill           | Chicago | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | Chicago       | Dentons United States, LLP | Lisaa Marvelle |

@CancelProcess
Scenario Outline: 020 Prepare Data for matter 2
	Given I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	Given I create a fee earner with details
		| EntityName                 |
		| <SupervisingFeeEarnerName> |
	Given I create a fee earner with details
		| EntityName                 |
		| <ResponsibleFeeEarnerName> |
	And I search or create a client
		| Entity Name | FeeEarnerFullName | ResponsibleFeeEarnerName   | SupervisingFeeEarnerName   |
		| <Client>    | <FeeEarnerName>   | <ResponsibleFeeEarnerName> | <SupervisingFeeEarnerName> |
	And I create the Payer with Api
		| PayerName   | Entity       |
		| <PayorName> | BlackBox Ltd |
	And I create a submatter 2 with details:
		| Client   | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department | Section | ChargeTypeGroupName | CostTypeGroupName | BillingOffice   | PayorName   |
		| <Client> | <FeeEarnerName>   | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | Bill                 | <Office> | Default    | Default | Auto_IncludeALL     | Auto_IncludeALL   | <BillingOffice> | <PayorName> |

Examples:
	| SupervisingFeeEarnerName | ResponsibleFeeEarnerName | Client                | OperatingUnit              | FeeEarnerName             | UnallocatedType       | Currency        | CurrencyMethod | Office  | Department | Section | Code | Description | BillingOffice | PayorName    |
	| NewMark EarnerSurname    | NewJade EarnerSurname    | NewRose ClientSurname | Dentons United States, LLP | NewTransfer EarnerSurname | Duplicate/Overpayment | USD - US Dollar | Bill           | Chicago | Default    | Default | CASH | CASH        | Chicago       | Peter Smithh |

@CancelProcess
Scenario Outline: 030 Verify the workflow is terminated and the form is closed
	Given I add a workflow user to a FeeEarner
	| User            | Name            |
	| <FeeEarnerName> | <FeeEarnerName> |
	Then I proxy as user '<FeeEarnerName>'
	When I make a client account transfer request
		| TransferType   | FromAccount   | ToAccount   | IntendedUse | Amount |
		| <TransferType> | <FromAccount> | <ToAccount> | General     | 100    |
	Then I terminate the process
	And I validate terminate was successfull


Examples:
	| TransferType              | FeeEarnerName             | FromAccount                                               | ToAccount                                |
	| Matter To Matter Transfer | NewTransfer EarnerSurname | Dentons US LLP (IOLTA) Interest on Lawyer Trust-204968895 | American Savings Bank - IOLTA-8103649398 |

