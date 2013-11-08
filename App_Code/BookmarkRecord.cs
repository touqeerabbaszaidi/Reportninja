using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BookmarkRecord
/// </summary>
public class BookmarkRecord
{
    public BookmarkRecord()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static string AddBookmarkRecord(string PageIds, string PageGUIDs, string ReportGUID, string VersionGUID)
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
                    myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_BookmarkRecord");
                    myParams[0].Value = 1;
                    myParams[5].Value = PageIds;
                    myParams[6].Value = PageGUIDs;
                    myParams[7].Value = ReportGUID;
                    myParams[8].Value = VersionGUID;
                    DataTable dtPageGUID = SqlHelper.ExecuteDataset(transaction, CommandType.StoredProcedure, "sp_BookmarkRecord", myParams).Tables[0];
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
    public static DataTable GetBookmarkRecordByGUID(string GUID)
    {
        DataTable dtBookmarks = new DataTable();
        try
        {
            SqlParameter[] myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_BookmarkRecord");
            myParams[0].Value = 2;
            myParams[2].Value = GUID;
            dtBookmarks = SqlHelper.ExecuteDataset(BVisionConfigurationManager.GetConnectionString(), CommandType.StoredProcedure, "sp_BookmarkRecord", myParams).Tables[0];

            return dtBookmarks;
        }
        catch (Exception ex)
        {
            return dtBookmarks;
        }
    }
}