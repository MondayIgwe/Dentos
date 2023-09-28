@release4 @frd047 @GlobalVendor
Feature: GlobalVendorFeature
	Global vendors can be created, updated and assigned to a vendor.	

@training @staging  @canada @europe @uk @singapore @ft @qa
Scenario: 010 Create Global Vendor
	Given I add a new global vendor
		| Code | Description    |
		| gv_  | global_vendor_ |
	When I submit the form
	Then I verify the global vendor was created

@training @staging  @canada @europe @uk @singapore @ft @qa
Scenario: 020 Update Global Vendor
	Given I edit the global vendor
		| Description |
		| update_gv_  |
	When I submit the form
	Then I verify the global vendor was updated

Scenario Outline: 030 Assign Global Vendor to a Vendor
	Given I select an existing vendor
		| Vendor           |
		| <VendorFullName> |
	When I select a global vendor
	Then I verify the vendor is updated with the global vendor
		| Vendor           |
		| <VendorFullName> |
@ft @qa @training @staging  @canada @europe @uk @singapore
Examples:
	| VendorFullName     |
	| Amex UnitedKingdom |
