using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.IO;
using IdentitySample.Helpers;

namespace IdentitySample.DAL
{
    public enum EmailType
    {
        ParticipateTournamentRequest,
        ParticipateTournamentResponse,
        CreateTournamentRequest,
        CreateTournamentResponse,
        CreateAccount,
        ResetPassord
    }
    public class MailOptions
    {
        public EmailType MailType { get; set; }
        public string AddressTo { get; set; }
        public string AddressCc { get; set; }
        public string Body { get; set; }
    }

    public class MailService : IMessageService
    {
        //private string ReplaceTags(string sEmail, string FirstName, string LastName, string UserName, string MessageBody)
        //{
        //    sEmail = sEmail.Replace("@FirstName", FirstName);
        //    sEmail = sEmail.Replace("@LastName", LastName);
        //    sEmail = sEmail.Replace("@UserName", UserName);
        //    sEmail = sEmail.Replace("@Date", DateTime.Now.ToString("d"));
        //    sEmail = sEmail.Replace("@Body", MessageBody);
        //    return sEmail;
        //}

        public MailMessage ParicipateInTournamentRequest(MailMessage mailMessage, MailOptions messageEx)
        {

            mailMessage.Subject = string.Format("Request to participate in tournament '{0}'", "test");
            

            //oMessage.Body = ReplaceTags(templateBody, "Konstantin", "Braun", "kb", message.Body);

            return mailMessage;
        }

        public Task SendAsync(MailOptions mailOptions)
        {
            SmtpClient oSmtpClient = new SmtpClient(Properties.Settings.Default.mailHost);
            oSmtpClient.EnableSsl = true;
            oSmtpClient.Credentials = new NetworkCredential(Properties.Settings.Default.mailAccount, Properties.Settings.Default.mailPassword);
            oSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            MailMessage mailMessage = new MailMessage();
            mailMessage.IsBodyHtml = true;
            mailMessage.From = new MailAddress(Properties.Settings.Default.mailDisplayAddress, Properties.Settings.Default.mailDisplayName);
            mailMessage.To.Add(new MailAddress(mailOptions.AddressTo, mailOptions.AddressTo.GetFullName()));
            mailMessage.CC.Add(new MailAddress(mailOptions.AddressCc, mailOptions.AddressCc.GetFullName()));

            string path = string.Format("{0}EmailTemplate.htm", HttpContext.Current.Server.MapPath("~/"));
            StreamReader srTemplate = new StreamReader(path);
            mailMessage.Body = srTemplate.ReadToEnd();

            oSmtpClient.Send(mailMessage);
            return Task.FromResult(0);
        }
    }
}