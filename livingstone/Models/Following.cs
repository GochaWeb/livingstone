using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace livingstone.Models
{
    public class Following
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public int FollowingID { get; set; }
    }
}