using Anaprosy.DAL;
using Anaprosy.DAL.Models;
using Anaprosy.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Anaprosy.Controllers
{
    public class ProductController : Controller
    {

        public readonly MyContext _myContext;


        public ProductController(MyContext myContext)
        {
            _myContext = myContext;

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddProduct([Bind("ProductList")] InventoryViewModel inventory)
        {
            inventory.ProductList.Add(new Product());
            List<SelectListItem> selectList = new List<SelectListItem>();
            foreach (var product in _myContext.Products.ToList().GroupBy(x => x.Name).Select(x => x.First()).ToList())
            {
                selectList.Add(new SelectListItem(product.Name, product.Id.ToString()));
            }

            ViewBag.Products = selectList;
            return PartialView("Create", inventory);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> EditProduct([Bind("ProductList")] InventoryViewModel inventory)
        //{
        //    inventory.ProductList.Add(new Product());
        //    List<SelectListItem> selectList = new List<SelectListItem>();
        //    foreach (var product in _myContext.Products.ToList().GroupBy(x => x.Name).Select(x => x.First()).ToList())
        //    {
        //        selectList.Add(new SelectListItem(product.Name, product.Name));
        //    }

        //    ViewBag.Products = selectList;
        //    return PartialView("Edit", inventory);
        //}




    }
}
