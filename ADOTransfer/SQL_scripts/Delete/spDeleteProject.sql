USE [SignalDB];

IF OBJECT_ID('spDeleteProject', 'P') IS NOT NULL
DROP PROCEDURE spDeleteProject;

GO

CREATE PROC spDeleteProject
@Id int
AS
BEGIN  
	DELETE FROM Requirements WHERE ProjectId=@Id ;
	DELETE FROM Projects WHERE Id=@Id;
END

GO
