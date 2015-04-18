USE [SignalDB];

IF OBJECT_ID('spGetProject', 'P') IS NOT NULL
DROP PROCEDURE spGetProject;

GO

CREATE PROC spGetProject  
@Id int
AS
BEGIN  
 SELECT * FROM Projects   
 WHERE Id=@Id
END;