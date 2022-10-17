using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDynamics.AI.Model
{
    public class UserLogin
    {
        [Required(ErrorMessage = "UserName is required"), StringLength(50),
         RegularExpression(@"^([A-Za-z0-9_-]){3,20}$",
         ErrorMessage = "El nombre de usuario debe de tener al menos 6 caractéres de A a Z, 0 a 9, _ o -")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required"), StringLength(60),
         RegularExpression(@"^(?=.*\w)(?=.*[0-9])(?=.*[A-Z])(?=.*[+=.\?\-,_!@#\$%&\*;:&])(?=.{6,40}$)((?!\(|\)|\^| |<|>|{|}|\[|\]|~|[\u0080-\u0100]|\/|'|\||\\).)*$",
         ErrorMessage = "The password should have at least 6 characters including at least one capital letter, a number and a special character. \n Valid password example: Abcd,1")]
        public string Password { get; set; }
    }
}
