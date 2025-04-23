using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Asset_Manager.Models;
using System.Linq;
using System.Threading.Tasks;

public class AssetAssignmentController : Controller
{
    private Repository<AssetAssignment> assignData { get; set; }
    private Repository<Branch> branchData { get; set; }
    private Repository<Asset> assetData { get; set; }


    public AssetAssignmentController(AssetDbContext ctx)
    {
        assignData = new Repository<AssetAssignment>(ctx);
        branchData = new Repository<Branch>(ctx);
        assetData = new Repository<Asset>(ctx);

    }

    public IActionResult Index(AssignGridData values)
    {
        var options = new QueryOptions<AssetAssignment>
        {
            Includes = "Branch,Asset",
            OrderByDirection = values.SortDirection,
            PageNumber = values.PageNumber,
            PageSize = values.PageSize, 
        };


        if (values.IsSortByBranch)
        {
            options.OrderBy = a => a.BranchId;
        }


        if (!string.IsNullOrEmpty(values.SearchQuery))
        {
            options.Where = a => a.Employee.Contains(values.SearchQuery);
        }

        var vm = new AssignListViewModel
        {
            Assign = assignData.List(options),
            CurrentRoute = values,
            TotalPages = values.GetTotalPages(assignData.Count),
            Branches = branchData.List(new QueryOptions<Branch>
            {
                OrderBy = a => a.BranchName,  
            }),
        };
        return View(vm);
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
    public ViewResult Add()
    {
        var vm = new AssignmentViewModel();
        LoadViewData(vm);
        return View("AssetAssignment", vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Add(AssignmentViewModel vm)
    {
        //var validate = new Validate(TempData);
        //if (!validate.IsAssetNameChecked)
        //{
        //    validate.CheckedAssetName(vm.AssetAssignment.AssetName, operation, assignData);
        //    if (!validate.IsValid)
        //    {
        //        ModelState.AddModelError(nameof(vm.AssetAssignment.AssetName) validate.ErrorMessage);
        //    }
        //}

        if (ModelState.IsValid) {
            assignData.Insert(vm.AssetAssignment);
            assignData.Save();
            //validate.ClearAssign();
            //TempData["message"] = $"{vm.AssetAssignment.} added to";
            return RedirectToAction("Index");

        }
        else
        {
            LoadViewData(vm);
            return View("AssignAsset", vm);
        }
    }

    private void LoadViewData(AssignmentViewModel vm)
    {
        vm.Branches = branchData.List(new QueryOptions<Branch>
        {
            OrderBy = b => b.BranchName,
        });
        vm.Assets = assetData.List(new QueryOptions<Asset>
        {
            OrderBy = b => b.AssetName,
        });
    }
}
