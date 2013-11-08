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
/// Summary description for PaymentsAndTransactions
/// </summary>
public class PaymentsAndTransactions
{
    public PaymentsAndTransactions()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataTableCollection GetPaymentMethodsAndSubscriptions()
    {
        DataTableCollection dtData = null;
        try
        {
            SqlParameter[] myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_Payments");
            myParams[0].Value = 1;
            dtData = SqlHelper.ExecuteDataset(BVisionConfigurationManager.GetConnectionString(), CommandType.StoredProcedure, "sp_Payments", myParams).Tables;
        }
        catch (Exception ex)
        {

        }

        return dtData;
    }
    public static DataTable GetPaymentMethodsAndSubscriptionsByUserId(int UserId)
    {
        DataTable dtData = null;
        try
        {
            SqlParameter[] myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_Payments");
            myParams[0].Value = 4;
            myParams[3].Value = UserId;
            dtData = SqlHelper.ExecuteDataset(BVisionConfigurationManager.GetConnectionString(), CommandType.StoredProcedure, "sp_Payments", myParams).Tables[0];
        }
        catch (Exception ex)
        {

        }

        return dtData;
    }
    public static string AddPayPalInfo(string GUID, int UserId, int PaymentMethodId)
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
                    myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_PaymentInfo");
                    myParams[0].Value = 2;
                    myParams[2].Value = GUID;
                    myParams[3].Value = UserId;
                    myParams[4].Value = PaymentMethodId;
                    myParams[5].Value = System.DateTime.Now;
                    myParams[6].Value = System.DateTime.Now;
                    myParams[19].Value = "Success";
                    DataTable dtPaymentInfo = SqlHelper.ExecuteDataset(transaction, CommandType.StoredProcedure, "sp_PaymentInfo", myParams).Tables[0];
                    transaction.Commit();
                    Status = Convert.ToString(dtPaymentInfo.Rows[0][0]);
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
    public static string AddCreditCardInfo(string GUID, int UserId, int PaymentMethodId, string CardNumber, string CardHolderName, string ExpirationDate, string CVV, string RandomAmount)
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
                    myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_PaymentInfo");
                    myParams[0].Value = 3;
                    myParams[2].Value = GUID;
                    myParams[3].Value = UserId;
                    myParams[4].Value = PaymentMethodId;
                    myParams[5].Value = System.DateTime.Now;
                    myParams[6].Value = System.DateTime.Now;
                    myParams[9].Value = CustomEncryption.Encrypt(CardNumber);
                    myParams[10].Value = CustomEncryption.Encrypt(CardHolderName);
                    myParams[11].Value = CustomEncryption.Encrypt(ExpirationDate);
                    myParams[12].Value = CustomEncryption.Encrypt(CVV);
                    myParams[15].Value = "Unverified";
                    myParams[16].Value = CustomEncryption.Encrypt(RandomAmount);
                    myParams[18].Value = 0;
                    myParams[19].Value = "Success";
                    DataTable dtPaymentInfo = SqlHelper.ExecuteDataset(transaction, CommandType.StoredProcedure, "sp_PaymentInfo", myParams).Tables[0];
                    transaction.Commit();
                    Status = Convert.ToString(dtPaymentInfo.Rows[0][0]);
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
    public static void AddTransaction(string GUID, int UserId, int PaymentInfoId, int SubscriptionId, string TransactionToken, string TransactionAmount, string PayerID,string Status,string Description,string AmountRefunded)
    {
        SqlParameter[] myParams = null;
        using (SqlConnection connection = new SqlConnection(BVisionConfigurationManager.GetConnectionString()))
        {
            connection.Open();
            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_Payments");
                    myParams[0].Value = 2;
                    myParams[2].Value = GUID;
                    myParams[3].Value = UserId;
                    myParams[4].Value = PaymentInfoId;
                    myParams[5].Value = SubscriptionId;
                    myParams[6].Value = System.DateTime.Now;
                    myParams[7].Value = TransactionToken;
                    myParams[8].Value = TransactionAmount;
                    myParams[9].Value = PayerID;
                    myParams[10].Value = Status;
                    myParams[11].Value = Description;
                    myParams[12].Value = AmountRefunded;
                    DataTable dtTransaction = SqlHelper.ExecuteDataset(transaction, CommandType.StoredProcedure, "sp_Payments", myParams).Tables[0];
                    transaction.Commit();
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
    public static string CheckUserAccountStatus(int UserId)
    {
        DataTable dtData = null;
        string Status = "";
        try
        {
            SqlParameter[] myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_Payments");
            myParams[0].Value = 3;
            myParams[3].Value = UserId;
            dtData = SqlHelper.ExecuteDataset(BVisionConfigurationManager.GetConnectionString(), CommandType.StoredProcedure, "sp_Payments", myParams).Tables[0];
            if (dtData.Rows.Count > 0)
            {
                if (Convert.ToString(dtData.Rows[0]["AccountType"]).Trim() == "Trial")
                {
                    if (Convert.ToDateTime(dtData.Rows[0]["AccountExpiryDate"]) <= System.DateTime.Now)
                    {
                        Status = "Trial Expired";
                    }
                    else
                    {
                        Status = "Trial Valid";
                    }
                }
                else //if Premium
                {
                    if (Convert.ToString(dtData.Rows[0]["PaymentStatus"]).Trim() == "Unpaid")
                    {
                        Status = "Premium Unpaid";
                    }
                    else
                    {
                        Status = "Premium Paid";

                        if (Convert.ToDateTime(dtData.Rows[0]["AccountExpiryDate"]) <= System.DateTime.Now)
                        {
                            Status = "Premium Expired";
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }

        return Status;
    }
    public static void UpdateStatus(int PaymentInfoId,string Status)
    {
        SqlParameter[] myParams = null;
        using (SqlConnection connection = new SqlConnection(BVisionConfigurationManager.GetConnectionString()))
        {
            connection.Open();
            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_PaymentInfo");
                    myParams[0].Value = 4;
                    myParams[1].Value = PaymentInfoId;
                    myParams[19].Value = Status;
                    SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, "sp_PaymentInfo", myParams);
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
    public static void DeleteCreditCard(int PaymentInfoId)
    {
        SqlParameter[] myParams = null;
        using (SqlConnection connection = new SqlConnection(BVisionConfigurationManager.GetConnectionString()))
        {
            connection.Open();
            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_PaymentInfo");
                    myParams[0].Value = 5;
                    myParams[1].Value = PaymentInfoId;
                    myParams[7].Value = "Yes";
                    myParams[8].Value = System.DateTime.Now;
                    SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, "sp_PaymentInfo", myParams);
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
    public static DataTable GetUsersPaymentInfoByPaymentInfoId(int PaymentInfoId)
    {
        DataTable dtData = null;
        try
        {
            SqlParameter[] myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_PaymentInfo");
            myParams[0].Value = 6;
            myParams[1].Value = PaymentInfoId;
            dtData = SqlHelper.ExecuteDataset(BVisionConfigurationManager.GetConnectionString(), CommandType.StoredProcedure, "sp_PaymentInfo", myParams).Tables[0];
        }
        catch (Exception ex)
        {

        }

        return dtData;
    }
    public static string VerifyCreditCard(int PaymentInfoId, string RandomAmount)
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
                    myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_PaymentInfo");
                    myParams[0].Value = 7;
                    myParams[1].Value = PaymentInfoId;
                    myParams[7].Value = "Yes";
                    myParams[8].Value = System.DateTime.Now;
                    myParams[15].Value = "Verified";
                    myParams[16].Value = RandomAmount;
                    myParams[17].Value = System.DateTime.Now;
                    DataTable dtPaymentInfo = SqlHelper.ExecuteDataset(transaction, CommandType.StoredProcedure, "sp_PaymentInfo", myParams).Tables[0];
                    transaction.Commit();
                    Status = Convert.ToString(dtPaymentInfo.Rows[0][0]);
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