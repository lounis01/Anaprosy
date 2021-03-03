using Anaprosy.DAL;
using Anaprosy.DAL.Models;
using Anaprosy.Models;
using Anaprosy.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Anaprosy.Controllers
{
    public class InventoryController : Controller
    {

        public readonly MyContext _myContext;


        public InventoryController(MyContext myContext)
        {
            _myContext = myContext;

        }


        public IActionResult Create()
        {
            List<SelectListItem> selectList = new List<SelectListItem>();
            foreach (var product in _myContext.Products.ToList().GroupBy(x => x.Name).Select(x => x.First()).ToList())
            {
                selectList.Add(new SelectListItem(product.Name, product.Id.ToString()));
            }
            var model = new InventoryViewModel();




            ViewBag.Products = selectList;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Date,ProductList,ID")] InventoryViewModel vm)
        {
            if (ModelState.IsValid)
            {
                foreach (var product in vm.ProductList)
                {
                    product.Name = _myContext.Products.Find(product.Id).Name;
                }
                Inventory inv = new Inventory();
                inv.Date = DateTime.UtcNow;
                inv.ID = Guid.NewGuid();
                inv.ProductList = vm.ProductList;
                inv.IsDeleted = false;
                foreach (var product in inv.ProductList)
                {
                    product.Id = Guid.NewGuid();
                }


                _myContext.Add(inv);
                await _myContext.SaveChangesAsync();


                return RedirectToAction(nameof(List));
            }
            return View(vm);

        }





        public IActionResult List()
        {
            List<Inventory> inventories = _myContext.Inventories.Where(i=>i.IsDeleted==false).ToList();
            foreach (var inv in inventories)
            {
                inv.ProductList = _myContext.Products.Where(p=>p.InventoryID==inv.ID).ToList();
                
            }
            _myContext.Dispose();
            return View(inventories);
        }


        public IActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = _myContext.Inventories.FirstOrDefault(m => m.ID == id);
            inventory.ProductList = _myContext.Products.Where(m => m.InventoryID == id).ToList();
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Inventory inv)
        {
            var inventory = await _myContext.Inventories.FindAsync(inv.ID);
            //Suppresiion logique 
            inventory.IsDeleted = true;
            await _myContext.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }




        //// GET: OrderItems/Edit/5
        //public async Task<IActionResult> Edit(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var inv = await _myContext.Inventories.FindAsync(id);
        //    inv.ProductList = _myContext.Products.Where(m => m.InventoryID == id).ToList();
        //    if (inv == null)
        //    {
        //        return NotFound();
        //    }
        //    InventoryViewModel vm = new InventoryViewModel();
        //    vm.ID = inv.ID;
        //    vm.Date = inv.Date;
        //    vm.ProductList = inv.ProductList;
        //    List<SelectListItem> selectList = new List<SelectListItem>();
        //    foreach (var product in _myContext.Products.ToList().GroupBy(x => x.Name).Select(x => x.First()).ToList())
        //    {
        //        selectList.Add(new SelectListItem(product.Name, product.Id.ToString()));
        //    }

        //    _myContext.Dispose();
        //    ViewBag.Products = selectList;
        //    return View(inv);
        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit([Bind("Date,ProductList,ID")] Inventory inventory)
        //{
           
        //    if (ModelState.IsValid)
        //    {
               
        //        _myContext.Update(inventory);
        //        await _myContext.SaveChangesAsync();
               
               
        //        return RedirectToAction(nameof(List));
        //    }
        //    return View(inventory);
        //}


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
