USE [SignalDB];

IF OBJECT_ID('spSaveProject', 'P') IS NOT NULL
DROP PROCEDURE spSaveProject;

GO

CREATE PROC spSaveProject
@Id int,
@Location nvarchar(80), 
@DistrictId int, 
@CurrRemark nvarchar(80),
@Rush bit,
@ProjNo nvarchar(20),
@FiveDigit nvarchar(10),
@CDs nvarchar(16), 
@JobTypeId int,
@HeadEngineerId int,
@DesignEngineerId int,
@CurrentComment nvarchar(255),
@DateAssigned datetime,
@StartDate datetime

AS
BEGIN  
UPDATE Projects SET
Location = @Location, 
DistrictId= @DistrictId, 
CurrRemark= @CurrRemark,
Rush = @Rush,
ProjNo = @ProjNo,
FiveDigit = @FiveDigit,
CDs = @CDs, 
JobTypeId = @JobTypeId,
HeadEngineerId = @HeadEngineerId,
DesignEngineerId = @DesignEngineerId,
CurrentComment = @CurrentComment,
DateAssigned = @DateAssigned,
StartDate = @StartDate,
DateUpdated = GetDate()
 WHERE Id=@Id   
END;