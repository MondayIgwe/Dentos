@release7 @frd066 @WIPProvisioning
Feature: WIPProvisioning
		Navigate to the WIP process and verify that the new fields are present in the grid view

Scenario Outline: 001 Create Matter
	Given I create the Payer with Api
		| PayerName   | Entity        |
		| <PayorName> | RedHat Entity |
	And I create a matter with details:
		| Client   | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department | Section | Rate     | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| <Client> | <FeeEarnerName>   | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | Bill                 | <Office> | Default    | Default | Standard | Desc_at_3e8glw7     | Desc_at_3e8glw7   | <PayorName> |
	And I submit a time modify
		| Time Type | Hours | Narrative | FeeEarnerName   | WorkRate | WorkCurrency |
		| FEES      | 0.1   | {Auto}+10 | <FeeEarnerName> | 100      | <Currency>   |
@ft
Examples:
	| Client                        | FeeEarnerName         | Office         | Currency            | PayorName |
	| Client_Automation NumberThree | DentonsAPI FeeEarner4 | London (UKIME) | GBP - British Pound |Mike Terry |
@qa
Examples:
	| Client                        | FeeEarnerName         | Office         | Currency            | PayorName  |
	| Client_Automation NumberThree | DentonsAPI FeeEarner4 | London (UKIME) | GBP - British Pound | Mike Terry |
@training @staging @uk
Examples:
	| Client                        | FeeEarnerName         | Office         | Currency            | PayorName  |
	| Client_Automation NumberThree | DentonsAPI FeeEarner4 | London (UKIME) | GBP - British Pound | Mike Terry |
@europe
Examples:
	| Client                        | FeeEarnerName         | Office      | Currency   | PayorName  |
	| Client_Automation NumberThree | DentonsAPI FeeEarner4 | London (EU) | EUR - Euro | Mike Terry |
@canada
Examples:
	| Client                        | FeeEarnerName         | Office    | Currency              | PayorName  |
	| Client_Automation NumberThree | DentonsAPI FeeEarner4 | Vancouver | CAD - Canadian Dollar | Mike Terry |
@singapore
Examples:
	| Client                        | FeeEarnerName         | Office    | Currency               | PayorName  |
	| Client_Automation NumberThree | DentonsAPI FeeEarner4 | Singapore | SGD - Singapore Dollar | Mike Terry |

Scenario Outline: 002 Verify that the new fields are present and populated
	Given I view an existing matter
	And I modify the Matter WIP
		| MatterWip   |
		| <MatterWIP> |
	And I submit the form
	When I navigate to the WIP process
	And I add the WIP form required fields
		| GL Date  | Through Date  | Edit Type  |
		| <GLDate> | <ThroughDate> | <EditType> |
	Then the new fields should be visible and populated on the WP Amounts child form
		| Client name | Currency   | Office   |
		| <Client>    | <Currency> | <Office> |

@ft
Examples:
	| GLDate    | ThroughDate | EditType                       | Office         | Client                        | Currency | MatterWIP                |
	| {Today}-3 | {Today}     | Calculate (create new records) | London (UKIME) | Client_Automation NumberThree | GBP      | David Test WIP Provision |
@qa
Examples:
	| GLDate    | ThroughDate | EditType                       | Office         | Client                        | Currency | MatterWIP                |
	| {Today}-5 | {Today}     | Calculate (create new records) | London (UKIME) | Client_Automation NumberThree | GBP      | David Test WIP Provision |
@training @staging
Examples:
	| GLDate    | ThroughDate | EditType                       | Office         | Client                        | Currency | MatterWIP          |
	| {Today}-5 | {Today}     | Calculate (create new records) | London (UKIME) | Client_Automation NumberThree | GBP      | Test WIP Provision |
@uk
Examples:
	| GLDate    | ThroughDate | EditType                       | Office         | Client                        | Currency | MatterWIP     |
	| {Today}-5 | {Today}     | Calculate (create new records) | London (UKIME) | Client_Automation NumberThree | GBP      | testProvision |
@canada
Examples:
	| GLDate    | ThroughDate | EditType                       | Office    | Client                        | Currency | MatterWIP     |
	| {Today}-5 | {Today}     | Calculate (create new records) | Vancouver | Client_Automation NumberThree | CAD      | testProvision |
@singapore
Examples:
	| GLDate    | ThroughDate | EditType                       | Office    | Client                        | Currency | MatterWIP    |
	| {Today}-5 | {Today}     | Calculate (create new records) | Singapore | Client_Automation NumberThree | SGD      | Dentons GAAP |
@europe
Examples:
	| GLDate    | ThroughDate | EditType                       | Office      | Client                        | Currency | MatterWIP     |
	| {Today}-5 | {Today}     | Calculate (create new records) | London (EU) | Client_Automation NumberThree | EUR      | Dentons GAAP |
