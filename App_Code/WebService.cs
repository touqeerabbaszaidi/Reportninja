using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

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
             str += dtReports.Rows[i].Field<string>("Name").ToString() + "," + dtReports.Rows[i].Field<string>("GUID") + "," + dtReports.Rows[i].Field<string>("VersionGUID") + "," + dtReports.Rows[i].Field<string>("TopPageGUID") + "," + CustomEncryption.Decrypt(dtReports.Rows[i].Field<string>("Password")) + "," + dtReports.Rows[i].Field<int>("ReportId") + "," + dtReports.Rows[i].Field<Boolean?>("Locked") + ";";
             
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
    public string DownloadPDF(string reportGuid, string versionGuid, string pageGuid)
    {
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
                
            }
        }
        catch (Exception ex)
        {

        }
        return LoadedHTML;
    }

    
}
