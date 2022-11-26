﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaThirdWeek.Domain.Entities
{
    public class Company : BaseEntity
    {

        public string Name { get; set; }
        public string Adress { get; set; }
        public string City { get; set; }
        public string TaxNumber { get; set; }
        public string Email { get; set; }

    }
}
