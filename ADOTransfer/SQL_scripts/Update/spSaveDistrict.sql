USE [SignalDB];

IF OBJECT_ID('spSaveDistrict', 'P') IS NOT NULL
DROP PROCEDURE spSaveDistrict;

GO

CREATE PROC spSaveDistrict
@Id int,
@Name nvarchar(20),
@Code nvarchar(4)

AS
BEGIN  
UPDATE Districts SET
Name = @Name, 
Code= @Code, 
DateUpdated = GetDate()
 WHERE Id=@Id   
END;