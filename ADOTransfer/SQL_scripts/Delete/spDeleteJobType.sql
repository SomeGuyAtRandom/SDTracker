USE [SignalDB];

IF OBJECT_ID('spDeleteJobType', 'P') IS NOT NULL
DROP PROCEDURE spDeleteJobType;

GO

CREATE PROC spDeleteJobType
@Id int
AS
BEGIN  
 DELETE FROM JobTypes
 WHERE Id=@Id   
END;