Feature: TrustReceipt

Fee Earner/Secretary request for the receipt to be reversed.
Azure Link: https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/56611


@CancelProcess
Scenario Outline: 010 Prepare Data for Workflow User for Fee Earner
	Given I create the Payer with Api
		| PayerName   | Entity       |
		| <PayorName> | CentralCoast |
	And I create a user with details
		| UserName        | DataRoleAlias | DefaultOperatingAlias   |
		| <FeeEarnerName> | Admin         | <DefaultOperatingAlias> |
	And I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	When I search or create a client
		| Entity Name | FeeEarnerFullName |
		| <Client>    | <FeeEarnerName>   |
	Then I create a matter with details:
		| Client   | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | ChargeTypeGroupName   | CostTypeGroupName   | BillingOffice   | PayorName   |
		| <Client> | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <ChargeTypeGroupName> | <CostTypeGroupName> | <BillingOffice> | <PayorName> |

@e2eft
Examples:
	| FeeEarnerName     | Client       | Currency            | CurrencyMethod | Office         | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | DefaultOperatingAlias          | PayorName   |
	| Billing FeeEarner | Edward Trust | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | Milan         | Dentons UK and Middle East LLP | James Mayor |

@CancelProcess
Scenario Outline: 020 Create a Client Account Receipt
	Given I 'add' a workflow user '<FeeEarnerName>' to fee earner '<FeeEarnerName>'
	And I search for process 'Client Account Receipt'
	And I add a new client account receipt
		| TransactionDate | ClientAccountReceiptType | ClientAccountAcct                    | DocumentNumber |
		| {Today}-1       | Cheque                   | Singapore - SCB Bank Trust Acc - GBP | {Auto}+10      |
	When I add client account receipt detail child form data
		| Amount | IntendedUse | Reason                 |
		| 100    | General     | Client Account Receipt |
	Then I update it
	And submit it
	And I validate submit was successful
	And I remove workflow user from fee earner
		| Search Column      | Search Operator | Search Value | FeeEarnerUser   |
		| Workflow User.Name | Equals          | null         | <FeeEarnerName> |

@e2eft
Examples:
	| FeeEarnerName     |
	| Billing FeeEarner |

@CancelProcess @e2eft
Scenario Outline: 030 Reverse a Client Account Receipt
	Given I search for process 'Client Account Receipt'
	And I select existing receipt
	Then I reverse the client account receipt
		| ReversalDate | Reason     |
		| {Today}      | Correction |
	When I submit it
	And I validate submit was successful
	And I search for process 'Client Account Receipt'
	And I select existing receipt
	And I verify that the reversed client account receipt is not available

@CancelProcess
Scenario Outline: 040 Create a Client Account Receipt
	Given I 'add' a workflow user '<FeeEarnerName>' to fee earner '<FeeEarnerName>'
	And I search for process 'Client Account Receipt'
	And I add a new client account receipt
		| TransactionDate | ClientAccountReceiptType | ClientAccountAcct                    | DocumentNumber |
		| {Today}-1       | EFT/Wire                 | Singapore - SCB Bank Trust Acc - GBP | {Auto}+10      |
	When I add client account receipt detail child form data
		| Amount | IntendedUse | Reason                 |
		| 100    | General     | Client Account Receipt |
	Then I update it
	And submit it
	And I validate submit was successful
	And I remove workflow user from fee earner
		| Search Column      | Search Operator | Search Value | FeeEarnerUser   |
		| Workflow User.Name | Equals          | null         | <FeeEarnerName> |

@e2eft
Examples:
	| FeeEarnerName     |
	| Billing FeeEarner |
