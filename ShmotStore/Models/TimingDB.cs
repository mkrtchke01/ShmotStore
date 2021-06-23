using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShmotStore.Models
{
    public class TimingDB
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public int IdForProduct { get; set; }
        public Cart CartT { get; set; }
    }
}
