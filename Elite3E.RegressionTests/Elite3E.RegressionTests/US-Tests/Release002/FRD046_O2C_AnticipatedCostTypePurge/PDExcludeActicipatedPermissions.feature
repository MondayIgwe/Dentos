@us
@ignore
Feature: Exclude Acticipated Permissions
	Verify the permissions on the Exclude Anticipated 

	Ignored because we do not have permissions for perge process.
@LoginAsAdminUser1
Scenario: 010 Impersonate user who doesn't have purge permissions
	When I view the purge disbursement without permissions
	Then exclude anticipated is selected and disabled

Scenario: 020 Cancel the Proxy
	Given I cancel the proxy

Scenario: 030 User with Purge Permissions
	When I view the purge disbursement
	Then exclude anticipated is selected and enabled
