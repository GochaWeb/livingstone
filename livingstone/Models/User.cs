namespace livingstone.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        

        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        public string Surname { get; set; }

        [Required]
        [StringLength(500)]
        public string Email { get; set; }

        [Required]
        [StringLength(32)]
        public string Password { get; set; }

        [Required]
        [StringLength(32)]
        public string Confirmation { get; set; }

        public bool IsActive { get; set; }

        [Required]
        public string RegNumber { get; set; }

        [Required]
        [StringLength(360)]
        public string Search { get; set; }

        public DateTime CreateDate { get; set; }

       
        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<CoverPhoto> CoverPhotoes { get; set; }

        public virtual ICollection<ProfilePhoto> ProfilePhotoes { get; set; }

        public virtual ICollection<Reply> Replies { get; set; }

        public virtual ICollection<Following> Followings { get; set; }

        public virtual ICollection<Retweet> Retweets { get; set; }
    }
}
