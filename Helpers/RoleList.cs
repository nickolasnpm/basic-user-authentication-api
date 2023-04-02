using System.ComponentModel.DataAnnotations;

namespace UserAuthentication.Helpers
{
    public class RoleList
    {
        private string _title;

        [StringLength(8, MinimumLength = 4, 
            ErrorMessage = "The role input is not within our system")]
        public string Title
        {
            get
            {
                return _title;   
            }
            set
            {
                string input = value.ToLower();
                
                if (input == "admin" || input == "user")
                {
                    _title = input;
                }
                else
                {
                    _title = "";
                }
            }
        }
    }
}
