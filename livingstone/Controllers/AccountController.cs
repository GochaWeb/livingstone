using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using livingstone.Models;
using livingstone.Helper;
using System.Net.Mail;
using System.Net;

namespace livingstone.Controllers
{
    public class AccountController : Controller
    {
        private Livingstone db = new Livingstone();
        string AuthSecret = "Yb27L3h52r6gLWlhaquLawIwJbdaAVS0JFpc1woH";



        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(UserValidation user)
        {
            Session.Clear();
            string conf = MainHelper.Random32();
            int count = db.Users.Count() + 1;
            db.Users.Add(new User()
            {
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Password = MainHelper.CalculateMD5Hash(user.Password + AuthSecret),
                Confirmation = conf,
                RegNumber=user.Name+count,
                Search=user.Name+" "+user.Surname,
                CreateDate=DateTime.Now
            });
            db.SaveChanges();
            db.ProfilePhotoes.Add(new ProfilePhoto()
            {
                UserID=db.Users.Max(x=>x.Id),
                Photo="noImage.png"
            });
            db.SaveChanges();
            TempData["Success"] = "წარმატებით გაიარეთ რეგისტრაცია, გთხოვთ გააქტიუროთ ელ.ფოსტა";
            string Url = "http://localhost:6038/Account/Confirmation/" + conf;
            string body = "პროფილის გასააქტიურებლად გთხოვთ ეწვიოთ ბმულს : <a href=\"" + Url + "\">გასააქტიურებელი ბმული. </a>";
            var senderEmail = new MailAddress("livingstonetest2@gmail.com", "Activation");
            var reciveEmail = new MailAddress(user.Email, "Reciver");


            var password = "919265gt";
            var sub = "Actiovation";


            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,

                Credentials = new NetworkCredential(senderEmail.Address, password),
            };
            using (var mess = new MailMessage(senderEmail, reciveEmail)
            {
                Subject = sub,
                Body = body,
                IsBodyHtml = true,


            })
            {
                smtp.Send(mess);

            }

            return RedirectToAction("Login");
            
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserValidation user)
        {
            var useractive = db.Users.Where(x => x.Email == user.Email).FirstOrDefault();
            if (useractive.IsActive == false)
            {
                TempData["Error"] = "გააქტიურე ელ.ფოსტა";
                return RedirectToAction("Login");
            }
            string pass = MainHelper.CalculateMD5Hash(user.Password + AuthSecret);
            User Us = db.Users.Where(x => x.Email == user.Email && x.Password == pass).FirstOrDefault();
            if (Us == null)
            {
                TempData["Error"] = "ასეთი მომხამარებელი არ არსებობს";
                return RedirectToAction("Login");
            }
            Session["user"] = Us;

            return RedirectToAction("Index", "Home");
        }

        

        public ActionResult Confirmation(string Id)
        {
            var user = db.Users.Where(x => x.Confirmation == Id).FirstOrDefault();
            user.IsActive = true;
            db.SaveChanges();
            TempData["Success"] = "ატივაცია წარმატებით დასრულდა";
            return RedirectToAction("Login");
        }

        public JsonResult Email(string Email)
        {
            var UserEmail = !db.Users.Any(x => x.Email == Email);
            return Json(UserEmail, JsonRequestBehavior.AllowGet);

        }
    }
}