using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace livingstone.Models
{
    public class RetweetReply
    {
        [Key]
        public int ReplyId { get; set; }

        public int UserID { get; set; }

        public int RetweetID { get; set; }

        public string ReplyText { get; set; }

        public DateTime ReplyDate { get; set; }

        public virtual Retweet Retweet { get; set; }

        public virtual User User { get; set; }
    }
}