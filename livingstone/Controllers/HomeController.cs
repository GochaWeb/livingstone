using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using livingstone.Models;
using livingstone.Filter;
using livingstone.Helper;
using System.Drawing;
using System.Drawing.Imaging;
using System.ComponentModel;

namespace livingstone.Controllers
{
    [Authorized]
    public class HomeController : Controller
    {
         Livingstone db = new Livingstone();

        [ValidateInput(false)]
        public ActionResult Index([DefaultValue(0)]int id)
        {
           
            User Us = (User)Session["user"];
            if (Us == null)
                return RedirectToAction("Login", "Account");
            Us = MainHelper.InitUser(Us.Id);
            ViewBag.Users = GetUsers.GetNotFollowin(Us.Id);
            ViewBag.Comment = db.Comments.OrderByDescending(x=>x.CommentDate).ToList();
            ViewBag.Id = Us.Id;
            ViewBag.rettweet = db.Retweets.OrderByDescending(x=>x.RetweetDate).ToList();
            var followers = db.Followings.Where(x => x.UserID == Us.Id).ToList();
            ViewBag.followers = db.Followings.Where(x => x.FollowingID == Us.Id).Count();
            ViewBag.Photo = Us.ProfilePhotoes.FirstOrDefault();
            ViewBag.Cover = Us.CoverPhotoes.FirstOrDefault();
            if(id > 0)
            {
                ViewBag.Users = GetUsers.GetNotFollowin(id);
                ViewBag.followers = db.Followings.Where(x => x.FollowingID == id).Count();
                User user = db.Users.Where(x => x.Id == id).FirstOrDefault();
                ViewBag.Id = user.Id;
                
                if (followers.Any(x => x.FollowingID == id))
                {
                    ViewBag.Check = true;
                }
                ViewBag.Photo = user.ProfilePhotoes.FirstOrDefault();
                ViewBag.Cover = user.CoverPhotoes.FirstOrDefault();
                return View(user);
            }
           

            return View(Us);
            
        }

  
       

        public JsonResult GetUser(string query)
        {
            if (query == "" || query==" ")
            {
                return Json(false);
            }
             List<GetUsers> userlist = new List<GetUsers>();
            var user = db.Users.Where(x => x.IsActive == true).ToList();
            var usersquery = user.Where(x => x.Search.Contains(query));
           foreach(var item in usersquery)
            {
                userlist.Add(new GetUsers()
                {
                     Id=item.Id,
                     Name=item.Name,
                     Surname=item.Surname,
                     Photo=item.ProfilePhotoes.FirstOrDefault().Photo,
                     Email=item.Email,
                     RegNumber= item.RegNumber
                     
                });
            }
           // userlis.Add(user);
            return Json(userlist);
        }

        public ActionResult upload()
        {
            return View();
        }


        [HttpPost]
        public ActionResult upload(HttpPostedFileBase Photo)
        {
            User Us = (User)Session["user"];
            var file = Photo as HttpPostedFileBase;
            string name = MainHelper.Random32();
            int size = file.ContentLength;
           
            var supportedTypes = new[] { "jpg", "jpeg", "png" };
            var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);


            if (!supportedTypes.Contains(fileExt))
            {
                ViewBag.Error = "ფაილი უნდა იყოს მხოლოდ ამ გაფართოებით .jpg.jpeg.png";
                return View();
            }
            if (size > 1024 * 1000)
            {
                ViewBag.Error = "მაქსიმუმ 1 mb";
                return View();
            }

            if (db.ProfilePhotoes.Any(x => x.UserID == Us.Id))
            {
                var Profile = db.ProfilePhotoes.Where(x => x.UserID == Us.Id).FirstOrDefault();
                string fullpath = Request.MapPath("/Content/Image/" + Profile.Photo);
                if (System.IO.File.Exists(fullpath))
                {
                    System.IO.File.Delete(fullpath);
                }
                db.ProfilePhotoes.Remove(db.ProfilePhotoes.Where(x => x.UserID == Us.Id).FirstOrDefault());
                db.SaveChanges();
            }


         
            using (var newimage = ScaleImage(Image.FromStream(Photo.InputStream, true, true), 600, 400))
            {

                string path = Server.MapPath("/Content/Image/" + name + ".png");

                newimage.Save(path, ImageFormat.Png);

            }
            db.ProfilePhotoes.Add(new ProfilePhoto()
            {
                UserID = Us.Id,
                Photo = name+".png"
            });
            
