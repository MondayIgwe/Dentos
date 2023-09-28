Feature: BillingWorkflow_SplitBilling


Scenario Outline: 010 Create user
	Given I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	Given I create a user with details
		| UserName | DataRoleAlias | UserRoleList   |
		| <User>   | Admin         | <UserRoleList> |
	And I add a workflow user to a FeeEarner
		| User   | Name            |
		| <User> | <FeeEarnerName> |

@ft
Examples:
	| FeeEarnerName | User       | UserRoleList |
	| FRD36 Pete    | FRD36 Pete |              |
@training @qa @uk @europe @canada @singapore
Examples:
	| FeeEarnerName | User       | UserRoleList |
	| FRD36 Pete    | FRD36 Pete |              |
@staging
Examples:
	| FeeEarnerName | User       | UserRoleList                                                                |
	| FRD36 Pete    | FRD36 Pete | 0:AD:G:Common Authorisations,0:AD:G:System Administrator (read-only setups) |

Scenario: 020 Sub matter creation and time card setup
	Given I create the Payer with Api
		| PayerName   | Entity        |
		| <PayorName> | FrontPage Ltd |
	And I create a matter with details:
		| Client   | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department | Section | ChargeTypeGroupName | CostTypeGroupName | Rate     | PayorName   |
		| <Client> | <FeeEarnerName>   | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | Bill                 | <Office> | Default    | Default | Desc_at_3e8glw7     | Desc_at_3e8glw7   | Standard | <PayorName> |
	And I create a submatter 1 with details:
		| Client   | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department | Section | ChargeTypeGroupName | CostTypeGroupName | Rate     | PayorName   |
		| <Client> | <FeeEarnerName>   | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | Bill                 | <Office> | Default    | Default | Desc_at_3e8glw7     | Desc_at_3e8glw7   | Standard | <PayorName> |
	And I create a submatter 2 with details:
		| Client   | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department | Section | ChargeTypeGroupName | CostTypeGroupName | Rate     | PayorName   |
		| <Client> | <FeeEarnerName>   | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | Bill                 | <Office> | Default    | Default | Desc_at_3e8glw7     | Desc_at_3e8glw7   | Standard | <PayorName> |
	When I add submatters
		| Split Description  | Split Type  | Split Percentage  |
		| <SplitDescription> | <SplitType> | <SplitPercentage> |
	And I view an existing matter
	And I edit the existing matter
		| InvoiceDistributionMethod | InvoiceOverride   | RemittanceAccount   | Language   | PresentationCurrency   | PresentationExchangeRate   |
		| Test Auto Dispatch        | <InvoiceOverride> | <RemittanceAccount> | <Language> | <PresentationCurrency> | <PresentationExchangeRate> |
	And I submit it
	And I submit a time modify
		| Time Type  | Hours | Narrative | FeeEarnerName   | WorkRate | WorkAmount | WorkCurrency | TaxCode   |
		| <TimeType> | 0.1   | {Auto}+10 | <FeeEarnerName> | 1        | 0.1        | <Currency>   | <TaxCode> |
	And I submit a time modify
		| Time Type  | Hours | Narrative | FeeEarnerName   | WorkRate | WorkAmount | WorkCurrency | TaxCode   |
		| <TimeType> | 0.1   | {Auto}+10 | <FeeEarnerName> | 1        | 0.1        | <Currency>   | <TaxCode> |
	Then I can generate the proforma
		| Description | Change Status To | ProformaStatus | IncludeOtherProformas   |
		| {Auto}+10   | <ChangeStatusTo> | Draft          | <IncludeOtherProformas> |

@ft @training @qa @uk
Examples:
	| FeeEarnerName | User       | Client       | SplitDescription | SplitType              | SplitPercentage | TimeType       | Hours1 | Hours2 | TaxCode                    | ChangeStatusTo | ToTaxArea      | Currency            | Office         | PayorName    | IncludeOtherProformas | Language                 | PresentationCurrency | PresentationExchangeRate | RemittanceAccount                     | InvoiceOverride                |
	| FRD36 Pete    | FRD36 Pete | FRD36 Client | 50/50            | Split in Proforma Edit | 50              | FEES (Default) | 0.01   | 0.20   | UK Output Domestic Zero 0% | Terminated     | United Kingdom | GBP - British Pound | London (UKIME) | Emilio Price | Yes                   | English (United Kingdom) | GBP                  | 1.00                     | UKME - Application Off-Set Bank - GBP | London (UKME) Nominal Sequence |
@staging
Examples:
	| FeeEarnerName | User       | Client       | SplitDescription | SplitType              | SplitPercentage | TimeType       | Hours1 | Hours2 | TaxCode                    | ChangeStatusTo | ToTaxArea      | Currency            | Office         | PayorName    | IncludeOtherProformas | Language                 | PresentationCurrency | PresentationExchangeRate | RemittanceAccount                       | InvoiceOverride                |
	| FRD36 Pete    | FRD36 Pete | FRD36 Client | 50/50            | Split in Proforma Edit | 50              | FEES (Default) | 0.01   | 0.20   | UK Output Domestic Zero 0% | Terminated     | United Kingdom | GBP - British Pound | London (UKIME) | Emilio Price | Yes                   | English (United Kingdom) | GBP                  | 1.00                     | Barclays Office - Checking GBP BARLOGBP | London (UKME) Nominal Sequence |
		
