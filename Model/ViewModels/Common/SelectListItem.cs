using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModels.Common
{
   public class SelectListItem
    {
        public string Code{ get; set; }
        public string Text { get; set; }
    }
    public enum UserEstatus
    {
        Active = 1,
        Inactive = 2,
    }
    public enum ProductEstatus
    {
        Active = 1,
        Cancel = 3,
    }
}
