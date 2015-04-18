USE [SignalDB];
GO
IF OBJECT_ID('spGetUserRoles', 'P') IS NOT NULL
DROP PROCEDURE spGetUserRoles;

GO

CREATE PROC spGetUserRoles

@UserName nvarchar(30) = null

AS
BEGIN
DECLARE 
	@ReturnTable TABLE 
	(

		Id int, 
		RoleType nvarchar(255),
		IsMember Bit
	)
DECLARE
	@Id int,
	@RoleType nvarchar(255),
	@IsMember Bit,
	@RoleId int = 0,
	@CountOf int =0,
	@UserId int =0



	DECLARE idxCursor CURSOR FOR SELECT Id, RoleType FROM Roles
	OPEN idxCursor

	FETCH NEXT FROM idxCursor INTO @RoleId, @RoleType
	WHILE @@FETCH_STATUS = 0 
	BEGIN
		
		SELECT @UserId = Id FROM UserPasswords WHERE UserName = @UserName;
		SELECT @CountOf = Count(*) FROM UserRoles WHERE RoleId = @RoleId AND UserId = @UserId;
		

		SELECT @IsMember = 0
		IF @CountOf > 0
		BEGIN
			SELECT @IsMember = 1
		END

		INSERT INTO @ReturnTable (Id, RoleType,IsMember)
		SELECT  @RoleId, @RoleType, @IsMember

		FETCH NEXT FROM idxCursor INTO @RoleId, @RoleType
	END
	
	CLOSE idxCursor    
	DEALLOCATE idxCursor
	SELECT * FROM 	@ReturnTable
END	

		

GO

exec spGetUserRoles @UserName = 'randrade'
go
SELECT * FROM UserRoles WHERE  UserId = 29;