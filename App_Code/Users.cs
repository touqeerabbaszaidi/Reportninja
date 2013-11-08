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
/// Summary description for Users
/// </summary>
public class Users
{
    public Users()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static string UserSignUp(string GUID, string UserName, string Email, string Password,string AccountType,int AddMonth)
    {
        string Status = "";
        string Photo = "images/ProfilePictures/ProfilePicture.png";
        string strAccountCheck = AccountCheck(Email);
        if (strAccountCheck == "No")
        {
            SqlParameter[] myParams = null;
            using (SqlConnection connection = new SqlConnection(BVisionConfigurationManager.GetConnectionString()))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_Users");
                        myParams[0].Value = 1;
                        myParams[2].Value = GUID;
                        myParams[3].Value = 2;
                        myParams[7].Value = UserName;
                        myParams[8].Value = Email;
                        myParams[9].Value = CustomEncryption.Encrypt(Password);
                        myParams[12].Value = Photo;
                        myParams[23].Value = "No";
                        myParams[24].Value = "No";
                        myParams[25].Value = "Yes";
                        myParams[26].Value = System.DateTime.Now;
                        myParams[27].Value = System.DateTime.Now;
                        myParams[42].Value = AccountType;
                        myParams[43].Value = System.DateTime.Now.AddMonths(AddMonth);
                        myParams[44].Value = "Unpaid";
                        DataTable dtUserData= SqlHelper.ExecuteDataset(transaction, CommandType.StoredProcedure, "sp_Users", myParams).Tables[0];
                        transaction.Commit();
                        Status = "Status~Done~" + Convert.ToString(dtUserData.Rows[0][0]);
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
        }
        else if (strAccountCheck == "Yes")
        {
            Status = "Status~Exists";
        }
        else
        {
            Status = "Status~"+strAccountCheck;
        }

        return Status;
    }
    public static string AccountCheck(string Email)
    {
        DataTable dtAccountCheck = new DataTable();
        try
        {
            SqlParameter[] myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_Users");
            myParams[0].Value = 10;
            myParams[8].Value = Email;
            dtAccountCheck = SqlHelper.ExecuteDataset(BVisionConfigurationManager.GetConnectionString(), CommandType.StoredProcedure, "sp_Users", myParams).Tables[0];

            if (dtAccountCheck.Rows.Count > 0)
            {
                return "Yes";
            }
            else
            {
                return "No";
            }
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
    public static DataTable UserLogin(string Email, string Password)
    {
        DataTable dtLogin = new DataTable();
        try
        {
            SqlParameter[] myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_Users");
            myParams[0].Value = 9;
            myParams[8].Value = Email;
            myParams[9].Value = Password;
            dtLogin = SqlHelper.ExecuteDataset(BVisionConfigurationManager.GetConnectionString(), CommandType.StoredProcedure, "sp_Users", myParams).Tables[0];

            return dtLogin;
        }
        catch (Exception ex)
        {
            return dtLogin;
        }
    }
    public static DataTable AdminLogin(string UserName, string Password)
    {
        DataTable dtLogin = new DataTable();
        try
        {
            SqlParameter[] myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_Users");
            myParams[0].Value = 23;
            myParams[7].Value = UserName;
            myParams[9].Value = Password;
            dtLogin = SqlHelper.ExecuteDataset(BVisionConfigurationManager.GetConnectionString(), CommandType.StoredProcedure, "sp_Users", myParams).Tables[0];

            return dtLogin;
        }
        catch (Exception ex)
        {
            return dtLogin;
        }
    }
    public static DataTable AllUsersData()
    {
        DataTable dtUsersData = new DataTable();
        try
        {
            SqlParameter[] myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_Users");
            myParams[0].Value = 0;
            dtUsersData = SqlHelper.ExecuteDataset(BVisionConfigurationManager.GetConnectionString(), CommandType.StoredProcedure, "sp_Users", myParams).Tables[0];

            return dtUsersData;
        }
        catch (Exception ex)
        {
            return dtUsersData;
        }
    }
    public static string UserDataByEmail(string Email)
    {
        string Status = "Error";
        DataTable dt = new DataTable();
        SqlParameter[] myParams = null;
        using (SqlConnection connection = new SqlConnection(BVisionConfigurationManager.GetConnectionString()))
        {
            connection.Open();
            try
            {
                myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_Users");
                myParams[0].Value = 10;
                myParams[8].Value = Email;
                dt = SqlHelper.ExecuteDataset(BVisionConfigurationManager.GetConnectionString(), CommandType.StoredProcedure, "sp_Users", myParams).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    Status = Convert.ToString(dt.Rows[0]["Email"]) + "^$^" + CustomEncryption.Decrypt(Convert.ToString(dt.Rows[0]["Password"])) + "^$^" + Convert.ToString(dt.Rows[0]["UserName"])+"^$^" + Convert.ToString(dt.Rows[0]["GUID"]);
                }
                else
                {
                    Status = "No record";
                }
                return Status;
            }
            catch (Exception ex)
            {
                Convert.ToString(ex.Message);
                return Status;
            }
            if (connection.State != ConnectionState.Closed)
            {
                connection.Close();
            }
        }
    }
    public static void UpdateLoginDate(string UserID)
    {
        try
        {
            SqlParameter[] myParams = null;
            using (SqlConnection connection = new SqlConnection(BVisionConfigurationManager.GetConnectionString()))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_Users");
                        myParams[0].Value = 4;
                        myParams[1].Value = CustomEncryption.Decrypt(UserID);
                        myParams[28].Value = System.DateTime.Now;
                        SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, "sp_Users", myParams);
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
        catch (Exception ex)
        {

        }
    }
    public static void UpdateAdminLoginDate(string UserID)
    {
        try
        {
            SqlParameter[] myParams = null;
            using (SqlConnection connection = new SqlConnection(BVisionConfigurationManager.GetConnectionString()))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_Users");
                        myParams[0].Value = 24;
                        myParams[1].Value = CustomEncryption.Decrypt(UserID);
                        myParams[28].Value = System.DateTime.Now;
                        SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, "sp_Users", myParams);
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
        catch (Exception ex)
        {

        }
    }
    public static string ChangePassword(string UserID, string Password)
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
                    myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_Users");
                    myParams[0].Value = 5;
                    myParams[1].Value = CustomEncryption.Decrypt(UserID);
                    myParams[9].Value = CustomEncryption.Encrypt(Password);
                    myParams[27].Value = System.DateTime.Now;
                    SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, "sp_Users", myParams);
                    transaction.Commit();
                    Status = "Done";
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
        return Status;
    }
    public static string ChangeUsername(string UserID, string Username)
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
                    myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_Users");
                    myParams[0].Value = 16;
                    myParams[1].Value = CustomEncryption.Decrypt(UserID);
                    myParams[7].Value = Username;
                    myParams[27].Value = System.DateTime.Now;
                    SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, "sp_Users", myParams);
                    transaction.Commit();
                    Status = "Done";
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
        return Status;
    }
    public static string ChangeEmail(string UserID, string Email)
    {
        string Status = "";
        string strAccountCheck = AccountCheck(Email);
        if (strAccountCheck == "No")
        {
            SqlParameter[] myParams = null;
            using (SqlConnection connection = new SqlConnection(BVisionConfigurationManager.GetConnectionString()))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_Users");
                        myParams[0].Value = 17;
                        myParams[1].Value = CustomEncryption.Decrypt(UserID);
                        myParams[8].Value = Email;
                        myParams[24].Value = "No";
                        myParams[27].Value = System.DateTime.Now;
                        SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, "sp_Users", myParams);
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
        }
        else if (strAccountCheck == "Yes")
        {
            Status = "Exists";
        }
        else
        {
            Status = strAccountCheck;
        }

        return Status;
    }
    public static string ForgottonPassword(string Email)
    {
        string Status = "Error";
        DataTable dt = new DataTable();
        SqlParameter[] myParams = null;
        using (SqlConnection connection = new SqlConnection(BVisionConfigurationManager.GetConnectionString()))
        {
            connection.Open();
            try
            {
                myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_Users");
                myParams[0].Value = 10;
                myParams[8].Value = Email;
                dt = SqlHelper.ExecuteDataset(BVisionConfigurationManager.GetConnectionString(), CommandType.StoredProcedure, "sp_Users", myParams).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    Status = Convert.ToString(dt.Rows[0]["Email"]) + "^$^" + CustomEncryption.Decrypt(Convert.ToString(dt.Rows[0]["Password"])) + "^$^" + Convert.ToString(dt.Rows[0]["UserName"]) + "^$^" + Convert.ToString(dt.Rows[0]["UserID"]);
                }
                else
                {
                    Status = "No record";
                }
                return Status;
            }
            catch (Exception ex)
            {
                Convert.ToString(ex.Message);
                return Status;
            }
            if (connection.State != ConnectionState.Closed)
            {
                connection.Close();
            }
        }
    }
    public static string ChangeAccountInfo(string UserID, string Phone,string Address)
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
                    myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_Users");
                    myParams[0].Value = 18;
                    myParams[1].Value = CustomEncryption.Decrypt(UserID);
                    myParams[13].Value = Address;
                    myParams[19].Value = Phone;
                    myParams[27].Value = System.DateTime.Now;
                    SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, "sp_Users", myParams);
                    transaction.Commit();
                    Status = "Done";
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
        return Status;
    }
    public static string ChangeSecurityQuestion(string UserID, string Question, string Answer)
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
                    myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_Users");
                    myParams[0].Value = 21;
                    myParams[1].Value = CustomEncryption.Decrypt(UserID);
                    myParams[40].Value = Question;
                    myParams[41].Value = Answer;
                    myParams[27].Value = System.DateTime.Now;
                    SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, "sp_Users", myParams);
                    transaction.Commit();
                    Status = "Done";
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
        return Status;
    }
    public static DataTable GetDataByUserId(string UserID)
    {
        DataTable dtUsersData = new DataTable();
        try
        {
            SqlParameter[] myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_Users");
            myParams[0].Value = 19;
            myParams[1].Value = CustomEncryption.Decrypt(UserID);
            dtUsersData = SqlHelper.ExecuteDataset(BVisionConfigurationManager.GetConnectionString(), CommandType.StoredProcedure, "sp_Users", myParams).Tables[0];

            return dtUsersData;
        }
        catch (Exception ex)
        {
            return dtUsersData;
        }
    }
    public static DataTable SecurityQuestionsList()
    {
        DataTable dtUsersData = new DataTable();
        try
        {
            SqlParameter[] myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_Users");
            myParams[0].Value = 20;
            dtUsersData = SqlHelper.ExecuteDataset(BVisionConfigurationManager.GetConnectionString(), CommandType.StoredProcedure, "sp_Users", myParams).Tables[0];

            return dtUsersData;
        }
        catch (Exception ex)
        {
            return dtUsersData;
        }
    }
    public static void UpdatePaymentStatus(int UserId, int MonthsToAdd)
    {
        try
        {
            SqlParameter[] myParams = null;
            using (SqlConnection connection = new SqlConnection(BVisionConfigurationManager.GetConnectionString()))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_Users");
                        myParams[0].Value = 22;
                        myParams[1].Value = UserId;
                        myParams[42].Value = "Premium"; 
                        myParams[43].Value = System.DateTime.Now.AddMonths(MonthsToAdd);
                        myParams[44].Value = "Paid";
                        SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, "sp_Users", myParams);
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
        catch (Exception ex)
        {

        }
    }
}