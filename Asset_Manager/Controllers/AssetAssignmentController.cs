using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Asset_Manager.Models;
using System.Linq;

public class AssetAssignmentController : Controller
{
    private Repository<AssetAssignment> assignData { get; }
    private Repository<Branch> branchData { get; }
    private Repository<Department> departmentData { get; }
    private Repository<Asset> assetData { get; }
    private Repository<Category> categoryData { get; }

    public AssetAssignmentController(AssetDbContext ctx)
    {
        assignData = new Repository<AssetAssignment>(ctx);
        branchData = new Repository<Branch>(ctx);
        departmentData = new Repository<Department>(ctx);
        assetData = new Repository<Asset>(ctx);
        categoryData = new Repository<Category>(ctx);
    }

    public IActionResult Index(AssignGridData values)
    {
        var options = new QueryOptions<AssetAssignment>
        {
            Includes = "Branch,Asset,Department,Asset.Category",
            OrderByDirection = values.SortDirection,
            PageNumber = values.PageNumber,
            PageSize = values.PageSize,
        };

        // Sorting
        if (values.IsSortByBranch)
            options.OrderBy = a => a.BranchId;
        else
            options.OrderBy = a => a.AssginedDate;

        //Combined filters in one lambda
        options.Where = a =>
            (values.ShowAll || a.ReturnDate == null) && // Active-only unless "Show All"
            (string.IsNullOrEmpty(values.SearchQuery) || a.Employee.Contains(values.SearchQuery)) &&
            (!values.BranchId.HasValue || a.BranchId == values.BranchId) &&
            (!values.CategoryId.HasValue || a.Asset.CategoryId == values.CategoryId) &&
            (!values.DepartmentId.HasValue || a.DepartmentId == values.DepartmentId);

        var vm = new AssignListViewModel
        {
            Assign = assignData.List(options),
            CurrentRoute = values,
            TotalPages = values.GetTotalPages(assignData.Count),
            Branches = branchData.List(new QueryOptions<Branch> { OrderBy = b => b.BranchName }),
            Departments = departmentData.List(new QueryOptions<Department> { OrderBy = b => b.DepartmentName }),
            Categories = categoryData.List(new QueryOptions<Category> { OrderBy = c => c.CategoryName })

        };

        return View(vm);
    }



    [HttpGet]
    public IActionResult GetByBranch(int branchId)
    {
        var departments = departmentData.List(new QueryOptions<Department>
        {
            Where = d => d.BranchId == branchId,
            OrderBy = d => d.DepartmentName
        });

        return Json(departments.Select(d => new {
            id = d.DepartmentId,
            name = d.DepartmentName
        }));
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

    [HttpGet]
    public IActionResult Add(int? assetId)
    {
        var vm = new AssignmentViewModel();

        if (assetId.HasValue)
        {
            vm.AssetAssignment.AssetId = assetId.Value;
        }

        LoadViewData(vm);
        return View("AssetAssignment", vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Add(AssignmentViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            LoadViewData(vm);
            return View("AssetAssignment", vm);
        }

        assignData.Insert(vm.AssetAssignment);

        // Update asset status to "Assigned"
        var asset = assetData.Get(vm.AssetAssignment.AssetId);
        if (asset != null)
        {
            asset.Status = "Assigned";
            assetData.Update(asset);
        }

        assignData.Save();
        assetData.Save();

        TempData["message"] = $"Asset assigned to {vm.AssetAssignment.Employee}.";
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var assignment = assignData.Get(new QueryOptions<AssetAssignment>
        {
            Includes = "Asset,Branch",
            Where = a => a.AssingmentId == id
        });

        if (assignment == null) return NotFound();

        var vm = new AssignmentViewModel
        {
            AssetAssignment = assignment
        };

        LoadViewData(vm);
        return View("AssetAssignment", vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(AssignmentViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            LoadViewData(vm);
            return View("AssetAssignment", vm);
        }

        // Update asset status if returned
        var asset = assetData.Get(vm.AssetAssignment.AssetId);
        if (asset != null)
        {
            asset.Status = vm.AssetAssignment.ReturnDate.HasValue ? "Available" : "Assigned";
            assetData.Update(asset);
        }

        assignData.Update(vm.AssetAssignment);
        assignData.Save();
        assetData.Save();

        TempData["message"] = $"Assignment updated for {vm.AssetAssignment.Employee}.";
        return RedirectToAction("Index");
    }

    // GET: Return confirmation page
    [HttpGet]
    public IActionResult ReturnAsset(int id)
    {
        var assignment = assignData.Get(new QueryOptions<AssetAssignment>
        {
            Where = a => a.AssingmentId == id,
            Includes = "Asset,Branch"
        });

        if (assignment == null)
            return NotFound();

        return View(assignment);
    }

    // POST: Process return
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ReturnAssetConfirmed(int id)
    {
        var assignment = assignData.Get(id);
        if (assignment == null) return NotFound();

        assignment.ReturnDate = DateTime.Now;
        assignData.Update(assignment);
        assignData.Save();

        var asset = assetData.Get(assignment.AssetId);
        if (asset != null)
        {
            asset.Status = "Available";
            assetData.Update(asset);
            assetData.Save();
        }

        TempData["message"] = "Asset successfully returned.";
        return RedirectToAction("Index");
    }


    [HttpGet]
    public IActionResult Delete(int id)
    {
        var assignment = assignData.Get(id);
        if (assignment == null) return NotFound();

        return View(assignment);
    }

    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var assignment = assignData.Get(id);
        if (assignment == null) return NotFound();

        // Optionally reset status
        var asset = assetData.Get(assignment.AssetId);
        if (asset != null)
        {
            asset.Status = "Available";
            assetData.Update(asset);
        }

        assignData.Delete(assignment);
        assignData.Save();
        assetData.Save();

        TempData["message"] = "Assignment deleted.";
        return RedirectToAction("Index");
    }

    public IActionResult Details(int id)
    {
        var asset = assetData.Get(new QueryOptions<Asset>
        {
            Where = a => a.AssetId == id,
            Includes = "Category,Supplier,AssetAssignments.Branch"
        });

        if (asset == null) return NotFound();

        return View(asset);
    }

    private void LoadViewData(AssignmentViewModel vm)
    {
        vm.Branches = branchData.List(new QueryOptions<Branch> { OrderBy = b => b.BranchName });
        vm.Departments = departmentData.List(new QueryOptions<Department> { OrderBy = b => b.DepartmentName });
        
        vm.Assets = assetData.List(new QueryOptions<Asset>
        {
            Where = a => a.Status == "Available", // Only unassigned
            OrderBy = a => a.AssetName
        });
    }
}
