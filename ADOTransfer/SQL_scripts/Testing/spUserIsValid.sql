USE [SignalDB];
GO

IF OBJECT_ID('spUserIsValid', 'P') IS NOT NULL
DROP PROCEDURE spUserIsValid;

GO
	
CREATE PROC spUserIsValid

@UserName nvarchar(30) = null,
@Password nvarchar(30) = null


AS
    BEGIN
	DECLARE @IsValid bit 

	DECLARE @ReturnCount int
	SELECT @ReturnCount = count(*) FROM UserPasswords WHERE UserName = @UserName AND Password = @Password AND IsDisabled=0 
    IF(@ReturnCount = 1)
		SET @IsValid = 1
	ELSE
		SET @IsValid = 0
    END
	SELECT @IsValid
;
GO

exec spUserIsValid 'user', 'user';