@europe
Examples:
	| FeeEarnerName | User       | Client       | SplitDescription | SplitType              | SplitPercentage | TimeType | Hours1 | Hours2 | TaxCode                   | ChangeStatusTo | ToTaxArea      | Currency | Office      | PayorName    | IncludeOtherProformas | Language                 | PresentationCurrency | PresentationExchangeRate | RemittanceAccount               | InvoiceOverride                |
	| FRD36 Pete    | FRD36 Pete | FRD36 Client | 50/50            | Split in Proforma Edit | 50              | FEES     | 0.01   | 0.20   | EU Output Conversion Code | Terminated     | United Kingdom | EUR      | London (EU) | Emilio Price | Yes                   | English (United Kingdom) | EUR                  | 1.00                     | Citibank Europe plc (BUDEURESC) | London (UKME) Nominal Sequence |
@canada
Examples:
	| FeeEarnerName | User       | Client       | SplitDescription | SplitType              | SplitPercentage | TimeType | Hours1 | Hours2 | TaxCode                         | ChangeStatusTo | ToTaxArea | Currency              | Office    | PayorName    | IncludeOtherProformas | Language                 | PresentationCurrency | PresentationExchangeRate | RemittanceAccount                   | InvoiceOverride                |
	| FRD36 Pete    | FRD36 Pete | FRD36 Client | 50/50            | Split in Proforma Edit | 50              | FEES     | 0.01   | 0.20   | CA Output Domestic Standard GST | Terminated     | Canada    | CAD - Canadian Dollar | Vancouver | Emilio Price | Yes                   | English (United Kingdom) | CAD                  | 1.00                     | Bank of Montreal IBA Trust-1248-814 | London (UKME) Nominal Sequence |

@singapore
Examples:
	| FeeEarnerName | User       | Client       | SplitDescription | SplitType              | SplitPercentage | TimeType | Hours1 | Hours2 | TaxCode                 | ChangeStatusTo | ToTaxArea      | Currency               | Office    | PayorName    | IncludeOtherProformas | Language                 | PresentationCurrency | PresentationExchangeRate | RemittanceAccount                          | InvoiceOverride |
	| FRD36 Pete    | FRD36 Pete | FRD36 Client | 50/50            | Split in Proforma Edit | 50              | FEES     | 0.01   | 0.20   | SG Output Domestic Zero | Terminated     | United Kingdom | SGD - Singapore Dollar | Singapore | Emilio Price | Yes                   | English (United Kingdom) | EUR                  | 1.00                     | Singapore - Application Off-Set Bank - SGD | Miscellaneous   |


Scenario Outline: 030 Split proforma workflow
	Given I proxy as user '<User>'
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	And I open the proforma for submission
	And I submit it
	And I cancel proxy
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	Then I open the split proforma
	And I verify the details in proforma edit
		| InvoiceDistributionMethod | AlternativeBankDetails   | PresentationCurrency   | PresentationExchangeRate   |
		| Test Auto Dispatch        | <AlternativeBankDetails> | <PresentationCurrency> | <PresentationExchangeRate> |
	And I split the proforma
@ft @training @qa @uk
Examples:
	| User       | AlternativeBankDetails                | PresentationCurrency | PresentationExchangeRate |
	| FRD36 Pete | UKME - Application Off-Set Bank - GBP | GBP                  | 1.00                     |
@staging
Examples:
	| User       | AlternativeBankDetails                  | PresentationCurrency | PresentationExchangeRate |
	| FRD36 Pete | Barclays Office - Checking GBP BARLOGBP | GBP                  | 1.00                     |
	
@europe
Examples:
	| User       | AlternativeBankDetails          | PresentationCurrency | PresentationExchangeRate |
	| FRD36 Pete | Citibank Europe plc (BUDEURESC) | EUR                  | 1.00                     |
@canada
Examples:
	| User       | AlternativeBankDetails              | PresentationCurrency | PresentationExchangeRate |
	| FRD36 Pete | Bank of Montreal IBA Trust-1248-814 | CAD                  | 1.00                     |
@singapore
Examples:
	| User       | AlternativeBankDetails                     | PresentationCurrency | PresentationExchangeRate |
	| FRD36 Pete | Singapore - Application Off-Set Bank - SGD | GBP                  | 1.00                     |
	

@ft @training @staging @qa @uk @europe @canada @singapore
Scenario Outline: 040 Sub matter proforma bill generation
	Given I proxy as user '<User>'
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	And I open the proforma linked to submatter1 for submission
	And I submit it
	And I cancel proxy
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	And I open the proforma linked to submatter1 for billing
	And I bill it without printing
	And the invoice number is generated
	When I search for 'Workflow Dashboard'
	And I open the Invoice Dispatch workflow task
	And I send the invoice routed to finance team when dispatch method not set
	And I view the invoices
	Given I proxy as user '<User>'
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	And I open the proforma linked to submatter2 for submission
	And I submit it
	And I cancel proxy
	Then I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	Then I open the proforma linked to submatter2 for billing
	And I bill it without printing
	And the invoice number is generated
	And I search for 'Workflow Dashboard'
	And I open the Invoice Dispatch workflow task
	And I send the invoice routed to finance team when dispatch method not set
	Then I view the invoices

@ft @training @staging @qa @uk @europe @canada @singapore
Examples:
	| FeeEarnerName | User       |
	| FRD36 Pete    | FRD36 Pete |
