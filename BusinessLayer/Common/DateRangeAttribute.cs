using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Common
{
    public class DateRangeAttribute : RangeAttribute
    {
        public DateRangeAttribute(string minDate)
            : base(typeof(DateTime), minDate, DateTime.Now.AddMonths(6).ToShortDateString())
        {
 
        }
    }
}
