using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Anaprosy.DAL.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        [DisplayName("Nom produit")] 
        public string Name { get; set; }
     
         public bool IsDeleted { get; set; }

        public int Quantity { get; set; }

        
        public Guid? InventoryID { get; set; }
        public Product()
        {
            InventoryID = Guid.NewGuid();
        }
        public Product(string id)
        {
            InventoryID = Guid.NewGuid();
            Id = new Guid(id);
        }
    }
}
