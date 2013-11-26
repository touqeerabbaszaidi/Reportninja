using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Text.RegularExpressions;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PdfSharp.Pdf.Security;
using Pechkin;
using Pechkin.Synchronized;
using Aspose.Pdf.Facades;
using Aspose.Pdf;
using Aspose.Pdf.InteractiveFeatures;
using Aspose.Pdf.DOM;
using Aspose.Pdf.Text;
/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService {
    static string LoadedHTML = "";
    public WebService () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }
  [WebMethod]
    public void ibtnSignUp_Click(string username, string email, string password)
    {
        try
        {
            if (username != "" && email != "" && password != "")
            {
                string GUID = System.Guid.NewGuid().ToString();
                string Status = Users.UserSignUp(GUID, username, email, password, "Trial", 1);
                string[] StatusArray = Status.Split('~');
                if (StatusArray[1] == "Done")
                {
                    string path = Server.MapPath("");
                    path += "/Editor/UserFiles/" + GUID + "/";
                    Directory.CreateDirectory(path + "PDF");
                    Directory.CreateDirectory(path + "Audio");
                    Directory.CreateDirectory(path + "Video");
                    Directory.CreateDirectory(path + "Image");
                    Directory.CreateDirectory(path + "Other");

                    Email objEmail = new Email();
                    string EmailStatus = objEmail.SignUpEmail(GUID, username, email, password);

                    //add email address to aweber-start
                    //AWeberSubscription(email,username);
                    //add email address to aweber-end

                    if (EmailStatus == "Success")
                    {
                        //Response.Redirect("index.html");
                    }
                    else
                    {
                        //ClientScript.RegisterStartupScript(GetType(), "hwa", "alert('" + EmailStatus + "');", true);
                    }
                }
                else if (StatusArray[1] == "Exists")
                {
                    //ClientScript.RegisterStartupScript(GetType(), "hwa", "$.validationEngine.buildPrompt('#ContentPlaceHolder1_txtEmail','A user already exists with this email address. Please choose a different email address.','error')", true);
                }
                else
                {
                    //ClientScript.RegisterStartupScript(GetType(), "hwa", "alert('" + StatusArray[1] + "');", true);
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

  string flag = "false";
  DataTable dtUserLogin;
     [WebMethod]
  public string btnLogIn_Click(string email, string password)
    {
      try
      {
          //mMenu.Style.Add("display", "block");
          dtUserLogin = Users.UserLogin(email, CustomEncryption.Encrypt(password));
          if (dtUserLogin.Rows.Count > 0)
          {
              if (Convert.ToString(dtUserLogin.Rows[0]["Verified"]) == "Yes")
              {
                  if (Convert.ToString(dtUserLogin.Rows[0]["Active"]) == "Yes")
                  {
                      if (Convert.ToInt32(dtUserLogin.Rows[0]["UserTypeID"]) == 2)
                      {
                          /*if ((cbRememberMe.Checked))
                          {
                              // For Saving Cookies //  
                              if (Request.Cookies["RN"] == null)
                              {
                                  Response.Cookies["RN"]["RNE"] = CustomEncryption.Encrypt(this.txtEmail.Value.Trim());
                                  Response.Cookies["RN"]["RNP"] = CustomEncryption.Encrypt(this.txtPassword.Value.Trim());
                                  Response.Cookies["RN"].Expires = DateTime.Now.AddYears(30);
                              }
                              else
                              {
                                  Response.Cookies["RN"]["RNE"] = CustomEncryption.Encrypt(this.txtEmail.Value.Trim());
                                  Response.Cookies["RN"]["RNP"] = CustomEncryption.Encrypt(this.txtPassword.Value.Trim());
                              }
                          }*/
                          //Session.Add("UserID", CustomEncryption.Encrypt(Convert.ToString(dtUserLogin.Rows[0]["UserID"])));
                          //Session.Add("UserEmail", CustomEncryption.Encrypt(Convert.ToString(dtUserLogin.Rows[0]["Email"])));
                          //Session.Add("UserName", CustomEncryption.Encrypt(Convert.ToString(dtUserLogin.Rows[0]["UserName"])));
                          //Session.Add("UserGUID", Convert.ToString(dtUserLogin.Rows[0]["GUID"]));
                          //Users.UpdateLoginDate(Convert.ToString(Session["UserID"]));
                          flag = "true";
                          
                          //Response.Redirect("Home");
                      }
                      else
                      {
                          //lblError.Text = "<br />You are not authorized to login from User's panel.<br />";
                      }
                  }
                  else
                  {
                      //lblError.Text = "<br />Your account has been temporarily suspended. Please contact the administrator.<br />";
                  }
              }
              else
              {
                  //lblError.Text = "<br />Your account is not yet verified. Please verify your account in order to proceed.<br /><a id='lnkResendEmail'>Resend account verification email</a><br />";
              }
          }
          else
          {
              //lblError.Text = "<br />Wrong email or password.<br />";
          }
      }
      catch (Exception ex)
      {

      }
         string str = "";
      if (dtUserLogin.Rows.Count > 0)
      {
          
          for (int i = 0; i < dtUserLogin.Rows.Count; i++)
          {

              str = dtUserLogin.Rows[i].Field<int>("UserID").ToString() + "," + dtUserLogin.Rows[i].Field<string>("UserName").ToString();
          }

          
        //  list.Add(dtUserLogin);
      }

      if (str != "")
      {
          return str;
      }
      else
      {
          str = "false";
          return str;
      }
  }     
    
     DataTable dtReports;
    [WebMethod]
     public string LoadReports(int id)
     {
         try
         {
             //Reports
             dtReports = Reports.GetAllReportsByUserId(id);
             if (dtReports.Rows.Count > 0)
             {
                 //dlReports.DataSource = dtReports;
                 //dlReports.DataBind();
                 //lblPostsStatus.Text = "";

             }
             else
             {
                 //lblPostsStatus.ForeColor = System.Drawing.Color.Gray;
                 //lblPostsStatus.Text = "There are no posts to display.";
                 //dlReports.DataSource = "";
                 //dlReports.DataBind();
             }
         }
         catch (Exception ex)
         {
             //lblError.Text = ex.Message;
         }
         string str = "";
         
         for (int i = 0; i < dtReports.Rows.Count; i++)
         {
             str += dtReports.Rows[i].Field<string>("Name").ToString() + "~" + dtReports.Rows[i].Field<string>("GUID") + "~" + dtReports.Rows[i].Field<string>("VersionGUID") + "~" + dtReports.Rows[i].Field<string>("TopPageGUID") + "~" + CustomEncryption.Decrypt(dtReports.Rows[i].Field<string>("Password")) + "~" + dtReports.Rows[i].Field<int>("ReportId") + "~" + dtReports.Rows[i].Field<Boolean?>("Locked") + "~" + dtReports.Rows[i].Field<int>("ReportID") +"$";
             
         }
         return str;
         
     }

    [WebMethod]

    public string SendEmailReport(string username, int userId, string reportGuid, string versionGuid, string reportName, string topPageGuid, string Emails)
    {
        string EmailStatus = "";
        try
        {
            //string[] EmailAddresses = txtEmailReportEmails.Value.Trim().Split(',');
            //string Emails = "touqeerabbas05@gmail.com";
            string value = "MobileViewer?i=" + reportGuid + "&v=" + versionGuid + "&p=" + topPageGuid;
            
            /*for (int i = 0; i < EmailAddresses.Length; i++)
            {
                Emails += EmailAddresses[i] + ",";
            }
             */
            //Emails = Emails.Remove(Emails.Length - 1, 1);

            //LogActivity.AddLog(Convert.ToInt32(CustomEncryption.Decrypt(Convert.ToString(Session["UserID"]))), reportGuid, versionGuid, null, "Report Sent");

            string Path = Server.MapPath("~/");
            Email objEmail = new Email();
            //string EmailStatus = objEmail.SendPDFReport(CustomEncryption.Decrypt(Convert.ToString(Session["UserName"])), hfFP.Value, Emails, hfRN.Value, Convert.ToInt32(CustomEncryption.Decrypt(Convert.ToString(Session["UserID"]))), hfRG.Value, hfVG.Value, cbAllowEditing.Checked);
            EmailStatus = objEmail.SendPDFReport(username, value , Emails, reportName, userId, reportGuid, versionGuid, true);
          

            if (EmailStatus == "Success")
            {
                //status =  EmailStatus;
                //txtEmailReportEmails.Value = "";
                //lblStatusMessage.Text = "";
                //lblStatusMessage.Text = "Report has been successfully sent.";
                //ClientScript.RegisterStartupScript(GetType(), "hwa", "$('#overlayStatus').fadeIn('fast', function () { $('#boxStatus').animate({ 'top': '160px' }, 500); });", true);
            }
            else
            {
                //ClientScript.RegisterStartupScript(GetType(), "hwa", "alert('" + EmailStatus + "');", true);
            }
        }
        catch (Exception ex)
        {

        }
        return EmailStatus;
        
    }

    [WebMethod]
    public void deleteReport(string reportGuid,string versionGuid) {
    try
        {
            string Status = Versions.DeleteReportVersion(reportGuid, versionGuid);
            if (Status == "Done")
            {
                LogActivity.AddLog(Convert.ToInt32(CustomEncryption.Decrypt(Convert.ToString(Session["UserID"]))), reportGuid, versionGuid, null, "Report Version Deleted");
                //lblStatusMessage.Text = "";
                //lblStatusMessage.Text = "Report has been deleted.";
                //ClientScript.RegisterStartupScript(GetType(), "hwa", "$('#overlayStatus').fadeIn('fast', function () { $('#boxStatus').animate({ 'top': '160px' }, 500); });", true);
            }
            //LoadReports();
        }
        catch (Exception ex)
        {

        }
    
    
    }
    [WebMethod]
    public void EditReport(string reportId,string reportName,string reportPassword)
    {
        try
        {
            //string EncryptedPass="";
            //if (txtEditReportPassword.Value.Trim() == "")
            //{
            //    EncryptedPass = Pass;
            //}
            //else
            //{
            //    EncryptedPass = txtEditReportPassword.Value.Trim();
            //}
            string Status = Reports.EditReport(Convert.ToInt32(reportId), reportName, CustomEncryption.Encrypt(reportPassword));
            if (Status == "Done")
            {
                //LoadReports();
                //txtEditReportName.Value = "";
                //txtEditReportPassword.Value = "";
            }
            else
            {
                //lblErrorEditReport.Text = "An error occured while updating the report. Please try again later.";
                //ClientScript.RegisterStartupScript(GetType(), "hwa", "$('#overlayEditReport').fadeIn('fast', function () { $('#boxEditReport').animate({ 'top': '160px' }, 500); });", true);
            }
        }
        catch (Exception ex)
        {

        }
    }

    [WebMethod]

    public string lockReport(string statusLocked,string reportGuid,string versionGuid,string pageGuid)
    {
        string status = "";
        try
        {
            if (statusLocked == "True")
            {

                //status = "This report has been locked and cannot be edited";
                status = "This report has been locked and cannot be edited.";

                //lblStatusMessage.Text = "";
                //lblStatusMessage.Text = "This report has been locked and cannot be edited. Please <a href='NewVersion?i=" + lblReportGUID.Text + "&v=" + lblVersionGUID.Text + "' ><span style='color:#0066CC;font-weight:bold;'>click here</span></a> to create a new version of this report.";
                //ClientScript.RegisterStartupScript(GetType(), "hwa", "$('#overlayStatus').fadeIn('fast', function () { $('#boxStatus').animate({ 'top': '160px' }, 500); });", true);
            }
            else
            {
                LoadedHTML = Pages.LoadPageHTML(reportGuid, versionGuid, pageGuid);
                if (LoadedHTML == "Done")
                {
                    LoadedHTML = "";
                }
                if (LoadedHTML == "")
                {
                    status = "Please create a report in order to lock it.";
                    //lblStatusMessage.Text = "";
                    //lblStatusMessage.Text = "Please create a report in order to lock it.";
                    //ClientScript.RegisterStartupScript(GetType(), "hwa", "$('#overlayStatus').fadeIn('fast', function () { $('#boxStatus').animate({ 'top': '160px' }, 500); });", true);
                }
                else
                {
                    string Status = Versions.LockReportVersion(reportGuid, versionGuid);
                    if (Status == "Done")
                    {
                        status = "Report has been locked.";
                        //LogActivity.AddLog(Convert.ToInt32($.Storage.get("id")), reportGuid,versionGuid, null, "Report Version Locked");
                        //lblStatusMessage.Text = "";
                        //lblStatusMessage.Text = "Report has been locked.";
                        //ClientScript.RegisterStartupScript(GetType(), "hwa", "$('#overlayStatus').fadeIn('fast', function () { $('#boxStatus').animate({ 'top': '160px' }, 500); });", true);
                    }
                    //LoadReports();
                }
            }
        }
        catch (Exception ex)
        {

        }
        return status;
    
    }

    public string logReport(string statusLocked, string reportGuid, string versionGuid, string pageGuid)
    {
        string status = "";
        try
        {
            if (statusLocked == "True")
            {

                //status = "This report has been locked and cannot be edited";
                status = "This report has been locked and cannot be edited.";

                //lblStatusMessage.Text = "";
                //lblStatusMessage.Text = "This report has been locked and cannot be edited. Please <a href='NewVersion?i=" + lblReportGUID.Text + "&v=" + lblVersionGUID.Text + "' ><span style='color:#0066CC;font-weight:bold;'>click here</span></a> to create a new version of this report.";
                //ClientScript.RegisterStartupScript(GetType(), "hwa", "$('#overlayStatus').fadeIn('fast', function () { $('#boxStatus').animate({ 'top': '160px' }, 500); });", true);
            }
            else
            {
                LoadedHTML = Pages.LoadPageHTML(reportGuid, versionGuid, pageGuid);
                if (LoadedHTML == "Done")
                {
                    LoadedHTML = "";
                }
                if (LoadedHTML == "")
                {
                    status = "Please create a report in order to lock it.";
                    //lblStatusMessage.Text = "";
                    //lblStatusMessage.Text = "Please create a report in order to lock it.";
                    //ClientScript.RegisterStartupScript(GetType(), "hwa", "$('#overlayStatus').fadeIn('fast', function () { $('#boxStatus').animate({ 'top': '160px' }, 500); });", true);
                }
                else
                {
                    string Status = Versions.LockReportVersion(reportGuid, versionGuid);
                    if (Status == "Done")
                    {
                        status = "Report has been locked.";
                        //LogActivity.AddLog(Convert.ToInt32($.Storage.get("id")), reportGuid,versionGuid, null, "Report Version Locked");
                        //lblStatusMessage.Text = "";
                        //lblStatusMessage.Text = "Report has been locked.";
                        //ClientScript.RegisterStartupScript(GetType(), "hwa", "$('#overlayStatus').fadeIn('fast', function () { $('#boxStatus').animate({ 'top': '160px' }, 500); });", true);
                    }
                    //LoadReports();
                }
            }
        }
        catch (Exception ex)
        {

        }
        return status;

    }

    
    //public void insertUsers(string username, string email, string password)
    //{
    //    try
    //    {
    //        if (username != "" && email != "" && password != "")
    //        {
    //            string GUID = System.Guid.NewGuid().ToString();
    //            string Status = Users.UserSignUp(GUID, username, email, password , "Trial", 1);
    //            string[] StatusArray = Status.Split('~');
    //            if (StatusArray[1] == "Done")
    //            {
    //                string path = Server.MapPath("");
    //                path += "/Editor/UserFiles/" + GUID + "/";
    //                Directory.CreateDirectory(path + "PDF");
    //                Directory.CreateDirectory(path + "Audio");
    //                Directory.CreateDirectory(path + "Video");
    //                Directory.CreateDirectory(path + "Image");
    //                Directory.CreateDirectory(path + "Other");

    //                Email objEmail = new Email();
    //                string EmailStatus = objEmail.SignUpEmail(GUID, username, email, password);

    //                //add email address to aweber-start
    //                AWeberSubscription(email, username);
    //                //add email address to aweber-end

    //                if (EmailStatus == "Success")
    //                {                        
    //                    //Response.Redirect("Status?i=1");
    //                }
    //                else
    //                {
    //                    //ClientScript.RegisterStartupScript(GetType(), "hwa", "alert('" + EmailStatus + "');", true);
    //                }
    //            }
    //            else if (StatusArray[1] == "Exists")
    //            {
    //                //ClientScript.RegisterStartupScript(GetType(), "hwa", "$.validationEngine.buildPrompt('#ContentPlaceHolder1_txtEmail','A user already exists with this email address. Please choose a different email address.','error')", true);
    //            }
    //            else
    //            {
    //                //ClientScript.RegisterStartupScript(GetType(), "hwa", "alert('" + StatusArray[1] + "');", true);
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //    }    
    //}

    //protected void AWeberSubscription(string email, string name)
    //{
    //    String consumerKey = "AkFuw03LfXkUt5SW0i3WmGnc";
    //    String consumerSecret = "YtmQ09RdIlAjB5uSsyalQSgEhxukuXctGGABAIE4";

    //    API api = new API(consumerKey, consumerSecret);

    //    api.OAuthToken = "AgcgPvSjae89yfW3zi2QXJwh";
    //    api.OAuthTokenSecret = "TtGTLVxiNarn3efut37Yu23ZkYe58hObusqsOVX9";
    //    // Get the returned oauth_verifier
    //    api.OAuthVerifier = "obg10l";
    //    // Get your account
    //    Account account = api.getAccount();

    //    // Loop through all lists in your account
    //    foreach (List list in account.lists().entries)
    //    {
    //        BaseCollection<Subscriber> target = list.subscribers();
    //        SortedList<string, object> parameters = new SortedList<string, object>();

    //        parameters.Add("email", email);

    //        parameters.Add("name", name);

    //        if (list.name == "reportninja")
    //        {
    //            Subscriber subscriber = target.create(parameters);
    //        }
    //    }
    //}
    [WebMethod]
    public string DownloadPDF(string reportGuid, string versionGuid, string pageGuid, int ReportID, int userId)
    {
        string PageHtml = "";
        try
        {
            
            LoadedHTML = Pages.LoadPageHTML(reportGuid, versionGuid, pageGuid);
            
            if (LoadedHTML == "Done")
            {
                LoadedHTML = "";
            }
            if (LoadedHTML == "")
            {
               // lblStatusMessage.Text = "";
                //lblStatusMessage.Text = "Please create a report in order to generate a PDF.";
                //ClientScript.RegisterStartupScript(GetType(), "hwa", "$('#overlayStatus').fadeIn('fast', function () { $('#boxStatus').animate({ 'top': '160px' }, 500); });", true);
            }
            else
            {
                //HiddenPagesHTML.InnerHtml = lblPagesHTML.Text.Trim();
                string Values = reportGuid + "~" + versionGuid + "~" + pageGuid;
               // Page.ClientScript.RegisterStartupScript(GetType(), "Script", "GeneratePDF(\'" + Values + "\');", true);
                DataTable dtpageHtml = Reports.GetReportsPageHTML(userId, ReportID);
                if(dtpageHtml.Rows.Count>0)
                { 
                PageHtml = Convert.ToString(dtpageHtml.Rows[0]["PagesHTML"]);
                }  
               
            }
        }
        catch (Exception ex)
        {

        }
        return PageHtml.Trim();
    }
    [WebMethod]
    public string FnChangePassword(string Id, string changePassword, string Email, string username)
    {
        string Status = "";
        try
        {
            Status = Users.ChangePassword(Id, changePassword.Trim());
            if (Status == "Done")
            {
                // For Saving Cookies //  
                //if (Request.Cookies["RN"] != null)
                //{
                //    Response.Cookies["RN"]["RNP"] = CustomEncryption.Encrypt(txtChangePassword.Value.Trim());
                //}
               // Email objEmail = new Email();
                //string EmailStatus = objEmail.ChangedPasswordEmail(username, Email, changePassword.Trim());
                //if (EmailStatus == "Success")
                //{
                //    //Response.Redirect("Status?i=3");
                //}
                //else
                //{
                //   // ClientScript.RegisterStartupScript(GetType(), "hwa", "alert('" + EmailStatus + "');", true);
                //}
                //txtChangePassword.Value = "";
                //txtConfirmChangePassword.Value = "";
            }
        }
        catch (Exception ex)
        {

        }
        return Status;
    }
    [WebMethod]
    public string ChangeUserEmail(string Id, string username, string ChangedEmail, string OldEmail)
    {
        string Status = "";
        try
        {
            Status = Users.ChangeEmail(Id, ChangedEmail.Trim());
            if (Status == "Done")
            {
                DataTable dtuser = Users.GetUserDataByEmail(ChangedEmail);
                if(dtuser.Rows.Count>0)
                {
                    string userGuid = Convert.ToString(dtuser.Rows[0]["GUID"]);
                    Email objEmail = new Email();
                    string EmailStatus = objEmail.ChangedEmailAddress(userGuid, username, ChangedEmail.Trim());
                    //if (EmailStatus == "Success")
                    //{
                    //   // Response.Redirect("Status?i=3");
                    //}
                    //else
                    //{
                    //    //ClientScript.RegisterStartupScript(GetType(), "hwa", "alert('" + EmailStatus + "');", true);
                    //}
                }
                
                //txtChangeEmail.Value = "";
                //Session.Remove("UserID");
                //Session.Remove("UserEmail");
                //Session.Remove("UserName");
                //Session.Remove("UserGUID");
                //Session.Abandon();
                //Response.Cookies["RN"].Expires = DateTime.Now.AddYears(-30);
                
            }
            else if (Status == "Exists")
            {
                //lblErrorEmail.Text = "A user already exists with this email address. Please choose a different email address.";
                //ClientScript.RegisterStartupScript(GetType(), "hwa", "$('#overlayEmail').fadeIn('fast', function () { $('#boxEmail').animate({ 'top': '160px' }, 500); });", true);
            }
            else
            {
                //ClientScript.RegisterStartupScript(GetType(), "hwa", "alert('" + Status + "');", true);
            }
            return Status;
        }
        catch (Exception ex)
        {

        }
        return Status;
    }
    [WebMethod]
    public string ChangeUserName(string Id, string ChangedUserName)
    {
        string Status = "";
        try
        {
            Status = Users.ChangeUsername(Id, ChangedUserName.Trim());
            if (Status == "Done")
            {
               // lblUsername.Text = txtChangeUsername.Value.Trim();
                //Session["UserName"] = CustomEncryption.Encrypt(txtChangeUsername.Value.Trim());
                //txtChangeUsername.Value = "";
            }
            return Status;
        }
        catch (Exception ex)
        {

        }
        return Status;
    }
[WebMethod]
    public void UpdatePhAndAddressChanges(string Id, string PhoneNo, string Address)
       {
           try
           {
               string Status = Users.ChangeAccountInfo(Id, PhoneNo.Trim(), Address.Trim());
           }
           catch (Exception ex)
           {

           }
       }
    [WebMethod]
public string GeneratePDPFile(string GUID, string pageGUID, string EmailLogGUID)

    {
    if (GUID != null || pageGUID != null || GUID!="" || pageGUID!="")
    {
        try
        {
            string[] stringSeparators = new string[] { "~^~" };
            DataTable dtBookmarks = BookmarkRecord.GetBookmarkRecordByGUID(GUID);
            if (dtBookmarks.Rows.Count > 0)
            {
                string N1 = "", N2 = "", N3 = "", PageIds = Convert.ToString(dtBookmarks.Rows[0]["PageIds"]), PageGUIDs = Convert.ToString(dtBookmarks.Rows[0]["PageGUIDs"]), ReportGUID = Convert.ToString(dtBookmarks.Rows[0]["ReportGUID"]), VersionGUID = Convert.ToString(dtBookmarks.Rows[0]["VersionGUID"]), PageGUID = pageGUID, PagesHTML = Convert.ToString(dtBookmarks.Rows[0]["PagesHTML"]);

                try
                {
                    //Increment downloads in EmailLog
                    if (EmailLogGUID != null)
                    {
                        if (EmailLogGUID != "" && EmailLogGUID != "undefined")
                        {
                            EmailLog.IncrementDownloads(EmailLogGUID);
                        }
                    }
                    //Load HTML
                    LoadedHTML = Pages.LoadPageHTML(ReportGUID, VersionGUID, PageGUID);
                    if (LoadedHTML == "Done")
                    {
                        LoadedHTML = "";
                    }
                    if (LoadedHTML == "")
                    {
                        //lblStatusMessage.Text = "";
                        //lblStatusMessage.Text = "Please create a report in order to generate a PDF.";
                        //ClientScript.RegisterStartupScript(GetType(), "hwa", "$('#overlayStatus').fadeIn('fast', function () { $('#boxStatus').animate({ 'top': '160px' }, 500); });", true);
                    }
                    else
                    {
                        string PageName = "";
                        string PageHTML = "";
                        string FilePaths = "";
                        string UserGUID = "";
                        string ReportName = "";
                        string ReportPassword = "";
                        string ButtonHtml = PagesHTML;
                        string[] Split1 = Regex.Split(ButtonHtml, "InnerLi\"");
                        for (int i = 1; i < Split1.Length; i++)
                        {
                            string[] Split2 = Regex.Split(Split1[i], "value=\"");
                            string[] Split3 = Regex.Split(Split2[1], "\"");

                            //Getting data by PageGUID which im receiving in Split3[0]
                            DataTable dtPageData = Pages.GetDataByGUID(ReportGUID, VersionGUID, Split3[0]);
                            if (dtPageData.Rows.Count > 0)
                            {
                                PageName += Convert.ToString(dtPageData.Rows[0]["Name"]) + "~^~";
                                PageHTML = Convert.ToString(dtPageData.Rows[0]["HTML"]);
                                string DecodedHTML = System.Web.HttpUtility.HtmlDecode(PageHTML);
                                PageHTML = DecodedHTML;
                                UserGUID = Convert.ToString(dtPageData.Rows[0]["UserGUID"]);
                                ReportName = Convert.ToString(dtPageData.Rows[0]["ReportName"]);
                                ReportPassword = Convert.ToString(dtPageData.Rows[0]["ReportPassword"]);

                                // create global configuration object
                                GlobalConfig gc = new GlobalConfig();

                                // set it up using fluent notation
                                gc.SetMargins(new Margins(47, 47, 47, 47))
                                  .SetDocumentTitle(ReportName)
                                  .SetPaperSize(PaperKind.A4);
                                //... etc

                                // create converter
                                //IPechkin pechkin = new SynchronizedPechkin(gc);
                                SynchronizedPechkin pechkin = new SynchronizedPechkin(gc);

                                //string ReportHtml = LoadedHTML;
                                string ReportHtml = PageHTML;
                                ReportHtml = ReportHtml.Replace("\"", "\'");

                                string RemoveDelBtn = Regex.Replace(ReportHtml, "<input class='delBtn'.+?return false;' type='button'>", string.Empty);
                                string FixFilePath = RemoveDelBtn.Replace("src='UserFiles/", "src='http://www.reportninja.org/Editor/UserFiles/");
                                FixFilePath = FixFilePath.Replace("href='UserFiles/", "href='http://www.reportninja.org/Editor/UserFiles/");
                                FixFilePath = FixFilePath.Replace("src='images/audio.png", "src='http://www.reportninja.org/Editor/images/audio.png");
                                FixFilePath = FixFilePath.Replace("src='images/video.png", "src='http://www.reportninja.org/Editor/images/video.png");
                                FixFilePath = FixFilePath.Replace("src='images/pdf_icon48.png", "src='http://www.reportninja.org/Editor/images/pdf_icon48.png");
                                FixFilePath = FixFilePath.Replace("src='images/word_icon48.png", "src='http://www.reportninja.org/Editor/images/word_icon48.png");
                                FixFilePath = FixFilePath.Replace("src='images/excel_icon48.png", "src='http://www.reportninja.org/Editor/images/excel_icon48.png");
                                FixFilePath = FixFilePath.Replace("src='images/powerpoint_icon48.png", "src='http://www.reportninja.org/Editor/images/powerpoint_icon48.png");
                                FixFilePath = FixFilePath.Replace("../PlayVideo?i=UserFiles", "http://www.reportninja.org/PlayVideo?i=UserFiles");
                                FixFilePath = FixFilePath.Replace("../PlayAudio?i=UserFiles", "http://www.reportninja.org/PlayAudio?i=UserFiles");
                                //string FixFilePath = RemoveDelBtn.Replace("src='UserFiles/", "src='http://localhost:12759/Website/Editor/UserFiles/");            
                                string FinalHtml = FixFilePath;
                                //Empty check
                                if (FinalHtml == "")
                                { FinalHtml = "Black Page"; }

                                byte[] buf = pechkin.Convert(new ObjectConfig().SetPrintBackground(true).SetAllowLocalContent(true).SetIncludeInOutline(true), FinalHtml);

                                try
                                {
                                    string GUId = System.Guid.NewGuid().ToString();
                                    string path = Server.MapPath(@"~/Editor/UserFiles/" + UserGUID + "/PDF/");
                                    string fn = path + GUId + ".pdf";
                                    string FilePath = "Editor/UserFiles/" + UserGUID + "/PDF/" + GUId + ".pdf";
                                    if (!System.IO.Directory.Exists(path))
                                        System.IO.Directory.CreateDirectory(path);
                                    FilePaths += FilePath + "~^~";
                                    FileStream fs = new FileStream(fn, FileMode.Create);
                                    fs.Write(buf, 0, buf.Length);
                                    fs.Close();
                                    System.Threading.Thread.Sleep(1000);
                                }
                                catch (Exception ex) { throw ex; }
                            }
                        }
                        PageName = PageName.Remove(PageName.Length - 3, 3);
                        //string[] ArrPageName = PageName.Split(',');                        
                        string[] ArrPageName = PageName.Split(stringSeparators, StringSplitOptions.None);
                        FilePaths = FilePaths.Remove(FilePaths.Length - 3, 3);
                        string MergedPDFGUID = System.Guid.NewGuid().ToString();
                        string ReportNameTimeStamp = ReportName + "-" + System.DateTime.Now.ToString("ddMMyyhhmmss");
                        string MergedPDFPath = Server.MapPath("~/");
                        string MergedPDFStaticPath = "Editor/UserFiles/" + UserGUID + "/PDF/" + ReportNameTimeStamp + ".pdf";
                        string MergedOutputFilePath = MergedPDFPath + MergedPDFStaticPath;

                        //string[] pdfFiles = new string[2];
                        //string[] pdfFiles = FilePaths.Split(',');
                        string[] pdfFiles = FilePaths.Split(stringSeparators, StringSplitOptions.None);
                        for (int i = 0; i < pdfFiles.Length; i++)
                        {
                            pdfFiles[i] = MergedPDFPath + pdfFiles[i];
                        }

                        //calculate each pdfs path,total pages,1st page number after being merged
                        string PDFFilePath = "";
                        string PDFFilePageCount = "";
                        string PDFFileMainPage = "";
                        int MainPageCount = 0;
                        foreach (string pdfFile in pdfFiles)
                        {
                            PdfDocument inputPDFDocument = PdfReader.Open(pdfFile, PdfDocumentOpenMode.Import);
                            int PageCount = 0;
                            foreach (PdfPage page in inputPDFDocument.Pages)
                            {
                                if (PageCount == 0)
                                {
                                    PDFFileMainPage += MainPageCount + "~^~";
                                }
                                MainPageCount++;
                                PageCount++;
                            }
                            PDFFilePageCount += PageCount + "~^~";
                            PDFFilePath += pdfFile + "~^~";
                        }
                        PDFFilePath = PDFFilePath.Remove(PDFFilePath.Length - 3, 3);
                        PDFFilePageCount = PDFFilePageCount.Remove(PDFFilePageCount.Length - 3, 3);
                        PDFFileMainPage = PDFFileMainPage.Remove(PDFFileMainPage.Length - 3, 3);
                        //string[] ArrPDFFilePath = PDFFilePath.Split(',');
                        //string[] ArrPDFFilePageCount = PDFFilePageCount.Split(',');
                        //string[] ArrPDFFileMainPage = PDFFileMainPage.Split(',');
                        string[] ArrPDFFilePath = PDFFilePath.Split(stringSeparators, StringSplitOptions.None);
                        string[] ArrPDFFilePageCount = PDFFilePageCount.Split(stringSeparators, StringSplitOptions.None);
                        string[] ArrPDFFileMainPage = PDFFileMainPage.Split(stringSeparators, StringSplitOptions.None);

                        //Merge all the docs into one single doc
                        PdfDocument outputPDFDocument = new PdfDocument();

                        foreach (string pdfFile in pdfFiles)
                        {
                            PdfDocument inputPDFDocument = PdfReader.Open(pdfFile, PdfDocumentOpenMode.Import);

                            outputPDFDocument.Version = inputPDFDocument.Version;
                            foreach (PdfPage page in inputPDFDocument.Pages)
                            {
                                outputPDFDocument.AddPage(page);
                            }
                        }
                        outputPDFDocument.Save(MergedOutputFilePath);
                        System.Threading.Thread.Sleep(1000);

                        //Adding bookmarks to merged document using Aspose
                        //PDF File path
                        Document pdfDocument = new Document(MergedOutputFilePath);
                        int totalpages = pdfDocument.Pages.Count;

                        //settings
                        pdfDocument.CenterWindow = true;
                        pdfDocument.DisplayDocTitle = false;
                        pdfDocument.OptimizeSize = false;
                        pdfDocument.FitWindow = true;
                        pdfDocument.NonFullScreenPageMode = PageMode.UseOutlines;
                        pdfDocument.PageLayout = PageLayout.SinglePage;

                        int TotalPageCount = 0;

                        #region BookMarks start

                        string[] SplitAllGUIDs = PageGUIDs.Split('^');

                        OutlineItemCollection pdfOutline = new OutlineItemCollection(pdfDocument.Outlines); ;
                        OutlineItemCollection pdfChildOutline = new OutlineItemCollection(pdfDocument.Outlines);
                        OutlineItemCollection pdfChildOutline1 = new OutlineItemCollection(pdfDocument.Outlines);
                        for (int j = 0; j < SplitAllGUIDs.Length; j++)
                        {
                            string[] SplitGUIDs = SplitAllGUIDs[j].Split('~');

                            if (SplitGUIDs[0] == N1)
                            {
                                if (SplitGUIDs[1] == N2)
                                {
                                    //2nd child child bookmark object
                                    int MainPageIndex3 = Convert.ToInt32(ArrPDFFileMainPage[TotalPageCount]);
                                    string FilePageName3 = ArrPageName[TotalPageCount];
                                    pdfChildOutline1 = new OutlineItemCollection(pdfDocument.Outlines);
                                    pdfChildOutline1.Title = FilePageName3;
                                    pdfChildOutline1.Action = new GoToAction(pdfDocument.Pages[MainPageIndex3 + 1]);
                                    pdfChildOutline.Add(pdfChildOutline1);

                                    N3 = SplitGUIDs[2];
                                    TotalPageCount++;
                                    //TotalPageCount =TotalPageCount+ 2;
                                }
                                else
                                {
                                    //1st child child bookmark object
                                    int MainPageIndex2 = Convert.ToInt32(ArrPDFFileMainPage[TotalPageCount]);
                                    string FilePageName2 = ArrPageName[TotalPageCount];
                                    pdfChildOutline = new OutlineItemCollection(pdfDocument.Outlines);
                                    pdfChildOutline.Title = FilePageName2;
                                    pdfChildOutline.Action = new GoToAction(pdfDocument.Pages[MainPageIndex2 + 1]);
                                    pdfOutline.Add(pdfChildOutline);

                                    N2 = SplitGUIDs[1];
                                    N3 = SplitGUIDs[2];
                                    TotalPageCount++;

                                    if (N3 != "0")
                                    {
                                        //2nd child child bookmark object
                                        int MainPageIndex3 = Convert.ToInt32(ArrPDFFileMainPage[TotalPageCount]);
                                        string FilePageName3 = ArrPageName[TotalPageCount];
                                        pdfChildOutline1 = new OutlineItemCollection(pdfDocument.Outlines);
                                        pdfChildOutline1.Title = FilePageName3;
                                        pdfChildOutline1.Action = new GoToAction(pdfDocument.Pages[MainPageIndex3 + 1]);
                                        pdfChildOutline.Add(pdfChildOutline1);

                                        N3 = SplitGUIDs[2];
                                        TotalPageCount++;
                                    }
                                }
                            }
                            else
                            {
                                //Parent bookmark object
                                int MainPageIndex1 = Convert.ToInt32(ArrPDFFileMainPage[TotalPageCount]);
                                string FilePageName1 = ArrPageName[TotalPageCount];
                                pdfOutline = new OutlineItemCollection(pdfDocument.Outlines);
                                pdfOutline.Title = FilePageName1;
                                pdfOutline.Action = new GoToAction(pdfDocument.Pages[MainPageIndex1 + 1]);
                                pdfDocument.Outlines.Add(pdfOutline);

                                N1 = SplitGUIDs[0];
                                TotalPageCount++;

                                if (SplitGUIDs[1] != "0")
                                {
                                    //1st child child bookmark object
                                    int MainPageIndex2 = Convert.ToInt32(ArrPDFFileMainPage[TotalPageCount]);
                                    string FilePageName2 = ArrPageName[TotalPageCount];
                                    pdfChildOutline = new OutlineItemCollection(pdfDocument.Outlines);
                                    pdfChildOutline.Title = FilePageName2;
                                    pdfChildOutline.Action = new GoToAction(pdfDocument.Pages[MainPageIndex2 + 1]);
                                    pdfOutline.Add(pdfChildOutline);

                                    N2 = SplitGUIDs[1];
                                    TotalPageCount++;

                                    if (SplitGUIDs[2] != "0")
                                    {
                                        //2nd child child bookmark object
                                        int MainPageIndex3 = Convert.ToInt32(ArrPDFFileMainPage[TotalPageCount]);
                                        string FilePageName3 = ArrPageName[TotalPageCount];
                                        pdfChildOutline1 = new OutlineItemCollection(pdfDocument.Outlines);
                                        pdfChildOutline1.Title = FilePageName3;
                                        pdfChildOutline1.Action = new GoToAction(pdfDocument.Pages[MainPageIndex3 + 1]);
                                        pdfChildOutline.Add(pdfChildOutline1);

                                        N3 = SplitGUIDs[2];
                                        TotalPageCount++;
                                    }
                                }
                            }
                        }

                        #endregion BookMark End
                        //save output
                        pdfDocument.Save(MergedOutputFilePath);

                        //Add page stamp
                        //open document
                        Document MergedDocument = new Document(MergedOutputFilePath);




                        //create page number stamp                        
                        //open document
                        PdfFileStamp fileStamp = new PdfFileStamp(MergedOutputFilePath, MergedOutputFilePath);

                        //get total number of pages
                        int totalPages = new PdfFileInfo(MergedOutputFilePath).NumberOfPages;
                        //create formatted text for page number
                        FormattedText formattedText = new FormattedText("Page # Of " + totalPages, System.Drawing.Color.Gray, System.Drawing.Color.White, Aspose.Pdf.Facades.FontStyle.Courier, EncodingType.Winansi, false, 12);

                        //set starting number for first page; you might want to start from 2 or more
                        fileStamp.StartingNumber = 1;
                        //add page number
                        fileStamp.AddPageNumber(formattedText, 1, 15, 15, 15, 15);

                        //save updated PDF file
                        fileStamp.Close();




                        //Adding Password to the document
                        PdfDocument MergedPDFDocument = PdfReader.Open(MergedOutputFilePath, PdfDocumentOpenMode.Modify);
                        PdfSecuritySettings securitySettings = MergedPDFDocument.SecuritySettings;

                        securitySettings.UserPassword = CustomEncryption.Decrypt(ReportPassword);
                        securitySettings.OwnerPassword = CustomEncryption.Decrypt(ReportPassword);

                        // Restrict some rights.
                        securitySettings.PermitAccessibilityExtractContent = false;
                        securitySettings.PermitAnnotations = false;
                        securitySettings.PermitAssembleDocument = false;
                        securitySettings.PermitExtractContent = false;
                        securitySettings.PermitFormsFill = false;
                        securitySettings.PermitFullQualityPrint = false;
                        securitySettings.PermitModifyDocument = false;
                        securitySettings.PermitPrint = false;
                        MergedPDFDocument.Save(MergedOutputFilePath);

                        //Delete separate files after getting merged
                        for (int i = 0; i < pdfFiles.Length; i++)
                        {
                            File.Delete(pdfFiles[i]);
                        }

                        //Downloading single merged file
                        // Response.Redirect("Download.ashx?FileName=" + MergedPDFStaticPath);
                        return MergedPDFStaticPath;
                    }
                    
                }
                catch (Exception ex)
                {

                }
            }
        }
        catch (Exception ex)
        {

        }

    }
    return "";

}
    
}
