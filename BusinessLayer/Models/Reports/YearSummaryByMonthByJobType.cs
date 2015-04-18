using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLayer.Models.Reports
{
    public partial class YearSummaryByMonthByJobType
    {
        public YearSummaryByMonthByJobType()
        {
            columns = new Cell[12];
            for (int i = 0; i < 12; i++)
            {
                columns[i] = new Cell();
 
            }
 
        }
        public string JobName { get; set; }
        public string JobCode { get; set; }

        public Cell[] columns { get; set; }

        public Cell SumCell { get; set; }

       

    }

    public class Cell 
    {
        public Cell()
        {
 
        }
        public Cell(int Total, DateTime StartDate, int JobTypeId)
        {
            this.Total = Total;
            this.StartDate = StartDate;
            this.JobTypeId = JobTypeId;
        }
        public int Total { get; set; }
        public DateTime StartDate { get; set; }
        public int JobTypeId { get; set; }

        public string reportName { get { return "YearSummaryByMonthByJobType"; } }
        public string columnName { get; set; }
 
    }
}
