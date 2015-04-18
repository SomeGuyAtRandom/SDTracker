USE [SignalDB];

IF OBJECT_ID('spDeleteDistrict', 'P') IS NOT NULL
DROP PROCEDURE spDeleteDistrict;

GO

CREATE PROC spDeleteDistrict
@Id int
AS
BEGIN  
 DELETE FROM Districts
 WHERE Id=@Id   
END;