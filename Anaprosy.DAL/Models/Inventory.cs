using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Anaprosy.DAL.Models
{
    public class Inventory
    {
        public Guid ID { get; set; }
        public DateTime Date { get; set; }

        public bool IsDeleted { get; set; }

        public List<Product> ProductList { get; set; }


        public Inventory()
        {
            ProductList = new List<Product>();
        }
    }
}
