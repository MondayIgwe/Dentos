@release9 @srs008 @Client_Maintenance
Feature: Client_Maintenance

A short summary of the feature


Scenario Outline: 010 Create a Client
	Given I search or create a client with fee earner
		| Entity Name  | FeeEarnerFullName |
		| <ClientName> | <FeeEarnerName>   |

@training @staging @canada @europe @uk @singapore @us @ft @qa @CancelProcess
Examples:
	| ClientName                 | BillingRules                                            | FeeEarnerName           |
	| FRD008Client ClientSurname | Proforma Time - Check the negative and positive amounts | FRD008Fee EarnerSurname |

@training @staging @canada @europe @uk @singapore @us @ft @qa @CancelProcess
Scenario Outline: 020 I want to check the "Global Client Reference" field is available and editable in Client Maintenance.
	When I search for the client
	And edit the client details
		| Global Client Number |
		| Ab 123 $. 0.00       |
	And I submit it
	Then I search for the client
	And I verify that the 'Global Client Number' field is correctly edited

@training @staging @canada @europe @uk @singapore @us @ft @qa @CancelProcess
Scenario Outline: 030 I want to check the field is available and editable in Client Maintenance.
	When I search for the client
	Then I update the 'Client and eBilling Additional Information' child forms with the details:
	And I submit it
	Then I search for the client
	And I verify that the 'Billing Accrual Update Required?' field is correctly edited


Scenario Outline: 040 I want to check the "Credit Details" fields are available and editable in Client Maintenance.
	When I search for the client
	Then I update the 'Credit Details' child forms with the details:
		| Risk Score | Credit Score Rating | Credit Limit | Currency   | AML Risk | Finance Risk Score |
		| 45.30      | 23.80               | 3,456.89     | <Currency> | 34.56    | 123.45             |
	And I submit it
	Then I search for the client
	And I verify that the 'Credit Details' field is correctly edited
	And I cancel it

@uk @ft @qa @training @staging @europe
Examples:
	| Currency   |
	| EUR - Euro |
@us
Examples:
	| Currency        |
	| USD - US Dollar |
@canada
Examples:
	| Currency              |
	| CAD - Canadian Dollar |
@singapore
Examples:
	| Currency               |
	| SGD - Singapore Dollar |

@CancelProcess
Scenario Outline: 050 I want to check the Client Defaults fields are available in Client Maintenance.
	When I search for the client
	Then I update the 'Client Defaults' child forms with the details:
		| Office   | Department   | PTA Fees 1      | PTA Cost 1      | PTA Charge 1      | PTA Fees 2      | PTA Cost 2      | PTA Charge 2      |
		| <Office> | <Department> | <PTAGroupFees1> | <PTAGroupDisb1> | <PTAGroupCharge1> | <PTAGroupFees2> | <PTAGroupDisb2> | <PTAGroupCharge2> |
	And I add another 'Client Defaults' child forms with the details:
		| Office          | Department          | PTA Fees 1      | PTA Cost 1      | PTA Charge 1      | PTA Fees 2      | PTA Cost 2      | PTA Charge 2      |
		| <UpdatedOffice> | <UpdatedDepartment> | <PTAGroupFees1> | <PTAGroupDisb1> | <PTAGroupCharge1> | <PTAGroupFees2> | <PTAGroupDisb2> | <PTAGroupCharge2> |
	And I submit it
	Then I search for the client
	And I verify that the 'Client Defaults' field is correctly edited

@uk @singapore
Examples:
	| UpdatedOffice | Office         | Department | PTAGroupFees1           | PTAGroupDisb1           | PTAGroupCharge1        | PTAGroupFees2           | PTAGroupDisb2       | PTAGroupCharge2      | UpdatedDepartment         |
	| Tbilisi       | London (UKIME) | Default    | ABA Bankruptcy Code Set | ABA Counseling Code Set | ABA eDicovery Code Set | ABA Litigation Code Set | ABA Patent Code Set | ABA Project Code Set | SG_Information Technology |
