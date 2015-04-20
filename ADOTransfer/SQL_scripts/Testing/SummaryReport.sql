USE [SignalDB];

-- ?reportName=YearSummaryByMonthByJobType&JobTypeId=1&startDate=09-01-2012&test=hello
-- DetailSummary

IF OBJECT_ID('rptDetailSummary', 'P') IS NOT NULL
DROP PROCEDURE rptDetailSummary

GO

CREATE PROC rptDetailSummary(@columnName nvarchar(100), @dateIn DateTime, @jobTypeId int )

AS


BEGIN
SELECT
Projects.Id as ProjectId,
Projects.Location as Location,
Projects.CurrRemark as CurrRemark,
Projects.ProjNo as ProjNo,
Projects.JobTypeId as JobTypeId,
JobTypes.JobCode as JobCode,

Projects.HeadEngineerId as HeadEngineerId,
EngineersH.Initials as HeadEngineerInitials,

Projects.DesignEngineerId as DesignEngineerId,
EngineersD.Initials as DesignEngineerInitials,

Projects.StartDate as StartDate

FROM Projects
INNER JOIN JobTypes
 ON JobTypes.Id = Projects.JobTypeId

INNER JOIN Engineers as EngineersH
 ON EngineersH.Id = Projects.HeadEngineerId

INNER JOIN Engineers as EngineersD
 ON EngineersD.Id = Projects.DesignEngineerId


 WHERE 
	CASE @columnName
		WHEN 'DateAssigned' THEN Projects.DateAssigned
		WHEN 'StartDate' THEN Projects.StartDate
		WHEN 'DateCreated' THEN Projects.DateCreated
		WHEN 'DateUpdated' THEN Projects.DateUpdated
		ELSE NULL
	END 
			>= @dateIn AND
	
				CASE @columnName
				WHEN 'DateAssigned' THEN Projects.DateAssigned
				WHEN 'StartDate' THEN Projects.StartDate
				WHEN 'DateCreated' THEN Projects.DateCreated
				WHEN 'DateUpdated' THEN Projects.DateUpdated
				ELSE NULL
			END
			< DateAdd(Month, 1, @dateIn)	AND Projects.JobTypeId = @jobTypeId

ORDER BY Projects.StartDate, Projects.Location


END


GO