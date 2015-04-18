using System;
using BusinessLayer.DbImp;
using BusinessLayer.Models.Reports;
using System.Collections.Generic;
using BusinessLayer.DbInterfaces;

namespace BusinessLayer.MockDbs
{
    public class ReportDb : IReportDb
    {
        private ReportDbImp context = new ReportDbImp();

        public IEnumerable<YearSummaryByMonthByJobType> rptYearSummaryByMonthByJobType(String columnName, DateTime dateIn)
        {
            return context.rptYearSummaryByMonthByJobType(columnName, dateIn);
        }

        public IEnumerable<DetailSummary> rptDetailSummary(String columnName, DateTime dateIn, int jobTypeId)
        {
            return context.rptDetailSummary(columnName, dateIn, jobTypeId);
        }
    }
}
