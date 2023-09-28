Feature: Trust Receipt On Account - Client Receipt Form

A short summary of the feature

@e2eft
Scenario Outline: 010 Perform Pre-requisite
	Given I create the Payer with Api
		| PayerName   | Entity       |
		| <PayorName> | CentralCoast |
	And I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	And I create a matter with details:
		| Client   | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department | Section | ChargeTypeGroupName | CostTypeGroupName | InputAmount   | PayorName   |
		| <Client> | <FeeEarnerName>   | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | Bill                 | <Office> | Default    | Default | Desc_at_3e8glw7     | Desc_at_3e8glw7   | <InputAmount> | <PayorName> |

Examples:
	| Client                        | FeeEarnerName         | Office         | OperationUnit | DisbursementType | InputTaxCode                   | Currency            | TransactionType | VoucherStatus | GLAccount                              | VoucherGLTaxCode               | InputAmount | PayorName |
	| Client_Automation NumberThree | DentonsAPI FeeEarner4 | London (UKIME) | Firm          | Court Fees       | UK Input Domestic Standard 20% | GBP - British Pound | Direct Debit    | Approved      | 3000-101010-1000-0000-0000-0000-000000 | UA Input Domestic Standard 20% | 100.00      | James May |

@e2eft
Scenario: 020 Create a bank account client account
	Given I try to add an account for bank account client account
	When I add bank account client account by filling all details
		| Bank   | AccountName | Description | MoneyType   | BankAccountType   | Status   | AccountNumber   | Currency   | Office   | Language                 | GLType   | CashGLAccount   | ContraGLAccount   | AnchorMask   |
		| <Bank> | {Auto}+36   | {Auto}+36   | <MoneyType> | <BankAccountType> | <Status> | <AccountNumber> | <Currency> | <Office> | English (United Kingdom) | <GLType> | <CashGLAccount> | <ContraGLAccount> | <AnchorMask> |
	Then I submit it
	And I validate submit was successful

@e2eft
Examples:
	| Bank            | MoneyType           | BankAccountType | Status   | AccountNumber | Office         | Currency            | GLType                                   | CashGLAccount                          | ContraGLAccount                        | AnchorMask |
	| Bank_at_zLCwqoW | General Trust Money | Client Account  | O - Open | 11223311      | London (UKIME) | GBP - British Pound | Local Adj - Dentons UK & Middle East LLP | 3000-201010-1000-0000-0000-8051-000000 | 3000-201010-1000-0000-0000-8051-000000 | 3000       |

@e2eft
Scenario Outline: 030 Create a Client Account Receipt
	Given I search for process 'Client Account Receipt'
	And I add a new client account receipt
		| TransactionDate | ClientAccountReceiptType | ClientAccountAcct                    | DocumentNumber |
		| {Today}-1       | Cheque                   | Singapore - SCB Bank Trust Acc - GBP | {Auto}+10      |
	When I add client account receipt detail child form data
		| Amount | IntendedUse | Reason                 |
		| 100    | General     | Client Account Receipt |
	Then I update it
	And submit it