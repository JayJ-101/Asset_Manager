using Asset_Manager.Models;
using Microsoft.AspNetCore.Mvc;

namespace Asset_Manager.Controllers
{
    public class ValidationController : Controller
    {
        private Repository<Category> categoryData { get; set; } 
        private Repository<Branch> branchData { get; set; }
        private Repository<Asset> assetData { get; set; }
        private Repository<PurchaseOrder> purchaseOrderData { get; set; }

        public ValidationController(AssetDbContext ctx)
        {
            categoryData = new Repository<Category>(ctx);
            branchData = new Repository<Branch>(ctx);
            assetData = new Repository<Asset>(ctx);
            purchaseOrderData = new Repository<PurchaseOrder>(ctx);
        }


        public JsonResult CheckCategory(string categoryName, string operation)
        {
            var validate = new Validate(TempData);
            validate.CheckCategory(categoryName, operation,categoryData);
            if (validate.IsValid)
            {
                validate.MarkCategoryChecked();
                return Json(true);
            }
            else
            {
                return Json(validate.ErrorMessage);
                    
            }
        }

        public JsonResult CheckBranch(string branchName, string operation)
        {
            var validate = new Validate(TempData);  
            validate.CheckBranch(branchName, operation, branchData);
            if (validate.IsValid)
            {
                validate.MarkBranchChecked();
                return Json(true);
            }
            else
            {
                return Json(validate.ErrorMessage);
            }
        }

        public JsonResult CheckAsset(string serialNumber, string operation)
        {
            var validate = new Validate(TempData);
            validate.CheckAsset(serialNumber, operation, assetData);
            if (validate.IsValid)
            {
                validate.MarkAssetChecked();
                return Json(true);
            }
            else
            {
                return Json(validate.ErrorMessage);
            }
        }

        public JsonResult CheckOrder(string orderNumber, string operation)
        {
            var validate = new Validate(TempData);
            validate.CheckPurchaseOrder(orderNumber, operation, purchaseOrderData);
            if (validate.IsValid)
            {
                validate.MarkOrderChecked();
                return Json(true);
            }
            else
            {
                return Json(validate.ErrorMessage);
            }
        }

    }
}
