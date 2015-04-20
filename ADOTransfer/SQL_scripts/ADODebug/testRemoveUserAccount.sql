USE [SignalDB];



-- use in conjunction with testUserRegistration

-- Test with :
-- UserName : account
-- Email    : aa@bb.com
-- Password : Account@1


IF OBJECT_ID('testRemoveUserAccount', 'P') IS NOT NULL
DROP PROCEDURE testRemoveUserAccount;

GO
	
CREATE PROC testRemoveUserAccount(@UserName nvarchar(30))
AS
BEGIN
DECLARE @Id int
BEGIN
	SELECT @Id =Id FROM UserPasswords WHERE UserName=@UserName;

	DELETE FROM Engineers WHERE Id = @Id;
	DELETE FROM UserRoles WHERE UserId = @Id;
	DELETE FROM UserPasswords WHERE Id = @Id;
END
END
GO

EXEC testRemoveUserAccount @UserName='account'
GO

-- DROP PROCEDURE testRemoveUserAccount;
GO
