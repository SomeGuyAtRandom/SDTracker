USE [SignalDB];

IF OBJECT_ID('spGetEngineerById', 'P') IS NOT NULL
DROP PROCEDURE spGetEngineerById;

GO

CREATE PROC spGetEngineerById  
@Id int
AS
BEGIN  
 SELECT * FROM Engineers   
 WHERE Id=@Id
END
GO

