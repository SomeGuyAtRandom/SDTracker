using System;
using System.Collections.Generic;
using BusinessLayer.Models.Reports;


namespace BusinessLayer.DbInterfaces
{
    public interface IReportDb
    {
        IEnumerable<YearSummaryByMonthByJobType> rptYearSummaryByMonthByJobType(String columnName, DateTime dateIn);
        IEnumerable<DetailSummary> rptDetailSummary(String columnName, DateTime dateIn, int jobTypeId);

    }
}
