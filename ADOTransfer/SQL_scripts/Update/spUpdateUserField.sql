USE [SignalDB];

GO
-- spUpdateUserField
IF OBJECT_ID('spUpdateUserField', 'P') IS NOT NULL
DROP PROCEDURE spUpdateUserField

GO

CREATE PROC spUpdateUserField

@Id int,
@columnName nvarchar(100),
@StringIn nvarchar(255)
AS 
BEGIN
IF @columnName = 'FirstName'
BEGIN
	UPDATE Engineers SET FirstName = @StringIn, DateUpdated = GetDate() WHERE Id = @Id
END
IF @columnName = 'LastName'
BEGIN
	UPDATE Engineers SET LastName = @StringIn, DateUpdated = GetDate() WHERE Id = @Id
END

IF @columnName = 'Email'
BEGIN
	UPDATE Engineers SET Email = @StringIn, DateUpdated = GetDate() WHERE Id = @Id
END

IF @columnName = 'Initials'
BEGIN
	UPDATE Engineers SET Initials = @StringIn, DateUpdated = GetDate() WHERE Id = @Id
END

IF @columnName = 'UserName'
BEGIN
	UPDATE Engineers SET UserName = @StringIn, DateUpdated = GetDate() WHERE Id = @Id
END

IF @columnName = 'Phone'
BEGIN
	UPDATE Engineers SET Phone = @StringIn, DateUpdated = GetDate() WHERE Id = @Id
END


END
GO


