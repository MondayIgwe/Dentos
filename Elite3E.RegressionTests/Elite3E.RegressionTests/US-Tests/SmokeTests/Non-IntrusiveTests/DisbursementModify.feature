﻿@us
Feature: DisbursementModify

Scenario: Verify Disbursement modify
	Given I search for process 'Disbursement Modify'
	When I select an existing record if present
	Then I verify the sections in disbursement modify