@ft
Examples:
	| UpdatedOffice | Office         | Department | PTAGroupFees1           | PTAGroupDisb1           | PTAGroupCharge1        | PTAGroupFees2           | PTAGroupDisb2       | PTAGroupCharge2      | UpdatedDepartment         |
	| Almaty        | London (UKIME) | Default    | ABA Bankruptcy Code Set | ABA Counseling Code Set | ABA eDicovery Code Set | ABA Litigation Code Set | ABA Patent Code Set | ABA Project Code Set | SG_Information Technology |
@qa
Examples:
	| UpdatedOffice | Office         | Department | PTAGroupFees1           | PTAGroupDisb1           | PTAGroupCharge1        | PTAGroupFees2           | PTAGroupDisb2       | PTAGroupCharge2      | UpdatedDepartment         |
	| Tbilisi       | London (UKIME) | Default    | ABA Bankruptcy Code Set | ABA Counseling Code Set | ABA eDicovery Code Set | ABA Litigation Code Set | ABA Patent Code Set | ABA Project Code Set | SG_Information Technology |
@europe
Examples:
	| UpdatedOffice | Office      | Department | PTAGroupFees1           | PTAGroupDisb1           | PTAGroupCharge1        | PTAGroupFees2           | PTAGroupDisb2       | PTAGroupCharge2      | UpdatedDepartment         |
	| Almaty        | London (EU) | Default    | ABA Bankruptcy Code Set | ABA Counseling Code Set | ABA eDicovery Code Set | ABA Litigation Code Set | ABA Patent Code Set | ABA Project Code Set | SG_Information Technology |
@us
Examples:
	| UpdatedOffice | Office  | Department | PTAGroupFees1           | PTAGroupDisb1           | PTAGroupCharge1        | PTAGroupFees2           | PTAGroupDisb2       | PTAGroupCharge2      | UpdatedDepartment         |
	| Tbilisi       | Chicago | Default    | ABA Bankruptcy Code Set | ABA Counseling Code Set | ABA eDicovery Code Set | ABA Litigation Code Set | ABA Patent Code Set | ABA Project Code Set | SG_Information Technology |
@canada
Examples:
	| UpdatedOffice | Office    | Department | PTAGroupFees1           | PTAGroupDisb1           | PTAGroupCharge1        | PTAGroupFees2           | PTAGroupDisb2       | PTAGroupCharge2      | UpdatedDepartment         |
	| Almaty        | Vancouver | Default    | ABA Bankruptcy Code Set | ABA Counseling Code Set | ABA eDicovery Code Set | ABA Litigation Code Set | ABA Patent Code Set | ABA Project Code Set | SG_Information Technology |

@training @uk @singapore
Examples:
	| UpdatedOffice | Office         | Department | PTAGroupFees1           | PTAGroupDisb1           | PTAGroupCharge1        | PTAGroupFees2           | PTAGroupDisb2       | PTAGroupCharge2      | UpdatedDepartment     |
	| Tbilisi       | London (UKIME) | Default    | ABA Bankruptcy Code Set | ABA Counseling Code Set | ABA eDicovery Code Set | ABA Litigation Code Set | ABA Patent Code Set | ABA Project Code Set | Anti Money Laundering |

@staging
Examples:
	| UpdatedOffice | Office         | Department | PTAGroupFees1           | PTAGroupDisb1           | PTAGroupCharge1        | PTAGroupFees2           | PTAGroupDisb2       | PTAGroupCharge2      | UpdatedDepartment         |
	| Tbilisi       | London (UKIME) | Default    | ABA Bankruptcy Code Set | ABA Counseling Code Set | ABA eDicovery Code Set | ABA Litigation Code Set | ABA Patent Code Set | ABA Project Code Set | SG_Information Technology |
