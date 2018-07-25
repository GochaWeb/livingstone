using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace livingstone.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "შეავსეთ ელ-ფოსტა!")]
        [StringLength(500, ErrorMessage = "მაქსიმუმ, 500")]
        [RegularExpression(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
+ "@"
+ @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$", ErrorMessage = "გთხოვთ, ჩაწერეთ ელ-ფოსტა!")]
        [DataType(DataType.EmailAddress)]
       // [System.Web.Mvc.Remote("Email", "Account", ErrorMessage = "ასეთი ელ.ფოსტა არსებობს")]
        public string Email { get; set; }

        [Required(ErrorMessage = "შეავსეთ პაროლი!")]
        [StringLength(32, ErrorMessage = "პაროლი უნდა შედგებოდეს, მინიმუმ, {2} და მაქსიმუმ {1} სიმბოლოსგან", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}