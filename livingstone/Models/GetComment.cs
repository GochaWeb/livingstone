using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace livingstone.Models
{
    public class GetComment
    {
       

        public static List<Comment> Comments(int id)
        {
            Livingstone db = new Livingstone();
            var commentlist = new List<Comment>();
            foreach(var item in db.Comments.Where(x => x.UserID == id))
            {
                commentlist.Add(new Comment()
                {
                     CommentId=item.CommentId,
                     UserID=item.UserID,
                     CommentText=item.CommentText,
                     CommentDate=item.CommentDate

                });
            }

            foreach(var item in db.Replies.Where(x => x.UserID == id))
            {


            }



            return commentlist;
            
        }

    }
}