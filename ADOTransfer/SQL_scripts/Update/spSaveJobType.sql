USE [SignalDB];

IF OBJECT_ID('spSaveJobType', 'P') IS NOT NULL
DROP PROCEDURE spSaveJobType;

GO

CREATE PROC spSaveJobType
@Id int,
@Name nvarchar(255),
@JobCode nvarchar(20)

AS
BEGIN  
UPDATE JobTypes SET
Name = @Name, 
JobCode= @JobCode, 
DateUpdated = GetDate()
 WHERE Id=@Id   
END;