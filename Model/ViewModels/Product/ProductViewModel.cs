﻿using System;
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
        public Person RegistrationPerson { get; set; }
        public Estatu Estatus { get; set; }
        public string Type { get; set; }
    }
}