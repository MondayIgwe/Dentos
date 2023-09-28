Use TE_3E_GD_QA
GO

--106060008

--Does Table Exist?
Declare @tableName varchar(100) = 'ClientManMet_at_YHQT9xOhn';
SELECT t.name 'TableName', t.type_desc 'TableDesc', t.create_date 'CreatedDate' FROM sys.tables t
WHERE name = @tableName

-- Query for MxMatterWIP_ccc
SELECT mt.DisplayName as 'MatterName', mt.Number as 'MatterNumber', BOA as 'BOA' 
FROM WipTestTable as fb inner join Matter as mt on fb.matter=mt.MattIndex
where mt.Number like '%10606000%'

-- Query for MxClientManagement_ccc
SELECT mt.DisplayName as 'MatterName', mt.Number as 'MatterNumber', BOA as 'BOA'
FROM ClientManMet_at_YHQT9xOhn as cmt inner join MattDate as md on cmt.MattDate=md.MattDateID inner join matter mt on md.MatterLkUp=mt.MattIndex 
where mt.Number like '%10606000%'

-- Query for MxInvestment_ccc
SELECT mt.DisplayName as 'MatterName', mt.Number as 'MatterNumber', BOA as 'BOA'
FROM MyTestInvest as imt inner join MattDate as md on imt.MattDate=md.MattDateID inner join matter mt on md.MatterLkUp=mt.MattIndex
where mt.Number like '%10606000%'

-- Query for MxMatterAgedAR_ccc
SELECT mt.DisplayName as 'MatterName', mt.Number as 'MatterNumber', BOA as 'BOA'
FROM MatterAgedARTable as mat inner join Matter as mt on mat.matter=mt.MattIndex


SELECT mt.DisplayName as 'MatterName', mt.Number as 'MatterNumber', BOA as 'BOA'
FROM MatterAgedAR_at_k1aJ0Js1S as mat inner join Matter as mt on mat.matter=mt.MattIndex
Where mt.Number like '%10617000%'

-- Matter should be present: 106170009

/*
The names of the metrics on the front-end are:

Client Management Metric (with BOA) : it is MxClientManagement_ccc

Matter Aged WIP Metric (By Aging with BOA): it is MxMatterWIP_ccc

Investment Metric (with BOA): it is MxInvestment_ccc

Matter Aged AR Metric (with BOA): it is MatterAgedARMetric_ccc
*/