USE [SignalDB];
GO
IF OBJECT_ID('
exec testReportBuilder 6, @DateIn = '2013-12-31'
go
', 'P') IS NOT NULL
DROP PROCEDURE spUserIsInRole;

GO

CREATE PROC spUserIsInRole

@UserName nvarchar(30) = null,
@Role nvarchar(30) = null

AS
    BEGIN
	DECLARE @IsInRole bit 
	DECLARE @ReturnCount int

		SELECT @ReturnCount = count(*) 
		FROM UserRoles 
		INNER JOIN UserPasswords
		ON UserPasswords.Id = UserRoles.UserId
		INNER JOIN Roles
		ON Roles.Id = UserRoles.RoleId
		WHERE UserPasswords.UserName = @UserName
		AND Roles.RoleType = @Role;

		IF(@ReturnCount >= 1)
			SET @IsInRole = 1;
		ELSE
			SET @IsInRole = 0;
	SELECT @IsInRole as IsInRole;
END
GO