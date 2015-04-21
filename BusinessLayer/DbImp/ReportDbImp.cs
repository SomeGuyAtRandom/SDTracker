using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using BusinessLayer.Models.Reports;
using System.Data.Entity;
using System.Configuration;
using BusinessLayer.Models;

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

        public IEnumerable<SummaryReport> rptSummaryReport(int CD, int HeadEngineerId, int DesignEngineerId, int Month, int Year, string ColumnName)
        {

            List<SummaryReport> rows = new List<SummaryReport>();

            DateTime dateIn = new DateTime();

            try { dateIn = new DateTime(Year, Month, 1); }
            catch { return rows; }


            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("rptSummaryReport", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if(HeadEngineerId !=0 )
                { 
                    SqlParameter pHeadEngineerId = new SqlParameter();
                    pHeadEngineerId.ParameterName = "@HeadEngineerId";
                    pHeadEngineerId.Value = HeadEngineerId;
                    cmd.Parameters.Add(pHeadEngineerId);
                }

                if (DesignEngineerId != 0)
                { 
                SqlParameter pDesignEngineerId = new SqlParameter();
                pDesignEngineerId.ParameterName = "@DesignEngineerId";
                pDesignEngineerId.Value = DesignEngineerId;
                cmd.Parameters.Add(pDesignEngineerId);
                }

                if (CD != 0)
                {
                    SqlParameter pCD = new SqlParameter();
                    pCD.ParameterName = "@CD";
                    pCD.Value = CD;
                    cmd.Parameters.Add(pCD);
                }

                


                SqlParameter pMonth = new SqlParameter();
                pMonth.ParameterName = "@MonthId";
                pMonth.Value = Month;
                cmd.Parameters.Add(pMonth);

                SqlParameter pYear = new SqlParameter();
                pYear.ParameterName = "@YearId";
                pYear.Value = Year;
                cmd.Parameters.Add(pYear);

                SqlParameter pColumnName = new SqlParameter();
                pColumnName.ParameterName = "@columnName";
                pColumnName.Value = ColumnName;
                cmd.Parameters.Add(pColumnName);


                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    rows.Add(SetRowSummaryReport(ColumnName, CD, HeadEngineerId, DesignEngineerId, rdr, Month, Year));
                }
            }

            return rows;

        }

        public IEnumerable<Engineer> HeadEngineers()
        {
            List<Engineer> engineers = new List<Engineer>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetAllHeadEngineers", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    engineers.Add(setGetUsersToAdmin(rdr));
                }
            }
            return engineers;
        }

        public IEnumerable<Engineer> DesignEngineers()
        {
            List<Engineer> engineers = new List<Engineer>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetAllDesignEngineers", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    engineers.Add(setGetUsersToAdmin(rdr));
                }
            }
            return engineers;
        }

        private Engineer setGetUsersToAdmin(SqlDataReader rdr)
        {
            Engineer engineer = new Engineer();

            engineer.Id = Convert.ToInt32(rdr["Id"]);
            engineer.FirstName = rdr["FirstName"].ToString();
            engineer.LastName = rdr["LastName"].ToString();
            engineer.Email = rdr["Email"].ToString();
            //user.Initials = rdr["Initials"].ToString();
            engineer.UserName = rdr["UserName"].ToString();

            //if (!(rdr["Phone"] is DBNull))
            //{
            //     user.Phone = rdr["Phone"].ToString();
            //}

            //user.DateCreated = Convert.ToDateTime(rdr["DateCreated"]);
            //user.DateUpdated = Convert.ToDateTime(rdr["DateUpdated"]);

            return engineer;

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

        private SummaryReport SetRowSummaryReport(String columnName, int CD, int HeadEngineerId, int DesignEngineerId, SqlDataReader rdr, int Month, int Year)
        {
            SummaryReport rowItem = new SummaryReport();

            rowItem.JobName = rdr.GetString(rdr.GetOrdinal("JobName"));
            rowItem.JobCode = rdr.GetString(rdr.GetOrdinal("JobCode"));
            int JobTypeId = Convert.ToInt32(rdr["JobTypeId"]);
            DateTime StartDate = new DateTime(Year, Month, 1);


            for (int i = 0; i < 12; i++)
            {
                string month = StartDate.ToString("MMM");

                if (month.Equals("Jul") && JobTypeId==1)
                {
                    month = "Jul";
 
                }
                int total = Convert.ToInt32(rdr[month + "Total"]);

                // 
                DateTime sDate = rdr.GetDateTime(rdr.GetOrdinal(month + "Date"));
                CellSummaryReport cell = 
                new CellSummaryReport()
                {
                    CD = CD,
                    columnName = columnName,
                    DesignEngineerId = DesignEngineerId,
                    HeadEngineerId = HeadEngineerId,
                    JobTypeId = JobTypeId,
                    StartDate = sDate,
                    Month = Month,
                    Year = Year,
                    Total = total
                };
                switch (i)
                {
                    case 0 : { rowItem.column0 = cell; break; }
                    case 1 : { rowItem.column1 = cell; break; }
                    case 2 : { rowItem.column2 = cell; break; }
                    case 3 : { rowItem.column3 = cell; break; }
                    case 4 : { rowItem.column4 = cell; break; }
                    case 5 : { rowItem.column5 = cell; break; }
                    case 6 : { rowItem.column6 = cell; break; }
                    case 7 : { rowItem.column7 = cell; break; }
                    case 8 : { rowItem.column8 = cell; break; }
                    case 9 : { rowItem.column9 = cell; break; }
                    case 10 : { rowItem.column10 = cell; break; }
                    case 11: { rowItem.column11 = cell; break; }
                }
                

                StartDate = StartDate.AddMonths(1);

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
