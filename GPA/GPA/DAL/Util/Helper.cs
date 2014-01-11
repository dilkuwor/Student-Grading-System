﻿using GPA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;

namespace GPA.DAL.Util
{
    public class Helper
    {
        public string EncryptPassword(String password)
        {
            UnicodeEncoding AE = new UnicodeEncoding();
            //string sha1 = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "sha1");
            return CalculateSha1(password, AE);

        }

        /// <summary>
        /// Calculates SHA1 hash
        /// </summary>
        /// <param name="text">input string</param>
        /// <param name="enc">Character encoding</param>
        /// <returns>SHA1 hash</returns>
        public string CalculateSha1(string text, Encoding enc)
        {
            byte[] buffer = enc.GetBytes(text);
            SHA1CryptoServiceProvider cryptoTransformSha1 =
            new SHA1CryptoServiceProvider();
            string hash = BitConverter.ToString(
            cryptoTransformSha1.ComputeHash(buffer)).Replace("-", "");
            return hash;
        }

        public string GenerageRandomPassword(int length)
        {
            string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            string res = "";
            Random rnd = new Random();
            while (0 < length--)
                res += valid[rnd.Next(valid.Length)];
            return res;

        }

        public string GenerageVerificationCode(int length)
        {
            string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            string res = "";
            Random rnd = new Random();
            while (0 < length--)
                res += valid[rnd.Next(valid.Length)];
            return res;

        }


        public void SendGradeNotification(string[] studentid, int courseid, string[] gradeid)
        {

            // 01.08.14 Added D.Shrestha Begin
            // get configuration for email            
            ApplicationSettingViewModel appSettingViewModel = new ApplicationSettingViewModel();
            bool result;
            UserDetail user;
            Cours course;
            Grade grade;
            String emailpattern = "<<Autogenerated FeedBack From Registrar. Please do not reply to this email >>\r\n\r\n";
            String table = "<table border='1'><tr><th>Subject</th><th>Grade</th></tr><tr><td>{0}</td><td>{1}</td></tr></table>";
            String email = string.Empty;
            using (var db = new GPAEntities())
            {
                int _studentid;
                int _gradeid;
                for (int count = 0; count < studentid.Count(); count++)
                {
                    _studentid= int.Parse(studentid[count]);
                    _gradeid = int.Parse(gradeid[count]);
                    user = db.UserDetails.Where(r => r.RegistrationID == _studentid).Single();
                    grade = db.Grades.Where(r => r.Id == _gradeid).Single();
                    course = db.Courses.Where(r => r.Id == courseid).Single();
                    email = String.Format(emailpattern+table, course.CourseName, grade.GradeScore);

                    
                    String subject = "Your Grades are Published.";
                    try
                    {
                        SendEmail sendEmail = new SendEmail(appSettingViewModel.SMTPServerName,
                        Int32.Parse(appSettingViewModel.SMTPServerPort), appSettingViewModel.SMTPUser, appSettingViewModel.SMTPPass);
                        result = sendEmail.Send(appSettingViewModel.SMTPUser, user.Email, subject, email);
                    }
                    catch (Exception ex)
                    {
                        // TODO: write in log
                        Console.Error.WriteLine("Fail in Sending Email " + ex.Message);
                    }

                }
            }


        }
    }
}