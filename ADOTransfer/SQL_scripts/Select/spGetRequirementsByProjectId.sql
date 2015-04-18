USE [SignalDB];

IF OBJECT_ID('spGetRequirementsByProjectId', 'P') IS NOT NULL
DROP PROCEDURE spGetRequirementsByProjectId;

GO

CREATE PROC spGetRequirementsByProjectId  
@ProjectId int
AS
BEGIN  
 SELECT 
	Requirements.Id,
	Requirements.RequirementId,
	RequirementType.RequirementName,
	Requirements.ProjectId,
	Requirements.Required,
	Requirements.StartDate,
	Requirements.FinishDate,
	Requirements.CurrentComment,
	Requirements.DateCreated, 
	Requirements.DateUpdated
 
 FROM Requirements
 INNER JOIN RequirementType
 ON  Requirements.RequirementId =  RequirementType.Id 
 WHERE Requirements.ProjectId=@ProjectId
END

GO

