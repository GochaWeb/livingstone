using livingstone.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace livingstone.Models
{
     
    public class GetUsers
    {
       
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }
        public string RegNumber { get; set; }
        public DateTime CreateDate { get; set; }

   
        public static List<GetUsers> GetNotFollowin(int id)
        {
            Livingstone db = new Livingstone();
           

            var followers = db.Followings.Where(x => x.UserID == id).ToList();
            var userlist = new List<GetUsers>();

            foreach (var item in db.Users)
            {
                if (followers.Any(x => x.FollowingID == item.Id))
                {
                   
                    //Followers
                }
                else
                {
                    if (item.ProfilePhotoes.FirstOrDefault().Photo == null)
                    {
                        userlist.Add(new GetUsers()
                        {
                            Id = item.Id,
                            Name = item.Name,
                            Surname = item.Surname,
                            Photo = "noImage.png",
                            Email = item.Email,
                            RegNumber = item.RegNumber,
                            CreateDate = item.CreateDate
                        });

                    }
                    else
                    {
                        userlist.Add(new GetUsers()
                        {
                            Id = item.Id,
                            Name = item.Name,
                            Surname = item.Surname,
                            Photo = item.ProfilePhotoes.FirstOrDefault().Photo,
                            Email = item.Email,
                            RegNumber = item.RegNumber,
                            CreateDate = item.CreateDate
                        });

                    }
                    
                    
                      

                    
                   
                }

            }
            userlist.Remove(userlist.Where(x => x.Id == id).FirstOrDefault());
            
            
            return userlist;
        }

       
    }
}