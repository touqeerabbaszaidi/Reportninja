using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Net.Mail;
using System.Net;
using System.IO;
//using System.Xml.Linq;

/// <summary>
/// Summary description for Email
/// </summary>
public class Email
{
    public Email()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string SignUpEmail(string GUID, string UserName, string EmailAddress, string Password)
    {
        //wwlllandlord@yahoo.com beenish@lylasolutions.com
        //wwll123 Beenish1

        System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
        client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
        //client.EnableSsl = false;
        //client.Host = "relay-hosting.secureserver.net";
        //client.Port = 25;                 // setup Smtp authentication    
        client.EnableSsl = false;
        client.Host = "mail.lylasolutions.com";
        client.Port = 26;                 // setup Smtp authentication             
        String email = "beenish@lylasolutions.com";
        String password = "Beenish1";
        System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(email, password);
        client.UseDefaultCredentials = false;
        client.Credentials = credentials;
        System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
        msg.From = new System.Net.Mail.MailAddress("beenish@lylasolutions.com");
        msg.To.Add(new System.Net.Mail.MailAddress(EmailAddress));
        msg.Subject = "Report Ninja: Verify your account";
        msg.IsBodyHtml = true;
        msg.Body = string.Format("<html><head></head><body><table width='860px'><tr><td align='center'><a href='http://www.reportninja.org' target='_blank'><img alt='' src='http://www.reportninja.org/images/Email/Header.png'/></a></td></tr><tr><td align='left' style='font-family: verdana; font-size: 15px; color: #333333;'>Dear <b>" + UserName + "</b><br/><br/>Thank you for creating a Report Ninja account. Please click on the activate button below to activate your account.<br></td></tr><tr><td align='center'><br/><a href='http://www.reportninja.org/Status?i=2&c=" + GUID + "'><img alt='' src='http://www.reportninja.org/images/Email/Activate.jpg'/></a><br/></td></tr><tr><td style='font-family: verdana; font-size: 12px; font-style: italic; color: #333333;'><br/>Email:&nbsp;" + EmailAddress + "</td></tr><tr><td style='font-family: verdana; font-size: 12px; font-style: italic; color: #333333;'>Password:&nbsp;" + Password + "<br/></td></tr><tr><td align='center'><br/><img alt='' src='http://www.reportninja.org/images/Email/Footer.png'/><br/></td></tr><tr><td style='font-family: verdana; font-size: 12px; font-style: italic; color: #333333;'><br/>This email is sent to you automatically by Report Ninja.</td></tr></table></body></html>");
        object userState = msg;
        try
        {
            //event handler for asynchronous call
            //client.SendCompleted += smtpClient_OnCompleted;
            //client.SendAsync(msg, userState);
            client.Send(msg);
            return "Success";
        }
        catch (Exception ex)
        {
            return Convert.ToString(ex.Message);
        }
    }
    public string ForgottonPassword(string Email, string Password, string UserName)
    {
        //wwlllandlord@yahoo.com beenish@lylasolutions.com
        //wwll123 Beenish1

        System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
        client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
        //client.EnableSsl = false;
        //client.Host = "relay-hosting.secureserver.net";
        //client.Port = 25;                 // setup Smtp authentication     
        client.EnableSsl = false;
        client.Host = "mail.lylasolutions.com";
        client.Port = 26;                 // setup Smtp authentication             
        String email = "beenish@lylasolutions.com";
        String password = "Beenish1";
        System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(email, password);
        client.UseDefaultCredentials = false;
        client.Credentials = credentials;
        System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
        msg.From = new System.Net.Mail.MailAddress("beenish@lylasolutions.com");
        msg.To.Add(new System.Net.Mail.MailAddress(Email));
        msg.Subject = "Report Ninja: Forgotton Password";
        msg.IsBodyHtml = true;
        msg.Body = string.Format("<html><head></head><body><table width='860px'><tr><td align='center'><a href='http://www.reportninja.org' target='_blank'><img alt='' src='http://www.reportninja.org/images/Email/Header.png'/></a></td></tr><tr><td align='left' style='font-family: verdana; font-size: 15px; color: #333333;'>Dear <b>" + UserName + "</b><br/><br/>Thank you for using Report Ninja. Your login credentials are listed below.<br></td></tr><tr><td style='font-family: verdana; font-size: 15px; font-style: italic; color: #333333;'><br/><b>Email:</b>&nbsp;" + Email + "</td></tr><tr><td style='font-family: verdana; font-size: 15px; font-style: italic; color: #333333;'><b>Password:</b>&nbsp;" + Password + "<br/></td></tr><tr><td align='center'><br/><img alt='' src='http://www.reportninja.org/images/Email/Footer.png'/><br/></td></tr><tr><td style='font-family: verdana; font-size: 12px; font-style: italic; color: #333333;'><br/>This email is sent to you automatically by Report Ninja.</td></tr></table></body></html>");
        object userState = msg;
        try
        {
            //event handler for asynchronous call
            //client.SendCompleted += smtpClient_OnCompleted;
            //client.SendAsync(msg, userState);
            client.Send(msg);
            return "Success";
        }
        catch (Exception ex)
        {
            return Convert.ToString(ex.Message);
        }
    }
    public string AdminEmailToIndividualUser(string UserName, string EmailAddress, string Subject, string Message)
    {
        //wwlllandlord@yahoo.com beenish@lylasolutions.com
        //wwll123 Beenish1

        System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
        client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
        //client.EnableSsl = false;
        //client.Host = "relay-hosting.secureserver.net";
        //client.Port = 25;                 // setup Smtp authentication 
        client.EnableSsl = false;
        client.Host = "mail.lylasolutions.com";
        client.Port = 26;                 // setup Smtp authentication             
        String email = "beenish@lylasolutions.com";
        String password = "Beenish1";
        System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(email, password);
        client.UseDefaultCredentials = false;
        client.Credentials = credentials;
        System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
        msg.From = new System.Net.Mail.MailAddress("beenish@lylasolutions.com");
        msg.To.Add(new System.Net.Mail.MailAddress(EmailAddress));
        msg.Subject = "Report Ninja: " + Subject;
        msg.IsBodyHtml = true;
        msg.Body = string.Format("<html><head></head><body><table width='860px'><tr><td align='center'><a href='http://www.reportninja.org' target='_blank'><img alt='' src='http://www.reportninja.org/images/Email/Header.png'/></a></td></tr><tr><td align='left' style='font-family: verdana; font-size: 15px; color: #333333;'>Dear <b>" + UserName + "</b><br/><br/>" + Message + "<br></td></tr><tr><td align='center'><br/><img alt='' src='http://www.reportninja.org/images/Email/Footer.png'/><br/></td></tr><tr><td style='font-family: verdana; font-size: 12px; font-style: italic; color: #333333;'><br/>This email is sent to you automatically by Report Ninja.</td></tr></table></body></html>");
        //object userState = msg;
        try
        {
            //event handler for asynchronous call
            //client.SendCompleted += smtpClient_OnCompleted;
            //client.SendAsync(msg, userState);
            client.Send(msg);
            return "Success";
        }
        catch (Exception ex)
        {
            return Convert.ToString(ex.Message);
        }
    }
    public string AdminEmailToAllUsers(string Subject, string Message)
    {
        //wwlllandlord@yahoo.com beenish@lylasolutions.com
        //wwll123 Beenish1
        string UserName, EmailAddress;

        DataTable dtUsers = Users.AllUsersData();
        if (dtUsers.Rows.Count > 0)
        {
            for (int i = 0; i < dtUsers.Rows.Count; i++)
            {
                UserName = Convert.ToString(dtUsers.Rows[i]["UserName"]);
                EmailAddress = Convert.ToString(dtUsers.Rows[i]["Email"]);
                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
                client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                //client.EnableSsl = false;
                //client.Host = "relay-hosting.secureserver.net";
                //client.Port = 25;                 // setup Smtp authentication 
                client.EnableSsl = false;
                client.Host = "mail.lylasolutions.com";
                client.Port = 26;                 // setup Smtp authentication             
                String email = "beenish@lylasolutions.com";
                String password = "Beenish1";
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(email, password);
                client.UseDefaultCredentials = false;
                client.Credentials = credentials;
                System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                msg.From = new System.Net.Mail.MailAddress("beenish@lylasolutions.com");
                msg.To.Add(new System.Net.Mail.MailAddress(EmailAddress));
                msg.Subject = "Report Ninja: " + Subject;
                msg.IsBodyHtml = true;
                msg.Body = string.Format("<html><head></head><body><table width='860px'><tr><td align='center'><a href='http://www.reportninja.org' target='_blank'><img alt='' src='http://www.reportninja.org/images/Email/Header.png'/></a></td></tr><tr><td align='left' style='font-family: verdana; font-size: 15px; color: #333333;'>Dear <b>" + UserName + "</b><br/><br/>" + Message + "<br></td></tr><tr><td align='center'><br/><img alt='' src='http://www.reportninja.org/images/Email/Footer.png'/><br/></td></tr><tr><td style='font-family: verdana; font-size: 12px; font-style: italic; color: #333333;'><br/>This email is sent to you automatically by Report Ninja.</td></tr></table></body></html>");
                object userState = msg;
                try
                {
                    //event handler for asynchronous call
                    //client.SendCompleted += smtpClient_OnCompleted;
                    //client.SendAsync(msg, userState);
                    client.Send(msg);
                }
                catch (Exception ex)
                {
                    //  return Convert.ToString(ex.Message);
                }
            }
        }
        return "Success";
    }
    public string ChangedPasswordEmail(string UserName, string EmailAddress, string Password)
    {
        //wwlllandlord@yahoo.com beenish@lylasolutions.com
        //wwll123 Beenish1

        System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
        client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
        //client.EnableSsl = false;
        //client.Host = "relay-hosting.secureserver.net";
        //client.Port = 25;                 // setup Smtp authentication    
        client.EnableSsl = false;
        client.Host = "mail.lylasolutions.com";
        client.Port = 26;                 // setup Smtp authentication             
        String email = "beenish@lylasolutions.com";
        String password = "Beenish1";
        System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(email, password);
        client.UseDefaultCredentials = false;
        client.Credentials = credentials;
        System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
        msg.From = new System.Net.Mail.MailAddress("beenish@lylasolutions.com");
        msg.To.Add(new System.Net.Mail.MailAddress(EmailAddress));
        msg.Subject = "Report Ninja: Password has been changed";
        msg.IsBodyHtml = true;
        msg.Body = string.Format("<html><head></head><body><table width='860px'><tr><td align='center'><a href='http://www.reportninja.org' target='_blank'><img alt='' src='http://www.reportninja.org/images/Email/Header.png'/></a></td></tr><tr><td align='left' style='font-family: verdana; font-size: 15px; color: #333333;'>Dear <b>" + UserName + "</b><br/><br/>You have successfully changed your Report Ninja account password. Use your new password to log in next time.<br></td></tr><tr><td style='font-family: verdana; font-size: 12px; font-style: italic; color: #333333;'><br/>Email:&nbsp;" + EmailAddress + "</td></tr><tr><td style='font-family: verdana; font-size: 12px; font-style: italic; color: #333333;'>New Password:&nbsp;" + Password + "<br/></td></tr><tr><td align='center'><br/><img alt='' src='http://www.reportninja.org/images/Email/Footer.png'/><br/></td></tr><tr><td style='font-family: verdana; font-size: 12px; font-style: italic; color: #333333;'><br/>This email is sent to you automatically by Report Ninja.</td></tr></table></body></html>");
        object userState = msg;
        try
        {
            //event handler for asynchronous call
            //client.SendCompleted += smtpClient_OnCompleted;
            //client.SendAsync(msg, userState);
            client.Send(msg);
            return "Success";
        }
        catch (Exception ex)
        {
            return Convert.ToString(ex.Message);
        }
    }
    public string ChangedEmailAddress(string GUID, string UserName, string EmailAddress)
    {
        //wwlllandlord@yahoo.com beenish@lylasolutions.com
        //wwll123 Beenish1

        System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
        client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
        //client.EnableSsl = false;
        //client.Host = "relay-hosting.secureserver.net";
        //client.Port = 25;                 // setup Smtp authentication    
        client.EnableSsl = false;
        client.Host = "mail.lylasolutions.com";
        client.Port = 26;                 // setup Smtp authentication             
        String email = "beenish@lylasolutions.com";
        String password = "Beenish1";
        System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(email, password);
        client.UseDefaultCredentials = false;
        client.Credentials = credentials;
        System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
        msg.From = new System.Net.Mail.MailAddress("beenish@lylasolutions.com");
        msg.To.Add(new System.Net.Mail.MailAddress(EmailAddress));
        msg.Subject = "Report Ninja: Verify your new account Email";
        msg.IsBodyHtml = true;
        msg.Body = string.Format("<html><head></head><body><table width='860px'><tr><td align='center'><a href='http://www.reportninja.org' target='_blank'><img alt='' src='http://www.reportninja.org/images/Email/Header.png'/></a></td></tr><tr><td align='left' style='font-family: verdana; font-size: 15px; color: #333333;'>Dear <b>" + UserName + "</b><br/><br/>You have successfully changed your Report Ninja account Email. Please click on the verify button below to verify your new email address.<br></td></tr><tr><td align='center'><br/><a href='http://www.reportninja.org/Status?i=4&c=" + GUID + "'><img alt='' src='http://www.reportninja.org/images/Email/Verify.png'/></a><br/></td></tr><tr><td align='center'><br/><img alt='' src='http://www.reportninja.org/images/Email/Footer.png'/><br/></td></tr><tr><td style='font-family: verdana; font-size: 12px; font-style: italic; color: #333333;'><br/>This email is sent to you automatically by Report Ninja.</td></tr></table></body></html>");
        object userState = msg;
        try
        {
            //event handler for asynchronous call
            //client.SendCompleted += smtpClient_OnCompleted;
            //client.SendAsync(msg, userState);
            client.Send(msg);
            return "Success";
        }
        catch (Exception ex)
        {
            return Convert.ToString(ex.Message);
        }
    }
    public string ResendVerificationEmail(string GUID, string UserName, string EmailAddress, string Password)
    {
        //wwlllandlord@yahoo.com beenish@lylasolutions.com
        //wwll123 Beenish1

        System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
        client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
        //client.EnableSsl = false;
        //client.Host = "relay-hosting.secureserver.net";
        //client.Port = 25;                 // setup Smtp authentication    
        client.EnableSsl = false;
        client.Host = "mail.lylasolutions.com";
        client.Port = 26;                 // setup Smtp authentication             
        String email = "beenish@lylasolutions.com";
        String password = "Beenish1";
        System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(email, password);
        client.UseDefaultCredentials = false;
        client.Credentials = credentials;
        System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
        msg.From = new System.Net.Mail.MailAddress("beenish@lylasolutions.com");
        msg.To.Add(new System.Net.Mail.MailAddress(EmailAddress));
        msg.Subject = "Report Ninja: Verify your account";
        msg.IsBodyHtml = true;
        msg.Body = string.Format("<html><head></head><body><table width='860px'><tr><td align='center'><a href='http://www.reportninja.org' target='_blank'><img alt='' src='http://www.reportninja.org/images/Email/Header.png'/></a></td></tr><tr><td align='left' style='font-family: verdana; font-size: 15px; color: #333333;'>Dear <b>" + UserName + "</b><br/><br/>Thank you for using Report Ninja. Please click on the verify button below to verify your account.<br></td></tr><tr><td align='center'><br/><a href='http://www.reportninja.org/Status?i=2&c=" + GUID + "'><img alt='' src='http://www.reportninja.org/images/Email/Verify.png'/></a><br/></td></tr><tr><td style='font-family: verdana; font-size: 12px; font-style: italic; color: #333333;'><br/>Email:&nbsp;" + EmailAddress + "</td></tr><tr><td style='font-family: verdana; font-size: 12px; font-style: italic; color: #333333;'>Password:&nbsp;" + Password + "<br/></td></tr><tr><td align='center'><br/><img alt='' src='http://www.reportninja.org/images/Email/Footer.png'/><br/></td></tr><tr><td style='font-family: verdana; font-size: 12px; font-style: italic; color: #333333;'><br/>This email is sent to you automatically by Report Ninja.</td></tr></table></body></html>");
        object userState = msg;
        try
        {
            //event handler for asynchronous call
            //client.SendCompleted += smtpClient_OnCompleted;
            //client.SendAsync(msg, userState);
            client.Send(msg);
            return "Success";
        }
        catch (Exception ex)
        {
            return Convert.ToString(ex.Message);
        }
    }
    public string SendPDFReport(string SenderName, string ViewerPath, string EmailAddresses, string ReportName, int UserId, string ReportGUID,string VersionGUID,bool AllowEditing)
    {
        //Attachment attachFile = new Attachment(FilePath);
        //string attachFilename = attachFile.Name;
        //string[] ActualName = attachFilename.Split('/');
        //attachFile.Name = ActualName[4];
        string Message = "";
        string[] EmailAddress = EmailAddresses.Split(',');

        System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
        client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
        //client.EnableSsl = false;
        //client.Host = "relay-hosting.secureserver.net";
        //client.Port = 25;                 // setup Smtp authentication    
        client.EnableSsl = false;
        client.Host = "mail.lylasolutions.com";
        client.Port = 26;                 // setup Smtp authentication             
        String email = "beenish@lylasolutions.com";
        String password = "Beenish1";
        System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(email, password);
        client.UseDefaultCredentials = false;
        client.Credentials = credentials;
        System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
        msg.From = new System.Net.Mail.MailAddress("beenish@lylasolutions.com");
        for (int i = 0; i < EmailAddress.Length; i++)
        {
            string GUID = System.Guid.NewGuid().ToString();

            msg.To.Clear();
            msg.To.Add(new System.Net.Mail.MailAddress(EmailAddress[i]));

            //msg.Attachments.Add(attachFile);
            msg.Subject = "Report Ninja: " + SenderName + " has sent you a PDF Report.";
            msg.IsBodyHtml = true;
            msg.Body = string.Format("<html><head></head><body><table width='860px'><tr><td align='center'><a href='http://www.reportninja.org' target='_blank'><img alt='' src='http://www.reportninja.org/images/Email/Header.png'/></a></td></tr><tr><td align='left' style='font-family: verdana; font-size: 15px; color: #333333;'><b>" + SenderName + "</b> has sent you a report <b>" + ReportName + "</b> using Report Ninja.<br><br></td></tr><tr><td align='center' style='font-family: verdana; font-size: 15px; color: #333333;'><a href='http://www.reportninja.org/" + ViewerPath + "&r="+GUID+"'><img alt='' src='http://www.reportninja.org/images/viewBtn.png'/></a><br><br></td></tr><tr><td align='center'><br/><img alt='' src='http://www.reportninja.org/images/Email/Footer.png'/><br/></td></tr><tr><td style='font-family: verdana; font-size: 12px; font-style: italic; color: #333333;'><br/>This email is sent to you automatically by Report Ninja.</td></tr></table></body></html>");
            object userState = msg;
            try
            {
                EmailLog.AddEmailLog(GUID, ReportGUID, VersionGUID, UserId, EmailAddress[i],AllowEditing);
                //event handler for asynchronous call
                //client.SendCompleted += smtpClient_OnCompleted;
                //client.SendAsync(msg, userState);
                client.Send(msg);
                Message= "Success";                
            }
            catch (Exception ex)
            {
                Message= Convert.ToString(ex.Message);
            }
        }
        return Message;
    }
    public string ContactUsEmailToAdmin(string Name, string EmailAddress, string Subject, string Message)
    {
        //wwlllandlord@yahoo.com beenish@lylasolutions.com
        //wwll123 Beenish1

        System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
        client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
        //client.EnableSsl = false;
        //client.Host = "relay-hosting.secureserver.net";
        //client.Port = 25;                 // setup Smtp authentication 
        client.EnableSsl = false;
        client.Host = "mail.lylasolutions.com";
        client.Port = 26;                 // setup Smtp authentication             
        String email = "beenish@lylasolutions.com";
        String password = "Beenish1";
        System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(email, password);
        client.UseDefaultCredentials = false;
        client.Credentials = credentials;
        System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
        msg.From = new System.Net.Mail.MailAddress("beenish@lylasolutions.com");
        msg.To.Add(new System.Net.Mail.MailAddress("lylasolutions@gmail.com"));
        msg.Subject = "Report Ninja Contact Us: " + Subject;
        msg.IsBodyHtml = true;
        msg.Body = string.Format("<html><head></head><body><table width='860px'><tr><td align='center'><a href='http://www.reportninja.org' target='_blank'><img alt='' src='http://www.reportninja.org/images/Email/Header.png'/></a></td></tr><tr><td align='left' style='font-family: verdana; font-size: 15px; color: #333333;'><br/><b>Name: </b>" + Name + "<br/><br/><b>Email: </b>" + EmailAddress + "<br/><br/><b>Subject: </b>" + Subject + "<br/><br/><b>Message: </b>" + Message + "<br></td></tr><tr><td align='center'><br/><img alt='' src='http://www.reportninja.org/images/Email/Footer.png'/><br/></td></tr><tr><td style='font-family: verdana; font-size: 12px; font-style: italic; color: #333333;'><br/>This email is sent to you automatically by Report Ninja.</td></tr></table></body></html>");
        //object userState = msg;
        try
        {
            //event handler for asynchronous call
            //client.SendCompleted += smtpClient_OnCompleted;
            //client.SendAsync(msg, userState);
            client.Send(msg);
            return "Success";
        }
        catch (Exception ex)
        {
            return Convert.ToString(ex.Message);
        }
    }
    protected void smtpClient_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
    {
        MailMessage mail = e.UserState as MailMessage;
        if (!e.Cancelled && e.Error != null)
        {
            //message.Text = "Mail sent successfully";
        }
    }
    public void smtpClient_OnCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
    {
        MailMessage mailMessage = default(MailMessage);

        mailMessage = (MailMessage)e.UserState;

        if ((e.Cancelled))
        {
            // lblMessage.Text = "Sending of email message was cancelled. Address=" + mailMessage.To(0).Address;
        }
        if ((e.Error != null))
        {
            // lblMessage.Text = "Error occured, info :" + e.Error.Message;
        }
        else
        {
            // lblMessage.Text = "Mail sent successfully";
        }
    }
}