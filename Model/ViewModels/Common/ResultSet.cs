using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModels.Common
{
    public class ResultSet<T>
    {
        public bool Success { get; set; }
        public T ObjectResult { get; set; }
        public string ErrorMessage { get; set; }
        
    }
}
