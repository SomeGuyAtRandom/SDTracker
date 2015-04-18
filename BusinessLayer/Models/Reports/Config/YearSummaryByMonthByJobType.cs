using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;



namespace BusinessLayer.Models.Reports
{

    [MetadataType(typeof(ConfigYearSummaryByMonthByJobType))]
    public partial class YearSummaryByMonthByJobType
    {
    }
    public class ConfigYearSummaryByMonthByJobType
    {
        

        [DisplayName("Total")]
        public Cell SumCell { get; set; }


    }


    
}
