using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

public class Email
{
    readonly string fromHost = "mail.syndicatepiling.co.za";
    readonly string fromEmail = "no-reply@syndicatepiling.co.za";
    readonly string fromPassword = "";

    public bool SendMailAttachments(string subject, string body, string to_email, string cc_email, string bcc_email, List<Attachment> attachments)
    {
        using (MailMessage mailMessage = new MailMessage())
        {
            mailMessage.From = new MailAddress(fromEmail);

            foreach (var address in to_email.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
            {
                mailMessage.To.Add(address.Trim());
            }

            foreach (var address in cc_email.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
            {
                mailMessage.CC.Add(address.Trim());
            }

            foreach (var address in bcc_email.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
            {
                mailMessage.Bcc.Add(address.Trim());
            }

            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = body;

            if (attachments != null)
            {
                foreach (Attachment attachment in attachments)
                {
                    mailMessage.Attachments.Add(attachment);
                }
            }

            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(RemoteServerCertificateValidationCallback);
            SmtpClient smtpClient = new SmtpClient
            {
                UseDefaultCredentials = true,
                Host = fromHost,
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromEmail, fromPassword),
                Timeout = 20000
            };
            try
            {
                smtpClient.Send(mailMessage);
                return true;
            }
            catch (Exception exception)
            {
                //new Repository().Command("INSERT INTO ExceptionLogs (TenantId, TableName, Message, StackTrace) VALUES (@TenantId, @TableName, @Message, @StackTrace)", new List<MySqlParameter>{
                //new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName = "@TableName", Value = "Email" },
                //new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName = "@Message", Value = "To Email : " + to_email + " / Subject : " + subject + " / Message : " + exception.Message },
                //new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName = "@StackTrace", Value = exception.StackTrace }});
                return false;
            }
        }
    }

    public void SendAutomatedInvoiceWithAttachments(string tenantId, string invoiceId, string subject, string body, string to_email, List<Attachment> attachments)
    {
        using (MailMessage mailMessage = new MailMessage())
        {
            mailMessage.From = new MailAddress(fromEmail);

            foreach (var address in to_email.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
            {
                mailMessage.To.Add(address.Trim());
            }

            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = body;

            if (attachments != null)
            {
                foreach (Attachment attachment in attachments)
                {
                    mailMessage.Attachments.Add(attachment);
                }
            }

            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(RemoteServerCertificateValidationCallback);
            SmtpClient smtpClient = new SmtpClient
            {
                UseDefaultCredentials = true,
                Host = fromHost,
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromEmail, fromPassword),
                Timeout = 20000
            };
            try
            {
                smtpClient.Send(mailMessage);

                new DatabaseTable().Insert("", new List<MySqlParameter>
                {
                    new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName = "@InvoiceEmailId", Value = Guid.NewGuid().ToString()},
                    new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName = "@InvoiceId", Value = invoiceId},
                    new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName = "@Subject", Value = subject},
                    new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName = "@Body", Value = body},
                    new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName = "@ToEmails", Value = to_email.Trim()},
                    new MySqlParameter() { MySqlDbType = MySqlDbType.Bit, ParameterName = "@Sent", Value = true},
                });
            }
            catch (Exception exception)
            {
                new DatabaseTable().Insert("", new List<MySqlParameter>
                {
                    new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName = "@InvoiceEmailId", Value = Guid.NewGuid().ToString()},
                    new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName = "@InvoiceId", Value = invoiceId},
                    new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName = "@Subject", Value = subject},
                    new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName = "@Body", Value = body},
                    new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName = "@ToEmails", Value = to_email.Trim()},
                    new MySqlParameter() { MySqlDbType = MySqlDbType.Bit, ParameterName = "@Sent", Value = false},
                    new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName = "@ErrorMessage", Value = exception.Message },
                    new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName = "@ErrorStackTrace", Value = exception.StackTrace }
                });
            }
        }
    }

    private bool RemoteServerCertificateValidationCallback(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
    {
        return true;
    }

    public void SendErrorAlert(string subject, string body)
    {
        using (MailMessage mailMessage = new MailMessage())
        {
            mailMessage.From = new MailAddress(fromEmail);
            mailMessage.To.Add("riley@webox.co.za");
            mailMessage.CC.Add("support@webox.co.za");

            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = body;

            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(RemoteServerCertificateValidationCallback);
            SmtpClient smtpClient = new SmtpClient
            {
                UseDefaultCredentials = true,
                Host = fromHost,
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromEmail, fromPassword),
                Timeout = 20000
            };

            smtpClient.Send(mailMessage);
        }
    }
}