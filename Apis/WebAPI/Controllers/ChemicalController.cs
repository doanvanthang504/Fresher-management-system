using Application.Interfaces;
using AutoFixture;
using Global.Shared.Commons;
using Global.Shared.ViewModels.ChemicalsViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class ChemicalController : BaseController
    {
        private readonly IChemicalService _chemicalService;

        public ChemicalController(IChemicalService chemicalService)
        {
            _chemicalService = chemicalService;
        }

        [HttpGet]
        public async Task<Pagination<ChemicalViewModel>> GetChemicalPagingsion(int pageIndex = 0, int pageSize = 10)
        {
            var result = await _chemicalService.GetChemicalPagingsionAsync(pageIndex, pageSize);

            if(result.TotalItemsCount == 0)
            {
                var _fixture = new Fixture();
                var temp = new Pagination<ChemicalViewModel>
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    TotalItemsCount = 10,
                    Items = _fixture.Build<ChemicalViewModel>().CreateMany(10).ToList()
                };
                return temp;
            }
            return result;
        }
        
        [HttpPost]
        public async Task<ChemicalViewModel?> CreateChemical(CreateChemicalViewModel chemical)
        {
            return await _chemicalService.CreateChemicalAsync(chemical);
        }
    }
}