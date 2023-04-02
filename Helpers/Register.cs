using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace UserAuthentication.Helpers
{
    public class Register
    {
        [Required]
        [StringLength(100)]
        public string firstName { get; set; }

        [Required]
        [StringLength(100)]
        public string lastName { get; set; }

        [Required]
        [Range(0, 100)]
        public int age { get; set; }

        [Required]
        public List<RoleList> roles { get; set; }

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

                if(Regex.IsMatch(value, pattern))
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

        private string _confirmPassword;

        [StringLength(20, MinimumLength = 8,
            ErrorMessage = "The Password and Confirmation Password are not matched")]
        public string confirmPassword
        {
            get
            {
                return _confirmPassword;
            }
            set
            {
                if (value == password)
                {
                    _confirmPassword = password;
                }
                else
                {
                    _confirmPassword = "";
                }

            }

        }
    }
}