using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Windows.Forms;

namespace UploadDataToDatabase.Class
{
    public class SendMailFunction
    {
       public string path = Environment.CurrentDirectory + @"\Resources\EmailTemplate.html";
        public bool SendMailtoReport (ScheduleReportItems items, List<EmailNeedSend> emailNeedSends)
        {
            try
            {
                if (emailNeedSends.Count > 0)
                {
                    MailMessage mail = new MailMessage();
                    if (items.IsBodyHTML)
                        mail.IsBodyHtml = true;
                    else mail.IsBodyHtml = false;
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                    mail.From = new MailAddress("techlinkvn.2019@gmail.com");
                    foreach (var email in emailNeedSends)
                    {
                        mail.To.Add(email.EmailReceive);

                    }
                    mail.Subject = items.Subject + " On " + DateTime.Now.ToString("MMM-dd-yyyy");

                    if (items.IsBodyHTML)
                    {
                        string pathTemplate = Environment.CurrentDirectory + @"\Resources\EmailTemplate.html";
                        if (File.Exists(pathTemplate))
                        {
                            string html = File.ReadAllText(pathTemplate);
                            string htmlReplaced = "";
                            htmlReplaced = html.Replace("@Replace1", items.Subject);
                            htmlReplaced = htmlReplaced.Replace("@Replace2", DateTime.Now.ToString("MMM-dd-yyyy"));
                            mail.Body = htmlReplaced;
                        }
                    }
                    else
                    {
                        mail.Body = items.Contents;
                    }
                    List<string> listfileattached = new List<string>();
                    if (items.AttachedFolder != "" && items.AttachedFolder != null)
                    {
                        DirectoryInfo d = new DirectoryInfo(items.AttachedFolder);//Assuming Test is your Folder
                        FileInfo[] Files = d.GetFiles(); //Getting excel files
                       
                        foreach (FileInfo file in Files)
                        {
                            System.Net.Mail.Attachment attachment;

                            if (file.Name.Contains(items.ReportName))
                            {
                                attachment = new System.Net.Mail.Attachment(file.FullName);
                                mail.Attachments.Add(attachment);
                                listfileattached.Add(file.FullName);
                            }
                        }
                    }
                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("techlinkvn.2019", "techlink123");
                    SmtpServer.EnableSsl = true;
                    SmtpServer.Send(mail);
                    mail.Dispose();
                    SmtpServer.Dispose();
                    try
                    {
                        foreach (var item in listfileattached)
                        {
                            if (File.Exists(item))
                            {
                                File.Delete(item);//Xoa file after send file

                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        Log.Logfile.Output(Log.StatusLog.Error, "Delete file attached fail :", ex.Message);

                    }
                    Log.Logfile.Output(Log.StatusLog.Normal, "Send mail suscess :", items.ReportName + "|" + items.ReportType + "|" + items.Subject);

                    return true;
                }

            }
            catch (Exception ex)
            {
                Log.Logfile.Output(Log.StatusLog.Error, "Send mail fail :", ex.Message);
            }

            return false;
        }
        public bool SendMailtoReporttest()
        {
            try
            {
                    MailMessage mail = new MailMessage();
 
                        mail.IsBodyHtml = true;

                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                    mail.From = new MailAddress("techlinkvn.2019@gmail.com");
                     mail.To.Add("tranducan.bkhcm11@gmail.com");

                    mail.Subject = "Test" + " On " + DateTime.Now.ToString("MMM-dd-yyyy");

                        string pathTemplate = Environment.CurrentDirectory + @"\Resources\EmailTemplate.html";
                        if (File.Exists(pathTemplate))
                        {
                            string html = File.ReadAllText(pathTemplate);
                            mail.Body = html;
                        }
                    DirectoryInfo d = new DirectoryInfo(@"C:\ERP_Temp\");//Assuming Test is your Folder
                    FileInfo[] Files = d.GetFiles(); //Getting excel files

                    List<string> listfileattached = new List<string>();
                System.Net.Mail.Attachment attachment;
                foreach (FileInfo file in Files)
                    {

                            attachment = new System.Net.Mail.Attachment(file.FullName);
                            mail.Attachments.Add(attachment);
                            listfileattached.Add(file.FullName);                  
                }

                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("techlinkvn.2019", "techlink123");
                    SmtpServer.EnableSsl = true;
                    SmtpServer.Send(mail);
                    mail.Dispose();
                   SmtpServer.Dispose();
               
                
                    try
                    {
                        foreach (var item in listfileattached)
                        {
                            if (File.Exists(item))
                            {
                                File.Delete(item);//Xoa file after send file

                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        Log.Logfile.Output(Log.StatusLog.Error, "Delete file attached fail :", ex.Message);

                    }
                    Log.Logfile.Output(Log.StatusLog.Normal, "Send mail suscess :", "Test Mail");

                    return true;

            }
            catch (Exception ex)
            {
                Log.Logfile.Output(Log.StatusLog.Error, "Send mail fail :", ex.Message);
            }

            return false;
        }
        //This function only make for Send backlog report
        public bool SendMailwithExportExcel(ScheduleReportItems items, List<EmailNeedSend> emailNeedSends, 
            ref DataGridView dgv_export,string filename, string PathFoler,string version)
        {
            try
            {
                UploadDataToDatabase.BackLogReport.BacklogReport backlog = new BackLogReport.BacklogReport();

                if (backlog.ExportExcelToReport(ref dgv_export, filename, PathFoler, version))
                {
                    Log.Logfile.Output(Log.StatusLog.Normal, "Export excel sucessfull");
                }
             
            }
            catch (Exception ex)
            {

                Log.Logfile.Output(Log.StatusLog.Error, "Export excel fail!", ex.Message);
            }
            try
            {
                if (emailNeedSends.Count > 0)
                {
                    MailMessage mail = new MailMessage();
                    if (items.IsBodyHTML)
                        mail.IsBodyHtml = true;
                    else mail.IsBodyHtml = false;
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                    mail.From = new MailAddress("techlinkvn.2019@gmail.com");
                   foreach (var email in emailNeedSends)
                    {
                        mail.To.Add(email.EmailReceive);
                     // mail.To.Add("tranducan.bkhcm11@gmail.com");

                    }
                    mail.Subject = items.Subject + " On " + DateTime.Now.ToString("MMM-dd-yyyy");

                    if (items.IsBodyHTML)
                    {
                        string pathTemplate = Environment.CurrentDirectory + @"\Resources\EmailTemplate.html";
                        if (File.Exists(pathTemplate))
                        {
                            string html = File.ReadAllText(pathTemplate);
                            string htmlReplaced = "";
                            htmlReplaced = html.Replace("@Replace1", items.Subject);
                            htmlReplaced = htmlReplaced.Replace("@Replace2", DateTime.Now.ToString("MMM-dd-yyyy"));
                            mail.Body = htmlReplaced;
                        }
                    }
                    else
                    {
                        mail.Body = items.Contents;
                    }

                    List<string> listfileattached = new List<string>();
                    if (items.AttachedFolder != "" && items.AttachedFolder != null)
                    {
                        DirectoryInfo d = new DirectoryInfo(items.AttachedFolder);//Assuming Test is your Folder
                        FileInfo[] Files = d.GetFiles(); //Getting excel files

                        foreach (FileInfo file in Files)
                        {
                            System.Net.Mail.Attachment attachment;

                            if (file.Name.Contains(items.ReportName))
                            {
                                attachment = new System.Net.Mail.Attachment(file.FullName);
                                mail.Attachments.Add(attachment);
                                listfileattached.Add(file.FullName);
                            }
                        }
                    }

                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("techlinkvn.2019", "techlink123");
                    SmtpServer.EnableSsl = true;
                    SmtpServer.Send(mail);
                    mail.Dispose();
                    SmtpServer.Dispose();
                    try
                    {
                        foreach (var item in listfileattached)
                        {
                            if (File.Exists(item))
                            {
                                File.Delete(item);//Xoa file after send file

                            }
                        }
                      
                    }
                    catch (Exception ex)
                    {
                        Log.Logfile.Output(Log.StatusLog.Error, "Delete file attached fail :", ex.Message);

                    }
                    Log.Logfile.Output(Log.StatusLog.Normal, "Send mail suscess :", items.ReportName + "|" + items.ReportType + "|" + items.Subject);

                    return true;
                }

            }
            catch (Exception ex)
            {
                Log.Logfile.Output(Log.StatusLog.Error, "Send mail fail :", ex.Message);
            }


            return true;
        }
        public bool SendMailtoReportByOutlook(ScheduleReportItems items, List<EmailNeedSend> emailNeedSends)
        {
            //try
            //{
            // //   Create the Outlook application.
            //    Outlook.Application oApp = new Outlook.Application();
            //  //  Create a new mail item.
            //   Outlook.MailItem oMsg = (Outlook.MailItem)oApp.CreateItem(Outlook.OlItemType.olMailItem);

            //    Set HTMLBody. 
            //    add the body of the email
            //    oMsg.HTMLBody = "Hello, Jawed your message body will go here!!";
            //    Add an attachment.
            //    String sDisplayName = "MyAttachment";
            //    int iPosition = (int)oMsg.Body.Length + 1;

            //    int iAttachType = (int)Outlook.OlAttachmentType.olByValue;
            //    now attached the file
            //    Outlook.Attachment oAttach = oMsg.Attachments.Add
            //                                  (@"C:\\fileName.jpg", iAttachType, iPosition, sDisplayName);
            //    Subject line
            //    oMsg.Subject = "Your Subject will go here.";
            //    Add a recipient.
            //   Outlook.Recipients oRecips = (Outlook.Recipients)oMsg.Recipients;
            //    Change the recipient in the next line if necessary.
            //   Outlook.Recipient oRecip = (Outlook.Recipient)oRecips.Add("tranducan.bkhcm11@gmail.com");
            //    oRecip.Resolve();
            //    Send.
            //   oMsg.Send();
            //    Clean up.
            //    oRecip = null;
            //    oRecips = null;
            //    oMsg = null;
            //    oApp = null;
            //}//end of try block
            //catch (Exception ex)
            //{
            //}//end of catch

            try
            {
                if (emailNeedSends.Count > 0)
                {
                    MailMessage mail = new MailMessage();
                    if (items.IsBodyHTML)
                        mail.IsBodyHtml = true;
                    else mail.IsBodyHtml = false;

                    SmtpClient SmtpServer = new SmtpClient("http://103.18.179.112/webmail/", 587);


                    mail.From = new MailAddress("tlms@techlink.vn");
                    foreach (var email in emailNeedSends)
                    {
                        if (items.ReportName == email.Function)
                        {
                            if (email.Status == "YES")
                            {
                                mail.To.Add(email.EmailReceive);
                            }
                        }
                    }

                    mail.Subject = items.Subject;
                    mail.Body = items.Contents;

                    DirectoryInfo d = new DirectoryInfo(items.AttachedFolder);//Assuming Test is your Folder
                    FileInfo[] Files = d.GetFiles("*.xls"); //Getting excel files
                    string str = "";
                    foreach (FileInfo file in Files)
                    {

                        if (file.Name.Contains(items.ReportName))
                        {
                            str = file.FullName;
                        }
                    }

                    string path = str;
                    if (File.Exists(path))
                    {
                        System.Net.Mail.Attachment attachment;
                        attachment = new System.Net.Mail.Attachment(path);
                        mail.Attachments.Add(attachment);
                    }

                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("tlms@techlink.vn", "techlink@123");
                    SmtpServer.EnableSsl = true;

                    SmtpServer.Send(mail);
                    MessageBox.Show("mail Send");

                    return true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return false;
        }
    }
    

}
