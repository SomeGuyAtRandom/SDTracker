USE [SignalDB];

SELECT TOP 100 * FROM Projects
GO

EXEC spGetProjectsWithSearch 
	@word1 ='a',
	@districtId=0, 
	@jobTypeId=0, 
	@FieldSelected='xxx', 
	@startDate='4/20/1990'
GO
