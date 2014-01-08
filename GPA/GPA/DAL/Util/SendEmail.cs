using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

/*
 * Project Name: GPA  
 * Date Started: 01/07/2014
 * Description: Sends email
 * Module Name: Email Module
 * Module Number: 008-100-101
 * Developer Name: Dipesh Shrestha
 * Version: 0.1
 * Date Modified:
 * 
 */
namespace GPA.DAL.Util
{

    public class SendEmail
    {
        private SmtpClient _client;

        public SendEmail(String serverName, int port, String loginName, String password)
        {
            _client = new SmtpClient(serverName)
            {
                Credentials = new NetworkCredential(loginName, password),
                EnableSsl = true,
                Port = port
            };
        }

        public bool Send(String from, String to, String subject, String body)
        {
            var message = new MailMessage(from, to, subject, body);
            message.BodyEncoding = Encoding.UTF8;
            try
            {
                _client.Send(message);
                Console.WriteLine("Sent");
            }
            catch (Exception ex)
            {
                // TODO: logging
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }
    }
}