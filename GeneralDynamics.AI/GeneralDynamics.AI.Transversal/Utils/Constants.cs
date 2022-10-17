using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDynamics.AI.Transversal.Utils
{
    public class Constants
    {
        public const string USERNAME_REGEX = @"^([A-Za-z0-9_-]){3,20}$";
        public const string PASSWORD_REGEX = @"^(?=.*\w)(?=.*[0-9])(?=.*[A-Z])(?=.*[+=.\?\-,_!@#\$%&\*;:&])(?=.{6,40}$)((?!\(|\)|\^| |<|>|{|}|\[|\]|~|[\u0080-\u0100]|\/|'|\||\\).)*$";
        public const string EMAIL_REGEX = @"^$|^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$";
    }
}
