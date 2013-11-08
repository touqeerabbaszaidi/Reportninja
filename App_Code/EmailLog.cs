using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EmailLog
/// </summary>
public class EmailLog
{
    public EmailLog()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static string AddEmailLog(string GUID, string ReportGUID, string VersionGUID, int SenderId, string SentTo, bool AllowEditing)
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
                    myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_EmailLog");
                    myParams[0].Value = 1;
                    myParams[2].Value = GUID;
                    myParams[10].Value = ReportGUID;
                    myParams[11].Value = VersionGUID;
                    myParams[5].Value = SenderId;
                    myParams[6].Value = SentTo;
                    myParams[7].Value = 0;
                    myParams[8].Value = 0;
                    myParams[9].Value = System.DateTime.Now;
                    myParams[12].Value = AllowEditing;
                    SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, "sp_EmailLog", myParams);
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
    public static DataTable IncrementViews(string GUID)
    {
        string Status = "";
        DataTable dtEmailLog=new DataTable();
        SqlParameter[] myParams = null;
        using (SqlConnection connection = new SqlConnection(BVisionConfigurationManager.GetConnectionString()))
        {
            connection.Open();
            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_EmailLog");
                    myParams[0].Value = 2;
                    myParams[2].Value = GUID;
                    dtEmailLog=SqlHelper.ExecuteDataset(transaction, CommandType.StoredProcedure, "sp_EmailLog", myParams).Tables[0];
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
        return dtEmailLog;
    }
    public static string IncrementDownloads(string GUID)
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
                    myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_EmailLog");
                    myParams[0].Value = 3;
                    myParams[2].Value = GUID;
                    SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, "sp_EmailLog", myParams);
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
    public static DataTable GetUserEmailLog(int UserId)
    {
        DataTable dtLog = new DataTable();
        try
        {
            SqlParameter[] myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_EmailLog");
            myParams[0].Value = 5;
            myParams[5].Value = UserId;
            dtLog = SqlHelper.ExecuteDataset(BVisionConfigurationManager.GetConnectionString(), CommandType.StoredProcedure, "sp_EmailLog", myParams).Tables[0];

            return dtLog;
        }
        catch (Exception ex)
        {
            return dtLog;
        }
    }
    public static DataTable GetUserEmailLogByReportVersion(string ReportGUID,string VersionGUID)
    {
        DataTable dtLog = new DataTable();
        try
        {
            SqlParameter[] myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_EmailLog");
            myParams[0].Value = 4;
            myParams[10].Value = ReportGUID;
            myParams[11].Value = VersionGUID;
            dtLog = SqlHelper.ExecuteDataset(BVisionConfigurationManager.GetConnectionString(), CommandType.StoredProcedure, "sp_EmailLog", myParams).Tables[0];

            return dtLog;
        }
        catch (Exception ex)
        {
            return dtLog;
        }
    }   
}