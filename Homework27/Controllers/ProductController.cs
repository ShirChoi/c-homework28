using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Homework28.Context;
using Microsoft.EntityFrameworkCore;
using Homework28.Models;
using System.Threading;
using Homework28.Models.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Homework28.Controllers {
    public class ProductController : Controller {
        private readonly MVCContext _context;
        public ProductController(MVCContext context) {
            _context = context;
        }

        public async Task<ActionResult> IndexAsync(int prodCategoryID = 0) {
            var result = _context.Products.AsQueryable();

            if(prodCategoryID > 0)
                result = result.Where(prod => prod.ProductCategoryID == prodCategoryID);
            var tst = result.ToList();
            var productList = new List<ProductViewModel>();

            foreach(var prod in await result.ToListAsync()) {
                productList.Add(new ProductViewModel {
                    ID = prod.ID,
                    Name = prod.Name,
                    Cost = prod.Cost,
                    ProductCategoryID = prod.ProductCategoryID,
                    ProductCategoryName = prod.ProductCategory.Name
                });
            }

            return View(productList);
        }


        [HttpGet]
        public async Task<IActionResult> Create() {
            var product = new ProductViewModel() {
                Categories = await _context
                .ProductCategories
                .Select(p => new SelectListItem { Value = p.ID.ToString(), Text = p.Name }).ToListAsync()
            };

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel product, CancellationToken token) {
            if(!ModelState.IsValid) {
                product.Categories = await _context.ProductCategories
                    .Select(p => new SelectListItem { Value = p.ID.ToString(), Text = p.Name }).ToListAsync();
                return View(product);
            }

            var productDB = new Product {
                Name = product.Name,
                Cost = product.Cost,
                ProductCategoryID = product.ProductCategoryID
            };

            _context.Products.Add(productDB);
            await _context.SaveChangesAsync(token);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int ID) {
            var product = await _context.Products.FindAsync(ID);
            if(product == null) {
                return RedirectToAction("Index");
            }

            var result = new ProductViewModel {
                ID = product.ID,
                Cost = product.Cost,
                ProductCategoryID = product.ProductCategoryID,
                Name = product.Name,
                Categories = await _context.ProductCategories.
                    Select(p => new SelectListItem { Value = p.ID.ToString(), Text = p.Name }).ToListAsync()
            };

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel productModel) {
            if(!ModelState.IsValid) {
                productModel.Categories = await _context.ProductCategories.
                    Select(p => new SelectListItem { Value = p.ID.ToString(), Text = p.Name }).ToListAsync();
                return View(productModel);
            }

            var phone = await _context.Products.FindAsync(productModel.ID);

            if(productModel == null) 
                return RedirectToAction("Index");
            

            phone.Cost = productModel.Cost;
            phone.Name = productModel.Name;
            phone.ProductCategoryID = productModel.ProductCategoryID;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Delete(int ID) {
            Product product = await _context.Products.FindAsync(ID);

            if(product == null)
                return RedirectToAction("Index");

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
