Use TE_3E_GD_QA
GO


-- Query for MxMatterWIP_ccc
SELECT mt.DisplayName as 'Matter Name', mt.Number as 'Matter Number', BOA as 'BOA found by MxMatterWIP_ccc' 
FROM FredBrother as fb
inner join Matter as mt on fb.matter=mt.MattIndex

-- Query for MxClientManagement_ccc
SELECT mt.DisplayName as 'Matter Name', mt.Number as 'Matter Number', BOA as 'BOA found by MxClientManagement_ccc'
FROM ClientManagementTable as cmt
inner join MattDate as md on cmt.MattDate=md.MattDateID
inner join matter mt on md.MatterLkUp=mt.MattIndex

-- Query for MxInvestment_ccc
SELECT mt.DisplayName as 'Matter Name', mt.Number as 'Matter Number', BOA as 'BOA found by MxInvestment_ccc'
FROM InvestmentMetricTable as imt
inner join MattDate as md on imt.MattDate=md.MattDateID
inner join matter mt on md.MatterLkUp=mt.MattIndex

-- Query for MxMatterAgedAR_ccc
SELECT mt.DisplayName as 'Matter Name', mt.Number as 'Matter Number', BOA as 'BOA found by MxMatterAgedAR_ccc'
FROM MatterAgedARTable as mat
inner join Matter as mt on mat.matter=mt.MattIndex

--In every case, the name of the table to search in the second line (“FROM…”) should be replaced with the name of the table you specified in the metric itself,
--In other words, if you called the above table “MariTable” the sql would change as below

-- Query for MxClientManagement_ccc
SELECT mt.DisplayName as 'Matter Name', mt.Number as 'Matter Number', BOA as 'BOA found by MxClientManagement_ccc'
FROM MariTable as cmt
inner join MattDate as md on cmt.MattDate=md.MattDateID
inner join matter mt on md.MatterLkUp=mt.MattIndex

--This will show the matter number and name together with the amount of BOA identified by the metric.

--If, on the other hand, you want to see everything in the table created (you won’t see the matter name and number) you can simply do this:

SELECT * FROM MariTable

--Again, replacing “MariTable” with the name of your table.
