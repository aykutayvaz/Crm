using Crm.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace Crm.Models
{
    public class UserFunctions
    {
        Repository<dt_User> db;

        JsonModel jsonmodel;

        public JsonModel Jsonmodel
        {
            get { return jsonmodel; }
            set { jsonmodel = value; }
        }

        public UserFunctions()
        {
             db =  new Repository<dt_User>();

             jsonmodel = new JsonModel();
        }

        public dt_User FindUser(string email, string password)
        {
            //password karsılastırıldı hashed karsılastırılcak
            string hashedpassword = Convert.ToBase64String(
          System.Security.Cryptography.SHA256.Create()
          .ComputeHash(Encoding.UTF8.GetBytes(password)));
            //using (db)
            //{
                var user = db.Where(u => u.UserPassword == password && u.UserEmail == email).FirstOrDefault();
                if (user != null)
                {
                    return user;
                }
            //}
            return null;
        }

        public bool isUsedEmail(string email)
        {
            bool isValid = false;
            //using (db)
            //{
                var user = db.FirstOrDefault(u => u.UserEmail.CompareTo(email)==0);
                if (user != null)
                {
                    isValid = true;
                }
            //}
            return isValid;
        }


        public bool IsValidEmail(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }


        public dt_User CreateUser(string name, string email, string password)
        {
            try
            {
                //using (db)
                //{
                    dt_User kisi = new dt_User();
                    kisi.UserName = name;
                    kisi.UserPassword = password;
                    kisi.UserEmail = email;
                    kisi.RegisterDate = DateTime.Now;
                    kisi.Photo = "~/Content/img/avatar.png";
                    kisi.VerificationCode = "0";
                    kisi.Role = "user";
                    db.Add(kisi);
                    //db.dt_User.Add(kisi);
                    //db.SaveChanges();
                    return kisi;
                //}
              
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public bool Login(string email, string password,string check)
        {
            bool validateLogin = false;
            if (!string.IsNullOrEmpty(password) || !string.IsNullOrEmpty(email))
            {

                dt_User user = FindUser(email, password);

                if (user != null)
                {
                    SimpleSessionPersister.Username = user.UserName;
                    SimpleSessionPersister.Role = user.Role;
                    SimpleSessionPersister.UserId = user.UserId.ToString();
                    Jsonmodel.Message = "Giriş başarılı";
                    Jsonmodel.Success = "success";
                    validateLogin = true;
                }
                else
                {
                    Jsonmodel.Message = "Giriş başarısız";
                    Jsonmodel.Success = "fail";
                }
                return validateLogin;
            }
            else
            {
                Jsonmodel.Message = "Giriş başarısız.Email veya password boş girilemez";
                Jsonmodel.Success = "fail";
                return validateLogin;
            }
        }


        public void Register(string name,string email,string password)
        {
            if (!string.IsNullOrEmpty(name) || !string.IsNullOrEmpty(email) || !string.IsNullOrEmpty(name))
            {
                if (!isUsedEmail(email))
                {

                    dt_User user = CreateUser(name, email, password);
                    if (user != null)
                    {
                        Jsonmodel.Message = "Kayıt başarılı.Giriş yapabilirsiniz";
                        Jsonmodel.Success = "success";
                    }
                    else
                    {
                        Jsonmodel.Message = "Kayıt başarısız";
                        Jsonmodel.Success = "fail";
                    }
                }
                else
                {
                    Jsonmodel.Message = "Kayıt başarısız.Email Kullanılıyor";
                    Jsonmodel.Success = "fail";
                }
            }
            else
            {
                Jsonmodel.Message = "Kayıt başarısız.Kullanıcı adı,şifre,ad Boş Bırakılamaz";
                Jsonmodel.Success = "fail";  
            }
        }
        public string ReturnJson()
        {
            string json = "";
            var x = new
            {
                process = Jsonmodel.Success,
                message = Jsonmodel.Message,

            };

            //var x = new { value = value };
            json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(x);
            return json;
        }
    }
}