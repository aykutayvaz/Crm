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
            string hashedpassword = Hash(password);
            //using (db)
            //{
                var user = db.Where(u => u.UserPassword == hashedpassword && u.UserEmail == email).FirstOrDefault();
                if (user != null)
                {
                    return user;
                }
            //}
            return null;
        }

        public dt_User FindUserByVerification(string verificationcode)
        {
            var user = db.Where(u => u.VerificationCode == verificationcode).FirstOrDefault();
            if (user != null)
            {
                return user;
            }
            return null;
        }

        public dt_User FindUserByEmail(string email)
        {
            var user = db.Where(u => u.UserEmail == email).FirstOrDefault();
            if (user != null)
            {
                return user;
            }
            return null;
        }

        public string CreateVerification(string email,string hosturl)
        {
            var user = db.Where(u => u.UserEmail == email).FirstOrDefault();
            if (user != null)
            {
                user.VerificationCode = RandomPasswordHashed();
                db.UpdateSaveChanges();
                string host = hosturl + "/Login/ChangePassword?code=" + user.VerificationCode;
                return host;
            }
            return null;
        }

        public void ChangePassword(dt_User user,string password)
        {
            user.UserPassword = Hash(password);
            user.VerificationCode = "0";
            db.UpdateSaveChanges();
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
                    kisi.UserPassword = Hash(password);
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

        public void ForgotPassword(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                if (isUsedEmail(email))
                {
                    
                    dt_User user = db.FirstOrDefault(u => u.UserEmail == email);
                    user.VerificationCode = RandomPasswordHashed();
                    db.UpdateSaveChanges();
                    if (SendMail("aa@fides.com.tr", email, "Şifre sıfırlama", "Şifreniz:" + "  " + user.UserPassword))
                    {
                        Jsonmodel.Message = "Şifre sıfırlama maili başarıyla gönderilmiştir.";
                        Jsonmodel.Success = "success";
                    }
                    else
                    {
                        Jsonmodel.Message = "Şifre sıfırlama başarısız.";
                        Jsonmodel.Success = "fail";
                    }
                }
            }
        }

        private string RandomPasswordHashed()
        {
            string pass = "";
            DateTime now = new DateTime();
            pass = Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 8);
            string hashedpassword = Convert.ToBase64String(
              System.Security.Cryptography.SHA256.Create()
              .ComputeHash(Encoding.UTF8.GetBytes(pass)));
            hashedpassword = hashedpassword.Replace("=", "");
            hashedpassword = hashedpassword.Replace("+", "");
            hashedpassword = hashedpassword.Replace("?", "");
            hashedpassword = hashedpassword.Replace("+", "");
            hashedpassword = hashedpassword.Replace(";", "");
            hashedpassword = hashedpassword.Replace("/", "");
            hashedpassword = hashedpassword.Replace(":", "");
            hashedpassword = hashedpassword.Replace("@", "");
            hashedpassword = hashedpassword.Replace("$", "");
            hashedpassword = hashedpassword.Replace(",", "");
            hashedpassword = hashedpassword.Replace("&", "");
            return hashedpassword.Substring(0,40);
        }

        public string Hash(string value)
        {
            string hashedpassword = Convert.ToBase64String(
                         System.Security.Cryptography.SHA256.Create()
                         .ComputeHash(Encoding.UTF8.GetBytes(value)));
            hashedpassword = hashedpassword.Replace("=", "");
            hashedpassword = hashedpassword.Replace("+", "");
            hashedpassword = hashedpassword.Replace("?", "");
            hashedpassword = hashedpassword.Replace("+", "");
            hashedpassword = hashedpassword.Replace(";", "");
            hashedpassword = hashedpassword.Replace("/", "");
            hashedpassword = hashedpassword.Replace(":", "");
            hashedpassword = hashedpassword.Replace("@", "");
            hashedpassword = hashedpassword.Replace("$", "");
            hashedpassword = hashedpassword.Replace(",", "");
            hashedpassword = hashedpassword.Replace("&", "");
            return hashedpassword.Substring(0, 40);
            
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

        private bool SendMail(string mailfrom,string mailto,string subject,string body)
        {
            try
            {
                SmtpClient client = new SmtpClient();
                client.Port = 587;
                client.Host = "smtp.fides.com.tr";
                client.EnableSsl = true;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("aa@fides.com.tr", "aa@*2015");

                MailMessage mm = new MailMessage(mailfrom,mailto, subject, body);
                mm.BodyEncoding = UTF8Encoding.UTF8;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                client.Send(mm);









                //MailMessage mail = new MailMessage(mailfrom, mailto);
                //SmtpClient client = new SmtpClient();
                //client.Port = 587;
                //client.DeliveryMethod = SmtpDeliveryMethod.Network;
                //client.UseDefaultCredentials = false;
                //client.Host = "smtp.fides.com.tr";
                //mail.Subject = subject;
                //mail.Body = body;
                //client.Send(mail);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}