using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Reports
/// </summary>
public class Reports
{
    public Reports()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static string AddReport(string UserID, string ReportName,string Password)
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
                    myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_Reports");
                    myParams[0].Value = 1;
                    myParams[2].Value = CustomEncryption.Decrypt(UserID);
                    myParams[3].Value = System.Guid.NewGuid().ToString();
                    myParams[4].Value = ReportName;
                    myParams[5].Value = System.DateTime.Now;
                    myParams[7].Value = Password;
                    SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, "sp_Reports", myParams);
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
    public static string EditReport(int ReportID, string ReportName, string ReportPassword)
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
                    myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_Reports");
                    myParams[0].Value = 2;
                    myParams[1].Value = ReportID;
                    myParams[4].Value = ReportName;
                    myParams[6].Value = System.DateTime.Now;
                    myParams[7].Value = ReportPassword;
                    SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, "sp_Reports", myParams);
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
    public static DataTable GetReportByReportID(int ReportID)
    {
        DataTable dtAllReports = new DataTable();
        try
        {
            SqlParameter[] myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_Reports");
            myParams[0].Value = 4;
            myParams[1].Value = ReportID;
            dtAllReports = SqlHelper.ExecuteDataset(BVisionConfigurationManager.GetConnectionString(), CommandType.StoredProcedure, "sp_Reports", myParams).Tables[0];

            return dtAllReports;
        }
        catch (Exception ex)
        {
            return dtAllReports;
        }
    }
    public static DataTable GetAllReports()
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
    public static DataTable GetAllReportsByUserId(int UserId)
    {
        DataTable dtReports = new DataTable();
        try
        {
            SqlParameter[] myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_Reports");
            myParams[0].Value = 5;
            myParams[2].Value = UserId;
            myParams[9].Value = "";
            dtReports = SqlHelper.ExecuteDataset(BVisionConfigurationManager.GetConnectionString(), CommandType.StoredProcedure, "sp_Reports", myParams).Tables[0];

            return dtReports;
        }
        catch (Exception ex)
        {
            return dtReports;
        }
    }
    public static string AddDuplicateReport(string UserID, string ReportName, string Password, string ReportGUID,string VersionGUID)
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
                    myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_Reports");
                    myParams[0].Value = 6;
                    myParams[2].Value = CustomEncryption.Decrypt(UserID);
                    myParams[3].Value = ReportGUID;
                    myParams[4].Value = ReportName;
                    myParams[5].Value = System.DateTime.Now;
                    myParams[7].Value = Password;
                    myParams[8].Value = VersionGUID;
                    SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, "sp_Reports", myParams);
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