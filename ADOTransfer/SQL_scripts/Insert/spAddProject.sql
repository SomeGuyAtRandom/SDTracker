USE [SignalDB];
GO

IF OBJECT_ID('spAddProject', 'P') IS NOT NULL
DROP PROCEDURE spAddProject;

GO
	
CREATE PROC spAddProject

@FiveDigit nvarchar(10)

AS
BEGIN  


DECLARE @ProjectId int
DECLARE @table table (id int)

SET NOCOUNT ON

 INSERT INTO Projects (Location,DistrictId,FiveDigit,DateCreated,DateUpdated)  
 OUTPUT inserted.id into @table
 SELECT PlaceLocation,DistrictId,FiveDigit,GetDate(),GetDate() FROM Places WHERE FiveDigit=@FiveDigit;
 
SELECT @ProjectId = id from @table;

 INSERT INTO Requirements (RequirementId,ProjectId,DateCreated,DateUpdated)
 SELECT Id,@ProjectId ,GetDate(),GetDate() FROM RequirementType;
 
SET NOCOUNT OFF

-- Return value:
SELECT @ProjectId as ProjectId
END


GO


SELECT * FROM Places ORDER BY FiveDigit

EXEC spAddProject @FiveDigit='00010'