            db.SaveChanges();
            ViewBag.file = db.ProfilePhotoes.Where(x => x.UserID == Us.Id).FirstOrDefault().Photo;
            return View();
        }

        public ActionResult UploadCover(HttpPostedFileBase Photo)
        {
            User Us = (User)Session["user"];
            if(Photo == null)
            {
                TempData["Error"] = "აირჩიეთ ფოტო";
                return RedirectToAction("Upload");
            }
            var file = Photo as HttpPostedFileBase;
            string name = MainHelper.Random32();
            int size = file.ContentLength;
          
            var supportedTypes = new[] { "jpg", "jpeg", "png" };
            var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);


            if (!supportedTypes.Contains(fileExt))
            {
                TempData["Error"] = "ფაილი უნდა იყოს მხოლოდ ამ გაფართოებით .jpg.jpeg.png";
                return RedirectToAction("Upload");
            }
            if (size > 1024 * 1000)
            {
                TempData["Error"] = "მაქსიმუმ 1 mb";
                return RedirectToAction("Upload");
            }

            if (db.CoverPhotoes.Any(x => x.UserID == Us.Id))
            {
                var Profile = db.CoverPhotoes.Where(x => x.UserID == Us.Id).FirstOrDefault();
                string fullpath = Request.MapPath("/Content/Image/" + Profile.Photo);
                if (System.IO.File.Exists(fullpath))
                {
                    System.IO.File.Delete(fullpath);
                }
                db.CoverPhotoes.Remove(db.CoverPhotoes.Where(x => x.UserID == Us.Id).FirstOrDefault());
                db.SaveChanges();
            }


         
            using (var newimage = ScaleImage(Image.FromStream(Photo.InputStream, true, true), 600, 400))
            {

                string path = Server.MapPath("/Content/Image/" + name + ".png");

                newimage.Save(path, ImageFormat.Png);

            }
            db.CoverPhotoes.Add(new CoverPhoto()
            {
                UserID = Us.Id,
                Photo = name + ".png"
            });
            db.SaveChanges();
            
