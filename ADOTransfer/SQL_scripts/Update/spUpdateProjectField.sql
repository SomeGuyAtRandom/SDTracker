USE [SignalDB];

GO

IF OBJECT_ID('spUpdateProjectDateTimeField', 'P') IS NOT NULL
DROP PROCEDURE spUpdateProjectDateTimeField

GO

CREATE PROC spUpdateProjectDateTimeField
@columnName nvarchar(100),
@Id int,
@ValueIn DateTime

AS 
BEGIN
IF @columnName = 'DateAssigned'
BEGIN
	UPDATE Projects SET DateAssigned = @ValueIn, DateUpdated = GetDate() WHERE Id = @Id
END

IF @columnName = 'StartDate'
BEGIN
	UPDATE Projects SET StartDate = @ValueIn, DateUpdated = GetDate() WHERE Id = @Id
END

END

GO

              
IF OBJECT_ID('spUpdateProjectStringField', 'P') IS NOT NULL
DROP PROCEDURE spUpdateProjectStringField

GO

CREATE PROC spUpdateProjectStringField
@columnName nvarchar(100),
@Id int,
@ValueIn nvarchar(255)
AS 
BEGIN
IF @columnName = 'Location'
BEGIN
	UPDATE Projects SET Location = @ValueIn, DateUpdated = GetDate() WHERE Id = @Id
END

IF @columnName = 'CurrRemark'
BEGIN
	UPDATE Projects SET CurrRemark = @ValueIn, DateUpdated = GetDate() WHERE Id = @Id
END

IF @columnName = 'ProjNo'
BEGIN
	UPDATE Projects SET ProjNo = @ValueIn, DateUpdated = GetDate() WHERE Id = @Id
END

IF @columnName = 'FiveDigit'
BEGIN
	UPDATE Projects SET FiveDigit = @ValueIn, DateUpdated = GetDate() WHERE Id = @Id
END

IF @columnName = 'CDs'
BEGIN
	UPDATE Projects SET CDs = @ValueIn, DateUpdated = GetDate() WHERE Id = @Id
END

IF @columnName = 'CurrentComment'
BEGIN
	UPDATE Projects SET CurrentComment = @ValueIn, DateUpdated = GetDate() WHERE Id = @Id
END

END

GO

IF OBJECT_ID('spUpdateProjectIntField', 'P') IS NOT NULL
DROP PROCEDURE spUpdateProjectIntField

GO

CREATE PROC spUpdateProjectIntField
@columnName nvarchar(100),
@Id int,
@ValueIn int
AS 
BEGIN
IF @columnName = 'Districts'
BEGIN
	UPDATE Projects SET DistrictId = @ValueIn, DateUpdated = GetDate() WHERE Id = @Id
END

IF @columnName = 'JobTypes'
BEGIN
	UPDATE Projects SET JobTypeId = @ValueIn, DateUpdated = GetDate() WHERE Id = @Id
END

IF @columnName = 'HeadEngineers'
BEGIN
	UPDATE Projects SET HeadEngineerId = @ValueIn, DateUpdated = GetDate() WHERE Id = @Id
END

IF @columnName = 'DesignEngineers'
BEGIN
	UPDATE Projects SET DesignEngineerId = @ValueIn, DateUpdated = GetDate() WHERE Id = @Id
END

END

GO

IF OBJECT_ID('spUpdateRequirementsFieldByIdDateTime', 'P') IS NOT NULL
DROP PROCEDURE spUpdateRequirementsFieldByIdDateTime

GO

CREATE PROC spUpdateRequirementsFieldByIdDateTime
@columnName nvarchar(100),
@Id int,
@ValueIn DateTime
AS 
BEGIN
IF @columnName = 'StartDate'
BEGIN
	UPDATE Requirements SET StartDate = @ValueIn, DateUpdated = GetDate() WHERE Id = @Id
END

IF @columnName = 'FinishDate'
BEGIN
	UPDATE Requirements SET FinishDate = @ValueIn, DateUpdated = GetDate() WHERE Id = @Id
END

END

GO

IF OBJECT_ID('spUpdateRequirementsFieldByIdString', 'P') IS NOT NULL
DROP PROCEDURE spUpdateRequirementsFieldByIdString

GO

CREATE PROC spUpdateRequirementsFieldByIdString
@columnName nvarchar(100),
@Id int,
@ValueIn nvarchar(255)
AS 
BEGIN
IF @columnName = 'CurrentComment'
BEGIN
	UPDATE Requirements SET CurrentComment = @ValueIn, DateUpdated = GetDate() WHERE Id = @Id
END

END

GO

IF OBJECT_ID('spUpdateRequirementsFieldByIdBool', 'P') IS NOT NULL
DROP PROCEDURE spUpdateRequirementsFieldByIdBool

GO

CREATE PROC spUpdateRequirementsFieldByIdBool
@columnName nvarchar(100),
@Id int,
@ValueIn bit
AS 
BEGIN
IF @columnName = 'Required'
BEGIN
	UPDATE Requirements SET Required = @ValueIn, DateUpdated = GetDate() WHERE Id = @Id
END

END

GO
