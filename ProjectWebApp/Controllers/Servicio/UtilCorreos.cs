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

        readonly string smtpAddress = "smtp.xyz.com";
        readonly int portNumber = 587;
        readonly bool enableSSL = true;
        //readonly string userName = "support@xyz.com";
        //readonly string UserpassWord = "56436578";

        public void SendEmail(USUARIOS _customers)
        {
            string userName = _customers.USER;

            string emailFrom = "soporte@lexabogados.cl";
            string password = "qwerty";
            string emailTo = _customers.CORREO;

            // Here you can put subject of the mail
            string subject = "Registration";
            string sURL = "www.dev-epsilon.cl";
            // Body of the mail
            string body = "<div style='border: medium solid grey; width: 500px; height: 266px;font-family: arial,sans-serif; font-size: 17px;'>";
            body += "<h3 style='background-color: blueviolet; margin-top:0px;'>Aspen Reporting Tool</h3>";
            body += "<br />";
            body += "Dear " + userName + ",";
            body += "<br />";
            body += "<p>";
            body += "Thank you for registering </p>";
            
            body += "<p><a href='" + sURL + "'>Click Here</a>To finalize the registration process</p>";
            body += " <br />";
            body += "Thanks,";
            body += "<br />";
            body += "<b>The Team</b>";
            body += "</div>";
            // this is done using  using System.Net.Mail; & using System.Net; 
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(emailFrom);
                mail.To.Add(emailTo);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                // Can set to false, if you are sending pure text.

                using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                {
                    smtp.Credentials = new NetworkCredential(emailFrom, password);
                    smtp.EnableSsl = enableSSL;
                    smtp.Send(mail);
                }
            }
        }
    }
   
}