@CancelProcess
Scenario Outline: 060 I want to create a duplicate in Client Maintenance.
	When I search for the client
	Then I update the 'Client Defaults' child forms with the details:
		| Office   | Department   | PTA Fees 1      | PTA Cost 1      | PTA Charge 1      | PTA Fees 2      | PTA Cost 2      | PTA Charge 2      |
		| <Office> | <Department> | <PTAGroupFees1> | <PTAGroupDisb1> | <PTAGroupCharge1> | <PTAGroupFees2> | <PTAGroupDisb2> | <PTAGroupCharge2> |
	And I add another 'Client Defaults' child forms with the details:
		| Office          | Department   | PTA Fees 1      | PTA Cost 1      | PTA Charge 1      | PTA Fees 2      | PTA Cost 2      | PTA Charge 2      |
		| <UpdatedOffice> | <Department> | <PTAGroupFees1> | <PTAGroupDisb1> | <PTAGroupCharge1> | <PTAGroupFees2> | <PTAGroupDisb2> | <PTAGroupCharge2> |
	And I submit it
	Then I verify that an error message 'Cannot have duplicate Office and Department combination' is generated


@training @staging @uk @singapore
Examples:
	| UpdatedOffice | Office         | Department | PTAGroupFees1           | PTAGroupDisb1           | PTAGroupCharge1        | PTAGroupFees2           | PTAGroupDisb2       | PTAGroupCharge2      |
	| Amsterdam     | London (UKIME) | Default    | ABA Bankruptcy Code Set | ABA Counseling Code Set | ABA eDicovery Code Set | ABA Litigation Code Set | ABA Patent Code Set | ABA Project Code Set |
@ft
Examples:
	| UpdatedOffice | Office         | Department | PTAGroupFees1           | PTAGroupDisb1           | PTAGroupCharge1        | PTAGroupFees2           | PTAGroupDisb2       | PTAGroupCharge2      |
	| Berlin        | London (UKIME) | Default    | ABA Bankruptcy Code Set | ABA Counseling Code Set | ABA eDicovery Code Set | ABA Litigation Code Set | ABA Patent Code Set | ABA Project Code Set |
@qa
Examples:
	| UpdatedOffice | Office         | Department | PTAGroupFees1           | PTAGroupDisb1           | PTAGroupCharge1        | PTAGroupFees2           | PTAGroupDisb2       | PTAGroupCharge2      |
	| Amsterdam     | London (UKIME) | Default    | ABA Bankruptcy Code Set | ABA Counseling Code Set | ABA eDicovery Code Set | ABA Litigation Code Set | ABA Patent Code Set | ABA Project Code Set |
@europe
Examples:
	| UpdatedOffice | Office      | Department | PTAGroupFees1           | PTAGroupDisb1           | PTAGroupCharge1        | PTAGroupFees2           | PTAGroupDisb2       | PTAGroupCharge2      |
	| Berlin        | London (EU) | Default    | ABA Bankruptcy Code Set | ABA Counseling Code Set | ABA eDicovery Code Set | ABA Litigation Code Set | ABA Patent Code Set | ABA Project Code Set |
@us
Examples:
	| UpdatedOffice | Office  | Department | PTAGroupFees1           | PTAGroupDisb1           | PTAGroupCharge1        | PTAGroupFees2           | PTAGroupDisb2       | PTAGroupCharge2      |
	| Amsterdam     | Chicago | Default    | ABA Bankruptcy Code Set | ABA Counseling Code Set | ABA eDicovery Code Set | ABA Litigation Code Set | ABA Patent Code Set | ABA Project Code Set |
@canada
Examples:
	| UpdatedOffice | Office    | Department | PTAGroupFees1           | PTAGroupDisb1           | PTAGroupCharge1        | PTAGroupFees2           | PTAGroupDisb2       | PTAGroupCharge2      |
	| Amsterdam     | Vancouver | Default    | ABA Bankruptcy Code Set | ABA Counseling Code Set | ABA eDicovery Code Set | ABA Litigation Code Set | ABA Patent Code Set | ABA Project Code Set |

	@training @staging @canada @europe @uk @singapore @ft @qa
	Scenario Outline: 070 Cleanup Client
	Given I search for the client
	Then delete the client
	And I submit it