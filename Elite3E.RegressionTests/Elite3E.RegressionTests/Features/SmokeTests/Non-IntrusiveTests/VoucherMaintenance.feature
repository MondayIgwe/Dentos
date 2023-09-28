Feature: VoucherMaintenance

@ft @qa @staging @training @singapore @europe @canada @uk 
Scenario: Verify Voucher Maintenance
	Given I search for process 'Voucher Maintenance'
	When I select an existing record if present
	Then I verify the sections in voucher maintenance 
