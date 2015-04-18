USE [xxxx];
GO

SELECT count(*) from Projects WHERE StartDate BETWEEN '2011-11-11' AND '2014-11-11'

go

exec testReportBuilder 6, @DateIn = '2013-12-31'
go

IF OBJECT_ID('testReportBuilder', 'P') IS NOT NULL
DROP PROCEDURE testReportBuilder

go

CREATE PROC testReportBuilder
@Month int,
@DateIn DateTime

AS
BEGIN 
DECLARE 
@Jan int = 0,
@Feb int = 0,
@Mar int = 0,
@EndDate DateTime


SET @EndDate = DateAdd(Month, DateDiff(Month, 0, @DateIn), 0)
SET @EndDate = DateAdd (month , -3 , @EndDate )

IF @Month = 1
BEGIN
	SELECT * FROM Districts
END

IF @Month >= 1
BEGIN
	SET @Jan = @Jan + 1
	SET @Feb = @Feb + 1
	SET @Mar = @Mar + 1
END

IF @Month = 2
BEGIN
	SELECT * FROM Engineers
END

IF @Month >= 2
BEGIN
	SET @Feb = @Feb + 1
	SET @Mar = @Mar + 1
END

IF @Month = 3
BEGIN
	SELECT count(*) from Projects WHERE StartDate BETWEEN '2011-11-11' AND '2014-11-11'
END

IF @Month >= 3
BEGIN
	SET @Mar = @Mar + 1
END


IF @Month = 4
BEGIN
	SELECT  @Jan as Jan, @Feb as Feb, @Mar as Mar

END

IF @Month = 5
BEGIN

SELECT TOP 1
   DAY(@DateIn)       AS "DAY()" 
  ,MONTH(@DateIn)     AS "MONTH()" 
  ,YEAR(@DateIn)      AS "YEAR()"   

END

IF @Month = 6
BEGIN
	SELECT  @EndDate as  EndDate
END

END;