            return RedirectToAction("Index");

        }
        [ValidateInput(false)]
        public JsonResult DelComment(string Value)
        {
           if(Value != null)
            {


                if (Value.Contains("A1"))
                {
                    string newstring = Value.Substring(Value.IndexOf(':') + 1);
                    int Id = Convert.ToInt32(newstring);
                    if (db.Comments.Any(x =>x.CommentId == Id))
                    {
                        if (db.Retweets.Any(x => x.CommentID == Id))
                        {
                            var newId = db.Retweets.Where(x => x.CommentID == Id).FirstOrDefault().RetweetId;
                            if (db.RetweetReplies.Any(x => x.RetweetID == newId))
                                db.RetweetReplies.RemoveRange(db.RetweetReplies.Where(x => x.RetweetID == newId));
                            db.Retweets.RemoveRange(db.Retweets.Where(x => x.CommentID == Id));
                        }
                            
                         

                        if (db.Replies.Any(x => x.CommentID == Id))
                            db.Replies.RemoveRange(db.Replies.Where(x => x.CommentID == Id));
                       
                        db.Comments.Remove(db.Comments.Where(x => x.CommentId == Id).FirstOrDefault());
                        db.SaveChanges();
                        return Json(true,JsonRequestBehavior.AllowGet);
                    }
                  
                }else if (Value.Contains("B2"))
                {
                    string newstring = Value.Substring(Value.IndexOf(':') + 1);
                    int Id = Convert.ToInt32(newstring);
                    if (db.Replies.Any(x => x.ReplyId == Id))
                    {
                        db.Replies.Remove(db.Replies.Where(x => x.ReplyId == Id).FirstOrDefault());
                        db.SaveChanges();
                        return Json(true,JsonRequestBehavior.AllowGet);
                    }
                }
                else if (Value.Contains("C3"))
                {
                    string newstring = Value.Substring(Value.IndexOf(':') + 1);
                    int Id = Convert.ToInt32(newstring);
                    if (db.Retweets.Any(x => x.RetweetId == Id))
                    {
                        if(db.RetweetReplies.Any(x=>x.RetweetID==Id))
                        db.RetweetReplies.RemoveRange(db.RetweetReplies.Where(x => x.RetweetID == Id));

                        db.Retweets.Remove(db.Retweets.Where(x => x.RetweetId == Id).FirstOrDefault());
                        db.SaveChanges();
                        return Json(true,JsonRequestBehavior.AllowGet);
                    }
                  
                }else if (Value.Contains("D4"))
                {
                    string newstring = Value.Substring(Value.IndexOf(':') + 1);
                    int Id = Convert.ToInt32(newstring);
                    db.RetweetReplies.Remove(db.RetweetReplies.Where(x => x.ReplyId == Id).FirstOrDefault());
                    db.SaveChanges();
                    return Json(true,JsonRequestBehavior.AllowGet);
                }
                return Json(false,JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false,JsonRequestBehavior.AllowGet);
            }

            
        }

        [ValidateInput(false)]
        public ActionResult crop(FormCollection form)
        {
            User Us = (User)Session["user"];
            var x = Convert.ToInt32(form["X"]);
            var y = Convert.ToInt32(form["Y"]);
            var w = Convert.ToInt32(form["W"]);
            var h = Convert.ToInt32(form["H"]);
            string name = db.ProfilePhotoes.Where(e => e.UserID == Us.Id).FirstOrDefault().Photo;
            string sourcefile = Request.MapPath("/Content/Image/"+ name);
            Image oImage = Image.FromFile(sourcefile);
            var bmp = new Bitmap(w, h, oImage.PixelFormat);
            var g = Graphics.FromImage(bmp);
            g.DrawImage(oImage, new Rectangle(0, 0, w, h), new Rectangle(x, y, w, h), GraphicsUnit.Pixel);
            System.Drawing.Imaging.ImageFormat frm = oImage.RawFormat;
            oImage.Dispose();
            string destFile = Request.MapPath("/Content/Image/" + name);
            bmp.Save(destFile, frm);

            
            return RedirectToAction("Index");
        }

        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Login", "Account");
        }

        [ValidateInput(false)]
        public JsonResult Following(int Id)
        {
            User Us = (User)Session["user"];
            Us = MainHelper.InitUser(Us.Id);

            if (db.Users.Any(x => x.Id == Id))
            {
               
                db.Followings.Add(new Models.Following()
                {
                    UserID=Us.Id,
                    FollowingID=Id,
                });
                db.SaveChanges();
                return Json(true);
            }
            return Json(false);
        }

        [ValidateInput(false)]
        public JsonResult WriteComment([DefaultValue(0)]int Id,string text)
        {
            if(Id>0 && text != null)
            {
                db.Comments.Add(new Comment()
                {
                    UserID = Id,
                    CommentText = text,
                    CommentDate = DateTime.Now

                });
                db.SaveChanges();
                return Json(true);
            }

            return Json(false);

        }
        [ValidateInput(false)]
        public JsonResult Retweet([DefaultValue(0)]int Id, string text)
        {
            User Us = (User)Session["user"];
            Us = MainHelper.InitUser(Us.Id);

            if (Id>0 && text != null)
            {
                db.Retweets.Add(new Retweet()
                {
                    UserID=Us.Id,
                    CommentID=Id,
                    RetweetText=text,
                    RetweetDate=DateTime.Now

                });
                db.SaveChanges();
                return Json(true,JsonRequestBehavior.AllowGet);
            }
            return Json(false,JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        public JsonResult AddReply(string Value,string text)
        {
            User Us = (User)Session["user"];
            Us = MainHelper.InitUser(Us.Id);

            if (Value.Contains("retweetid")==true)
            {
                string newstring = Value.Substring(Value.IndexOf(':') + 1);
                int Id = Convert.ToInt32(newstring);
                db.RetweetReplies.Add(new RetweetReply()
                {
                     UserID=Us.Id,
                     RetweetID=Id,
                     ReplyText=text,
                     ReplyDate=DateTime.Now
                });
                db.SaveChanges();
                return Json(false);
            }
            db.Replies.Add(new Reply()
            {
                 UserID=Us.Id,
                 ReplyText=text,
                 CommentID=Convert.ToInt32(Value),
                  ReplyDate=DateTime.Now
                
            });
            db.SaveChanges();
            return Json(true);
        }



        public static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);

            return newImage;
        }
    }
}