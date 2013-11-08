using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Reports
/// </summary>
public class Versions
{
    public Versions()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static string AddReportVersion(string ReportGUID, string VersionGUID)
    {
        string Status = "";
        SqlParameter[] myParams = null;
        using (SqlConnection connection = new SqlConnection(BVisionConfigurationManager.GetConnectionString()))
        {
            connection.Open();
            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_Versions");
                    myParams[0].Value = 4;
                    myParams[3].Value = VersionGUID;
                    myParams[9].Value = System.DateTime.Now;
                    myParams[13].Value = ReportGUID;
                    SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, "sp_Versions", myParams);
                    transaction.Commit();
                    Status = "Done";
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Status = ex.Message;
                }
            }
            if (connection.State != ConnectionState.Closed)
            {
                connection.Close();
            }
        }
        return Status;
    }
    public static DataTable GetAllReportVersions()
    {
        DataTable dtAllReports = new DataTable();
        try
        {
            SqlParameter[] myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_Reports");
            myParams[0].Value = 0;
            dtAllReports = SqlHelper.ExecuteDataset(BVisionConfigurationManager.GetConnectionString(), CommandType.StoredProcedure, "sp_Reports", myParams).Tables[0];

            return dtAllReports;
        }
        catch (Exception ex)
        {
            return dtAllReports;
        }
    }
    public static string SavePagesHTML(string VersionGUID, string PagesHTML)
    {
        string Status = "";
        SqlParameter[] myParams = null;
        using (SqlConnection connection = new SqlConnection(BVisionConfigurationManager.GetConnectionString()))
        {
            connection.Open();
            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_Versions");
                    myParams[0].Value = 6;
                    myParams[3].Value = VersionGUID;
                    myParams[5].Value = PagesHTML;
                    myParams[10].Value = System.DateTime.Now;

                    SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, "sp_Versions", myParams);
                    transaction.Commit();
                    Status = "Done";
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Status = ex.Message;
                }
            }
            if (connection.State != ConnectionState.Closed)
            {
                connection.Close();
            }
        }
        return Status;
    }
    public static string LoadPagesHTML(string ReportGUID,string VersionGUID)
    {
        string Status = "";
        SqlParameter[] myParams = null;
        using (SqlConnection connection = new SqlConnection(BVisionConfigurationManager.GetConnectionString()))
        {
            connection.Open();
            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_Versions");
                    myParams[0].Value = 7;
                    myParams[3].Value = VersionGUID;
                    myParams[13].Value = ReportGUID;

                    DataTable dtPageHTML = SqlHelper.ExecuteDataset(transaction, CommandType.StoredProcedure, "sp_Versions", myParams).Tables[0];
                    transaction.Commit();
                    if (dtPageHTML.Rows.Count > 0)
                    { Status = Convert.ToString(dtPageHTML.Rows[0]["PagesHTML"]); }
                    else
                    { Status = "Done"; }

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Status = ex.Message;
                }
            }
            if (connection.State != ConnectionState.Closed)
            {
                connection.Close();
            }
        }
        return Status;
    }
    public static DataTable GetReportByReportAndVersionGUID(string ReportGUID, string VersionGUID)
    {
        DataTable dtReport = new DataTable();
        try
        {
            SqlParameter[] myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_Versions");
            myParams[0].Value = 8;
            myParams[3].Value = VersionGUID;
            myParams[13].Value = ReportGUID;
            dtReport = SqlHelper.ExecuteDataset(BVisionConfigurationManager.GetConnectionString(), CommandType.StoredProcedure, "sp_Versions", myParams).Tables[0];

            return dtReport;
        }
        catch (Exception ex)
        {
            return dtReport;
        }
    }
    public static string LockReportVersion(string ReportGUID, string VersionGUID)
    {
        string Status = "";
        SqlParameter[] myParams = null;
        using (SqlConnection connection = new SqlConnection(BVisionConfigurationManager.GetConnectionString()))
        {
            connection.Open();
            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_Versions");
                    myParams[0].Value = 3;
                    myParams[3].Value = VersionGUID;
                    myParams[7].Value = 1;
                    myParams[12].Value = System.DateTime.Now;
                    myParams[13].Value = ReportGUID;
                    SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, "sp_Versions", myParams);
                    transaction.Commit();
                    Status = "Done";
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Status = ex.Message;
                }
            }
            if (connection.State != ConnectionState.Closed)
            {
                connection.Close();
            }
        }
        return Status;
    }
    public static string DeleteReportVersion(string ReportGUID, string VersionGUID)
    {
        string Status = "";
        SqlParameter[] myParams = null;
        using (SqlConnection connection = new SqlConnection(BVisionConfigurationManager.GetConnectionString()))
        {
            connection.Open();
            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_Versions");
                    myParams[0].Value = 9;
                    myParams[3].Value = VersionGUID;
                    myParams[8].Value = 1;
                    myParams[11].Value = System.DateTime.Now;
                    myParams[13].Value = ReportGUID;
                    SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, "sp_Versions", myParams);
                    transaction.Commit();
                    Status = "Done";
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Status = ex.Message;
                }
            }
            if (connection.State != ConnectionState.Closed)
            {
                connection.Close();
            }
        }
        return Status;
    }
}