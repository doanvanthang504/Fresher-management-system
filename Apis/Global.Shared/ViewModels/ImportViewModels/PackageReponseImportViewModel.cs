using System.Collections.Generic;

namespace Global.Shared.ViewModels.ImportViewModels
{
    public class PackageReponseFromRECFileImportViewModel
    {
        public PackageReponseFromRECFileImportViewModel()
        {
            ListAccountImportViewModel = new List<AccountImportViewModel>();
            ListClassImportViewModel = new List<ClassImportViewModel>();
            ListFresherImportViewModel = new List<FresherImportViewModel>();
        }

        public IEnumerable<AccountImportViewModel> ListAccountImportViewModel { get; set; }

        public IEnumerable<ClassImportViewModel> ListClassImportViewModel { get; set; }

        public IEnumerable<FresherImportViewModel> ListFresherImportViewModel { get; set; }
    }
}
