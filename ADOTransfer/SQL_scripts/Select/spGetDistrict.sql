USE [SignalDB];

IF OBJECT_ID('spGetDistrict', 'P') IS NOT NULL
DROP PROCEDURE spGetDistrict;

GO

CREATE PROC spGetDistrict  
@Id int
AS
BEGIN  
 SELECT * FROM Districts   
 WHERE Id=@Id
END;