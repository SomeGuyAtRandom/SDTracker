using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using BusinessLayer.Models.Reports;
using System.Data.Entity;
using System.Configuration;


namespace BusinessLayer.DbImp
{
    public class ReportDbImp : DbContext 
    {
        protected String connectionString = ConfigurationManager.ConnectionStrings["SDTrackerContext"].ConnectionString;

        public IEnumerable<YearSummaryByMonthByJobType> rptYearSummaryByMonthByJobType(String columnName, DateTime dateIn)
        {
            List<YearSummaryByMonthByJobType> rows = new List<YearSummaryByMonthByJobType>();
            if (columnName == "") { return rows; }
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("rptYearSummaryByMonthByJobType", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter pDateIn = new SqlParameter();
                pDateIn.ParameterName = "@dateIn";
                pDateIn.Value = dateIn;
                cmd.Parameters.Add(pDateIn);

                SqlParameter pColumnName = new SqlParameter();
                pColumnName.ParameterName = "@columnName";
                pColumnName.Value = columnName;
                cmd.Parameters.Add(pColumnName);


                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    rows.Add(SetRowYearSummaryByMonthByJobType(columnName, rdr, dateIn));
                }
            }

            return rows;

        }

        public IEnumerable<DetailSummary> rptDetailSummary(String columnName, DateTime dateIn, int jobTypeId)
        {
            List<DetailSummary> rows = new List<DetailSummary>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("rptDetailSummary", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter pColumnName = new SqlParameter();
                pColumnName.ParameterName = "@columnName";
                pColumnName.Value = columnName;
                cmd.Parameters.Add(pColumnName);

                SqlParameter pDateIn = new SqlParameter();
                pDateIn.ParameterName = "@dateIn";
                pDateIn.Value = dateIn;
                cmd.Parameters.Add(pDateIn);
                //@jobTypeId

                SqlParameter pJobTypeId = new SqlParameter();
                pJobTypeId.ParameterName = "@jobTypeId";
                pJobTypeId.Value = jobTypeId;
                cmd.Parameters.Add(pJobTypeId);


                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    rows.Add(SetRowDetailSummary(rdr, dateIn));
                }
            }

            return rows;

        }

        private DetailSummary SetRowDetailSummary(SqlDataReader rdr, DateTime dateIn)
        {
            DetailSummary rowItem = new DetailSummary();

            rowItem.Id = Convert.ToInt32(rdr["ProjectId"]);

            if (!(rdr["Location"] is DBNull))
            {
                rowItem.Location = rdr["Location"].ToString();
            }

            if (!(rdr["CurrRemark"] is DBNull))
            {
                rowItem.CurrRemark = rdr["CurrRemark"].ToString();
            }

            if (!(rdr["ProjNo"] is DBNull))
            {
                rowItem.ProjNo = rdr["ProjNo"].ToString();
            }

            if (!(rdr["JobCode"] is DBNull))
            {
                rowItem.JobCode = rdr["JobCode"].ToString();
            }


            if (!(rdr["HeadEngineerInitials"] is DBNull))
            {
                rowItem.HeadEngineerInitials = rdr["HeadEngineerInitials"].ToString();
            }

            if (!(rdr["DesignEngineerInitials"] is DBNull))
            {
                rowItem.DesignEngineerInitials = rdr["DesignEngineerInitials"].ToString();
            }

            if (!(rdr["StartDate"] is DBNull))
            {
                rowItem.StartDate = rdr.GetDateTime(rdr.GetOrdinal("StartDate"));
            }



            return rowItem;

        }

        private YearSummaryByMonthByJobType SetRowYearSummaryByMonthByJobType(String columnName, SqlDataReader rdr, DateTime dateIn)
        {
            YearSummaryByMonthByJobType rowItem = new YearSummaryByMonthByJobType();


            rowItem.JobName = rdr.GetString(rdr.GetOrdinal("JobName"));
            rowItem.JobCode = rdr.GetString(rdr.GetOrdinal("JobCode"));

            rowItem.columns = new Cell[12];

            DateTime StartDate = dateIn.AddYears(-1);

            string datestring = "" + StartDate.ToString("MM");
            datestring += "-01";
            datestring += "-" + StartDate.ToString("yyyy");
            StartDate = DateTime.Parse(datestring);
            int JobTypeId = Convert.ToInt32(rdr["JobTypeId"]);

            for (int i = 0; i < 12; i++)
            {
                string month = StartDate.ToString("MMM");
                int total = Convert.ToInt32(rdr[month + "Total"]);
                DateTime sDate = rdr.GetDateTime(rdr.GetOrdinal(month + "Date"));

                rowItem.columns[i] = new Cell(total, sDate, JobTypeId);
                rowItem.columns[i].columnName = columnName;
                StartDate = StartDate.AddMonths(1);

            }
            rowItem.SumCell = new Cell(Convert.ToInt32(rdr["SumTotal"]), dateIn, JobTypeId);

            return rowItem;

        }

    }
}
