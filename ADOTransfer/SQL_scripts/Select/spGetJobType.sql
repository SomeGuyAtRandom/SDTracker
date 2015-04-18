USE [SignalDB];

IF OBJECT_ID('spGetJobType', 'P') IS NOT NULL
DROP PROCEDURE spGetJobType;

GO

CREATE PROC spGetJobType 
@Id int
AS
BEGIN  
 SELECT * FROM JobTypes   
 WHERE Id=@Id
END;