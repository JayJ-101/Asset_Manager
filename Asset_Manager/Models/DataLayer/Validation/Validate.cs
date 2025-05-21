using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Asset_Manager.Models  
{
    public class Validate
    {
        private const string CategoryKey = "validCategory";
        private const string BranchKey = "validBranch";
        private const string SupplierKey = "validSupplier";
        private const string AssetKey = "validAsset";
      



        // constructor and private TempData property
        private ITempDataDictionary tempData { get; set; }
        public Validate(ITempDataDictionary temp) => tempData = temp;

        // public properties
        public bool IsValid { get; private set; }
        public string ErrorMessage { get; private set; } = string.Empty;

        public void CheckCategory(string categoryName, string operation, Repository<Category> data)
        {
            Category? entity = null;
            if (operation.EqualsNoCase("add")) // only check database on add
            {
                entity = data.Get(new QueryOptions<Category>
                {
                    Where = a => a.CategoryName == categoryName
                });
            }
            IsValid = entity == null ? true : false;
            ErrorMessage = IsValid ? "" :
                $"Category {entity!.CategoryName} is already in the database.";
        }
        public void MarkCategoryChecked() => tempData[CategoryKey] = true;
        public void ClearCategory() => tempData.Remove(CategoryKey);
        public bool IsCategoryChecked => tempData.Keys.Contains(CategoryKey);


        public void CheckBranch(string branchName, string operation, Repository<Branch> data)
        {
            Branch? entity = null;
            if (operation.EqualsNoCase("add")) // only check database on add
            {
                entity = data.Get(new QueryOptions<Branch>
                {
                    Where = a => a.BranchName == branchName
                });
            }
            IsValid = (entity == null) ? true : false;
            ErrorMessage = (IsValid) ? "" :
                $"Branch {entity!.BranchName} is already in the database.";
        }
        public void MarkBranchChecked() => tempData[BranchKey] = true;
        public void ClearBranch() => tempData.Remove(BranchKey);
        public bool IsBranhChecked => tempData.Keys.Contains(BranchKey);

        public void CheckSupplier(string supplierName, string operation, Repository<Supplier> data)
        {
            Supplier? entity = null;
            if (operation.EqualsNoCase("add")) // only check database on add
            {
                entity = data.Get(new QueryOptions<Supplier>
                {
                    Where = a => a.SupplierName == supplierName
                });
            }
            IsValid = (entity == null) ? true : false;
            ErrorMessage = (IsValid) ? "" :
                $"Branch {entity!.SupplierName} is already in the database.";
        }
        public void MarkSupplierChecked() => tempData[SupplierKey] = true;
        public void ClearSupplier() => tempData.Remove(SupplierKey);
        public bool IsSupplierChecked => tempData.Keys.Contains(SupplierKey);

        public void CheckAsset(string serialNumber, string operation, Repository<Asset> data)
        {
            Asset? entity = null;
            if (operation.EqualsNoCase("add")) // only check database on add
            {
                entity = data.Get(new QueryOptions<Asset>
                {
                    Where = s => s.SerialNumber == serialNumber
                });
            }
            IsValid = (entity == null) ? true : false;
            ErrorMessage = (IsValid) ? "" :
                $"Serial number {entity!.SerialNumber} is already in the database.";
        }
        public void MarkAssetChecked() => tempData[AssetKey] = true;
        public void ClearAsset() => tempData.Remove(AssetKey);
        public bool IsAssetChecked => tempData.Keys.Contains(AssetKey);

       
    }
}
