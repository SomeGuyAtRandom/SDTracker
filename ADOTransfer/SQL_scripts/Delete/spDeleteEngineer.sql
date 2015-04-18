USE [SignalDB];

IF OBJECT_ID('spDeleteEngineer', 'P') IS NOT NULL
DROP PROCEDURE spDeleteEngineer;

GO

CREATE PROC spDeleteEngineer
@Id int
AS
BEGIN  
 DELETE FROM Engineers
 WHERE Id=@Id;

 DELETE FROM UserPasswords
 WHERE Id=@Id;
    
END;