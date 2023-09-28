@release2 @frd046 @ExcludeActicipatedPermissions
Feature: Exclude Acticipated Permissions
	Verify the permissions on the Exclude Anticipated 

@LoginAsAdminUser1 @ft @training @staging  @canada @europe @uk @singapore   @qa
Scenario: 010 Impersonate user who doesn't have purge permissions
	When I view the purge disbursement without permissions
	Then exclude anticipated is selected and disabled

@ft @training @staging  @canada @europe @uk @singapore   @qa
Scenario: 020 Cancel the Proxy
	Given I cancel the proxy

@ft @training @staging  @canada @europe @uk @singapore   @qa
Scenario: 030 User with Purge Permissions
	When I view the purge disbursement
	Then exclude anticipated is selected and enabled
