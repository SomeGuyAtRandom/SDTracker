USE [SignalDB];
GO

IF OBJECT_ID('rptYearSummaryByMonthByJobType', 'P') IS NOT NULL
DROP PROCEDURE rptYearSummaryByMonthByJobType

GO

CREATE PROC rptYearSummaryByMonthByJobType(@columnName nvarchar(100), @dateIn DateTime)

AS
BEGIN
DECLARE 
	@ReturnTable TABLE 
	(
	   JobTypeId int,
	   JobName nvarchar(255),
	   JobCode nvarchar(255),
	   JulTotal int,
	   AugTotal int,
	   SepTotal int,
	   OctTotal int,
	   NovTotal int,
	   DecTotal int,
	   JanTotal int,
	   FebTotal int,
	   MarTotal int,
	   AprTotal int,
	   MayTotal int,
	   JunTotal int,
	   SumTotal int,
  	   JulDate DateTime,
	   AugDate DateTime,
	   SepDate DateTime,
	   OctDate DateTime,
	   NovDate DateTime,
	   DecDate DateTime,
	   JanDate DateTime,
	   FebDate DateTime,
	   MarDate DateTime,
	   AprDate DateTime,
	   MayDate DateTime,
	   JunDate DateTime

	)
DECLARE
	@StartDate DateTime = DATEADD(mm, DATEDIFF(mm, 0, DateAdd(Month, -12, @dateIn)) - 1, 0),
	@JobTypeId int =0,
	@JobName nvarchar(255),
	@JobCode nvarchar(255),
	@Jan int = 0,
	@Feb int = 0,
	@Mar int = 0,
	@Apr int = 0,
	@May int = 0,
	@Jun int = 0,
	@Jul int = 0,
	@Aug int = 0,
	@Sep int = 0,
	@Oct int = 0,
	@Nov int = 0,
	@Dec int = 0,

	@JanD datetime = @dateIn,
	@FebD datetime = @dateIn,
	@MarD datetime = @dateIn,
	@AprD datetime = @dateIn,
	@MayD datetime = @dateIn,
	@JunD datetime = @dateIn,
	@JulD datetime = @dateIn,
	@AugD datetime = @dateIn,
	@SepD datetime = @dateIn,
	@OctD datetime = @dateIn,
	@NovD datetime = @dateIn,
	@DecD datetime = @dateIn,


	@MonthSelect int = 0,
	@RowCount int,
	@Total int = 0,
	@Month int = 0

	DECLARE idxCursor CURSOR FOR SELECT Id, Name, JobCode FROM JobTypes
	OPEN idxCursor

	FETCH NEXT FROM idxCursor INTO @JobTypeId, @JobName, @JobCode
	WHILE @@FETCH_STATUS = 0 
	BEGIN

		WHILE @Month < 12
		BEGIN
			SET @StartDate = DateAdd(Month, 1, @StartDate)	
			SELECT @MonthSelect = MONTH(@StartDate)

			SELECT @RowCount = COUNT(*) 
			FROM Projects 
			WHERE 
				CASE @columnName
					WHEN 'DateAssigned' THEN DateAssigned
					WHEN 'StartDate' THEN StartDate
					WHEN 'DateCreated' THEN DateCreated
					WHEN 'DateUpdated' THEN DateUpdated
					ELSE NULL
				END 
			>= @StartDate AND
	
				CASE @columnName
				WHEN 'DateAssigned' THEN DateAssigned
				WHEN 'StartDate' THEN StartDate
				WHEN 'DateCreated' THEN DateCreated
				WHEN 'DateUpdated' THEN DateUpdated
				ELSE NULL
			END
			< DateAdd(Month, 1, @StartDate)	AND JobTypeId = @JobTypeId

			IF @MonthSelect = 1
			BEGIN
				SELECT @Jan = @RowCount
				SELECT @JanD = @StartDate
			END

			IF @MonthSelect = 2
			BEGIN
				SELECT @Feb = @RowCount
				SELECT @FebD = @StartDate
			END

			IF @MonthSelect = 3
			BEGIN
				SELECT @Mar = @RowCount
				SELECT @MarD = @StartDate
			END

			IF @MonthSelect = 4
			BEGIN
				SELECT @Apr = @RowCount
				SELECT @AprD = @StartDate
			END

			IF @MonthSelect = 5
			BEGIN
				SELECT @May = @RowCount
				SELECT @MayD = @StartDate
			END

			IF @MonthSelect = 6
			BEGIN
				SELECT @Jun = @RowCount
				SELECT @JunD = @StartDate

			END

			IF @MonthSelect = 7
			BEGIN
				SELECT @Jul = @RowCount
				SELECT @JulD = @StartDate
			END

			IF @MonthSelect = 8
			BEGIN
				SELECT @Aug = @RowCount
				SELECT @AugD = @StartDate
			END

			IF @MonthSelect = 9
			BEGIN
				SELECT @Sep = @RowCount
				SELECT @SepD = @StartDate
			END

			IF @MonthSelect = 10
			BEGIN
				SELECT @Oct = @RowCount
				SELECT @OctD = @StartDate
			END

			IF @MonthSelect = 11
			BEGIN
				SELECT @Nov = @RowCount
				SELECT @NovD = @StartDate
			END

			IF @MonthSelect = 12
			BEGIN
				SELECT @Dec = @RowCount
				SELECT @DecD = @StartDate
			END
			
			SET @Total = @Total + @RowCount
			SET @Month = @Month + 1
		
		END


		INSERT INTO @ReturnTable 
			        (JobTypeId, JobName, JobCode,  JulTotal,AugTotal,SepTotal,OctTotal,NovTotal,DecTotal,JanTotal,FebTotal,MarTotal,AprTotal,MayTotal,JunTotal,SumTotal,
					                               JulDate, AugDate, SepDate, OctDate, NovDate, DecDate, JanDate, FebDate, MarDate, AprDate, MayDate, JunDate
					
					)
			SELECT  @JobTypeId,@JobName,@JobCode, @Jul,    @Aug,    @Sep,    @Oct,    @Nov,    @Dec,     @Jan,   @Feb,    @Mar,    @Apr,    @May,    @Jun,    @Total,
								                  @JulD,   @AugD,   @SepD,   @OctD,   @NovD,   @DecD,    @JanD,  @FebD,   @MarD,   @AprD,   @MayD,   @JunD;


		SET @Month = 0
		SET @StartDate = DATEADD(mm, DATEDIFF(mm, 0, DateAdd(Month, -12, @dateIn)) - 1, 0);

		SET @Jul= 0
		SET @Aug= 0
		SET @Sep= 0 
		SET @Oct= 0
		SET @Nov= 0
		SET @Dec= 0
		SET @Jan= 0
		SET @Feb= 0
		SET @Mar= 0
		SET @Apr= 0
		SET @May= 0
		SET @Jun= 0
		SET @Total= 0

		FETCH NEXT FROM idxCursor INTO @JobTypeId, @JobName, @JobCode
	END
	CLOSE idxCursor    
	DEALLOCATE idxCursor

	SELECT * FROM 	@ReturnTable
END

GO