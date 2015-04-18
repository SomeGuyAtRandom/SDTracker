USE [SignalDB];
GO

IF OBJECT_ID('spAddProject', 'P') IS NOT NULL
DROP PROCEDURE spAddProject;

GO
	
CREATE PROC spAddProject

@PlaceId int

AS
BEGIN  
DECLARE
@ProjectId int = 0


SET NOCOUNT ON
 INSERT INTO Projects (Location,DistrictId,FiveDigit,DateCreated,DateUpdated)  
 SELECT PlaceLocation,DistrictId,FiveDigit,GetDate(),GetDate() FROM Places WHERE Id=@PlaceId;
 
 SELECT @ProjectId = @@Identity

 INSERT INTO Requirements (RequirementId,ProjectId,DateCreated,DateUpdated)
 SELECT Id,@ProjectId ,GetDate(),GetDate() FROM RequirementType;
 
SET NOCOUNT OFF

SELECT @ProjectId as ProjectId
END;

GO
