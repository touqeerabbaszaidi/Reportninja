using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for MediaLibrary
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class MediaLibrary : System.Web.Services.WebService {

    public MediaLibrary () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }
    [WebMethod(EnableSession = true)]
    public string GetMediaLibraryFiles()
    {
        string html = "";
        DataTable dtMediaContent= MediaContent.GetMediaContentByUserId(Convert.ToInt32(CustomEncryption.Decrypt(Convert.ToString(Session["UserID"]))));
        if (dtMediaContent.Rows.Count > 0)
        {
            for (int i = 0; i < dtMediaContent.Rows.Count; i++)
			{
                string FileType = "";
                if (Convert.ToString(dtMediaContent.Rows[i]["TypeOfFile"]).Trim() == "word")
                    FileType = "Microsoft word document";
                else if (Convert.ToString(dtMediaContent.Rows[i]["TypeOfFile"]).Trim() == "excel")
                    FileType = "Microsoft excel spreadsheet";
                else if (Convert.ToString(dtMediaContent.Rows[i]["TypeOfFile"]).Trim() == "powerpoint")
                    FileType = "Microsoft powerpoint document";
                else if (Convert.ToString(dtMediaContent.Rows[i]["TypeOfFile"]).Trim() == "audio")
                    FileType = "Audio file";
                else if (Convert.ToString(dtMediaContent.Rows[i]["TypeOfFile"]).Trim() == "video")
                    FileType = "Video file";
                else if (Convert.ToString(dtMediaContent.Rows[i]["TypeOfFile"]).Trim() == "image")
                    FileType = "Image file";
                else if (Convert.ToString(dtMediaContent.Rows[i]["TypeOfFile"]).Trim() == "pdf")
                    FileType = "PDF file";

                string Values = Convert.ToString(dtMediaContent.Rows[i]["TypeOfFile"]) + "~" + Convert.ToString(dtMediaContent.Rows[i]["GUID"]);
                html += @"<div style='padding:5px;'><div style='width:85px;float:left;'><img src='../" + dtMediaContent.Rows[i]["IconPath"] + "'/></div><div style='width:380px;float:left;'><label style='color:#0066CC;font-size:16px;'>" + FileType + "</label> <div style='padding-top: 2px; margin-bottom: 2px;clear:both;'></div><label style='color:#808080;font-size:10px;'>" + dtMediaContent.Rows[i]["FormattedCreatedDate"] + "</label> <div style='padding-top: 5px; margin-bottom: 5px;clear:both;'></div><label style='color:#3C3C3C;font-size:12px;'>" + dtMediaContent.Rows[i]["Description"] + "</label></div><div style='width:100px;float:right;text-align:right;padding-top:15px;'><img onclick='MediaLibraryContent(\"" + Values + "\");' src='../images/useBtn.png'/></div></div><div style='padding-top: 5px; margin-bottom: 5px;clear:both;'></div>";
                    
                        
			}
            
        }
        return html;
    }
    [WebMethod(EnableSession = true)]
    public string GetMediaLibraryFileData(string MediaContentGUID)
    {
        string html = "";
        DataTable dtMediaContent = MediaContent.GetMediaContentByFileGUID(MediaContentGUID);
        if (dtMediaContent.Rows.Count > 0)
        {
            html = Convert.ToString(dtMediaContent.Rows[0]["TypeOfFile"]) + "~" + Convert.ToString(dtMediaContent.Rows[0]["FilePath"]) + "~" + Convert.ToString(dtMediaContent.Rows[0]["IconPath"]) + "~"+Convert.ToString(dtMediaContent.Rows[0]["Description"]);            
        }
        return html;
    }
    
}
