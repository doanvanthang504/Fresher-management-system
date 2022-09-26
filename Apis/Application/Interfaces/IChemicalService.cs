using Global.Shared.Commons;
using Global.Shared.ViewModels.ChemicalsViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IChemicalService
    {
        public Task<List<ChemicalViewModel>> GetChemicalAsync();

        public Task<ChemicalViewModel?> CreateChemicalAsync(CreateChemicalViewModel chemical);

        public Task<Pagination<ChemicalViewModel>> GetChemicalPagingsionAsync(int pageIndex = 0, int pageSize = 10);
    }
}
