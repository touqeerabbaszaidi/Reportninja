using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class SaveReportHTML : System.Web.Services.WebService {

    public SaveReportHTML()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }
    [WebMethod]
    public string SaveReport(string ReportGUID,string VersionGUID, string PageGUID, string HTML)
    {
        try
        {
            string EncodedHTML = System.Web.HttpUtility.HtmlEncode(HTML);
            Pages.SavePageHTML(ReportGUID,VersionGUID, PageGUID, EncodedHTML);
            return "Done";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
    [WebMethod(EnableSession = true)]
    public string SavePages(string ReportGUID, string VersionGUID, string PageGUID, string PageName, string LogActivityBit, string Control)
    {
        try
        {
            //AddLogActivity(ReportGUID, VersionGUID, PageGUID, LogActivityBit, Control);
            string GUID=Pages.SavePages(ReportGUID,VersionGUID, PageGUID,PageName);
            return GUID;
        }
        catch (Exception ex)
        {
            return "Error";
        }
    }
    [WebMethod]
    public string SavePagesHTML(string ReportGUID, string VersionGUID, string PagesHTML)
    {
        try
        {
            Versions.SavePagesHTML(VersionGUID, PagesHTML);
            return "Done";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
    [WebMethod(EnableSession = true)]
    public string DeletePage(string ReportGUID, string VersionGUID, string PageGUID)
    {
        try
        {
            AddLogActivity(ReportGUID, VersionGUID, PageGUID, "2", "Page");
            Pages.DeletePage(ReportGUID, VersionGUID, PageGUID);
            return "Done";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
    [WebMethod(EnableSession = true)]
    public string SaveLogActivity(string ReportGUID, string VersionGUID, string PageGUID, string HTML,string LogActivityBit,string Control,string FilePath,string Description)
    {
        try
        {
            AddLogActivity(ReportGUID, VersionGUID, PageGUID, LogActivityBit, Control);
            if (Control == "Audio" || Control == "Video")
            {
                MediaContent.UpdateContent(FilePath, Description);
            }
            if (Control == "MultiImageParagraph")
            {
                MediaContent.UpdateContentById(FilePath, Description);
            }
            string EncodedHTML = System.Web.HttpUtility.HtmlEncode(HTML);
            Pages.SavePageHTML(ReportGUID, VersionGUID, PageGUID, EncodedHTML);
            return "Done";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
    [WebMethod(EnableSession = true)]
    public void AddLogActivity(string ReportGUID, string VersionGUID, string PageGUID, string LogActivityBit, string Control)
    {
        string Status = "";
        if (LogActivityBit == "1")
        { Status = "Updated"; }
        else if (LogActivityBit == "2")
        { Status = "Deleted"; }
        else
        { Status = "Created"; }
        string Description = Control + " " + Status;
        LogActivity.AddLog(Convert.ToInt32(CustomEncryption.Decrypt(Convert.ToString(Session["UserID"]))), ReportGUID, VersionGUID, PageGUID,Description);
    }
    [WebMethod(EnableSession = true)]
    public string SaveBookmarkRecord(string PageIds, string PageGUIDs, string ReportGUID, string VersionGUID)
    {
        try
        {
            string GUID = BookmarkRecord.AddBookmarkRecord(PageIds, PageGUIDs,ReportGUID, VersionGUID);
            return GUID;
        }
        catch (Exception ex)
        {
            return "Error";
        }
    }
    [WebMethod(EnableSession = true)]
    public string UseReport(string UserID, string ReportName, string Password, string ReportGUID, string VersionGUID)
    {
        try
        {
            string Status = Reports.AddDuplicateReport(CustomEncryption.Encrypt(UserID), ReportName, Password, ReportGUID, VersionGUID);
            
            return Status;
        }
        catch (Exception ex)
        {
            return "Error";
        }
    }
}
