using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShmotStore.Models
{
    public class FilterModel
    {
        public IEnumerable<Product> products { get; set; }
        public SelectList Seasons { get; set; }
        public SelectList Genders { get; set; }
    }
}
