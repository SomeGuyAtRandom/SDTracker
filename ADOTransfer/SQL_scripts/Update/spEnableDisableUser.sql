USE [SignalDB];


IF OBJECT_ID('spEnableDisableUser', 'P') IS NOT NULL
DROP PROCEDURE spEnableDisableUser;

GO

CREATE PROC spEnableDisableUser
@Id int,
@IsDisabled Bit

AS
BEGIN  
UPDATE UserPasswords SET IsDisabled=@IsDisabled  WHERE  Id=@Id
END

GO