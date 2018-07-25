namespace livingstone.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Reply")]
    public partial class Reply
    {
        public int ReplyId { get; set; }

        public int UserID { get; set; }

        public int CommentID { get; set; }

        public string ReplyText { get; set; }

        public DateTime ReplyDate { get; set; }

        public virtual Comment Comment { get; set; }

        public virtual User User { get; set; }
    }
}
