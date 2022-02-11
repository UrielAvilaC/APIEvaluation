using Model.ViewModels.Auth;
using Model.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModels.Product
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Code{ get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime RegistrationDate { get; set; }
        public PersonViewModel RegistrationPerson { get; set; }
        public EstatusProduct Estatus { get; set; }
        public string idEstatus { get; set; }
        public string Type { get; set; }
    }
    public class LogProductViewModel
    {
        public string Type { get; set; }
        public DateTime AffectedDate { get; set; }
        public string AffectedPerson { get; set; }
        public string OldCode { get; set; }
        public string OldName { get; set; }
        public string OldPrice { get; set; }
        public string OldType{ get; set; }
        public EstatusProduct OldEstatus { get; set; }
        public string NewCode { get; set; }
        public string NewName { get; set; }
        public string NewPrice { get; set; }
        public string NewType{ get; set; }
        public EstatusProduct NewEstatus { get; set; }
    }
}
