USE [SignalDB];
GO

IF OBJECT_ID('spDeleteUserDetail', 'P') IS NOT NULL
DROP PROCEDURE spDeleteUserDetail;

GO
	
CREATE PROC spDeleteUserDetail

@Id int

AS
BEGIN  

DELETE FROM Engineers WHERE Id = @Id;
DELETE FROM UserPasswords WHERE Id = @Id;

END;

GO
