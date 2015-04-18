using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLayer.Models.Reports
{
    public partial class DetailSummary
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public string CurrRemark { get; set; }
        public string ProjNo { get; set; }

        public string JobCode { get; set; }
        public string HeadEngineerInitials { get; set; }
        public string DesignEngineerInitials { get; set; }
        public DateTime StartDate { get; set; }

    }
}
