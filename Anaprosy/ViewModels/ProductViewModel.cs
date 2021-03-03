using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Anaprosy.ViewModels
{
    public class ProductViewModel
    {
        public Guid ID { get; set; }

        [DisplayName("Nom produit")]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public int Quantity  { get; set; }

    }
}
