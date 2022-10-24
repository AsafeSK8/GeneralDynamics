using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GeneralDynamics.AI.Transversal.Utils
{
    public static class InputValidation
    {
        public static bool ValidateRegex(string pattern, string testString, int maxSecondsTime = 20)
        {
            try
            {
                if (string.IsNullOrEmpty(testString)) return false;
                else return Regex.IsMatch(testString, pattern) ? true : false;
            }
            catch
            {
                throw;
            }
        }
    }
}
