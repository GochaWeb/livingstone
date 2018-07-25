namespace livingstone.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Comment")]
    public partial class Comment
    {
       

        public int CommentId { get; set; }

        public int UserID { get; set; }

        [Required]
        public string CommentText { get; set; }

        public DateTime CommentDate { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Reply> Replies { get; set; }
        public virtual ICollection<Retweet> Retweets { get; set; }
    }
}
