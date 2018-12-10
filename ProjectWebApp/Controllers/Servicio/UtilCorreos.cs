using ProjectWebApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;


namespace ProjectWebApp.Controllers.Servicio
{
    public class UtilCorreos
    {

        //Hope you find it useful, it contain too many things

        readonly string smtpAddress = "smtp.gmail.com";
        readonly int portNumber = 587;
        readonly bool enableSSL = true;
        readonly string userName = "support@xyz.com";
        readonly string UserpassWord = "56436578";

        public void SendEmail(USUARIOS _customers)
        {


                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("me@mydomain.com");
                mail.To.Add("marcelo.martinez.stuardo@gmail.com,yamilesro@gmail.com");
                mail.Subject = "Mensaje de Prueba de Template";
                mail.Body = "Report";
                mail.IsBodyHtml = true;
                mail.Body = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/assets/templates/MailConfirmaAsignacionTemplate.html"));
            //Attachment attachment = new Attachment(filename);
            //mail.Attachments.Add(attachment);

            SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("marcelo.martinez.stuardo@gmail.com", "M4rc3l0x84");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
            
        }
    }
   
}