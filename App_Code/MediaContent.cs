using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

/// <summary>
/// Summary description for MediaContent
/// </summary>
public class MediaContent
{
    public MediaContent()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static string AddContent(string Path, string Description)
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
                    myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_MediaContent");
                    myParams[0].Value = 1;
                    myParams[2].Value = System.Guid.NewGuid().ToString();
                    myParams[3].Value = Path;
                    myParams[4].Value = Description;
                    DataTable dtStatus=SqlHelper.ExecuteDataset(transaction, CommandType.StoredProcedure, "sp_MediaContent", myParams).Tables[0];
                    transaction.Commit();
                    //Status = "Done";
                    if (dtStatus.Rows.Count > 0)
                    {
                        Status=Convert.ToString(dtStatus.Rows[0][0]);
                    }
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
    public static string UpdateContent(string Path, string Description)
    {
        string[] FilePath = Path.Split('=');
        string Status = "";
        SqlParameter[] myParams = null;
        using (SqlConnection connection = new SqlConnection(BVisionConfigurationManager.GetConnectionString()))
        {
            connection.Open();
            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_MediaContent");
                    myParams[0].Value = 2;
                    myParams[3].Value = FilePath[1];
                    myParams[4].Value = Description;
                    SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, "sp_MediaContent", myParams);
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
    public static string UpdateContentById(string ID, string Description)
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
                    myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_MediaContent");
                    myParams[0].Value = 4;
                    myParams[1].Value = ID;
                    myParams[4].Value = Description;
                    SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, "sp_MediaContent", myParams);
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
    public static DataTable GetContent(string Path)
    {
        DataTable dtContent = new DataTable();
        try
        {
            SqlParameter[] myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_MediaContent");
            myParams[0].Value = 3;
            myParams[3].Value = Path;
            dtContent = SqlHelper.ExecuteDataset(BVisionConfigurationManager.GetConnectionString(), CommandType.StoredProcedure, "sp_MediaContent", myParams).Tables[0];

            return dtContent;
        }
        catch (Exception ex)
        {
            return dtContent;
        }
    }
    public static DataTable GetContentById(int ID)
    {
        DataTable dtContent = new DataTable();
        try
        {
            SqlParameter[] myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_MediaContent");
            myParams[0].Value = 5;
            myParams[1].Value = ID;
            dtContent = SqlHelper.ExecuteDataset(BVisionConfigurationManager.GetConnectionString(), CommandType.StoredProcedure, "sp_MediaContent", myParams).Tables[0];

            return dtContent;
        }
        catch (Exception ex)
        {
            return dtContent;
        }
    }   


    //Media Library Table
    public static string MimeType(string Extension)
    {
        string mime = "application/octetstream";
        if (string.IsNullOrEmpty(Extension))
            return mime;

        string ext = Extension.ToLower();
        Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
        if (rk != null && rk.GetValue("Content Type") != null)
            mime = Convert.ToString(rk.GetValue("Content Type"));
        return mime;
    }
    public static string AddMediaContent(List<Array> ContentList)
    {
        string Status = "";
        foreach (Array ContentArray in ContentList)
        {
            //casting System.Array to string[]
            string[] ArrayDetail = ContentArray.OfType<object>().Select(o => o.ToString()).ToArray();

            SqlParameter[] myParams = null;
            using (SqlConnection connection = new SqlConnection(BVisionConfigurationManager.GetConnectionString()))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_MediaLibrary");
                        myParams[0].Value = 1;
                        myParams[2].Value = System.Guid.NewGuid().ToString();
                        myParams[3].Value = Convert.ToInt32(CustomEncryption.Decrypt(ArrayDetail[0]));
                        myParams[4].Value = Convert.ToString(ArrayDetail[1]);
                        myParams[5].Value = Convert.ToString(ArrayDetail[2]);
                        myParams[6].Value = Convert.ToString(ArrayDetail[3]);
                        myParams[7].Value = Convert.ToString(ArrayDetail[4]);
                        myParams[9].Value = System.DateTime.Now;
                        DataTable dtStatus = SqlHelper.ExecuteDataset(transaction, CommandType.StoredProcedure, "sp_MediaLibrary", myParams).Tables[0];
                        transaction.Commit();
                        Status = "Done";
                        //if (dtStatus.Rows.Count > 0)
                        //{
                        //    Status = Convert.ToString(dtStatus.Rows[0][0]);
                        //}
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
        return Status;
    }
    public static DataTable GetMediaContentByUserId(int UserId)
    {
        DataTable dtContent = new DataTable();
        try
        {
            SqlParameter[] myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_MediaLibrary");
            myParams[0].Value = 2;
            myParams[3].Value = UserId;
            dtContent = SqlHelper.ExecuteDataset(BVisionConfigurationManager.GetConnectionString(), CommandType.StoredProcedure, "sp_MediaLibrary", myParams).Tables[0];

            return dtContent;
        }
        catch (Exception ex)
        {
            return dtContent;
        }
    }
    public static string DeleteMediaContent(int MediaContentId)
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
                    myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_MediaLibrary");
                    myParams[0].Value = 3;
                    myParams[1].Value = MediaContentId;
                    SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, "sp_MediaLibrary", myParams);
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
    public static DataTable GetMediaContentByFileGUID(string MediaContentGUID)
    {
        DataTable dtContent = new DataTable();
        try
        {
            SqlParameter[] myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_MediaLibrary");
            myParams[0].Value = 4;
            myParams[2].Value = MediaContentGUID;
            dtContent = SqlHelper.ExecuteDataset(BVisionConfigurationManager.GetConnectionString(), CommandType.StoredProcedure, "sp_MediaLibrary", myParams).Tables[0];

            return dtContent;
        }
        catch (Exception ex)
        {
            return dtContent;
        }
    }
}