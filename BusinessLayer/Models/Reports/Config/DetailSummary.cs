using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;


namespace BusinessLayer.Models.Reports
{
    [MetadataType(typeof(ConfigDetailSummary))]
    public partial class DetailSummary
    {
    }

    public class ConfigDetailSummary
    {
        [DisplayName("Location")]
        public string Location { get; set; }

        [DisplayName("Remark")]
        public string CurrRemark { get; set; }

        [DisplayName("Proj")]
        public string ProjNo { get; set; }

        [DisplayName("Type")]
        public string JobCode { get; set; }

        [DisplayName("Head Engineer")]
        public string HeadEngineerInitials { get; set; }

        [DisplayName("Design Engineer")]
        public string DesignEngineerInitials { get; set; }

        [DisplayName("Start")]
        public DateTime StartDate { get; set; }
    }
}
