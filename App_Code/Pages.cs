using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;

/// <summary>
/// Summary description for Pages
/// </summary>
public class Pages
{
    public Pages()
    {
        

        //
        // TODO: Add constructor logic here
        //
    }

    public static string AddPage(int ReportID, int VersionID, string Name, string Description)
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
                    myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_Pages");
                    myParams[0].Value = 1;
                    myParams[2].Value = ReportID;
                    myParams[3].Value = System.Guid.NewGuid().ToString();
                    myParams[4].Value = Name;
                    myParams[5].Value = Description;

                    SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, "sp_Pages", myParams);
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

    public static string SavePageHTML(string ReportGUID, string VersionGUID, string PageGUID, string HTML)
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
                    myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_Pages");
                    myParams[0].Value = 10;
                    myParams[4].Value = PageGUID;
                    myParams[7].Value = HTML;
                    myParams[12].Value = ReportGUID;
                    myParams[13].Value = VersionGUID;

                    SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, "sp_Pages", myParams);
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

    public static string SavePages(string ReportGUID, string VersionGUID, string PageGUID, string PageName)
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
                    myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_Pages");
                    myParams[0].Value = 11;
                    myParams[4].Value = PageGUID;
                    myParams[5].Value = PageName;
                    myParams[9].Value = System.DateTime.Now;
                    myParams[10].Value = System.DateTime.Now;
                    myParams[12].Value = ReportGUID;
                    myParams[13].Value = VersionGUID;

                    DataTable dtPageGUID = SqlHelper.ExecuteDataset(transaction, CommandType.StoredProcedure, "sp_Pages", myParams).Tables[0];
                    transaction.Commit();
                    Status = Convert.ToString(dtPageGUID.Rows[0][0]);
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

    public static string LoadPageHTML(string ReportGUID, string VersionGUID, string PageGUID)
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
                    myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_Pages");
                    myParams[0].Value = 3;
                    myParams[4].Value = PageGUID;
                    myParams[12].Value = ReportGUID;
                    myParams[13].Value = VersionGUID;

                    DataTable dtPageHTML = SqlHelper.ExecuteDataset(transaction, CommandType.StoredProcedure, "sp_Pages", myParams).Tables[0];
                    transaction.Commit();
                    if (dtPageHTML.Rows.Count > 0)
                    { Status = Convert.ToString(dtPageHTML.Rows[0]["HTML"]); }
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

    public static string DeletePage(string ReportGUID, string VersionGUID, string PageGUID)
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
                    myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_Pages");
                    myParams[0].Value = 5;
                    myParams[4].Value = PageGUID;
                    myParams[8].Value = 1;
                    myParams[11].Value = System.DateTime.Now;
                    myParams[12].Value = ReportGUID;
                    myParams[13].Value = VersionGUID;

                    SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, "sp_Pages", myParams);
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

    public static DataTable GetDataByGUID(string ReportGUID, string VersionGUID, string PageGUID)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlParameter[] myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_Pages");
            myParams[0].Value = 6;
            myParams[4].Value = PageGUID;
            myParams[12].Value = ReportGUID;
            myParams[13].Value = VersionGUID;
            dt = SqlHelper.ExecuteDataset(BVisionConfigurationManager.GetConnectionString(), CommandType.StoredProcedure, "sp_Pages", myParams).Tables[0];

            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }

    public static string LoadPageTitle(string PageGUID)
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
                    myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_Pages");
                    myParams[0].Value = 8;
                    myParams[4].Value = PageGUID;

                    DataTable dtPageData = SqlHelper.ExecuteDataset(transaction, CommandType.StoredProcedure, "sp_Pages", myParams).Tables[0];
                    transaction.Commit();
                    if (dtPageData.Rows.Count > 0)
                    { Status = Convert.ToString(dtPageData.Rows[0]["Name"]); }
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
}