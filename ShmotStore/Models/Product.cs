using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShmotStore.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Production { get; set; }

        public double Price { get; set; }
        public string Desc { get; set; }
        public string Image { get; set; }
        public string Season { get; set; }

        public string Gender { get; set; }
        public int Amount { get; set; }
        public string Size { get; set; }
    }
}
