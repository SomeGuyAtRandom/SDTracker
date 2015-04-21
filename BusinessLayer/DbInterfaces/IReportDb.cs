using System;
using System.Collections.Generic;
using BusinessLayer.Models.Reports;
using BusinessLayer.Models;

namespace BusinessLayer.DbInterfaces
{
    public interface IReportDb
    {
        IEnumerable<YearSummaryByMonthByJobType> rptYearSummaryByMonthByJobType(String columnName, DateTime dateIn);

        IEnumerable<SummaryReport> rptSummaryReport(int CD, int HeadEngineerId, int DesignEngineerId, int Month, int Year, string ColumnName);

        IEnumerable<DetailSummary> rptDetailSummary(String columnName, DateTime dateIn, int jobTypeId);

        IEnumerable<Engineer> HeadEngineers();

        IEnumerable<Engineer> DesignEngineers();
    }
}
