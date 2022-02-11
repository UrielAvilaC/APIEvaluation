using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModels.Auth
{
    public class UserViewModel
    {
        public string User { get; set; }
        public string Password { get; set; }
    }
    public class PersonViewModel
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
    }
}
