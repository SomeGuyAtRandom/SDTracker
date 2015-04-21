using System;
using BusinessLayer.DbImp;
using BusinessLayer.Models.Reports;
using System.Collections.Generic;
using BusinessLayer.DbInterfaces;
using BusinessLayer.Models;

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

        public IEnumerable<Engineer> HeadEngineers()
        {
            return context.HeadEngineers();
        }

        public IEnumerable<Engineer> DesignEngineers()
        {
            return context.DesignEngineers();
        }

        public IEnumerable<SummaryReport> rptSummaryReport(int CD, int HeadEngineerId, int DesignEngineerId, int Month, int Year, string ColumnName)
        {
            return context.rptSummaryReport( CD,  HeadEngineerId,  DesignEngineerId,  Month,  Year,  ColumnName); 
        }
    }
}
