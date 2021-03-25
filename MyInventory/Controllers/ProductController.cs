using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyInventory.Data;
using MyInventory.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace MyInventory.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            var list = _context.Products.Include(p => p.Category).ToList();
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product record, IFormFile ImagePath)
        {
            var product = new Product();
            product.Name = record.Name;
            product.Code = record.Code;
            product.Description = record.Description;
            product.Price = record.Price;
            product.Available = 0;
            product.DateAdded = DateTime.Now;
            product.status = "Active";
            product.Category = record.Category;

            if (ImagePath != null)
            {
                if (ImagePath.Length > 0)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot/img/products", ImagePath.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        ImagePath.CopyTo(stream);
                    }
                    product.ImagePath = ImagePath.FileName;
                }
            }




            _context.Products.Add(product);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var product = _context.Products.Where(p => p.ProductId == id).SingleOrDefault();
            if (product == null)
            {
                return RedirectToAction("Index");
            }
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(int? id, Product record)
        {
            var product = _context.Products.Where(p => p.ProductId == id).SingleOrDefault();
            product.Name = record.Name;
            product.Code = record.Code;
            product.Description = record.Description;
            product.Price = record.Price;
            product.DateModified = DateTime.Now;
            product.status = record.status;
            product.Category = record.Category;

            _context.Products.Update(product);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var product = _context.Products.Where(p => p.ProductId == id).SingleOrDefault();
            if (product == null)
            {
                return RedirectToAction("Index");
            }

            _context.Products.Remove(product);
            _context.SaveChanges();

            return RedirectToAction("Index");

        }

    }
}