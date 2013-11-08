using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Net.Mail;
using System.Net;
using System.IO;

/// <summary>
/// Summary description for Survey
/// </summary>
public class Survey
{
    public Survey()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static string SaveSurveyUserAsAnonymous()
    {
        string Status = "";
        string GUID = System.Guid.NewGuid().ToString();
        SqlParameter[] myParams = null;
        using (SqlConnection connection = new SqlConnection(BVisionConfigurationManager.GetConnectionString()))
        {
            connection.Open();
            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_Survey");
                    myParams[0].Value = 1;
                    myParams[2].Value = GUID;
                    myParams[3].Value = "Anonymous";
                    myParams[4].Value = "";
                    myParams[8].Value = System.DateTime.Now;
                    DataTable dtUserData = SqlHelper.ExecuteDataset(transaction, CommandType.StoredProcedure, "sp_Survey", myParams).Tables[0];
                    transaction.Commit();
                    Status = Convert.ToString(dtUserData.Rows[0][0]);
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
    public static void SaveSurveyData(int SurveyUserId, int QuestionId, string Answer)
    {
        SqlParameter[] myParams = null;
        using (SqlConnection connection = new SqlConnection(BVisionConfigurationManager.GetConnectionString()))
        {
            connection.Open();
            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_Survey");
                    myParams[0].Value = 2;
                    myParams[1].Value = SurveyUserId;
                    myParams[9].Value = QuestionId;
                    myParams[12].Value = Answer;
                    SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, "sp_Survey", myParams);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }
            if (connection.State != ConnectionState.Closed)
            {
                connection.Close();
            }
        }
    }
    public static void SaveSurveyUserData(int SurveyUserId, string Name, string Email, string PhoneNumber)
    {
        SqlParameter[] myParams = null;
        using (SqlConnection connection = new SqlConnection(BVisionConfigurationManager.GetConnectionString()))
        {
            connection.Open();
            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_Survey");
                    myParams[0].Value = 3;
                    myParams[1].Value = SurveyUserId;
                    myParams[3].Value = Name;
                    myParams[4].Value = Email;
                    myParams[5].Value = PhoneNumber;
                    myParams[6].Value = true;
                    myParams[7].Value = true;
                    SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, "sp_Survey", myParams);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }
            if (connection.State != ConnectionState.Closed)
            {
                connection.Close();
            }
        }
    }
    public static DataTable GetSurveys()
    {
        DataTable dtData = new DataTable();
        try
        {
            SqlParameter[] myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_Survey");
            myParams[0].Value = 0;
            dtData = SqlHelper.ExecuteDataset(BVisionConfigurationManager.GetConnectionString(), CommandType.StoredProcedure, "sp_Survey", myParams).Tables[0];

            return dtData;
        }
        catch (Exception ex)
        {
            return dtData;
        }
    }
    public static DataTable GetSurveysBySurveyUserId(int SurveyUserId)
    {
        DataTable dtData = new DataTable();
        try
        {
            SqlParameter[] myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_Survey");
            myParams[0].Value = 4;
            myParams[1].Value = SurveyUserId;
            dtData = SqlHelper.ExecuteDataset(BVisionConfigurationManager.GetConnectionString(), CommandType.StoredProcedure, "sp_Survey", myParams).Tables[0];

            return dtData;
        }
        catch (Exception ex)
        {
            return dtData;
        }
    }
}