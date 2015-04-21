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
            //columns = new CellSummaryReport[12];
            //for (int i = 0; i < 12; i++)
            //{
            //    columns[i] = new CellSummaryReport();
 
            //}
 
        }
        public string JobName { get; set; }
        public string JobCode { get; set; }

        public int CD { get; set; }
        public int HeadEngineerId { get; set; }
        public int DesignEngineerId { get; set; }

        public int Total { get; set; }

        public int Month { get; set; }
        public int Year { get; set; }
        public DateTime StartDate { get; set; }

        public CellSummaryReport column0 { get; set; }
        public CellSummaryReport column1 { get; set; }
        public CellSummaryReport column2 { get; set; }
        public CellSummaryReport column3 { get; set; }
        public CellSummaryReport column4 { get; set; }
        public CellSummaryReport column5 { get; set; }
        public CellSummaryReport column6 { get; set; }
        public CellSummaryReport column7 { get; set; }
        public CellSummaryReport column8 { get; set; }
        public CellSummaryReport column9 { get; set; }
        public CellSummaryReport column10 { get; set; }
        public CellSummaryReport column11 { get; set; }
        
        
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
        public int Total { get; set; }
        public DateTime StartDate { get; set; }

        public int Month { get; set; }
        public int Year { get; set; }

        public int JobTypeId { get; set; }

        public int CD { get; set; }
        public int HeadEngineerId { get; set; }
        public int DesignEngineerId { get; set; }


        public string reportName { get { return "SummaryReport"; } }
        public string columnName { get; set; }

    }

}
