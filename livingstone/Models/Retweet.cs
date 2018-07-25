using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace livingstone.Models
{
    [Table("Retweet")]
    public partial class Retweet
    {
        public int RetweetId { get; set; }

        public int UserID { get; set; }

        public int CommentID { get; set; }

        public string RetweetText { get; set; }

        public DateTime RetweetDate { get; set; }

        public virtual Comment Comment { get; set; }

        public virtual ICollection<RetweetReply> RetweetReplies { get; set; }

        public virtual User User { get; set; }

       

      
    }
}