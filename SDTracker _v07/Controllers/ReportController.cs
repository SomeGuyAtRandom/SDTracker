using BusinessLayer.DbImp;
using BusinessLayer.Models.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;

using BusinessLayer.DbInterfaces;

namespace SDTracker.Controllers
{
    public class ReportController : Controller
    {
        private IReportDb dbRepo;
        public ReportController(IReportDb dbRepo)
        {
            this.dbRepo = dbRepo;
        }


        private List<SelectListItem> GetMonthSelectCbo(int selectedId)
        {
            List<SelectListItem> monthCbo = new List<SelectListItem>();
            DateTime seed = DateTime.Parse("01-01-2001");
            for (int i = 0; i < 12; i++)
            {
                SelectListItem item;
                item = new SelectListItem
                {
                    Text = seed.ToString("MMM"),
                    Value = "" + seed.Month,
                    Selected = (selectedId == seed.Month)

                };
                monthCbo.Add(item);
                seed = seed.AddMonths(1);
            }
            return monthCbo;
        }

        private List<SelectListItem> GetYearSelectCbo(int selectedId)
        {
            List<SelectListItem> yearCbo = new List<SelectListItem>();
            DateTime seed = DateTime.Parse("01-01-2001");
            DateTime cur = DateTime.Now;

            while (cur.CompareTo(seed) > 0)
            {
                SelectListItem item;
                item = new SelectListItem
                {
                    Text = cur.ToString("yyyy"),
                    Value = cur.ToString("yyyy"),
                    Selected = (selectedId == cur.Year)

                };
                yearCbo.Add(item);
                cur = cur.AddYears(-1);
            }
            return yearCbo;

        }

        private List<SelectListItem> GetColumnNameCbo(string selectedId)
        {
            List<SelectListItem> fieldCbo = new List<SelectListItem>();
            DateTime seed = DateTime.Parse("01-01-2001");
            DateTime cur = DateTime.Now;

            SelectListItem[] items = new SelectListItem[3];


            items[0] = new SelectListItem
            {
                Text = "StartDate",
                Value = "StartDate",
                Selected = (selectedId == "StartDate")

            };

            items[1] = new SelectListItem
            {
                Text = "DateAssigned",
                Value = "DateAssigned",
                Selected = (selectedId == "DateAssigned")

            };

            items[2] = new SelectListItem
            {
                Text = "DateUpdated",
                Value = "DateUpdated",
                Selected = (selectedId == "DateUpdated")

            };

            foreach (SelectListItem item in items)
            {
                fieldCbo.Add(item);
            }
            


            return fieldCbo;

        }
        
        public ActionResult YearSummaryByMonthByJobType()
        {

            List<YearSummaryByMonthByJobType> rpt = dbRepo.rptYearSummaryByMonthByJobType("", DateTime.Now).ToList();
            ViewBag.MonthSelectCbo = GetMonthSelectCbo(0);
            ViewBag.YearSelectCbo = GetYearSelectCbo(0);
            ViewBag.ColumnNameCbo = GetColumnNameCbo("");

            return View("YearSummaryByMonthByJobType", rpt);
        }

        [HttpPost]
        public ActionResult YearSummaryByMonthByJobType(string MonthSelectCbo, string YearSelectCbo, string ColumnNameCbo)
        {
            int iMonth = 0;
            int iYear = 0;

            try { iMonth = Int32.Parse(MonthSelectCbo); }
            catch { }

            try { iYear = Int32.Parse(YearSelectCbo); }
            catch { }

            DateTime dateIn = DateTime.Now;

            try { dateIn = DateTime.Parse(MonthSelectCbo + "-01-" + YearSelectCbo); }
            catch { }

            ViewBag.MonthSelectCbo = GetMonthSelectCbo(iMonth);
            ViewBag.YearSelectCbo = GetYearSelectCbo(iYear);
            ViewBag.ColumnNameCbo = GetColumnNameCbo(ColumnNameCbo);

           
            List<YearSummaryByMonthByJobType> rpt;
            if (iMonth == 0 || iYear == 0)
            {
                rpt = dbRepo.rptYearSummaryByMonthByJobType("", DateTime.Now).ToList();

            }
            else 
            {
                rpt = dbRepo.rptYearSummaryByMonthByJobType(ColumnNameCbo, dateIn).ToList();
            }
            
            return View(rpt);
        }

        public ActionResult DetailSummary(int? page, string reportName, string columnName, string dateIn, string jobTypeId)
        {
            

            DateTime d = DateTime.Parse(dateIn);
            int jId = Int32.Parse(jobTypeId);
            List<DetailSummary> rpt = dbRepo.rptDetailSummary(columnName, d, jId).ToList();
            return View("DetailSummary", rpt.ToPagedList(page ?? 1, 5));
 
        }

    }
}
