using Asset_Manager.Models;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Asset_Manager.Controllers
{
    public class BranchController: Controller
    {
        private Repository<Branch> branchData { get; set; }
        public BranchController(AssetDbContext ctx) => branchData = new Repository<Branch>(ctx);

        public IActionResult Index()
        {
            var branches = branchData.List(new QueryOptions<Branch>()
            {
                OrderBy =b => b.BranchName
            });
            return View(branches);
        }


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
        public ViewResult Add() => View("Branch", new Branch());

        [HttpPost]
        public IActionResult Add(Branch branch, string operation)
        {
            //server - side version of remote validation
            var validate = new Validate(TempData);
            if (!validate.IsCategoryChecked)
            {
                validate.CheckBranch(branch.BranchName, operation, branchData);
                if (!validate.IsValid)
                {
                    ModelState.AddModelError(nameof(branch.BranchName), validate.ErrorMessage);
                }
            }

            if (ModelState.IsValid) 
            {
                branchData.Insert(branch);
                branchData.Save();
                validate.ClearCategory();
                TempData["message"] = $"{branch.BranchName} added to Categories.";
                return RedirectToAction("Index");  // PRG pattern
            }
            else
            {
                return View("Branch", branch);
            }
        }

        // edit
        [HttpGet]
        public ViewResult Edit(int id) => View("Branch", branchData.Get(id));

        [HttpPost]
        public IActionResult Edit(Branch branch)
        {
            // no remote validation of author on edit
            if (ModelState.IsValid)
            {
                branchData.Update(branch);
                branchData.Save();
                TempData["message"] = $"{branch.BranchName} updated.";
                return RedirectToAction("Index");  // PRG pattern
            }
            else
            {
                return View("Branch", branch);
            }
        }

        // delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var branch = branchData.Get(new QueryOptions<Branch>
            {
                Where = b => b.BranchId == id,
                //Includes = "Books"
            });

            //if (genre != null && genre.Books.Count > 0)
            //{
            //    TempData["message"] = $"Can't delete genre {genre.Name} " +
            //                          "because it has related books.";
            //    return RedirectToAction("Related", "Book",
            //        new { id = "genre-" + id });
            //}
            //else
            //{
            //    return View("Genre", genre);
            //}
            return View("Branch", branch);
        }

        [HttpPost]
        public IActionResult Delete(Branch branch)
        {
            branchData.Delete(branch);
            branchData.Save();
            TempData["message"] = $"{branch.BranchName} removed from branch's.";
            return RedirectToAction("Index");  // PRG pattern
        }
    }
}
