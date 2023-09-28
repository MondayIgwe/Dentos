Feature: MatterQuickBilling

A short summary of the feature

 
Scenario Outline: 010 Create Matter
	Given I create the Payer with Api
		| PayerName   | Entity   |
		| <PayerName> | <Entity> |
	And I create a new matter
	When I update the matter
		| Client   | Status   | Open Date  | Matter Name  | Master Matter  | Matter Currency Method | Statement Site  |
		| <Client> | <Status> | <OpenDate> | <MatterName> | <MasterMatter> | <MatterCurrencyMethod> | <StatementSite> |
	And I update the effective dated information
		| Child Form                  | Office  |
		| Effective Dated Information | Default |
	And I update the matter rates
		| Child Form   | Rate   |
		| Matter Rates | <Rate> |
	And I add a Matter Payer
		| Start Date |
		| {Today}+1  |
	When I set 'Quick Bill' checkbox to 'true'
	And I add a new cost type group
		| Cost Type Group |
		| <CostTypeGroup> |
	And I add new charge type group
		| Charge Type Group |
		| Desc_at_3e8glw7   |
	Then verify the matter number is generated
@ft
Examples:
	| ToTaxArea                         | PayerName  | Entity        | Client                           | Status     | OpenDate | MatterName | MasterMatter | MatterCurrencyMethod | StatementSite      | Office         | CostTypeGroup   | Rate               |
	| United Kingdom - Land Transaction | Dave Peter | SeasonsEntity | Client_Automation Matter_HTOvMOn | Fully Open | {Today}  | {Auto}+10  | True         | Bill                 | New/Edit Stmt Site | London (UKIME) | Desc_at_VowqCrR | Standard rate + 5% |
@singapore
Examples:
	| ToTaxArea                         | PayerName  | Entity        | Client                           | Status     | OpenDate | MatterName | MasterMatter | MatterCurrencyMethod | StatementSite      | Office         | CostTypeGroup   | Rate               |
	| United Kingdom - Land Transaction | Dave Peter | SeasonsEntity | Client_Automation Matter_HTOvMOn | Fully Open | {Today}  | {Auto}+10  | True         | Bill                 | New/Edit Stmt Site |Singapore | Desc_at_VowqCrR | Standard rate + 5% |




@ft @singapore
Scenario Outline: 020 Verify that the Quick billing is present and selectable
	Given I view an existing matter
	And I confirm 'Quick Bill' checkbox is set to 'true'
	And I submit the form

