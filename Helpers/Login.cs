using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace UserAuthentication.Helpers
{
    public class Login
    {
        private string _emailAddress;

        [StringLength(50, MinimumLength = 1, 
            ErrorMessage = "Please enter a valid email address")]
        public string emailAddress
        {
            get
            {
                return _emailAddress;
            }
            set
            {
                string pattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

                if (Regex.IsMatch(value, pattern))
                {
                    _emailAddress = value;
                }
                else
                {
                    _emailAddress = "";
                }
            }
        }

        private string _password;

        [StringLength(20, MinimumLength = 8, 
            ErrorMessage = "Password should have 8 to 20 characters including uppercase, lowercase, numbers and special character")]
        public string password
        {
            get
            {
                return _password;
            }
            set
            {
                string pattern = "^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[!@#$%&_])[A-Za-z0-9!@#$%_]{8,20}$";

                if (Regex.IsMatch(value, pattern))
                {
                    _password = value;
                }
                else
                {
                    _password = "";
                }
            }
        }
    }
}
