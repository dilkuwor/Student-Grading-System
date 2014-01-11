using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GPA.DAL;
using GPA.DAL.Util;


namespace GPA.Models.Manager
{
    public class AccountManager
    {

        public void RegisterUser(RegisterViewModel model,int userId)
        {

            using (var db = new GPAEntities())
            {

                UserDetail user = new UserDetail();
                User _user = db.Users.Where(r => r.UserID == userId).SingleOrDefault();
                string[] roles = _user.Role.Split(',');
                user.FName = model.RegisterUserViewModel.FName;
                user.LName = model.RegisterUserViewModel.LName;
                user.Email = model.RegisterUserViewModel.Email;
                user.Address = model.RegisterUserViewModel.Address;
                user.City = model.RegisterUserViewModel.City;
                user.Zip = model.RegisterUserViewModel.Zip;
                user.LandNumber = model.RegisterUserViewModel.LandNumber;
                user.MobileNumber = model.RegisterUserViewModel.MobileNumber;
                user.UserID = userId;
                db.UserDetails.Add(user);
                db.SaveChanges();
                InsertNewRole(roles, db.UserDetails.Where(r => r.UserID == user.UserID).Single().RegistrationID);

            }
        }
        /// <summary>
        /// inserts new role if its not already exist
        /// </summary>
        /// <param name="roles"></param>
        public void InsertNewRole(String[] roles,int userid)
        {
            List<Role> _roles = new List<Role>();
            using (var db = new GPAEntities())
            {
                foreach (String s in roles)
                {
                    var _role = db.Roles.Where(r => r.RoleName == s).SingleOrDefault();
                    if (_role == null)
                    {
                        Role newrole = new Role();
                        newrole.RoleName = s;
                        db.Roles.Add(newrole);
                        _roles.Add(newrole);
                     
                    }
                    else
                    {
                        _roles.Add(_role);
                    }
                }
                db.SaveChanges();
                //send registration id
                InsertUserRoleMapping(_roles, userid);
                
            }
            
        }

        public void InsertUserRoleMapping(List<Role> roles, int userid)
        {
            List<UserRole> rolesmapping = new List<UserRole>();
            using (var db = new GPAEntities())
            {
                UserRole urole;
                foreach (Role r in roles)
                {
                    urole = new UserRole();
                    urole.UserRefID = userid;
                    urole.RoleRef_ID = r.Role_ID;
                    rolesmapping.Add(urole);
                }

                db.UserRoles.AddRange(rolesmapping);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Returns the list of users
        /// </summary>
        /// <returns></returns>
        public List<UserDetail> GetUserListByRole(string role)
        {
            var users = new List<UserDetail>();
            using (var db = new GPAEntities())
            {
                users = (from u in db.UserDetails
                        join ur in db.UserRoles on u.RegistrationID equals ur.UserRefID
                        join r in db.Roles on ur.RoleRef_ID equals r.Role_ID
                        where r.RoleName == role
                        select u).ToList();
            }
            return users;
        }
      
        public bool ValidateUser(LoginViewModel model, out User user)
        {
            //LoginViewModel
            String userName = model.UserName;
            String password = model.Password;
            Helper helper = new Helper();
            user = null;
            String epassword = helper.EncryptPassword(password);
            using (var db = new GPAEntities())
            {
                var _user = db.Users.Where(r => r.UserName == userName && r.Password == epassword).SingleOrDefault();              
                  
                if (_user!= null)
                {
                    user = _user;
                    return true;
                }                    
                else
                {
                    return false;
                }
                   
            }


        }

        public Boolean IsUserRegistered(User user)
        {
            Boolean flag = false;
            using (var db = new GPAEntities())
            {
                var userRegistraion = db.UserDetails.Where(r => r.UserID == user.UserID).SingleOrDefault();
                if (userRegistraion != null)
                {
                    flag = true;
                }
                else

                    flag = false;

            }
            return flag;
        }

        #region TestData
        public void InsertTestData()
        {

            Helper helper = new Helper();
            User dil = new User();
            dil.UserName = "dil.kuwor@gmail.com";
            dil.Password = helper.EncryptPassword("dil123");
            dil.Role = "Admin";
            String v1 = helper.GenerageVerificationCode(4);
            dil.VerificationCode = v1;

            User laxman = new User();
            laxman.UserName = "laxman.gm@gmail.com";
            laxman.Password = helper.EncryptPassword("laxman123");
            laxman.Role = "Staff,Student";
            String v2 = helper.GenerageVerificationCode(4);
            laxman.VerificationCode = v2;

            User dipesh = new User();
            dipesh.UserName = "dipshrestha@gmail.com";
            dipesh.Password = helper.EncryptPassword("dip123");
            dipesh.Role = "Faculty";
            String v3 = helper.GenerageVerificationCode(4);
            dipesh.VerificationCode = v3;

            List<User> users = new List<User>();
            users.Add(dil);
            users.Add(laxman);
            users.Add(dipesh);
            using (var db = new GPAEntities())
            {
                db.Users.AddRange(users);
                db.SaveChanges();
            }

        }
        #endregion
      


        public String GetUserVerificationCode(int userId)
        {
            string verification = "";
            using (var db = new GPAEntities())
            {
                verification = db.Users.Where(r => r.UserID == userId).Select(s => s.VerificationCode).Single();

            }

            return verification;

        }

        /// <summary>
        /// return role of user by roleid
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public Role GetRoleByRoleID(int roleid)
        {
            Role role;
            using (var db = new GPAEntities())
            {

                role = db.Roles.Where(r => r.Role_ID == roleid).SingleOrDefault();
            }

            return role;
        }

        //public Role GetCurrentRole(User user)
        //{
        //    dynamic role;
        //    using (var db = new TestDBEntities())
        //    {
        //        var roles = db.UserRoles.Where(r => r.UserId == user.Id).ToList();
        //        int roleid = roles[0].RoleId;
        //        role = db.Roles.Where(r => r.Id == roleid).Single();
        //    }

        //    return role;

        //}



        /// <summary>
        /// Returns the registerd user object by theire user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserDetail FindUserByUserID(int userId)
        {
            UserDetail user;
            using (var db = new GPAEntities())
            {
                user = db.UserDetails.Where(r => r.UserID == userId).Single();
            }
            return user;

        }

        /// <summary>
        /// returns the user given by the client by their user id
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public User GetDefaultUserByID(int userid)
        {
            User user = new User();
            using (var db = new GPAEntities())
            {
                user = db.Users.Where(r => r.UserID == userid).SingleOrDefault();
            }
            return user;

        }

      

        /// <summary>
        /// Returns the available roles of the user by buserid
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<Role> GetUsrRoles(int userid)
        {
            List<Role> roles;
            using (var db = new GPAEntities())
            {
                roles = (from r in db.Roles
                         join ur in db.UserRoles on r.Role_ID equals ur.RoleRef_ID
                         where ur.UserRefID == userid
                         select r).ToList();
            }

            return roles;
        }

    }


}