using Asset_Manager.Models;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel.DataAnnotations;

namespace Asset_Manager.Controllers
{
    public class CategoryController : Controller
    {
        private Repository<Category> categoryData { get; set; }
        public CategoryController(AssetDbContext ctx) =>categoryData = new Repository<Category>(ctx);


        public IActionResult Index()
        {
            var categorties = categoryData.List(new QueryOptions<Category>
            {
                OrderBy = c => c.CategoryName
            });
            return View(categorties);
        }

        // select (posted from genre drop down on Index page). 
        [HttpPost]
        public RedirectToActionResult Select(int id, string operation)
        {
            switch (operation.ToLower())
            {
                case "edit":
                    return RedirectToAction("Edit", new { id });
                case "delete":
                    return RedirectToAction("Delete", new { id });
                default:
                    return RedirectToAction("Index");
            }
        }

        // add
        [HttpGet]
        public ViewResult Add() => View("Category", new Category());

        [HttpPost]
        public IActionResult Add(Category category, string operation)
        {
            //server - side version of remote validation
            var validate = new Validate(TempData);
            if (!validate.IsCategoryChecked)
            {
                validate.CheckCategory(category.CategoryName, operation, categoryData);
                if (!validate.IsValid)
                {
                    ModelState.AddModelError(nameof(category.CategoryName), validate.ErrorMessage);
                }
            }

            if (ModelState.IsValid)
            {
                categoryData.Insert(category);
                categoryData.Save();
                validate.ClearCategory();
                TempData["message"] = $"{category.CategoryName} added to Categories.";
                return RedirectToAction("Index");  // PRG pattern
            }
            else
            {
                return View("Category", category);
            }
        }


        // edit
        [HttpGet]
        public ViewResult Edit(int id) => View("Category", categoryData.Get(id));

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            // no remote validation of author on edit
            if (ModelState.IsValid)
            {
                categoryData.Update(category);
                categoryData.Save();
                TempData["message"] = $"{category.CategoryName} updated.";
                return RedirectToAction("Index");  // PRG pattern
            }
            else
            {
                return View("Category", category);
            }
        }
        // delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var category = categoryData.Get(new QueryOptions<Category>
            {
                Where = a => a.CategoryId == id
                //Includes = "Assets"
            });

            //if ( category != null && category.Assets.Count > 0)
            //{
            //    TempData["message"] = $"Can't delete category {category.CategoryName} " +
            //                          "because they have related Assets.";
            //    return RedirectToAction("Related", "Assets",
            //        new { id = "category-" + id });
            //}
            //else
            //{
            //    return View("Category", category);
            //}
            return View("Category", category);
        }

        [HttpPost]
        public RedirectToActionResult Delete(Category category)
        {
            categoryData.Delete(category);
            categoryData.Save();
            TempData["message"] = $"{category.CategoryName} removed from Categories.";
            return RedirectToAction("Index");  // PRG pattern
        }
    }
}
