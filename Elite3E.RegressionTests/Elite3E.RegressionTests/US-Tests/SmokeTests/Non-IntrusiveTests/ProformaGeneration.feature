﻿@us
Feature: ProformaGeneration


Scenario: Verify Proforma Generation
	Given I search for process 'Proforma Generation'
	When I select an existing record if present
	Then I verify the sections in proforma genration
