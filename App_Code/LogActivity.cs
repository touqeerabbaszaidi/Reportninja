using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LogActivity
/// </summary>
public class LogActivity
{
	public LogActivity()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static string AddLog(int UserId, string ReportGUID, string VersionGUID, string PageGUID, string Description)
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
                    myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_LogActivity");
                    myParams[0].Value = 1;
                    myParams[2].Value = UserId;
                    myParams[3].Value = ReportGUID;
                    myParams[4].Value = VersionGUID;
                    myParams[5].Value = PageGUID;
                    myParams[6].Value = Description;
                    myParams[7].Value = System.DateTime.Now;
                    SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, "sp_LogActivity", myParams);
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
    public static DataTable GetUserLog(int UserId)
    {
        DataTable dtLog = new DataTable();
        try
        {
            SqlParameter[] myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_LogActivity");
            myParams[0].Value = 2;
            myParams[2].Value = UserId;
            dtLog = SqlHelper.ExecuteDataset(BVisionConfigurationManager.GetConnectionString(), CommandType.StoredProcedure, "sp_LogActivity", myParams).Tables[0];

            return dtLog;
        }
        catch (Exception ex)
        {
            return dtLog;
        }
    }
    public static DataTable GetUserLogByReportVersion(string ReportGUID,string VersionGUID)
    {
        DataTable dtLog = new DataTable();
        try
        {
            SqlParameter[] myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_LogActivity");
            myParams[0].Value = 3;
            myParams[3].Value = ReportGUID;
            myParams[4].Value = VersionGUID;
            dtLog = SqlHelper.ExecuteDataset(BVisionConfigurationManager.GetConnectionString(), CommandType.StoredProcedure, "sp_LogActivity", myParams).Tables[0];

            return dtLog;
        }
        catch (Exception ex)
        {
            return dtLog;
        }
    }   
}