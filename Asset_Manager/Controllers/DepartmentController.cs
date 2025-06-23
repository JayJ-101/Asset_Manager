using Asset_Manager.Models;
using Microsoft.AspNetCore.Mvc;

namespace Asset_Manager.Controllers
{
    public class DepartmentController : Controller
    {
        private Repository<Department> departmentData { get; set; }
        private Repository<Branch> branchData { get; set; }

        public DepartmentController(AssetDbContext ctx)
        {
            departmentData = new Repository<Department>(ctx);
            branchData = new Repository<Branch>(ctx);
        }



        public IActionResult Index()
        {
            var departments = departmentData.List(new QueryOptions<Department>
            {
                Includes = "Branch",
                OrderBy = d => d.DepartmentName
            });

            return View(departments);
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
        
        [HttpGet]
        public IActionResult Add()
        {
            LoadBranches();
            return View("Department", new Department());
        }
       
        
        [HttpPost]
        public IActionResult Add(Department department)
        {
            if (ModelState.IsValid)
            {
                departmentData.Insert(department);
                departmentData.Save();
                TempData["message"] = $"Department '{department.DepartmentName}' added.";
                return RedirectToAction("Index");
            }
            LoadBranches();
            return View("Department", department);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var department = departmentData.Get(id);
            LoadBranches();
            return View("Department", department);
        }

        [HttpPost]
        public IActionResult Edit(Department department)
        {
            if (ModelState.IsValid)
            {
                departmentData.Update(department);
                departmentData.Save();
                TempData["message"] = $"Department '{department.DepartmentName}' updated.";
                return RedirectToAction("Index");
            }
            LoadBranches();
            return View("Department", department);
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var department = departmentData.Get(id);
            return View("Department", department);
        }

        [HttpPost]
        public IActionResult Delete(Department department)
        {
            departmentData.Delete(department);
            departmentData.Save();
            TempData["message"] = $"Department '{department.DepartmentName}' deleted.";
            return RedirectToAction("Index");
        }


        private void LoadBranches()
        {
            ViewBag.Branches = branchData.List(new QueryOptions<Branch>
            {
                OrderBy = b => b.BranchName
            });
        }
    }

}
