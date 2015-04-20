USE [SignalDB];

IF OBJECT_ID('spUpdateUserPassword', 'P') IS NOT NULL
DROP PROCEDURE spUpdateUserPassword;
GO

CREATE PROC spUpdateUserPassword
	@UserName nvarchar(20),
	@PassWord nvarchar(20)
	AS
BEGIN  
UPDATE UserPasswords
	SET 
		PassWord= @PassWord,
		DateUpdated= GETDATE()
	WHERE
		UserName = @UserName;

END
