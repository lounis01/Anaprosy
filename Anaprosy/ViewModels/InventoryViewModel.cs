using Anaprosy.DAL.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Anaprosy.ViewModels
{
    public class InventoryViewModel
    {
        public Guid ID { get; set; }
        public DateTime Date { get; set; }
        public List<Product> ProductList { get; set; }

               
        public InventoryViewModel()
        {
            ProductList = new List<Product>();
            ID = Guid.NewGuid();
        }
    }
}
