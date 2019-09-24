using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UploadDataToDatabase.Class
{
    public class SendMailFunction
    {
        public bool SendMailtoReport (ScheduleReportItems items, List<EmailNeedSend> emailNeedSends)
        {
            try
            {
                if (emailNeedSends.Count > 0)
                {
                    MailMessage mail = new MailMessage();
                    mail.IsBodyHtml =false;
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                    mail.From = new MailAddress("techlinkvn.2019@gmail.com");
                    foreach (var email in emailNeedSends)
                    {
                        if(items.ReportName == email.Function)
                        {
                            if(email.Status == "YES")
                            {
                                mail.To.Add(email.EmailReceive);
                            }
                        }
                    }
                    //  mail.To.Add("DUC.NA@techlink.vn");
                    //  mail.To.Add("tranducan.bkhcm11@gmail.com");
                    //  mail.To.Add("securitylong100@gmail.com");
                    string[] content = items.Contents.Split('\n');
                    string contentLine = "";
                    for (int i = 0; i < content.Count(); i++)
                    {
                        contentLine += content[i] + '\n';
                    }
                    mail.Subject = items.Subject;
                    mail.Body = items.Contents;



                    System.Net.Mail.Attachment attachment;
                    attachment = new System.Net.Mail.Attachment(@"C:\ERP_Temp\20190909 143451.xls");
                    mail.Attachments.Add(attachment);

                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("techlinkvn.2019", "techlink123");
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
