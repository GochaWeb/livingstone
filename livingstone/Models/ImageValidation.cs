using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace livingstone.Models
{
    public class ImageValidation
    {
        [Required(ErrorMessage = "გთხოვთ აირჩიოთ ფაილი")]
        [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.jpeg)$", ErrorMessage = "ფაილი უნდა იყოს მხოლოდ ამ გაფართოებით .jpg .jpeg .png")]
        public HttpPostedFileBase Photo { get; set; }

       
    }

    
}