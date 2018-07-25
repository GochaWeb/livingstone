namespace livingstone.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CoverPhoto")]
    public partial class CoverPhoto
    {
        public int Id { get; set; }

        public int UserID { get; set; }

        [Required]
        [StringLength(36)]
        public string Photo { get; set; }

        public virtual User User { get; set; }
    }
}
