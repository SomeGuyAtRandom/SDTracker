using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLayer.Models.Reports
{
    [MetadataType(typeof(SummaryReportConfig))]
    public class SummaryReport
    {
        public SummaryReport()
        {
            columns = new CellSummaryReport[12];
            for (int i = 0; i < 12; i++)
            {
                columns[i] = new CellSummaryReport();
 
            }
 
        }
        public string JobName { get; set; }
        public string JobCode { get; set; }

        public int CD { get; set; }
        public int HeadEngineerId { get; set; }
        public int DesignEngineerId { get; set; }


        public CellSummaryReport[] columns { get; set; }

        public CellSummaryReport SumCell { get; set; }
    }
    public class SummaryReportConfig
    {
        [DisplayName("Total")]
        public CellSummaryReport SumCell { get; set; }

    }

    public class CellSummaryReport
    {
        public CellSummaryReport()
        {

        }
        public CellSummaryReport(int Total, DateTime StartDate, int JobTypeId, int CD, int HeadEngineerId, int DesignEngineerId)
        {
            this.Total = Total;
            this.StartDate = StartDate;
            this.JobTypeId = JobTypeId;

            this.CD = CD;
            this.HeadEngineerId = HeadEngineerId;
            this.DesignEngineerId = DesignEngineerId;

        }
        public int Total { get; set; }
        public DateTime StartDate { get; set; }
        
        public int JobTypeId { get; set; }

        public int CD { get; set; }
        public int HeadEngineerId { get; set; }
        public int DesignEngineerId { get; set; }


        public string reportName { get { return "SummaryReport"; } }
        public string columnName { get; set; }

    }

}
