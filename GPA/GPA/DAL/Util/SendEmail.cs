using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

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

        //static void Main(string[] args)
        //{
        //    test();
        //}

        private static void test()
        {
            SendEmail email = new SendEmail("smtp.gmail.com", 587, "gpa.application@gmail.com", "gp@application");
            email.Send("gpa.application@gmail.com", "suunil.basnet@gmail.com,dipshrestha@gmail.com,kengsrengkh@gmail.com,dil.kuwor@gmail.com,mpg.pan@gmail.com,laxman.gm@gmail.com", "test for email", "testbody");
        }
    }
}