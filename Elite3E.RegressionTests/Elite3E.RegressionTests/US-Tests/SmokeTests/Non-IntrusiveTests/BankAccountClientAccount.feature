﻿@us
Feature: BankAccountClientAccount


Scenario:Verify Bank Account Client Account
	Given I search for process 'Bank Account Client Account'
	When I select an existing record if present
	Then I verify the sections in bank account client account